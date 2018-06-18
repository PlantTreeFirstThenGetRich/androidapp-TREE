using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.Text;
using System.IO;
using UnityEngine.UI;
using System;
using System.Net;
using UnityEngine.Networking;

public class GetWeather : MonoBehaviour
{

    private string c_url = "http://api.map.baidu.com/location/ip?ak=bretF4dm6W5gqjQAXuvP0NXW6FeesRXb&coor=bd09ll";//Location

    // Location
    // public Text txLocation;
    public string myCity = "";
    public string cityCode = "";
    public string cityCodeJson = "";

    // Weather
    public Text txWeather;

    private bool net_state = false;
    private DateTime dNow;
    private float deltaTimeforWeather = 0.0f; // update Weather
    private float howlongToUpdate = 10.0f; // 60f表示一分钟  3600f表示一小时

    public Sprite[] sprites;
    SpriteRenderer spriteRenderer;
    private int flag = 0;

    // Use this for initialization
    void Start()
    {
        txWeather = GameObject.Find("Canvas/Weather").GetComponent<Text>();
        spriteRenderer = GetComponent<Renderer>() as SpriteRenderer;
        // Debug.Log("r40 " + sprites.Length);

        if (Application.internetReachability == NetworkReachability.NotReachable || true)
        {
            net_state = false;
            Debug.Log("======Without network======");
            txWeather.text = "连网更新天气";
        }
        else
        {
            net_state = true;
            Debug.Log("======With network======");
            StartCoroutine(GetLocation());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (net_state == false)
        {
            // Start时没有联网才会不断判断网络状况
            dNow = System.DateTime.Now;
            deltaTimeforWeather += Time.deltaTime;
            if (deltaTimeforWeather > howlongToUpdate)
            {
                // Debug.Log("deltaTimeforWeather > howlongToUpdate");
                if (Application.internetReachability == NetworkReachability.NotReachable)
                {
                    return;
                }
                else
                {
                    // 从没有网变为有网
                    Debug.Log("----------Network changed");
                    net_state = true;
                    if (flag == 0)
                    {
                        StartCoroutine(GetLocation());
                    }
                    
                }
            }
        }
    }


    IEnumerator GetLocation()
    {
        if (c_url != null)
        {
            WWW www = new WWW(c_url);
            while (!www.isDone) { yield return www; }

            if (www.text != null)
            {
                JsonData jd = JsonMapper.ToObject(www.text);
                JsonData cityInfo = jd["content"];
                JsonData cityDetail = cityInfo["address_detail"];
                myCity = cityDetail["city"].ToString();
                myCity = myCity.Replace("市", string.Empty);
                string getResult = PostWebRequest("http://www.moodiary.top:3000", "cityName=" + myCity);
                GetWeatherFromStr(getResult);
            }
        }
    }


    private string PostWebRequest(string postUrl, string paramData)
    {
        // 把字符串转换为bype数组  
        byte[] bytes = Encoding.GetEncoding("utf-8").GetBytes(paramData);

        HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(new Uri(postUrl));
        webReq.Method = "POST";
        webReq.ContentType = "application/x-www-form-urlencoded;charset=gb2312";
        webReq.ContentLength = bytes.Length;
        using (Stream newStream = webReq.GetRequestStream())
        {
            newStream.Write(bytes, 0, bytes.Length);
        }
        using (WebResponse res = webReq.GetResponse())
        {
            //在这里对接收到的页面内容进行处理  
            Stream responseStream = res.GetResponseStream();
            StreamReader streamReader = new StreamReader(responseStream, Encoding.GetEncoding("UTF-8"));
            string str = streamReader.ReadToEnd();
            streamReader.Close();
            responseStream.Close();
            //返回：服务器响应流   
            return str;
        }
    }


    private void GetWeatherFromStr(string result)
    {
        JsonData jd = JsonMapper.ToObject(result);
        string temperature = jd["temp2"].ToString();
        string weather = jd["weather"].ToString();
        Debug.Log("temprature " + temperature + "    weather " + weather);
        double tempMax = Convert.ToDouble(System.Text.RegularExpressions.Regex.Replace(temperature, "[℃]", ""));
        saveWeatherToFile(tempMax, weather);
        string readWeather = SuperGameMaster.saveData.lastWeather.ToString();

        txWeather.text = "";
        if (readWeather=="Sunny")
        {
            if (sprites.Length == 5) { spriteRenderer.sprite = sprites[0]; }
        }
        else if (readWeather == "Cloudy")
        {
            if (sprites.Length == 5) { spriteRenderer.sprite = sprites[1]; }
        }
        else if(readWeather=="Hot")
        {
            if (sprites.Length == 5) { spriteRenderer.sprite = sprites[2]; }
        }
        else if(readWeather=="Rain")
        {
            if (sprites.Length == 5) { spriteRenderer.sprite = sprites[3]; }
        }
        else if (readWeather == "Snowy")
        {
            if (sprites.Length == 5) { spriteRenderer.sprite = sprites[4]; }
        }
        else
        {
            if (sprites.Length == 5) { spriteRenderer.sprite = sprites[0]; }
        }
    }


    private void saveWeatherToFile(double t, string w)
    {
        if (w.Contains("晴"))
        {
            if (t > 29)
            {
                SuperGameMaster.saveData.lastWeather = Weathers.Hot;
            }
            else
            {
                SuperGameMaster.saveData.lastWeather = Weathers.Sunny;
            }
        }
        else if (w.Contains("雨"))
        {
            SuperGameMaster.saveData.lastWeather = Weathers.Rain;
        }
        else if (w.Contains("雪"))
        {
            SuperGameMaster.saveData.lastWeather = Weathers.Snowy;
        }
        else if (w.Contains("多云") || w.Contains("阴"))
        {
            SuperGameMaster.saveData.lastWeather = Weathers.Cloudy;
        }
        else
        {
            SuperGameMaster.saveData.lastWeather = Weathers.Sunny;
        }
        SuperGameMaster.SaveDataToFile();
    }
}
