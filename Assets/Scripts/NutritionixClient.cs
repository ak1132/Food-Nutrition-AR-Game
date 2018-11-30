using System;
using System.Collections;
using System.Collections.Generic;
using Nutritionix;
using UnityEngine;

public class NutritionixClient : MonoBehaviour {

    private readonly string myApiId = "20cc37ae";
    private readonly string myApiKey = "b38f89ed44a520302234f5b83853b4c9";
    private readonly string URL = "https://trackapi.nutritionix.com/v2/natural/nutrients";
    private readonly string timezone = "US/Eastern";
    private readonly string locale = "en_US";
    private readonly string contentType = "application/json";

    private double Calories = 0d;
    private double TotalFat = 0d;
    private double DietaryFiber = 0d;
    private double Sugars = 0d;
    private double Protein = 0d;
    private double TotalWeight = 0d;

    private string foodQuery;

    private DebugWriter debugWriter;

    private void Start()
    {
        debugWriter = FindObjectOfType<DebugWriter>();
    }

    /*
     * We have to calculate the weighted sum and average of nutrient values
     * This weighted sum will decide the character's changes
     *  calories, TotalFat, Dietary Fiber, Sugars, Protein
     */

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
        foodQuery = Query;
        StartCoroutine(OnResponse(request, Weights));
    }

    private IEnumerator OnResponse (WWW request, List<double> Weights) {
        yield return request;
        NutritionixResponse response = NutritionixResponse.FromJson (request.text);
        debugWriter.WriteToFile("Response from Nutritionix : \n" + request.text);
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

        PlayerPrefs.SetFloat("currentCalories", (float)(Calories / TotalWeight));
        PlayerPrefs.SetFloat("currentTotalFat", (float)(TotalFat / TotalWeight));
        PlayerPrefs.SetFloat("currentProtein", (float)(Protein / TotalWeight));
        PlayerPrefs.SetFloat("currentDietaryFiber", (float)(DietaryFiber / TotalWeight));
        PlayerPrefs.SetString("currentFoods", foodQuery);

        FindObjectOfType<AnimationController>().UpdateCharacterAnimation();

    }

}