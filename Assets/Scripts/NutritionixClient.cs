using System;
using System.Collections;
using System.Collections.Generic;
using Nutritionix;
using UnityEngine;
using UnityEngine.Networking;

public class NutritionixClient : MonoBehaviour {

    private readonly string myApiId = "xxxx";
    private readonly string myApiKey = "xxxx";
    private readonly string URL = "https://trackapi.nutritionix.com/v2/natural/nutrients";
    private readonly string timezone = "US/Eastern";
    private readonly string locale = "en_US";
    private readonly string contentType = "application/json";

    public string dummyFood = "apple";
    public Dictionary<int, string> nutrientNames = new Dictionary<int, string> ();

    /*
     * We will use a Tuple<string,double> and make rest calls to our neural network
     * We have to calculate the weighted sum and average of nutrient values
     * This weighted sum will decide the character's changes
     *  calories, TotalFat, Dietary Fiber, Sugars, Protein
     */

    private double Calories = 0d;
    private double TotalFat = 0d;
    private double DietaryFiber = 0d;
    private double Sugars = 0d;
    private double Protein = 0d;
    private double TotalWeight = 0d;

    public void GetNutrientsFromFood (string Query, List<double> Weights) {
        WWWForm form = new WWWForm ();
        Dictionary<string, string> requestHeaders = form.headers;

        requestHeaders["x-app-id"] = myApiId;
        requestHeaders["x-app-key"] = myApiKey;
        requestHeaders["x-remote-user-id"] = "0";
        requestHeaders["Content-Type"] = contentType;

        // Assumption : There is only one quantity of each item

        NutritionixRequest nutritionData = new NutritionixRequest
        {
            Query = Query,
            Timezone = timezone,
            Locale = locale,
            Aggregate = "string",
            NumServings = 1
        };

        string JSONString = Serialize.ToJson(nutritionData);
        byte[] formData = System.Text.Encoding.UTF8.GetBytes(JSONString);
        WWW request = new WWW(URL, formData, requestHeaders);
        StartCoroutine(OnResponse(request, Weights));
    }

    private IEnumerator OnResponse (WWW request, List<double> Weights) {
        yield return request;
        NutritionixResponse response = NutritionixResponse.FromJson (request.text);
        Debug.Log(request.text);

        IList<Food> foods = response.Foods;

        for(int i=0; i<foods.Count; i++){
            Food food = foods[i];
            double Weight = Weights[i];

            Calories += food.NfCalories*Weight;
            TotalFat += food.NfTotalFat * Weight;
            Protein += food.NfProtein * Weight;
            DietaryFiber += food.NfDietaryFiber * Weight;
            Sugars += food.NfProtein * Weight;

            TotalWeight += Weight;
        }

        PlayerPrefs.SetFloat("Calories", (float)(Calories / TotalWeight));
        PlayerPrefs.SetFloat("TotalFat", (float)(TotalFat / TotalWeight));
        PlayerPrefs.SetFloat("Protein", (float)(Protein / TotalWeight));
        PlayerPrefs.SetFloat("DietaryFiber", (float)(DietaryFiber / TotalWeight));
        PlayerPrefs.SetFloat("Sugars", (float)(Sugars / TotalWeight));

    }

}
