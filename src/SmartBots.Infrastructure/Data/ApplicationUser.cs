using Microsoft.AspNetCore.Identity;
using SmartBots.Domain.Entities;

namespace SmartBots.Infrastructure.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Todo> Todos { get; set; } = [];
        public ICollection<ExchangeAccount> ExchangeAccounts { get; set; } = [];
        public ICollection<TradingBot> TradingBots { get; set; } = [];

        public ApplicationUser()
        {

        }
    }
}
