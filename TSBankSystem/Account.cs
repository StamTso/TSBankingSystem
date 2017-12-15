using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TSBankSystem
{
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
