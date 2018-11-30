using UnityEngine;

public class LoadScript : MonoBehaviour
{

    void Start()
    {
        PlayerPrefs.DeleteAll();

        //If there is a DB load from there
        PlayerPrefs.SetFloat("totalCalories", 0f);
        PlayerPrefs.SetFloat("totalFat", 0f);
        PlayerPrefs.SetFloat("totalProtein", 0f);
        PlayerPrefs.SetFloat("totalDietaryFiber", 0f);
    }
}
