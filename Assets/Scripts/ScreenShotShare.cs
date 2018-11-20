using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScreenShotShare : MonoBehaviour
{
    public bool takingScreenshot = false;

    public void TakeAShot()
    {
        StartCoroutine("CaptureIt");
    }

    IEnumerator CaptureIt()
    {
        string timeStamp = System.DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss");
        string pathToSave = Application.productName +"_"+ timeStamp + ".png";
        NativeToolkit.SaveScreenshot(pathToSave,Application.productName,".png");
        yield return new WaitForEndOfFrame();
    }
}