using UnityEngine;
using UnityEngine.UI;

public class DisplayInfo : MonoBehaviour
{

    public GameObject targetCanvas;
    public GameObject nutrientCanvas;

    public void DisplayNutritionPage()
    {
        targetCanvas.SetActive(false);
        nutrientCanvas.SetActive(true);
        SetNutritionInfo();
    }

    private void SetNutritionInfo()
    {
        float Calories = PlayerPrefs.GetFloat("totalCalories");
        float TotalFat = PlayerPrefs.GetFloat("totalProtein");
        float Protein = PlayerPrefs.GetFloat("totalFat");
        float DietaryFiber = PlayerPrefs.GetFloat("totalDietaryFiber");

        string foods = PlayerPrefs.GetString("currentFoods");

        Text[] textFields = nutrientCanvas.GetComponentsInChildren<Text>();
        textFields[1].text = foods;
        textFields[3].text = Calories.ToString();
        textFields[5].text = TotalFat.ToString();
        textFields[7].text = Protein.ToString();
        textFields[9].text = DietaryFiber.ToString();
    }

    public void GoBack()
    {
        nutrientCanvas.SetActive(false);
        targetCanvas.SetActive(true);
    }
}