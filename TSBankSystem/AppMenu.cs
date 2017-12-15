using System;
using System.Collections.Generic;
using System.Text;

namespace TSBankSystem
{
    public static class AppMenu
    {
        public static void PrintMenu()
        {
            
                if (LoginScreen.Username == "admin")
                {
                    Console.Clear();
                    Console.WriteLine("\n-----------\nMain Menu\n------------");
                    Console.WriteLine($"Welcome {LoginScreen.Username}!");
                    Console.WriteLine("Please choose one of the actions below:");
                    Console.WriteLine("-----------------------");
                    Console.WriteLine("-Press A: View  your Cooperative Account Balance\n\n-Press B: View all internal Member Accounts\n\n-Press D: Deposit to Member Account\n\n-Press W: Withdraw from Member Account\n\n" +
                        "-Press P: Print statement with the transctions of the day\n\n-Press Q: Log out");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("--------------------------------------------------------------------------------\n\t\t\t\tTSB App Main Menu\n");
                    Console.WriteLine($"--------------------------------------------------------------------------------\n\t\t\t\tWelcome {LoginScreen.Username}!\n--------------------------------------------------------------------------------\n");
                    Console.WriteLine("Please choose one of the actions below:\n");
                    Console.WriteLine("-Press A: View your Account Balance\n\n-Press D: Deposit to Member/Cooperative Account\n\n" +
                        "-Press P: Print statement with the transctions of the day\n\n-Press Q: Log out");
                }           
        }

        public static void ExecuteSelection()
        {
            bool exit = false;
            while (!exit)
            {
                PrintMenu();
                string selection = Console.ReadLine();
                selection = selection.ToUpper();
                Console.Clear();

                switch (selection)
                {
                    case "A":
                        InternalBankAccount.DisplayBalance();
                        break;
                    case "B":
                        if (LoginScreen.Username == "admin")
                        {
                            InternalBankAccount.DisplayAllUserAccounts();
                        }
                        else
                        {
                            Console.WriteLine("Low access level. Not authorized for this selection");
                            Console.ReadKey();
                        }
                        break;
                    case "D":
                        InternalBankAccount.Deposit();
                        break;
                    case "W":
                        if (LoginScreen.Username == "admin")
                        {
                            InternalBankAccount.Withdraw();
                        }
                        else
                        {
                            Console.WriteLine("Low access level. Not authorized for this selection");
                            Console.ReadKey();
                        }
                        break;
                    case "P":
                        break;
                    case "Q":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine($"{selection} is not a valid choice", selection);
                        Console.ReadKey();
                        break;
                }
            }
                
            
        }
    }
}
