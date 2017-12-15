using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSBankSystem
{
    public static class LoginScreen
    {
        public static string Username { get; set; }
        public static string Password { get; set;}
        
                
        public static void PrintLoginScreen()
        {
            Console.WriteLine("--------------------------------------------------------------------------------\n\t\t\tWelcome to TS Bank e-banking application\n");
            Console.WriteLine("--------------------------------------------------------------------------------\nUser Login\n--------------------------------------------------------------------------------\n");
            Console.Write("Enter Username: ");
            Username = Console.ReadLine();
            Console.Write("Enter Password: ");
            ConsoleKeyInfo key;
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

        

        public static bool CheckCredentials()
        {
            using (DBAccess dbx = new DBAccess())
            {
                var admin = dbx.Users.Find(1);
                string adminName = dbx.Entry(admin).Property(i => i.Username).CurrentValue;
                string adminPass = dbx.Entry(admin).Property(i => i.Password).CurrentValue;
                var user1 = dbx.Users.Find(2);
                string user1Name = dbx.Entry(user1).Property(i => i.Username).CurrentValue;
                string user1Pass = dbx.Entry(user1).Property(i => i.Password).CurrentValue;
                var user2 = dbx.Users.Find(3);
                string user2Name = dbx.Entry(user2).Property(i => i.Username).CurrentValue;
                string user2Pass = dbx.Entry(user2).Property(i => i.Password).CurrentValue;
                

                if ((Username == adminName && Password == adminPass) || (Username == user1Name && Password == user1Pass) || (Username == user2Name && Password == user2Pass))
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
}
