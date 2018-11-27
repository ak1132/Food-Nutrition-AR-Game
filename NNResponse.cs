using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

public class NNResponse {

    [JsonProperty("names")]
    public IList<string> Names { get; set; }

    [JsonProperty("areas")]
    public IList<double> Areas { get; set; }

    public static NNResponse FromJson(string json)
    {
        return JsonConvert.DeserializeObject<NNResponse>(json, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}


