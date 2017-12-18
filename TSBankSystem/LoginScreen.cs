using System;
using System.Linq;

namespace TSBankSystem
{
    //Class which handles the initial login screen
    //and the verification of user's credentials against 
    //sql database
    public static class LoginScreen
    {
        public static string Username { get; set; }
        private static string Password { get; set; }

        //Method for printing the login screen        
        public static void PrintLoginScreen()
        {
            Console.WriteLine("--------------------------------------------------------------------------------\n\t\t\tWelcome to TS Bank e-banking application\n");
            Console.WriteLine("--------------------------------------------------------------------------------\nUser Login\n---------------");
            //Reading the user's input for username and password
            Console.Write("Enter Username: ");
            Username = Console.ReadLine();
            Console.Write("Enter Password: ");
            ConsoleKeyInfo key;//Password is read as char and * are displayed instead of the actual keys pressed
            Password = "";
            do
            {
                key = Console.ReadKey(true);
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    Password += key.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && Password.Length > 0)
                    {
                        Password = Password.Substring(0, (Password.Length - 1));
                        Console.Write("\b \b");
                    }
                }
            }
            while (key.Key != ConsoleKey.Enter);
        }


        //Method for checking the user input of username and password
        //against the database
        public static bool CheckCredentials()
        {
            using (DBAccess db = new DBAccess())//Open connection with database
            {

                var hasUser = (from a in db.Users
                               where a.Username == Username && a.Password == Password
                               select (a.Username)).FirstOrDefault();

                //returning true only  when username and repsective password match with 
                //input from login screen
                if (hasUser == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
    }
}
