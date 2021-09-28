using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Function.Api.Model
{
    public class AccountDetails
    {
        public string AccountName { get; set; }

        public string NumberOfCalves { get; set; }

        public string Address { get; set; }

        public string ContactPerson { get; set; }

        public string ContactNumber { get; set; }

        public string TechnicalContactPerson { get; set; }

        public string TechnicalContactNumber { get; set; }
    }
}
