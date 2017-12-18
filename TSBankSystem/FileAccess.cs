using System;
using System.IO;

namespace TSBankSystem
{
    //Class that handles the print of statements
    //according to the user's transactions of the day
    public static class FileAccess
    {
        //Method for printing the transactions made by the user
        //during the day
        //It keeps track of transaction while user is logged in
        public static void PrintTransactions()
        {
            //Gives the file the name according to the required format
            string filename = $"statement_{LoginScreen.Username}_{DateTime.Now.ToString("dd_MM_yyyy")}.txt";

            foreach (var transactionlistitem in InternalBankAccount.TransactionsList)
            {
                if (File.Exists(filename) == false)
                {
                    using (FileStream fs = File.Create(filename))
                    {
                        Console.WriteLine($"\nStatement named: {filename} \nwith current transactions successfully created");
                    }
                    using (StreamWriter sw1 = File.AppendText(filename))
                    {
                        sw1.WriteLine("\tTS Bank Daily Statement File");
                    }
                }

                using (StreamWriter sw2 = File.AppendText(filename))
                {
                    sw2.WriteLine();
                    sw2.WriteLine(transactionlistitem);
                }
            }
            Console.WriteLine($"\nTransactions were succesfully added to {filename} statement");
        }
    }
}
