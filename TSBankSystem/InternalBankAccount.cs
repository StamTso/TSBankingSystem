using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace TSBankSystem
{
    public class InternalBankAccount
    {
        public static Account Account { get; set; }

        public static void DisplayBalance()
        {
            using (DBAccess db = new DBAccess())
            {
                string userBalance = (from u in db.Accounts
                        where u.User.Username.Equals(LoginScreen.Username)
                        select ($"Account holder: {u.User.Username}\nDate of last transcaction: {u.Transaction_Date}\nAccount Balance: {u.Amount}" )).First().ToString();
               
                Console.WriteLine(userBalance);
            }
            Console.ReadKey();
        }

        public static void DisplayAllUserAccounts()
        {
            using (DBAccess db = new DBAccess())
            {
                List<int> acc = (from b in db.Accounts
                                     select b.Id).ToList();

                for (int i = 2; i <= acc.Count; i++)
                {
                    string account = (from a in db.Accounts
                                      where a.Id.Equals(i)
                                      select (a.User.Username + a.Transaction_Date + a.Amount)).First().ToString();
                    Console.WriteLine($"User{i - 1} account:");
                    Console.WriteLine(account.ToString());
                    Console.WriteLine();
                }
            }
            Console.ReadKey();
        }

        public static void Deposit()
        {
            Console.WriteLine("--------------------------------------------------------------------------------\n\t\t\t\tDeposit Tab\n");
            Console.WriteLine("--------------------------------------------------------------------------------\n\n");
            Console.Write("Please enter the amount of money you want to deposit:");
            decimal amount = decimal.Parse(Console.ReadLine());
            Console.Write("Please enter the name of the account you want to deposit money to:");
            string name = Console.ReadLine();

            using (DBAccess db = new DBAccess())
            {
                List<string> nameslist = (from n in db.Users
                                      select n.Username).ToList();


                bool isvalid = false;

                foreach (var nameitem in nameslist)
                {
                    if(nameitem == name)
                    {
                        isvalid = true;
                        if (amount > 0 &&  name != LoginScreen.Username)
                        {
                            Account beneficiary = (from x in db.Accounts
                                                   where x.User.Username == name
                                                   select x).First();
                            Account principal = (from y in db.Accounts
                                                 where y.User.Username == LoginScreen.Username
                                                 select y).First();

                            if (principal.Amount >= amount)
                            {
                                beneficiary.Amount += amount;
                                principal.Amount -= amount;
                                principal.Transaction_Date = DateTime.Now;
                                beneficiary.Transaction_Date = DateTime.Now;
                                Console.WriteLine($"\nYou have succesfully transfered {amount} in {name}'s account");
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
                if (isvalid == false)
                {
                    Console.WriteLine("\nInvalid name of account was given");
                    Console.WriteLine("Account name should be a simple member account and should already exists.");
                }
            }
        }

        public static void Withdraw()
        {
            Console.WriteLine("\t\tWithdraw menu");
            Console.WriteLine("--------------------------------------\n\n");
            Console.Write("Please enter the amount of money you want to withdraw:");
            decimal amount = decimal.Parse(Console.ReadLine());
            Console.Write("Please enter the name of the account you want to withdraw money from:");
            string name = Console.ReadLine();

            using (DBAccess db = new DBAccess())
            {
                List<string> nameslist = (from n in db.Users
                                          select n.Username).ToList();

                bool isvalid = false;

                foreach (var nameitem in nameslist)
                {
                    if(nameitem == name)
                    {
                        isvalid = true;
                        if (amount > 0 && name != LoginScreen.Username)
                        {
                            Account beneficiary = (from x in db.Accounts
                                                   where x.User.Username == name
                                                   select x).First();
                            Account cooperativeacc = (from y in db.Accounts
                                                      where y.User.Username == LoginScreen.Username
                                                      select y).First();

                            if (beneficiary.Amount >= amount)
                            {
                                beneficiary.Amount -= amount;
                                cooperativeacc.Amount += amount;
                                cooperativeacc.Transaction_Date = DateTime.Now;
                                beneficiary.Transaction_Date = DateTime.Now;
                                Console.WriteLine($"\nYou have succesfully withdrawed {amount} euros from {name}'s account");
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
                if (isvalid == false)
                {
                    Console.WriteLine("\nInvalid name of account was given");
                    Console.WriteLine("Account name should be a simple member account and should already exists.");
                }
            }      
        }

        public override string ToString()
        {
            return String.Format("{0, 10} {1, 30:yyyy/MM/dd HH:mm:ss.FFF} {2, 20:C2}\n", Account.User, Account.Transaction_Date, Account.Amount);
        }

        public static void Buffer()
        {

        }

    }
}
