using SmartBots.Domain.Common;
using SmartBots.Domain.Enums;
using SmartBots.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace SmartBots.Domain.Entities
{
    public class Exchange : BaseAuditableEntity, IUserOwnedEntity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public ExchangeType Type { get; set; }

        [Required]
        public string ApiKey { get; set; }

        [Required]
        public string ApiSecret { get; set; }

        [Required]
        public bool IsTest { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }

        public void Update(
            string name,
            string apiKey,
            string apiSecret,
            bool isTest,
            ExchangeType type)
        {
            Name = name;
            ApiKey = apiKey; 
            ApiSecret = apiSecret;
            IsTest = isTest;
            Type = type;
        }
    }
}
