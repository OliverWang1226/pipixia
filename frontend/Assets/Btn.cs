using System.Collections;
using System.Collections.Generic;
using System.Timers;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;
using Timer = System.Timers.Timer;
using System.Text;
using System.Net;
using Newtonsoft.Json;
using System.IO;

public class UserScore
{
    /// <summary>
    /// 
    /// </summary>
    public string phone_number { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int curr_score { get; set; }
}

public class UserRank {
    /// <summary>
    /// 
    /// </summary>
    public int curr_rank { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int best_score { get; set; }
}

public class Btn : MonoBehaviour
{
    public Animator hero_animator;
    public Animator boss_animator;
    public Image my_img;
    public GameObject myImg;
    public bool is_gap = true; //仅在gap = false时点击按钮才视为有效
    public float cycle = 2; //update函数执行周期
    public float gap_time = 0.5f;
    public int current_type; //当前img的种类： current_type = 1 时一只皮皮虾，2为两只
    public int score = 0;
    public GameObject overDialog;

    public bool is_clicked = false;
    void Awake()
    {
        //获取按钮的gameobject
        GameObject leftBtnObj = GameObject.Find("Canvas/leftBtn");
        GameObject rightBtnObj = GameObject.Find("Canvas/rightBtn");
        GameObject canvas = GameObject.Find("Canvas");
        overDialog = canvas.transform.Find("overDialog").gameObject;
        //将按钮的gameobject转换为button
        Button left_btn = (Button)leftBtnObj.GetComponent<Button>() as Button;
        Button right_btn = (Button)rightBtnObj.GetComponent<Button>() as Button;

        //为按钮添加响应事件
        left_btn.onClick.AddListener(leftClick);
        right_btn.onClick.AddListener(rightClick);

        //获取image对象
        myImg = GameObject.Find("Canvas/Image");
        my_img = myImg.GetComponent<Image>();

        //获取角色对象
        GameObject character = GameObject.Find("character");
        GameObject boss = GameObject.Find("boss");
        //获取角色对应的animator
        hero_animator = character.GetComponent<Animator>();
        boss_animator = boss.GetComponent<Animator>();
    }

    void Start()
    {
        //设置函数执行周期
        InvokeRepeating("setBlank", 0, 2);
    }
    //在更新图片之前，先进入gap_t
    public void setBlank()
    {
        my_img.sprite = Resources.Load("blank", typeof(Sprite)) as Sprite;
        is_gap = true;
        Invoke("updateImg", gap_time);
    }
    //周期性执行该函数，以动态更新图片
    public void updateImg()
    {
        is_gap = false;
        //两只皮皮虾时，下一种一定是一只
        if (current_type == 2)
        {
            my_img.sprite = Resources.Load("boss", typeof(Sprite)) as Sprite;
            current_type = 1;
        }
        else
        {
            System.Random rand = new System.Random();
            int type = rand.Next(0, 10);
            if (type < 6)
            {
                my_img.sprite = Resources.Load("boss", typeof(Sprite)) as Sprite;
                current_type = 1;
            }
            else
            {
                my_img.sprite = Resources.Load("double", typeof(Sprite)) as Sprite;
                current_type = 2;
            }
        }
        is_clicked = false;


    }
    //boss hero回复idle状态
    public void backToIdle()
    {
        hero_animator.Play("Idel", 0, 0f);
        boss_animator.Play("bossIDLE", 0, 0f);
    }
    //左边button点击事件
    void leftClick()
    {
        if (!is_clicked && !is_gap)
        {
            if (current_type == 2)
            {
                //Game Over
                //set cache
                //若用户已登陆，上传用户得分
                if (PlayerPrefs.GetInt("is_log") == 1)
                {
                    //获取手机号
                    string phone_number = PlayerPrefs.GetString("phone_number");
                    //上传得分
                    UserScore new_score = new UserScore();
                    new_score.phone_number = phone_number;
                    new_score.curr_score = score;

                    string json = JsonConvert.SerializeObject(new_score);
                    string post_url = "http://39.106.107.66:3000/update";
                    string resp_json = postData(post_url, json);

                    UserRank new_rank = new UserRank();
                    new_rank = JsonConvert.DeserializeObject<UserRank>(resp_json);
                    Debug.Log(resp_json);

                    PlayerPrefs.SetInt("my_rank", new_rank.curr_rank);
                    PlayerPrefs.SetInt("my_best", new_rank.best_score);
                    
                }
                overDialog.SetActive(true);
            }
            else
            {
                //执行动作并更新分数
                is_clicked = true;
                hero_animator.Play("Attack", 0, 0f);
                boss_animator.Play("bossAttack", 0, 0f);
                score = score + 1;
                GameObject.Find("Canvas/Text").GetComponent<Text>().text = "Score: " + score;
            }
            Invoke("backToIdle", 1);
        }

    }
    void rightClick()
    {
        if (!is_clicked && !is_gap)
        {
            if (current_type == 1)
            {
                if (PlayerPrefs.GetInt("is_log") == 1)
                {
                    //获取手机号
                    string phone_number = PlayerPrefs.GetString("phone_number");
                    //上传得分
                    UserScore new_score = new UserScore();
                    new_score.phone_number = phone_number;
                    new_score.curr_score = score;

                    string json = JsonConvert.SerializeObject(new_score);
                    string post_url = "http://39.106.107.66:3000/update";
                    string resp_json = postData(post_url, json);
                    Debug.Log(resp_json);
                    UserRank new_rank = new UserRank();
                    new_rank = JsonConvert.DeserializeObject<UserRank>(resp_json);
                    PlayerPrefs.SetInt("my_rank", new_rank.curr_rank);
                    PlayerPrefs.SetInt("my_best", new_rank.best_score);
                    
                }
                overDialog.SetActive(true);
            }
            else
            {
                //执行动作并更新分数
                is_clicked = true;
                hero_animator.Play("Attack", 0, 0f);
                boss_animator.Play("bossAttack", 0, 0f);
                score = score + 1;
                GameObject.Find("Canvas/Text").GetComponent<Text>().text = "Score: " + score;
            }
            Invoke("backToIdle", 1);
        }

    }
    // Update is called once per frame
    void Update()
    {

    }
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
