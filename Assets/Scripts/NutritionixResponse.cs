using System;
using System.Collections.Generic;

namespace Nutritionix
{
    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public class FullNutrient
    {

        [JsonProperty("attr_id")]
        public int AttrId { get; set; }

        [JsonProperty("attr_name")]
        public string AttrName { get; set; }

        [JsonProperty("value")]
        public double Value { get; set; }
    }

    public class Metadata
    {
    }

    public class Photo
    {

        [JsonProperty("thumb")]
        public string Thumb { get; set; }

        [JsonProperty("highres")]
        public object HighRes { get; set; }

        [JsonProperty("is_user_uploaded")]
        public bool IsUserUploaded { get; set; }
    }

    public class Food
    {

        [JsonProperty("food_name")]
        public string FoodName { get; set; }

        [JsonProperty("brand_name")]
        public object BrandName { get; set; }

        [JsonProperty("serving_qty")]
        public int ServingQty { get; set; }

        [JsonProperty("serving_unit")]
        public string ServingUnit { get; set; }

        [JsonProperty("serving_weight_grams")]
        public int ServingWeightGrams { get; set; }

        [JsonProperty("nf_calories")]
        public int NfCalories { get; set; }

        [JsonProperty("nf_total_fat")]
        public double NfTotalFat { get; set; }

        [JsonProperty("nf_saturated_fat")]
        public double NfSaturatedFat { get; set; }

        [JsonProperty("nf_cholesterol")]
        public int NfCholesterol { get; set; }

        [JsonProperty("nf_sodium")]
        public double NfSodium { get; set; }

        [JsonProperty("nf_total_carbohydrate")]
        public double NfTotalCarbohydrate { get; set; }

        [JsonProperty("nf_dietary_fiber")]
        public double NfDietaryFiber { get; set; }

        [JsonProperty("nf_sugars")]
        public double NfSugars { get; set; }

        [JsonProperty("nf_protein")]
        public double NfProtein { get; set; }

        [JsonProperty("nf_potassium")]
        public double NfPotassium { get; set; }

        [JsonProperty("nf_p")]
        public double NfP { get; set; }

        [JsonProperty("full_nutrients")]
        public IList<FullNutrient> FullNutrients { get; set; }

        [JsonProperty("nix_brand_name")]
        public object NixBrandName { get; set; }

        [JsonProperty("nix_brand_id")]
        public object NixBrandId { get; set; }

        [JsonProperty("nix_item_name")]
        public object NixItemName { get; set; }

        [JsonProperty("nix_item_id")]
        public object NixItemId { get; set; }

        [JsonProperty("upc")]
        public object Upc { get; set; }

        [JsonProperty("consumed_at")]
        public DateTime ConsumedAt { get; set; }

        [JsonProperty("metadata")]
        public Metadata Metadata { get; set; }

        [JsonProperty("source")]
        public object Source { get; set; }

        [JsonProperty("ndb_no")]
        public object NdbNo { get; set; }

        [JsonProperty("tags")]
        public object Tags { get; set; }

        [JsonProperty("alt_measures")]
        public object AltMeasures { get; set; }

        [JsonProperty("lat")]
        public int Lat { get; set; }

        [JsonProperty("lng")]
        public int Lng { get; set; }

        [JsonProperty("meal_type")]
        public int MealType { get; set; }

        [JsonProperty("photo")]
        public Photo Photo { get; set; }

        [JsonProperty("sub_recipe")]
        public object SubRecipe { get; set; }
    }

    public partial class NutritionixResponse
    {
        [JsonProperty("foods")]
        public IList<Food> Foods { get; set; }

        public static NutritionixResponse FromJson(string json) => JsonConvert.DeserializeObject<NutritionixResponse>(json, Nutritionix.Converter.Settings);
    }

    public static partial class Serialize
    {
        public static string ToJson(this NutritionixResponse self) => JsonConvert.SerializeObject(self, Nutritionix.Converter.Settings);
    }

}