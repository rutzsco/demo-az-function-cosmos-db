using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Function.Api.Model
{
    public class Account
    {
        public Account(AccountDetails accountDetails)
        {
            Id = Guid.NewGuid();
            ItemType = "Account";
            AccountDetails = accountDetails ?? throw new ArgumentNullException(nameof(accountDetails));
        }

        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        public string ItemType { get; set; }

        public AccountDetails AccountDetails { get; set; }
    }
}
