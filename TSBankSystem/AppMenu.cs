using System;
using System.Linq;

namespace TSBankSystem
{
    //Class that prints the admin and users menus
    //and handles the execution of the user's selections
    public static class AppMenu
    {
        private static string superuser;
        private static string Superuser//Retrieving the superuser from the database
        {
            get
            {
                using (DBAccess db = new DBAccess())
                {
                    superuser = (from su in db.Users
                                 where su.Id == 1
                                 select su.Username).First().ToString();
                }
                return superuser;
            }
        }


        //Method for printing the Main Menu
        public static void PrintMenu()
        {
            if (LoginScreen.Username == Superuser)
            {
                PrintHeader(LoginScreen.Username.ToUpper());
                Console.WriteLine("-Press A: View  your Cooperative Account Balance\n\n-Press B: View all internal Member Accounts\n\n-Press D: Deposit to Member Account\n\n-Press W: Withdraw from Member Account\n\n" +
                    "-Press P: Send today's Statement\n\n-Press Q: Log out");
            }
            else
            {
                PrintHeader(LoginScreen.Username.ToUpper());
                Console.WriteLine("-Press A: View your Account Balance\n\n-Press D: Deposit to Member/Cooperative Account\n\n" +
                    "-Press P: Send today's Statement\n\n-Press Q: Log out");
            }
        }

        //Method for executing the appropriate method
        //according to the choice of user
        public static void ExecuteSelection()
        {
            bool exit = false;
            while (!exit)
            {
                PrintMenu();
                Console.Write("\nPlease choose one of the actions above:");
                string selection = Console.ReadLine();
                selection = selection.ToUpper();
                Console.Clear();

                switch (selection)
                {
                    case "A":
                        InternalBankAccount.DisplayBalance();
                        PrintReturn();
                        break;
                    case "B":
                        if (LoginScreen.Username == Superuser)
                        {
                            InternalBankAccount.DisplayAllUserAccounts();
                            PrintReturn();
                        }
                        else
                        {
                            Console.WriteLine($"\n{selection} is not a valid choice", selection);
                            PrintReturn();
                        }
                        break;
                    case "D":
                        InternalBankAccount.Deposit();
                        PrintReturn();
                        break;
                    case "W":
                        if (LoginScreen.Username == Superuser)
                        {
                            InternalBankAccount.Withdraw();
                            PrintReturn();
                        }
                        else
                        {
                            Console.WriteLine($"\n{selection} is not a valid choice", selection);
                            PrintReturn();
                        }
                        break;
                    case "P":
                        FileAccess.PrintTransactions();
                        PrintReturn();
                        break;
                    case "Q":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine($"\n{selection} is not a valid choice", selection);
                        PrintReturn();
                        break;
                }
            }
        }

        //Method for printing the text displayed when it should return to Main Menu
        private static void PrintReturn()
        {
            Console.WriteLine("\nPress ENTER to return to Main Menu");
            Console.ReadKey();
        }

        //Method for printing the header in Main Menu
        private static void PrintHeader(string header)
        {
            Console.Clear();
            Console.WriteLine("******************************************************************************");
            Console.WriteLine($"*                           Welcome {header}!                                  *");
            Console.WriteLine("******************************************************************************\n\n");
        }
    }
}
