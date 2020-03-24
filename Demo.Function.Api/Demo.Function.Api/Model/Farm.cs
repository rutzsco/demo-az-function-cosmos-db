using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Function.Api.Model
{
    public class Farm
    {
        public Farm(FarmDetails farmDetails)
        {
            Id = Guid.NewGuid();
            ItemType = "Farm";
            FarmDetails = farmDetails ?? throw new ArgumentNullException(nameof(farmDetails));
        }

        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        public string ItemType { get; set; }

        public FarmDetails FarmDetails { get; set; }
    }
}
