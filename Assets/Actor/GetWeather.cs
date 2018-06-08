using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.Text;
using System.IO;
using UnityEngine.UI;

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
    // public Text txCity;
    // public Text txTemp;
    public Text txWeather;
    // public Image imWeather;

    // private DateTime dNow;
    // private float deltaTimeforWeather = 0.0f; // update Weather
    // private float howlongToUpdate = 3600f;

    // Use this for initialization
    void Start () {
        // txLocation = GameObject.Find("Canvas/Text").GetComponent<Text>();
        if(Application.internetReachability == NetworkReachability.NotReachable)
        {
            // without network
            Debug.Log("======Without network======");
        }
        else
        {
            Debug.Log("======With network======");
            StartCoroutine(GetLocation());

            // txCity = GameObject.Find("Canvas/Text0").GetComponent<Text>();
            // txTemp = GameObject.Find("Canvas/Text1").GetComponent<Text>();
            txWeather = GameObject.Find("Canvas/Weather").GetComponent<Text>();
            // imWeather = GameObject.Find("Canvas/Image").GetComponent<Image>();

            if (myCity.Length != 0)
            {
                Debug.Log("----Start myCity=" + myCity);
            }
            UpdateWeather();
        }

        
    }
	
	// Update is called once per frame
	void Update () {
        /*try
        {
            dNow = System.DateTime.Now;
            Debug.Log("-------------" + dNow.ToString());
            txTime.text = dNow.ToString("yyyy年MM月dd日") + dow[System.Convert.ToInt16(dNow.DayOfWeek)];
            txHour.text = dNow.ToString("HH");
            txMinute.text = dNow.ToString("mm");
        } catch(Exception e)
        {
            Console.WriteLine(e.Message);
        }*/


        /*
        deltaTimeforWeather += Time.deltaTime;
        if (deltaTimeforWeather > howlongToUpdate)//每一小时更新一次  
        {
            UpdateWeather();
            deltaTimeforWeather = 0.0f;
        }*/
    }

    public void UpdateWeather()
    {
        StartCoroutine(GetWeatherIE());
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
                Debug.Log("GetLocation myCity=" + myCity);
                // txLocation.text = "定位：" + myCity;
            }
        }
    }

    IEnumerator GetWeatherIE()
    {
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
            if (File.Exists(jsonFile))
            {
                // txTemp.text = "jsonFile exists" + jsonFile;
            }
            else
            {
                // txTemp.text = "NO " + jsonFile;
            }
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
                // Debug.Log("----- i = " + i + "  " + jsonProvince["province"].ToString() + "   " + found);
                for (var j = 0; j < jsonCity.Count; j++)
                {
                    JsonData temp1 = jsonCity[j];
                    string myCityName = temp1["cityName"].ToString();
                    // Debug.Log("----- j = " + j + "  " + myCityName + "   " + found);
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
                // txCity.text = jdInfo["city"].ToString();
                string result_city = jdInfo["city"].ToString();
                string strLow = jdInfo["temp1"].ToString();
                string strHigh = jdInfo["temp2"].ToString();
                // txTemp.text = strLow + "~" + strHigh;
                string result_temp = strLow + "~" + strHigh;
                string result_weather = jdInfo["weather"].ToString();
                txWeather.text = result_city + " " + result_weather + " " + result_temp;
                // txWeather.text = jdInfo["weather"].ToString();
                Debug.Log(jdInfo["img1"].ToString());
                // string imagePath = "./Resources/" + jdInfo["img1"].ToString();
                /* string imagePath = Application.dataPath + "/Resources/no.gif";
                byte[] imageByte = File.ReadAllBytes(imagePath);
                Texture2D imageTexture = new Texture2D(1, 1);
                if (ImageConversion.LoadImage(imageTexture, imageByte))
                {
                    Sprite imageSprite = Sprite.Create(imageTexture, new Rect(0, 0, imageTexture.width, imageTexture.height), new Vector2(0, 0));
                    imWeather.sprite = imageSprite;
                } */
            }
        }
    }
}
