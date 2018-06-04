using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveManager : MonoBehaviour
{
	public void SaveData(string sName, SaveDataFormat sData) {
		using (FileStream fileStream = new FileStream (SaveDataFormat.SavePath + sName,
			                               FileMode.Create, FileAccess.Write)) {
			BinaryFormatter binaryFormatter = new BinaryFormatter ();
			binaryFormatter.Serialize (fileStream, sData);
		}
		Debug.Log (string.Concat(new object [] {
			"[SaveManager] save data to ",
			SaveDataFormat.SavePath,
			sName
		}));
	}

	public SaveDataFormat LoadData(string lName) {
		SaveDataFormat saveDataFormat = new SaveDataFormat ();
		try {
			Debug.Log(string.Concat(new object [] {
				"[SaveManager] load data from",
				SaveDataFormat.SavePath,
				lName
			}));
			using (FileStream fileStream = new FileStream (SaveDataFormat.SavePath + lName, 
				FileMode.Open, FileAccess.Read)) {
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				saveDataFormat = (binaryFormatter.Deserialize(fileStream) as SaveDataFormat);
			}
			Debug.Log("[SaveManager] load data finished.");
			Debug.Log("[SaveManager] backup data.");
			if (saveDataFormat != null) {
				this.SaveData(lName + ".back", new SaveDataFormat(saveDataFormat));
			}
		} catch (FileNotFoundException e) {
			saveDataFormat.initialize ();
			this.SaveData (lName, new SaveDataFormat(saveDataFormat));
			this.SaveData (lName + ".back", new SaveDataFormat (saveDataFormat));
			Debug.Log (string.Concat(new object [] {
				"[SaveManager] load data solution to exception (FileNotFoundException):",
				SaveDataFormat.SavePath,
				lName,
				e.ToString()
			}));
		} catch (IsolatedStorageException e2) {
			saveDataFormat.initialize ();
			this.SaveData (lName, new SaveDataFormat(saveDataFormat));
			this.SaveData (lName + ".back", new SaveDataFormat (saveDataFormat));
			Debug.Log (string.Concat(new object [] {
				"[SaveManager] load data solution to exception (IsolatedStorageException):",
				SaveDataFormat.SavePath,
				lName,
				e2.ToString()
			}));
		} catch (SerializationException e3) {
			if (lName.Remove (0, lName.Length - 5) == ".back") {
				Debug.Log (string.Concat(new object [] {
					"[SaveManager] read backup file failed.(SerializationException):",
					SaveDataFormat.SavePath,
					lName,
					e3.ToString()
				}));
				return null;
			}
			Debug.Log (string.Concat(new object [] {
				"[SaveManager] read data file failed. (SerializationException)",
				SaveDataFormat.SavePath,
				lName,
				e3.ToString()
			}));
			Debug.Log (string.Concat(new object [] {
				"[SaveManager] recover from the backup file. (SerializationException)",
				SaveDataFormat.SavePath,
				lName,
				e3.ToString()
			}));
			return this.LoadData (lName + ".back");
		}
		return saveDataFormat;
	}
}

