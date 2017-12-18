using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TSBankSystem
{
    //Class modeling the table "Accounts" in the afdemp_csharp_1 database
    public class Account
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("user_id")]
        public User User { get; set; }
        public DateTime Transaction_Date { get; set; }
        public decimal Amount { get; set; }
    }
}
