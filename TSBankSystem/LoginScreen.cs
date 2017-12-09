using System;
using System.Collections.Generic;
using System.Text;

namespace TSBankSystem
{
    public class LoginScreen
    {
        public string Username { get; set; }
        public string Password { get; set; }

        
        public LoginScreen()
        {
            Console.WriteLine("-------------------------------\nWelcome to TS e - Banking system");
            Console.WriteLine("-------------------------------\nUser Login\n------------------------------\n");
            Console.Write("Enter Username: ");
            Username = Console.ReadLine();
            Console.Write("Enter Password: ");
            ConsoleKeyInfo key;
            string Password = "";
            do
            {
                key = Console.ReadKey(true);
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter) 
                {
                    this.Password += key.KeyChar;
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

        public bool CheckCredentials(string username, string password)
        {
            if ((username == "admin" && password == "admin") || (username == "user1" && password == "password1") || (username =="user2" && password == "password2"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
