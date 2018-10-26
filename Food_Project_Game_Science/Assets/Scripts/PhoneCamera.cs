using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using File = System.IO.File;


public class PhoneCamera : MonoBehaviour {

	/*private bool camAvailable;
	private WebCamTexture backCam;
	private Texture defaultBackground;
	
	public Rawimage background;
	public AspectRatioFilter fit;


	// Use this for initialization
	void Start () {
		defaultBackground = background.texture;
		WebCamDevice[] devices = WebCamTexture.decives;
		
		if (devices.Length == 0)
		{
			Debug.Log("No camera found");
			camAvailable = false;
			return;
		}
		
	}*/
	
	public WebCamTexture mCamera = null;
	public GameObject plane;

	// Use this for initialization
	void Start ()
	{
			Debug.Log ("Script has been started");
			plane = GameObject.FindWithTag ("Player");

			mCamera = new WebCamTexture ();
			plane.GetComponent<Renderer>().material.mainTexture = mCamera;
			mCamera.Play ();

	}
	
	
	void OnMouseDown()
    {
		
        ScreenCapture.CaptureScreenshot("currentFood.png");
		Texture2D tex = LoadPNG("currentFood.png");
		
		Color32 foodAvgColor = AverageColorFromTexture(tex);
		
		if ((foodAvgColor.r > foodAvgColor.g+40) && (foodAvgColor.r > foodAvgColor.b+40)){
			PlayerPrefs.SetFloat("Vitamins", PlayerPrefs.GetFloat("Vitamins", 0)+10);
			
		}
		
		PlayerPrefs.SetFloat("Vitamins", 15);	//This line is a quick fix because my camera wasn't working with scene changes
		Application.LoadLevel ("OpeningScene");
    }
	
	
	Texture2D LoadPNG(string filePath) {
	 
		 Texture2D tex = null;
		 byte[] fileData;
	 
		 fileData = File.ReadAllBytes(filePath);
		 tex = new Texture2D(2, 2);
		 tex.LoadImage(fileData); //this will auto-resize the texture dimensions
	 }
	
	//Takes average color from a 
	Color32 AverageColorFromTexture(Texture2D tex){
		Color32[] texColors = tex.GetPixels32();
		int total = texColors.Length;
		float r = 0;
		float g = 0;
		float b = 0;
		for(int i = 0; i < total; i++) {
			r += texColors[i].r;
			g += texColors[i].g;
			b += texColors[i].b;
		}

		return new Color32((byte)(r / total) , (byte)(g / total) , (byte)(b / total) , 0);
	}
	
	
	// Update is called once per frame
	void Update () {
		
	}
}


 
