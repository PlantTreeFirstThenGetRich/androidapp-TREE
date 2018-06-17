using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.Text;
using System.IO;
using UnityEngine.UI;
using System;

public class GetWeather : MonoBehaviour {

    private string m_url = "http://www.weather.com.cn/data/cityinfo/101031000.html";// 天气
    private string w_url = "http://www.weather.com.cn/data/cityinfo/";
    private string c_url = "http://api.map.baidu.com/location/ip?ak=bretF4dm6W5gqjQAXuvP0NXW6FeesRXb&coor=bd09ll";//Location

    // Location
    // public Text txLocation;
    public string myCity = "";
    public string cityCode = "";
    public string cityCodeJson = "";

    // Weather
    public Text txWeather;
    // public Image imWeather;

    private bool net_state = false;
    private DateTime dNow;
    private float deltaTimeforWeather = 0.0f; // update Weather
    private float howlongToUpdate = 5.0f; // 60f表示一分钟  3600f表示一小时

    // Use this for initialization
    void Start () {
        txWeather = GameObject.Find("Canvas/Weather").GetComponent<Text>();

        if (Application.internetReachability == NetworkReachability.NotReachable || true)
        {
            // TODO: without network——读取本地文件
            net_state = false;
            Debug.Log("======Without network======");
            txWeather.text = "R:" + SuperGameMaster.saveData.lastWeather.ToString() + "连网以更新天气";
        }
        else
        {
            // TOD0: 将天气状况保存在本地
            net_state = true;
            Debug.Log("======With network======");
            StartCoroutine(GetLocation());
            // imWeather = GameObject.Find("Canvas/Image").GetComponent<Image>();
            if (myCity.Length != 0)
            {
                Debug.Log("----Start myCity=" + myCity);
            }
            StartCoroutine(GetWeatherIE());
        }
    }
	
	// Update is called once per frame
	void Update () {
        if(net_state == false)
        {
            // Start时没有联网才会不断判断网络状况
            dNow = System.DateTime.Now;
            // Debug.Log("-------------" + dNow.ToString());
            deltaTimeforWeather += Time.deltaTime;
            // Debug.Log("deltaTimeforWeather=" + deltaTimeforWeather);
            if (deltaTimeforWeather > howlongToUpdate)
            {
                Debug.Log("deltaTimeforWeather > howlongToUpdate");
                if (Application.internetReachability == NetworkReachability.NotReachable)
                {
                    return;
                }
                else
                {
                    // 从没有网变为有网
                    Debug.Log("----------Network changed");
                    net_state = true;
                    txWeather.text = "正在获取天气状态";
                    StartCoroutine(GetLocation());
                    StartCoroutine(GetWeatherIE());
                }
            }
        }
    }

    public void UpdateWeather()
    {
        StartCoroutine(GetWeatherIE());
    }

    IEnumerator GetLocation()
    {
        Debug.Log("---------GetLocation");
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
                Debug.Log("GetLocation myCity=" + myCity);
            }
        }
    }

    IEnumerator GetWeatherIE()
    {
        Debug.Log("---------GetWeather");
        while (myCity.Length == 0)
        {
            yield return null;
        }
        if (myCity.Length != 0)
        {
            // 从json文件中读取城市编码信息
            var gb2312 = Encoding.GetEncoding("GB2312");
            string jsonFile = Application.dataPath + "/Resources/cityCode.json";
            Debug.Log("----jsonFile" + jsonFile); // TODO: apk拿不到路径
            StreamReader sr = new StreamReader(jsonFile, gb2312, true);
            string allCityCodeStr = sr.ReadToEnd();
            sr.Close();
            JsonData allCityCodeJd = JsonMapper.ToObject(allCityCodeStr);
            JsonData allCityCode = allCityCodeJd["cityCode"];

            // 遍历json，根据cityName找到cityCode
            Debug.Log("-----开始遍历json，找对应cityCode");
            for (var i = 0; i < 32; i++)
            {
                JsonData jsonProvince = allCityCode[i];
                JsonData jsonCity = jsonProvince["city"];
                bool found = false;
                for (var j = 0; j < jsonCity.Count; j++)
                {
                    JsonData temp1 = jsonCity[j];
                    string myCityName = temp1["cityName"].ToString();
                    if (myCityName.Contains(myCity) || myCity.Contains(myCityName))
                    {
                        Debug.Log("----遍历cityCode.json myCityName=" + myCityName);
                        cityCode = temp1["cityCode"].ToString();
                        found = true;
                        break;
                    }
                }
                if (found)
                {
                    break;
                }
            }

            w_url = w_url + cityCode + ".html";
            Debug.Log("-----w_url=" + w_url);
            WWW www = new WWW(w_url);
            while (!www.isDone)
            {
                yield return www;
            }
            if (www.text != null)
            {
                JsonData jd = JsonMapper.ToObject(www.text);
                JsonData jdInfo = jd["weatherinfo"];
                Debug.Log(jdInfo);
                string resultCity = jdInfo["city"].ToString();
                string strLow = jdInfo["temp1"].ToString();
                string strHigh = jdInfo["temp2"].ToString();
                string resultTemp = strLow + "~" + strHigh;
                string resultWeather = jdInfo["weather"].ToString();
                txWeather.text = resultCity + " " + resultWeather + " " + resultTemp;
                /* string imagePath = Application.dataPath + "/Resources/no.gif";
                byte[] imageByte = File.ReadAllBytes(imagePath);
                Texture2D imageTexture = new Texture2D(1, 1);
                if (ImageConversion.LoadImage(imageTexture, imageByte))
                {
                    Sprite imageSprite = Sprite.Create(imageTexture, new Rect(0, 0, imageTexture.width, imageTexture.height), new Vector2(0, 0));
                    imWeather.sprite = imageSprite;
                } */
                double tempMax = Convert.ToDouble(System.Text.RegularExpressions.Regex.Replace(strHigh, "[℃]", ""));
                saveWeatherToFile(tempMax, resultWeather);
            }
        }
    }

    public void saveWeatherToFile(double t, string w)
    {
        if (w.Contains("晴"))
        {
            if (t > 29)
            {
                SuperGameMaster.saveData.lastWeather = Weathers.Hot;
                Debug.Log("更新为Hot");
            }
            else
            {
                SuperGameMaster.saveData.lastWeather = Weathers.Sunny;
                Debug.Log("更新为Sunny");
            }
        }
        else if (w.Contains("雨"))
        {
            SuperGameMaster.saveData.lastWeather = Weathers.Rain;
            Debug.Log("更新为Rain");
        }
        else if (w.Contains("雪"))
        {
            SuperGameMaster.saveData.lastWeather = Weathers.Snowy;
            Debug.Log("更新为Snowy");
        }
        else if (w.Contains("多云") || w.Contains("阴"))
        {
            SuperGameMaster.saveData.lastWeather = Weathers.Cloudy;
            Debug.Log("更新为Cloudy");
        }
        else
        {
            SuperGameMaster.saveData.lastWeather = Weathers.Sunny;
            Debug.Log("更新为Sunny");
        }
        SuperGameMaster.SaveDataToFile();
    }
}
