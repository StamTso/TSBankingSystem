using System;
using System.Collections.Generic;
using System.Linq;

namespace TSBankSystem
{
    //Class which holds all the actions available to admin and users
    //displayed via the Main Menu of the class AppMenu
    public class InternalBankAccount
    {
        public static Account Account { get; }
        //Holds the transactions made by user during one session
        public static List<string> TransactionsList = new List<string>();

        //Method for displaying the admin/user's account balance
        public static void DisplayBalance()
        {
            using (DBAccess db = new DBAccess())
            {
                string userBalance = (from u in db.Accounts
                                      where u.User.Username.Equals(LoginScreen.Username)
                                      select ($"Account holder: {u.User.Username}\nDate of last transcaction: {u.Transaction_Date.ToString("dd-MM-yyyy HH.mm.ss.FFF")}\nAccount Balance: {String.Format("{0:0.00}", u.Amount)}€")).First().ToString();

                Console.WriteLine("******************************************************************************");
                Console.WriteLine("*                           Display Balance Tab                              *");
                Console.WriteLine("******************************************************************************\n\n");
                Console.WriteLine(userBalance);
            }
        }

        //Method for displaying all user acounts, only accesible to admin.
        public static void DisplayAllUserAccounts()
        {
            using (DBAccess db = new DBAccess())
            {
                List<int> acc = (from b in db.Accounts
                                 select b.Id).ToList();

                Console.WriteLine("******************************************************************************");
                Console.WriteLine("*                          Display All Account Tab                           *");
                Console.WriteLine("******************************************************************************\n\n");

                for (int i = 2; i <= acc.Count; i++)
                {
                    string account = (from a in db.Accounts
                                      where a.Id.Equals(i)
                                      select ($"Account Holder: {a.User.Username}\nDate of Last Transaction:{a.Transaction_Date.ToString("dd-MM-yyyy HH.mm.ss.FFF")}\nAccount Balance:{String.Format("{0:0.00}", a.Amount)}€")).First().ToString();

                    Console.WriteLine($"User{i - 1} account:");
                    Console.WriteLine(account);
                    Console.WriteLine();
                }
            }
        }

        //Method for depositing money to another account
        public static void Deposit()
        {
            Console.WriteLine("******************************************************************************");
            Console.WriteLine("*                             Deposit Tab                                    *");
            Console.WriteLine("******************************************************************************\n\n");
            Console.Write("Please enter the amount of money you want to deposit:");

            string line = Console.ReadLine();//variable for reading user input
            decimal amount = 0;//variable used for parshing the user input
            try//If parsing succeeds then proceeds with the deposit
            {
                amount = decimal.Parse(line);

                Console.Write("\nPlease enter the name of the account you want to deposit money to:");
                string name = Console.ReadLine();

                using (DBAccess db = new DBAccess())
                {
                    List<string> nameslist = (from n in db.Users
                                              select n.Username).ToList();//Creating lists with all users


                    bool existinlist = false;//isvalid is checking if the name exist in the db

                    foreach (var nameitem in nameslist)
                    {
                        if (nameitem == name)
                        {
                            existinlist = true;
                            if (amount > 0 && name != LoginScreen.Username)//Checks the validity of the input for the transaction
                            {
                                Account beneficiary = (from x in db.Accounts
                                                       where x.User.Username == name
                                                       select x).First();//finds the beneficiary of the transaction
                                Account principal = (from y in db.Accounts
                                                     where y.User.Username == LoginScreen.Username
                                                     select y).First();//finds the principal of the transaction

                                if (principal.Amount >= amount)//checks if the principal has suficient balance
                                {
                                    string transactionlistitem;//initialize a new item for the memory buffer list
                                    beneficiary.Amount += amount;
                                    principal.Amount -= amount;
                                    principal.Transaction_Date = DateTime.Now;
                                    beneficiary.Transaction_Date = DateTime.Now;
                                    principal.Transaction_Date.ToString("dd-MM-yyyy HH.mm.ss.FFF");

                                    Console.WriteLine($"\nYou have succesfully transfered {String.Format("{0:0.00}", amount)}€ in {name}'s account");
                                    transactionlistitem = $"-On {principal.Transaction_Date.ToString("dd-MM-yyyy HH.mm.ss.FFF")}, {String.Format("{0:0.00}", amount)}€ were transfered to {name.ToUpper()}";
                                    TransactionsList.Add(transactionlistitem);
                                    db.SaveChanges();

                                }
                                else
                                {
                                    Console.WriteLine("\nYou don't have enough money in your account for this transaction");

                                }
                            }
                            else
                            {
                                Console.WriteLine("\nInvalid amount of money or own account name was given");
                                Console.WriteLine("Amount should be a positive integer in euros and the transaction must consider a different account.");


                            }
                        }
                    }
                    if (existinlist == false)
                    {
                        Console.WriteLine("\nInvalid name of account was given");
                        Console.WriteLine("Account name should be a simple member account and should already exists.");

                    }
                }
            }
            catch (FormatException)//If an exception is catched then it ends the method and returns to main menu
            {
                Console.WriteLine("{0} is not a valid number form\nDeposit process is ended", line);

            }
        }

        //Method for withdrawing money from user accounts. Accesible only to admin
        public static void Withdraw()
        {
            Console.WriteLine("******************************************************************************");
            Console.WriteLine("*                             Withdraw Tab                                   *");
            Console.WriteLine("******************************************************************************\n\n");
            Console.Write("Please enter the amount of money you want to withdraw:");

            //Same process as in "Deposit" Method
            //This time it removes money from the beneficiary
            //and adds to the cooperative account
            string line = Console.ReadLine();
            decimal amount = 0;
            try
            {
                amount = decimal.Parse(line);

                Console.Write("\nPlease enter the name of the account you want to withdraw money from:");
                string name = Console.ReadLine();

                using (DBAccess db = new DBAccess())
                {
                    List<string> nameslist = (from n in db.Users
                                              select n.Username).ToList();

                    bool existinlist = false;

                    foreach (var nameitem in nameslist)
                    {
                        if (nameitem == name)
                        {
                            existinlist = true;
                            if (amount > 0 && name != LoginScreen.Username)
                            {
                                Account beneficiary = (from x in db.Accounts
                                                       where x.User.Username == name
                                                       select x).First();
                                Account cooperativeacc = (from y in db.Accounts
                                                          where y.User.Username == LoginScreen.Username
                                                          select y).First();//The admin's cooperative account

                                if (beneficiary.Amount >= amount)
                                {
                                    string transactionlistitem;
                                    beneficiary.Amount -= amount;
                                    cooperativeacc.Amount += amount;
                                    cooperativeacc.Transaction_Date = DateTime.Now;
                                    beneficiary.Transaction_Date = DateTime.Now;


                                    Console.WriteLine($"\nYou have succesfully withdrawed {String.Format("{0:0.00}", amount)}€ from {name}'s account");
                                    transactionlistitem = $"-On {cooperativeacc.Transaction_Date.ToString("dd-MM-yyyy HH.mm.ss.FFF")}, {String.Format("{0:0.00}", amount)}€ were withdrawed from {name.ToUpper()}";
                                    TransactionsList.Add(transactionlistitem);
                                    db.SaveChanges();

                                }
                                else
                                {
                                    Console.WriteLine($"\n{name} doesn't have enough money in his account for this transaction");

                                }
                            }
                            else
                            {
                                Console.WriteLine("\nInvalid amount of money or own account name was given");
                                Console.WriteLine("\nAmount should be a positive integer in euros and the transaction must consider a different account.");

                            }
                        }
                    }
                    if (existinlist == false)
                    {
                        Console.WriteLine("\nInvalid name of account was given");
                        Console.WriteLine("Account name should be a simple member account and should already exists.");

                    }
                }
            }
            catch
            {
                Console.WriteLine("{0} is not a valid number form\nWithdraw process is ended", line);
            }
        }

        public override string ToString()
        {
            return String.Format("{0, 10} {1, 30:yyyy/MM/dd HH:mm:ss.FFF} {2, 20:C2}\n", Account.User, Account.Transaction_Date, Account.Amount);
        }
    }
}
