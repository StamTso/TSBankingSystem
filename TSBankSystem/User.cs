using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TSBankSystem
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
