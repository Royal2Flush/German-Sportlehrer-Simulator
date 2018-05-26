using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	private Text statusText;
	private static UIManager instance;
	private int points = 0;

	public static UIManager Instance
	{
		get { return instance ?? (instance = new GameObject ("UIManager").AddComponent<UIManager> ()); }
	}

	public void AddPoints(int p) {
		points += p;
		if (statusText != null) {
			statusText.text = "Points: " + points.ToString ();
		}
	}

	// Use this for initialization
	void Start () {
		GameObject st = GameObject.Find ("StatusText");
		statusText = st.GetComponent<Text> ();
		Debug.Log (statusText);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
