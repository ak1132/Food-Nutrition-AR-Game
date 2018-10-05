using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterActivity : MonoBehaviour {

public Transform target;
	UnityEngine.AI.NavMeshAgent agent;
	
	// Use this for initialization
	void Start () {
		//Agent line is for navigation
		agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
		
		//This sets the players new proportions (exaggerated for demo)
		transform.localScale = new Vector3(1+(PlayerPrefs.GetInt("Fat", 0)/5), 1+(PlayerPrefs.GetInt("Height", 0)/5), 1+(PlayerPrefs.GetInt("Fat", 0)/5));
	}
	
	// Update is called once per frame
	void Update () {
		
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
