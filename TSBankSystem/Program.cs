using System;

namespace TSBankSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            int counter = 1;
            while (counter <=3)
            {
                Console.Clear();
                LoginScreen ls = new LoginScreen();
                if (ls.CheckCredentials(ls.Username, ls.Password))
                {
                    AppMenu menu = new AppMenu();
                    if (ls.Username == "admin")
                    {
                        menu.AdminMenu();
                    }
                    else
                    {
                        menu.UserMenu();
                    }
                    menu.Choice();
                }
                else
                {
                    if (counter < 3)
                    {
                        Console.WriteLine("\nLogin failed! Wrong username or password given.");
                        Console.WriteLine($"You have {3 - counter} tries left. Press Enter to try again");
                        counter++;
                    }
                    else
                    {
                        Console.WriteLine("\nAccess denied! You failed to login 3 times in a row.");
                        Console.WriteLine($"Aplication ended. Press any key to exit.");
                        counter++;
                    }                  
                }

                Console.ReadKey();
            }
            


        }
    }
}
