using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class overCfm : MonoBehaviour {
	public string score;

	// Use this for initialization
	void Awake () {
		GameObject canvas = GameObject.Find("Canvas");
		GameObject overCfmObj = canvas.transform.Find("overDialog/overCfmBtn").gameObject;
		Button over_btn = (Button)overCfmObj.GetComponent<Button>() as Button;
		over_btn.onClick.AddListener(overClick);
		//over_btn.onClick.AddListener(overClick);
		//get Score
		score = GameObject.Find("Canvas/Text").GetComponent<Text>().text;
		GameObject.Find("Canvas/overDialog/Text").GetComponent<Text>().text = "Your " +  score;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void overClick() {
		Application.LoadLevel("rank");
	}
}
