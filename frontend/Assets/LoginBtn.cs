using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginBtn : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Button login_btn = GameObject.Find("Canvas/LoginBtn").GetComponent<Button>() as Button;
		login_btn.onClick.AddListener(()=> {
			Application.LoadLevel("game");
		});
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
