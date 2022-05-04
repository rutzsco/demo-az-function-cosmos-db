using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Function.Api.Model
{
    public class GenerateMeasurementsCommand
    {
        [JsonProperty(PropertyName = "count")]
        public int Count { get; set; }

        [JsonProperty(PropertyName = "accountId")]
        public Guid AccountId { get; set; }

        [JsonProperty(PropertyName = "deviceId")]
        public Guid DeviceId { get; set; }
    }
}
