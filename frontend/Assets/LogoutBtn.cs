using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LogoutBtn : MonoBehaviour {
	public GameObject tip;
	public GameObject tip_panel;
	public GameObject title;
	public GameObject introduceObj;
	public GameObject canvas;
	public GameObject loginBtnObj;
	public GameObject logoutBtnObj;
	public GameObject logDialogObj;
	// Use this for initialization
	void Start () {
		canvas = GameObject.Find("Canvas");

		//tip
		tip = canvas.transform.Find("tipPanel/Text").gameObject;
		tip_panel = canvas.transform.Find("tipPanel").gameObject;

		// title and introduction
		title = canvas.transform.Find("title").gameObject;
		introduceObj = canvas.transform.Find("introduction").gameObject;

		// dialog
		logDialogObj = canvas.transform.Find("LoginDialog").gameObject;

		// login logout btns
		loginBtnObj = canvas.transform.Find("LoginBtn").gameObject;
		logoutBtnObj = canvas.transform.Find("LogoutBtn").gameObject;
		Button logout_btn = (Button)logoutBtnObj.GetComponent<Button>() as Button;


		logout_btn.onClick.AddListener(() => {
			// tip
			tip.GetComponent<Text>().text = "退出登录";
			tip_panel.SetActive(true);
			Invoke("hideTips", 0.5f);
			// reset cache
			PlayerPrefs.SetInt("is_log", 0);
			PlayerPrefs.SetInt("my_rank", -1);
			PlayerPrefs.SetInt("my_best", 0);
			// hide logout btn
			logoutBtnObj.SetActive(false);
			// show login btn
			Invoke("showLoginBtn", 0.3f);
		});
	}
	void hideTips() {
		tip_panel.SetActive(false);
	}
	void showLoginBtn() {
		loginBtnObj.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
