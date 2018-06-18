using System;
using System.Security.Cryptography;

[Serializable]
public class LeafDataFormat
{
	public LeafDataFormat () {
		// random select x position
		int rngNum = this.GenerateRandomValue();
		this.x = (float)((rngNum % 100 / 100.0) * 100);
		//this.x = (float)(rngNum % 100 / 100.0);
		this.y = -0;
		// generate after 3 minute;
		this.timeSpanSec = SuperGameMaster.InitLeafGenerateTime;
		this.newFlag = false;
	}

	public LeafDataFormat(LeafDataFormat ori) {
		this.x = ori.x;
		this.y = ori.y;
		this.timeSpanSec = ori.timeSpanSec;
		this.newFlag = ori.newFlag;
	}

	private int GenerateRandomValue(){
		byte[] randomBytes = new byte[4];
		RNGCryptoServiceProvider rngCrypto = new RNGCryptoServiceProvider ();
		rngCrypto.GetBytes (randomBytes);
		int rngNum = BitConverter.ToInt32 (randomBytes, 0);
		return rngNum;
	}


	public float x;
	public float y;
	public int timeSpanSec;
	public bool newFlag;
}


