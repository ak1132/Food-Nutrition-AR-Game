using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Nutritionix;
using Newtonsoft.Json.Linq;

public class NutritionixClient : MonoBehaviour {

    private readonly string myApiId = "20cc37ae";
    private readonly string myApiKey = "b38f89ed44a520302234f5b83853b4c9";
    private readonly string URL = "https://trackapi.nutritionix.com/v2/natural/nutrients";
    private readonly string timezone = "US/Eastern";
    private readonly string locale = "en_US";
    private readonly string contentType = "application/json";

    public string dummyFood = "apple";

    public void GetNutrientsFromFood(string food)
    {
        WWWForm form = new WWWForm();
        Dictionary<string, string> requestHeaders = form.headers;

        requestHeaders["x-app-id"]= myApiId;
        requestHeaders["x-app-key"] = myApiKey;
        requestHeaders["x-remote-user-id"] = "0";
        requestHeaders["Content-Type"] = contentType;

        NutritionixRequest nutritionData = new NutritionixRequest
        {
            Query = food,
            Timezone = timezone,
            Locale = locale,
            Aggregate = "string",
            NumServings = 1
        };

        if (nutritionData.Query == null)
        {
            nutritionData.Query = dummyFood;
        }

        string JSONString = Serialize.ToJson(nutritionData);
        Debug.Log(JSONString);
        byte[] formData = System.Text.Encoding.UTF8.GetBytes(JSONString);

        WWW request = new WWW(URL, formData, requestHeaders);
        StartCoroutine(OnResponse(request));    
    }

    private IEnumerator OnResponse(WWW request)
    {
        yield return request;
        NutritionixResponse response = NutritionixResponse.FromJson(request.ToString());
        //Debug.Log(request.text);
        GetAttributeNamesForIds(response);
        Debug.Log(response.ToJson());
    }

    private IEnumerator GetAttributeNamesForIds(NutritionixResponse response)
    {
        string Attr_URL = "http://127.0.0.1:5000/get_attr_name/";
        IList<Food> foods = response.Foods;

        foreach(Food food in foods)
        {
            IList<FullNutrient> FullNutrients = food.FullNutrients;
            foreach(FullNutrient nutrient in FullNutrients)
            {
                WWWForm form = new WWWForm();
                form.AddField("id", nutrient.AttrId);
                UnityWebRequest w = UnityWebRequest.Post(Attr_URL, form);
                yield return w.SendWebRequest();
                if (w.isNetworkError || w.isHttpError)
                {
                    Debug.Log(w.error);
                }
                else
                {
                    string[] resp = w.downloadHandler.text.Split(',');
                    nutrient.AttrName = resp[1];
                }
            }
        }
    }

}
