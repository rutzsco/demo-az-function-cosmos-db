using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Function.Api.Model
{
    public class Measurement
    {
        public static Measurement CreateTempurature(Guid accountId, Guid deviceId, decimal value)
        {
            Measurement measurement = new Measurement()
            {
                Id = Guid.NewGuid(),
                AccountId = accountId,
                DeviceId = deviceId,
                Type = "Tempurature",
                Timestamp = DateTime.UtcNow,
                TempuratureMeasurement = new TempuratureMeasurement() { Value = value, Unit = "F" }
            };
            return measurement;
        }

        public Measurement()
        { 
        }

        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        [JsonProperty(PropertyName = "accountId")]
        public Guid AccountId { get; set; }

        [JsonProperty(PropertyName = "deviceId")]
        public Guid DeviceId { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "timestamp")]
        public DateTime Timestamp { get; set; }

        [JsonProperty(PropertyName = "tempuratureMeasurement")]
        public TempuratureMeasurement TempuratureMeasurement { get; set; }
    }

    public class TempuratureMeasurement
    {
        [JsonProperty(PropertyName = "value")]
        public decimal Value { get; set; }

        [JsonProperty(PropertyName = "unit")]
        public string Unit { get; set; }
    }

}
