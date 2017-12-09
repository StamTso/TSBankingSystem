using System;
using System.Collections.Generic;
using System.Text;

namespace TSBankSystem
{
    public class AppMenu
    {
        public void AdminMenu()
        {
            Console.Clear();
            Console.WriteLine("\n-----------\nMain Menu\n------------");
            Console.WriteLine("Welcome Admin!");
            Console.WriteLine("Please choose one of the actions below:");
            Console.WriteLine("-----------------------");
            Console.WriteLine("-View Cooperative's internal bank account: Press I\n\n-View bank members internal accounts: Press B\n\n-Withdraw money from a member's internal account: Press W\n\n" +
                "-Print statement with the transctions of the day: Press P\n\n-Exit the application: press Q");
        }

        public void UserMenu()
        {
            Console.Clear();
            Console.WriteLine("\n-----------\nMain Menu\n------------");
            Console.WriteLine("Welcome User!");
            Console.WriteLine("Please choose one of the actions below:");
            Console.WriteLine("-----------------------");
            Console.WriteLine("-View your bank account: Press A\n\n-Deposit to Cooperative's Account: Press D\n\n-Deposit to a member's account: Press M\n\n" +
                "-Print statement with the transctions of the day: Press P\n\n-Exit the application: press Q");            
        }

        public string Choice()
        {            
            string userchoice = Console.ReadLine();
            Console.Clear();

            switch (userchoice)
            {
                case "A":
                    return "A";
                case "B":
                    return "B";
                case "D":
                    return "D";
                case "I":
                    return "I";
                case "M":
                    return "M";
                case "N":
                    return "N";
                case "W":
                    return "W";
                case "P":
                    return "P";
                case "Q":
                        Environment.Exit(0);
                    return "Q";
                default:
                    return null;
            }
        }
    }
}
