// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using Nutritionix;
//
//    var welcome = Nutritionix.FromJson(jsonString);

namespace Nutritionix
{
    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class NutritionixInfo
    {
        [JsonProperty("query")]
        public string Query { get; set; }

        [JsonProperty("num_servings")]
        public long NumServings { get; set; }

        [JsonProperty("aggregate")]
        public string Aggregate { get; set; }

        [JsonProperty("line_delimited")]
        public bool LineDelimited { get; set; }

        [JsonProperty("use_raw_foods")]
        public bool UseRawFoods { get; set; }

        [JsonProperty("include_subrecipe")]
        public bool IncludeSubrecipe { get; set; }

        [JsonProperty("timezone")]
        public string Timezone { get; set; }

        [JsonProperty("consumed_at")]
        public string ConsumedAt { get; set; }

        [JsonProperty("lat")]
        public long Lat { get; set; }

        [JsonProperty("lng")]
        public long Lng { get; set; }

        [JsonProperty("meal_type")]
        public long MealType { get; set; }

        [JsonProperty("use_branded_foods")]
        public bool UseBrandedFoods { get; set; }

        [JsonProperty("locale")]
        public string Locale { get; set; }
    }

    public partial class NutritionixInfo
    {
        public static NutritionixInfo FromJson(string json) => JsonConvert.DeserializeObject<NutritionixInfo>(json, Nutritionix.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this NutritionixInfo self) => JsonConvert.SerializeObject(self, Nutritionix.Converter.Settings);
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
