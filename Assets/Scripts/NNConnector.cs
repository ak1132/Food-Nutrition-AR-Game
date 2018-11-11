using System.Collections;
using UnityEngine;
using UnityEngine.Networking;


public class NNConnector : MonoBehaviour
{

    private readonly string screenshotURL = "http://127.0.0.1:5000/nn/";

    public void SendDatatoModel()
    {
        StartCoroutine("Send");
    }

    IEnumerator Send()
    {
        string timeStamp = System.DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss");
        string pathToSave = Application.productName +"_"+ timeStamp + ".png";
        //NativeToolkit.SaveScreenshot(pathToSave,Application.productName,".png");
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
        form.AddBinaryData("image", bytes, "screenshot.png", "image/png");

        using(var w = UnityWebRequest.Post(screenshotURL, form))
        {
            yield return w.SendWebRequest();
            if (w.isNetworkError || w.isHttpError)
            {
                Debug.Log(w.error);
            }
            else
            {
                Debug.Log("Successfully uploaded screenshot");
            }
        }

    }
}