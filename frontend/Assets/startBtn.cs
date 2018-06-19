using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class startBtn : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		GameObject startBtnObj = GameObject.Find("Canvas/startBtn");
		GameObject rankBtnObj = GameObject.Find("Canvas/rankBtn");
		GameObject instructionBtnObj = GameObject.Find("Canvas/instructionBtn");
		
		Button start_btn = (Button) startBtnObj.GetComponent<Button>() as Button;
		Button rank_btn = (Button) rankBtnObj.GetComponent<Button>() as Button;
		Button instr_btn = (Button) instructionBtnObj.GetComponent<Button>() as Button;

		start_btn.onClick.AddListener(startClick);
		rank_btn.onClick.AddListener(rankClick);
		instr_btn.onClick.AddListener(instrClick);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void startClick() {
			Application.LoadLevel("game");
		}
	void rankClick() {
		Application.LoadLevel("rank");
	}
	void instrClick() {
		Application.LoadLevel("instr");
	}
}
