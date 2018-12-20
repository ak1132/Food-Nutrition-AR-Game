using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public Button viewButton;
    public Button resetButton;

    void Start()
    {
        Init();
    }

    public void Init()
    {
        PlayerPrefs.DeleteAll();

        //If there is a DB load from there
        PlayerPrefs.SetFloat("totalCalories", 0f);
        PlayerPrefs.SetFloat("totalFat", 0f);
        PlayerPrefs.SetFloat("totalProtein", 0f);
        PlayerPrefs.SetFloat("totalDietaryFiber", 0f);

        viewButton.interactable = false;
        resetButton.interactable = false;
    }

    public void OnApplicationQuit()
    {
        PlayerPrefs.DeleteAll();
    }

    public void Quit()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
                activity.Call<bool>("moveTaskToBack", true);
            }
            else
            {
                Application.Quit();
            }
        }
    }
}