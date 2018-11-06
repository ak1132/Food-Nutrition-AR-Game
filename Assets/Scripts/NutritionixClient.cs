using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NutritionixClient : MonoBehaviour {

    private const string myApiId = "20cc37ae";
    private const string myApiKey = "b38f89ed44a520302234f5b83853b4c9";
    private const string URL = "https://trackapi.nutritionix.com/v2/natural/nutrients";
    private const string timezone = "US/Eastern";

    public void GetNutrientsFromFood(string food)
    {
        WWWForm form = new WWWForm();
        Dictionary<string, string> requestHeaders = form.headers;

        requestHeaders["x-app-id"]= myApiId;
        requestHeaders["x-app-key"] = myApiKey;
        requestHeaders["x-remote-user-id"] = "0";
        requestHeaders["Content-Type"] = "application/json";

        Nutritionix.NutritionixInfo nutritionData = new Nutritionix.NutritionixInfo
        {
            Query = food,
            Timezone = timezone
        };

        string JSONString = Nutritionix.Serialize.ToJson(nutritionData);
        byte[] formData = System.Text.Encoding.UTF8.GetBytes(JSONString);

        WWW request = new WWW(URL, formData, requestHeaders);
        StartCoroutine(OnResponse(request));
        
    }

    private IEnumerator OnResponse(WWW request)
    {
        yield return request;
        Debug.Log(request.text);
    }

}
