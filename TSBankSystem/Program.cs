using System;
using System.Globalization;
using System.Text;

namespace TSBankSystem
{
    class Program
    {
        //Main method of TSBankSystem
        static void Main(string[] args)
        {
            //Configuring the window title
            string apptitle = "TSB e-banking App";
            Console.Title = apptitle;
            //Creating el-GR culture
            Console.OutputEncoding = Encoding.Unicode;
            CultureInfo.CreateSpecificCulture("el-GR");

            //"while" loop counting the number of login tries
            //After successful login the methods for printing the menu are called
            int logintry = 1;
            while (logintry <= 3)
            {
                Console.Clear();
                LoginScreen.PrintLoginScreen();
                if (LoginScreen.CheckCredentials())
                {
                    AppMenu.ExecuteSelection();
                    logintry = 1;
                }
                else
                {
                    if (logintry < 3)
                    {
                        Console.WriteLine("\nLogin failed! Wrong username or password given.");
                        Console.WriteLine($"You have {3 - logintry} tries left. \n\nPress Enter to try again");
                        logintry++;
                        Console.ReadKey();
                    }
                    else//In 3 consecutive unsuccessfull tries the application ends
                    {
                        Console.WriteLine("\nAccess denied! You failed to login 3 times in a row.");
                        Console.WriteLine($"Aplication ended. \n\nPress any key to exit.");
                        logintry++;
                        Console.ReadKey();
                    }
                }
            }
        }
    }
}
