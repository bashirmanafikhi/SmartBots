using Microsoft.AspNetCore.Identity;
using SmartBots.Data.Models;

namespace SmartBots.Infrastructure.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Todo> Todos { get; set; } = [];
        public ApplicationUser()
        {

        }

        //[Required]
        //[StringLength(1000)]
        //public string Name { get; set; }

        //public ICollection<Exchange> Exchanges { get; set; }
        //public ICollection<TradingBot> TradingBots { get; set; }
    }

}
