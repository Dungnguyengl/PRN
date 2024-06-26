using System.ComponentModel.DataAnnotations;

namespace PizzaStoreManagement.Models
{
    public class Accounts
    {
        public int AccountId { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? FullName { get; set; }
        public AccountType Type { get; set; }
    }
}
