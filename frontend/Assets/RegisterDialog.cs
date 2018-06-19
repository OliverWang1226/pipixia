using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System.IO;
using System.Text;
using Newtonsoft.Json;
public class UserInfo
{
    /// <summary>
    /// 
    /// </summary>
    public string phone_number { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string password { get; set; }
}

public class RegisterResp
{
    /// <summary>
    /// 
    /// </summary>
    public int result_status { get; set; } // 0手机号已存在， 1注册成功
}
public class RegisterDialog : MonoBehaviour
{
    public GameObject titleObj;
    public GameObject introductionObj;
    public GameObject LoginDialogObj;
    public GameObject RegisterDialogObj;
    public GameObject canvas;
    public GameObject tip_panel;
    public GameObject tip;
    // Use this for initialization
    void Awake()
    {
        canvas = GameObject.Find("Canvas");

        // Get hiding objects
        titleObj = canvas.transform.Find("title").gameObject;
        introductionObj = canvas.transform.Find("introduction").gameObject;
        LoginDialogObj = canvas.transform.Find("LoginDialog").gameObject;
        RegisterDialogObj = canvas.transform.Find("RegisterDialog").gameObject;


        // Get button objects
        GameObject RegisterCfmBtnObj = GameObject.Find("Canvas/RegisterDialog/RegisterCfmBtn");
        GameObject RegisterCncBtnObj = GameObject.Find("Canvas/RegisterDialog/RegisterCncBtn");

        // Get buttons
        Button cfm_btn = (Button)RegisterCfmBtnObj.GetComponent<Button>() as Button;
        Button cnc_btn = (Button)RegisterCncBtnObj.GetComponent<Button>() as Button;

        // Add event Listener
        cfm_btn.onClick.AddListener(cfmClick);
        cnc_btn.onClick.AddListener(cncClick);
    }

    // Update is called once per frame
    void Update()
    {
    }
    // Confirm to Register
    void cfmClick()
    {
        // get uset information
        string phone_num = canvas.transform.Find("RegisterDialog/phoneRegister/Text").gameObject.GetComponent<Text>().text;
        string pass_word = canvas.transform.Find("RegisterDialog/passRegister").gameObject.GetComponent<InputField>().text;
        string re_pass = canvas.transform.Find("RegisterDialog/repassRegister").gameObject.GetComponent<InputField>().text;
        
        tip_panel = canvas.transform.Find("tipPanel").gameObject;
        tip = canvas.transform.Find("tipPanel/Text").gameObject;
        // check phone number 
        if (!CheckPhoneIsAble(phone_num))
        {
            tip.GetComponent<Text>().text = "手机号不合法";
            tip_panel.SetActive(true);
            Invoke("hideTips", 0.8f);
        }

        // check pass words
        if (pass_word != re_pass)
        {
            tip.GetComponent<Text>().text = "两次输入密码不一致";
            tip_panel.SetActive(true);
            Invoke("hideTips", 0.8f);
        }
        else if (pass_word.Length < 6)
        {
            tip.GetComponent<Text>().text = "密码长度不得小于6位";
            tip_panel.SetActive(true);
            Invoke("hideTips", 0.8f);
        }
        else
        {
            // Send phone_num and pass_word to backends
            UserInfo info = new UserInfo();
            info.phone_number = phone_num;
            info.password = pass_word;

            string json = JsonConvert.SerializeObject(info);

            string post_url = "http://39.106.107.66:3000/add-user";
            string resp_json = postData(post_url, json);
            // if(phone_num is occupied) ... else ...

            RegisterResp resp = JsonConvert.DeserializeObject<RegisterResp>(resp_json);
            if (resp.result_status == 0)
            {
                tip.GetComponent<Text>().text = "该手机号已注册过";
                tip_panel.SetActive(true);
                Invoke("hideTips", 0.8f);
            }
            else
            {
                tip.GetComponent<Text>().text = "注册成功,请登录";
                tip_panel.SetActive(true);
                Invoke("hideTips", 0.8f);
                RegisterDialogObj.SetActive(false);
                Invoke("showLogin", 0.3f);
            }

        }

    }
    void cncClick()
    {
        RegisterDialogObj.SetActive(false);
        Invoke("showTitle", 0.3f);
    }
    void hideTips()
    {
        tip_panel.SetActive(false);
    }
    void showTitle()
    {
        titleObj.SetActive(true);
        introductionObj.SetActive(true);
    }
    void showLogin()
    {
        LoginDialogObj.SetActive(true);
    }
    // check wheather phone number is leagal
    public bool CheckPhoneIsAble(string input)
    {
        if (input.Length < 11)
        {
            return false;
        }
        //电信手机号码正则
        string dianxin = @"^1[3578][01379]\d{8}$";
        Regex regexDX = new Regex(dianxin);
        //联通手机号码正则
        string liantong = @"^1[34578][01256]\d{8}";
        Regex regexLT = new Regex(liantong);
        //移动手机号码正则
        string yidong = @"^(1[012345678]\d{8}|1[345678][012356789]\d{8})$";
        Regex regexYD = new Regex(yidong);
        if (regexDX.IsMatch(input) || regexLT.IsMatch(input) || regexYD.IsMatch(input))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    // send post request and return a result
    public static string postData(string url, string content)
    {
        string result = "";
        HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
        req.Method = "POST";
        req.ContentType = "application/json";


        #region 添加Post 参数  
        byte[] data = Encoding.UTF8.GetBytes(content);
        req.ContentLength = data.Length;
        using (Stream reqStream = req.GetRequestStream())
        {
            reqStream.Write(data, 0, data.Length);
            reqStream.Close();
        }
        #endregion

        HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
        Stream stream = resp.GetResponseStream();
        //获取响应内容  
        using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
        {
            result = reader.ReadToEnd();
        }
        return result;
    }
}
