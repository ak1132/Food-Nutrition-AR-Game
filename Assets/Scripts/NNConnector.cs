using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Nutritionix;
using System.Collections.Generic;
using System;

public class NNConnector : MonoBehaviour
{

    private readonly string screenshotURL = "http://localhost:5000/nn";

    public void SendDatatoModel()
    {
        StartCoroutine(Send());
    }

    IEnumerator Send()
    {
        string timeStamp = System.DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss");
        string pathToSave = Application.productName +"_"+ timeStamp + ".png";
        yield return new WaitForEndOfFrame();

        int width = Screen.width;
        int height = Screen.height;
        var tex = new Texture2D(width, height, TextureFormat.RGB24, false);

        tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        tex.Apply();

        byte[] bytes = tex.EncodeToPNG();
        ScreenCapture.CaptureScreenshot(Application.persistentDataPath + pathToSave);
        Destroy(tex);

        WWWForm form = new WWWForm();
        form.AddField("frameCount", Time.frameCount.ToString());
        form.AddBinaryData("file", bytes, "screenshot.png", "image/png");

        using(var w = UnityWebRequest.Post(screenshotURL, form))
        {
            yield return w.SendWebRequest();
            if (w.isNetworkError || w.isHttpError)
            {
                Debug.Log(w.error);
            }
            else
            {
                string response = w.downloadHandler.text.Trim();
                Debug.Log("Response:\n"+response);
                NNResponse nNResponse = NNResponse.FromJson(response);
                IList<string> foods = nNResponse.Names;
                IList<double> areas = nNResponse.Areas;

                List<double> Weights = new List<double>();
                string Query = "";

                for(int i = 0; i < foods.Count; i++)
                {
                    Weights.Add(areas[i]);
                    Query += foods[i] + " ";
                }

                if (Weights.Count != 0)
                    GetComponent<NutritionixClient>().GetNutrientsFromFood(Query, Weights);
                else
                    Exception("Empty Response from the neural network");
            }
        }

    }

    private Exception Exception(string v)
    {
        throw new Exception(v);
    }
}