using System.ComponentModel.DataAnnotations;

namespace TSBankSystem
{
    //Class modeling the table "Users" in the afdemp_csharp_1 database
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
