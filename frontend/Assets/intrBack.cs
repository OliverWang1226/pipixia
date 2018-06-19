using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class intrBack : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		GameObject backObj = GameObject.Find("Canvas/intrBackBtn");
		Button back_btn = (Button)backObj.GetComponent<Button>() as Button;
		back_btn.onClick.AddListener( () => {
			Application.LoadLevel("start");
		});
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
