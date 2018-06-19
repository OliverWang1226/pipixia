using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using System.Text;
public class LogInfo
{
    /// <summary>
    /// 
    /// </summary>
    public string number { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string password { get; set; }
}

public class LoginResp
{
    /// <summary>
    /// 
    /// </summary>
    public int result_status { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int curr_rank { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int best_score { get; set; }
}
public class LoginDialog : MonoBehaviour
{
    public GameObject titleObj;
    public GameObject introduceObj;
    public GameObject RegisterDialogObj;
    public GameObject LoginDialogObj;
    public GameObject canvas;
    public GameObject tip;
    public GameObject tip_panel;

    public GameObject loginBtnObj;
    public GameObject logoutBtnObj;

    // Use this for initialization
    void Awake()
    {
        // get root
        canvas = GameObject.Find("Canvas");

        // get title and introduce, the hide them
        titleObj = canvas.transform.Find("title").gameObject;
        introduceObj = canvas.transform.Find("introduction").gameObject;
        titleObj.SetActive(false);
        introduceObj.SetActive(false);      

        // route to hiding objects from root
        RegisterDialogObj = canvas.transform.Find("RegisterDialog").gameObject;
        LoginDialogObj = canvas.transform.Find("LoginDialog").gameObject;

        // get login logout btns
        loginBtnObj = canvas.transform.Find("LoginBtn").gameObject;
        logoutBtnObj = canvas.transform.Find("LogoutBtn").gameObject;

        // LoginDialog button objects
        GameObject LoginCfmBtnObj = GameObject.Find("Canvas/LoginDialog/LoginCfmBtn");
        GameObject LoginCncBtnObj = GameObject.Find("Canvas/LoginDialog/LoginCncBtn");
        GameObject toRegisterBtnObj = GameObject.Find("Canvas/LoginDialog/toRegisterBtn");

        // get Button Objects from GameObj
        Button log_cfm_btn = (Button)LoginCfmBtnObj.GetComponent<Button>() as Button;
        Button log_cnc_btn = (Button)LoginCncBtnObj.GetComponent<Button>() as Button;
        Button to_register_btn = (Button)toRegisterBtnObj.GetComponent<Button>() as Button;

        // add onclick listeners
        log_cfm_btn.onClick.AddListener(cfmClick);
        to_register_btn.onClick.AddListener(toRegClick);
    }

    // Update is called once per frame
    void Update()
    {

    }
    // confirm to Login
    void cfmClick()
    {
        tip = canvas.transform.Find("tipPanel/Text").gameObject;
        tip_panel = canvas.transform.Find("tipPanel").gameObject;
        // check infomations
        // get user information
        string phone_num = GameObject.Find("Canvas/LoginDialog/phoneLog/Text").GetComponent<Text>().text;
        string pass_word = GameObject.Find("Canvas/LoginDialog/passwordLog").GetComponent<InputField>().text;
        // send to backend and get response
        //       ........
        LogInfo user = new LogInfo();
        user.number = phone_num;
        user.password = pass_word;

        string json = JsonConvert.SerializeObject(user);
        string post_url = "http://39.106.107.66:3000/login";
        string resp_json = postData(post_url, json);

        LoginResp resp = JsonConvert.DeserializeObject<LoginResp>(resp_json);

        if (resp.result_status == 0)
        {
            tip.GetComponent<Text>().text = "手机号或密码错误";
            tip_panel.SetActive(true);
            Invoke("hideTips", 0.8f);
        }
        else
        {
            tip.GetComponent<Text>().text = "登陆成功";
            tip_panel.SetActive(true);
            Invoke("hideTips", 0.8f);
            // if right
            // set user info cache
            PlayerPrefs.SetString("phone_number", phone_num);
            PlayerPrefs.SetInt("my_rank", resp.curr_rank);
            PlayerPrefs.SetInt("my_best", resp.best_score);
            PlayerPrefs.SetInt("is_log", 1);
            // close Dialog
            LoginDialogObj.SetActive(false);
            loginBtnObj.SetActive(false);
            Invoke("showLogoutBtn", 0.3f);
            Invoke("showTitle", 0.3f);
        }


    }

    // route to Register Dialog
    void toRegClick()
    {
        LoginDialogObj.SetActive(false);
        Invoke("showRegister", 0.2f);
    }
    // show Register Dialog
    void showRegister()
    {
        RegisterDialogObj.SetActive(true);
    }
    
    void showLogoutBtn() {
        logoutBtnObj.SetActive(true);
    }
    // show title introduction
    void showTitle()
    {
        introduceObj.SetActive(true);
        titleObj.SetActive(true);
    }

    void hideTips()
    {
        tip_panel.SetActive(false);
    }
    
    public static string postData(string url, string content)
    {
        string result = "";

        HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
        req.Method = "POST";
        req.ContentType = "application/json";

        byte[] data = Encoding.UTF8.GetBytes(content);
        req.ContentLength = data.Length;

        using (Stream reqStream = req.GetRequestStream())
        {
            reqStream.Write(data, 0, data.Length);
            reqStream.Close();
        }

        HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
        Stream s = resp.GetResponseStream();

        using (StreamReader reader = new StreamReader(s, Encoding.UTF8))
        {
            result = reader.ReadToEnd();
        }

        return result;
    }
}

