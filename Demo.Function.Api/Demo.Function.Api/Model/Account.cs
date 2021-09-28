using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Function.Api.Model
{
    public class Account
    {
        public Account()
        { 
        }

        public Account(AccountDetails accountDetails)
        {
            Id = Guid.NewGuid();

            AccountName = accountDetails.AccountName;
            PostalCode = accountDetails.PostalCode;
            ContactPerson = accountDetails.ContactPerson;
        }

        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        public string AccountName { get; set; }

        public string PostalCode { get; set; }

        public string ContactPerson { get; set; }
    }
}
