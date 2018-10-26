using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour {

	public Slider ProteinSlider;
	public Slider CarbohydrateSlider;
	public Slider FatSlider;
	public Slider VitaminSlider;
	public Slider MineralSlider;
	public Slider WaterSlider;


	// Use this for initialization
	void Start () {
		
	}
	
	public void DoneEating(){
		//Protein, Carbs, Fats, Vitamins, Minerals, Water
		PlayerPrefs.SetFloat("Protein", PlayerPrefs.GetFloat("Protein", 0)+ProteinSlider.value);
		PlayerPrefs.SetFloat("Carbohydrates", PlayerPrefs.GetFloat("Carbohydrates", 0)+CarbohydrateSlider.value);
		PlayerPrefs.SetFloat("Fats", PlayerPrefs.GetFloat("Fats", 0)+FatSlider.value);
		PlayerPrefs.SetFloat("Vitamins", PlayerPrefs.GetFloat("Vitamins", 0)+VitaminSlider.value);
		PlayerPrefs.SetFloat("Minerals", PlayerPrefs.GetFloat("Minerals", 0)+MineralSlider.value);
		PlayerPrefs.SetFloat("Water", PlayerPrefs.GetFloat("Water", 0)+WaterSlider.value);
		Application.LoadLevel ("OpeningScene");
	}
	
	

	
	
	//These functions are for different button press functions
	public void MakeHealthy(){
		PlayerPrefs.SetInt("Height", PlayerPrefs.GetInt("Height", 0)+4);
	}
	
	public void MakeHealthier(){
		PlayerPrefs.SetInt("Height", PlayerPrefs.GetInt("Height", 0)+10);
	}
	
	public void MakeFat(){
		PlayerPrefs.SetInt("Fat", PlayerPrefs.GetInt("Fat", 0)+4);
	}
	
	public void MakeFatter(){
		PlayerPrefs.SetInt("Fat", PlayerPrefs.GetInt("Fat", 0)+10);
	}
	
	public void GoToOpening()  {
		
		Application.LoadLevel ("OpeningScene");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
