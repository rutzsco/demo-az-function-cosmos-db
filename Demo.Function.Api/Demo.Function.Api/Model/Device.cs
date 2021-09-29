using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Function.Api.Model
{
    public class Device
    {
        public Device()
        {
            Type = "DeviceSummary";
        }

        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        [JsonProperty(PropertyName = "accountId")]
        public Guid AccountId { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; private set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

    }
}
