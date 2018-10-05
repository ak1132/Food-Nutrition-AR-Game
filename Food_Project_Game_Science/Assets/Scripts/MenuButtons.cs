using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtons : MonoBehaviour {



	// Use this for initialization
	void Start () {
		
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
