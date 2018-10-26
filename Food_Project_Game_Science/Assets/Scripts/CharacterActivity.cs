using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterActivity : MonoBehaviour {

public Transform target;
	UnityEngine.AI.NavMeshAgent agent;
	
	/*
	These variables didn't work when I tried to make the code below shorter, I probably just overlooked something
	 float protein = PlayerPrefs.GetFloat("Protein", 0);
	 float carbs = PlayerPrefs.GetFloat("Carbohydrates", 0);
	 float fats = PlayerPrefs.GetFloat("Fats", 0);
	 float vitamins = PlayerPrefs.GetFloat("Vitamins", 0);
	 float minerals = PlayerPrefs.GetFloat("Minerals", 0);
	 float water = PlayerPrefs.GetFloat("Water", 0);
	 */
	
	
	// Use this for initialization
	void Start () {
		//Agent line is for navigation
		agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
		
		//This sets the players new proportions (exaggerated for demo)
		transform.localScale = new Vector3((int) 1+(PlayerPrefs.GetFloat("Carbohydrates", 0)+2*PlayerPrefs.GetFloat("Fats", 0))/5 , (int) 1+(PlayerPrefs.GetFloat("Vitamins", 0)+PlayerPrefs.GetFloat("Minerals", 0))/5, (int) 1+(PlayerPrefs.GetFloat("Carbohydrates", 0)+2*PlayerPrefs.GetFloat("Fats", 0))/5); //Stuff from
		GetComponent<UnityEngine.AI.NavMeshAgent>().speed = 2+PlayerPrefs.GetFloat("Protein", 0)+PlayerPrefs.GetFloat("Water", 0)-PlayerPrefs.GetFloat("Fats", 0);
	}
	
	// Update is called once per frame
	void Update () {
		
		//Eating too much of any one food makes the character not want to move
		if ((PlayerPrefs.GetFloat("Protein", 0) >= 30.0) || (PlayerPrefs.GetFloat("Carbohydrates", 0) >= 30.0) || (PlayerPrefs.GetFloat("Fats", 0) >= 30.0) || (PlayerPrefs.GetFloat("Vitamins", 0) >= 30.0) || (PlayerPrefs.GetFloat("Minerals", 0) >= 30.0) || (PlayerPrefs.GetFloat("Water", 0) >= 30.0)){
			transform.localPosition = new Vector3(-4, 0, -4);
		}
		
		
		
		//Rest of the update is for making character move on mouse click		
		agent.SetDestination(target.position);
		if (Input.GetMouseButtonDown(0)) {
			RaycastHit hit;
                
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100)) {
                    target.position = hit.point;
            }
		}
	}
}
