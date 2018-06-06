using System;
using UnityEngine;
using System.Runtime.Serialization;
using System.Collections.Generic;

[Serializable]
public class SaveDataFormat
{
	public SaveDataFormat () {}

	public SaveDataFormat (SaveDataFormat ori) {
		this.supportID = ori.supportID;
		this.registerTime = ori.registerTime;
		this.lastWaterTime = new DateTime(ori.lastWaterTime.Year, ori.lastWaterTime.Month, 
			ori.lastWaterTime.Day, ori.lastWaterTime.Hour, ori.lastWaterTime.Minute, 
			ori.lastWaterTime.Second, ori.lastWaterTime.Millisecond);
		this.lastDateTime = new DateTime(ori.lastDateTime.Year, ori.lastDateTime.Month, 
			ori.lastDateTime.Day, ori.lastDateTime.Hour, ori.lastDateTime.Minute, 
			ori.lastDateTime.Second, ori.lastDateTime.Millisecond);
		this.lastWeather = ori.lastWeather;
		this.LeafList = new List<LeafDataFormat> (ori.LeafList);
		this.OwnLeaveNumber = ori.OwnLeaveNumber;
	}

	public void initialize() {
		this.supportID = 0;
		this.registerTime = new DateTime (1970, 1, 1);
		this.lastWaterTime = new DateTime (1970, 1, 1);
		this.lastDateTime = new DateTime (1970, 1, 1);
		this.lastWeather = Weathers.NONE;
		this.LeafList = new List<LeafDataFormat> ();
		for (int i = 0; i < SuperGameMaster.InitTotalLeadNumber; i++) {
			this.LeafList.Add (new LeafDataFormat ());
		}
		this.OwnLeaveNumber = 0;
	}
		
	public static readonly string SavePath = Application.persistentDataPath + "/";
	public int supportID;
	public DateTime registerTime;
	public DateTime lastWaterTime;
	public DateTime lastDateTime;
	public Weathers lastWeather;
	public List<LeafDataFormat> LeafList;

	public int OwnLeaveNumber;



}

