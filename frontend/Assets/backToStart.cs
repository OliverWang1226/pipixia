using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class backToStart : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		GameObject backObj = GameObject.Find("Canvas/backDialog/backCfmBtn");

		Button back_btn = (Button)backObj.GetComponent<Button>() as Button;

		back_btn.onClick.AddListener(backClick);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void backClick() {
		Application.LoadLevel("start");
	}
}
