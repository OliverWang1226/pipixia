using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class backToStartBtn : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject backToStartBtnObj = GameObject.Find("Canvas/foolPanel/backToStart");
		Button back_start_btn = (Button)backToStartBtnObj.GetComponent<Button>() as Button;
		back_start_btn.onClick.AddListener(()=>{
			Application.LoadLevel("start");
		});
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
