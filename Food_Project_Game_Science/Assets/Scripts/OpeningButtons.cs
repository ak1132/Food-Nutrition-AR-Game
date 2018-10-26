using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningButtons : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	public void GoToFoodScan()  {
		Application.LoadLevel ("CameraScene");
	}
	
	public void ResetGame(){
		PlayerPrefs.DeleteAll();
		Application.LoadLevel ("OpeningScene");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
