using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using UnityEngine.UI;
public class TopTenItem
{
    /// <summary>
    /// 
    /// </summary>
    public int score { get; set; }
}

public class Root
{
    /// <summary>
    /// 
    /// </summary>
    public List <TopTenItem > topTen { get; set; }
}

public class RankScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
		string url = "http://39.106.107.66:3000/get-rank";
		string res = "";
		string get_rank = postData(url, res);
		GameObject canvas = GameObject.Find("Canvas");
		GameObject[] rank_list = new GameObject[10];
		
		rank_list[0] = canvas.transform.Find("rankScroll/rankView/rankContent/1st").gameObject;
		rank_list[1] = canvas.transform.Find("rankScroll/rankView/rankContent/2nd").gameObject;
		rank_list[2] = canvas.transform.Find("rankScroll/rankView/rankContent/3rd").gameObject;
		rank_list[3] = canvas.transform.Find("rankScroll/rankView/rankContent/4th").gameObject;
		rank_list[4] = canvas.transform.Find("rankScroll/rankView/rankContent/5th").gameObject;
		rank_list[5] = canvas.transform.Find("rankScroll/rankView/rankContent/6th").gameObject;
		rank_list[6] = canvas.transform.Find("rankScroll/rankView/rankContent/7th").gameObject;
		rank_list[7] = canvas.transform.Find("rankScroll/rankView/rankContent/8th").gameObject;
		rank_list[8] = canvas.transform.Find("rankScroll/rankView/rankContent/9th").gameObject;
		rank_list[9] = canvas.transform.Find("rankScroll/rankView/rankContent/10th").gameObject;
		Root rank = new Root();

		rank  = JsonConvert.DeserializeObject<Root>(get_rank);
		for(int i = 0; i < rank.topTen.Count; i++) {
			rank_list[i].SetActive(false);
			rank_list[i].GetComponent<Text>().text = " No." + (i + 1) + " : " +rank.topTen[i].score;
			rank_list[i].GetComponent<Text>().fontSize = 20;
			rank_list[i].SetActive(true);
		}

		GameObject historyObj = GameObject.Find("Canvas/foolPanel/best");
		GameObject rankObj = GameObject.Find("Canvas/foolPanel/myRank");
		if(PlayerPrefs.GetInt("is_log") == 0) historyObj.GetComponent<Text>().text  = "历史最高: 未登录";
		else historyObj.GetComponent<Text>().text  = "历史最高: " + PlayerPrefs.GetInt("my_best");
		
		string cur_rank;
		if(PlayerPrefs.GetInt("my_rank") == -1) cur_rank = "未上榜";
		else cur_rank = "" + PlayerPrefs.GetInt("my_rank");
		Debug.Log("my best score: " + PlayerPrefs.GetInt("my_best"));
		rankObj.GetComponent<Text>().text = "我的排名: " + cur_rank;
		
	}
	
	// Update is called once per frame
	void Update () {
		
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
