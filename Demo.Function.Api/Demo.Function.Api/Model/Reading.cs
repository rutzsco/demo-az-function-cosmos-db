using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Function.Api.Model
{
    public class Reading
    {
        public Reading()
        { 
        }

        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        [JsonProperty(PropertyName = "accountId")]
        public Guid AccountId { get; set; }

        [JsonProperty(PropertyName = "deviceId")]
        public Guid DeviceId { get; set; }

        [JsonProperty(PropertyName = "type")]
        public Guid Type { get; set; }

        public TempuratureReading TempuratureReading { get; set; }
    }

    public class TempuratureReading
    {
        public decimal Value { get; set; }

        public string Unit { get; set; }
    }
}
