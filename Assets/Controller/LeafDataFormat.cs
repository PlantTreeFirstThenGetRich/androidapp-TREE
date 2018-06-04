using System;

[Serializable]
public class LeafDataFormat
{
	public LeafDataFormat () {}

	public LeafDataFormat(LeafDataFormat ori) {
		this.x = ori.x;
		this.y = ori.y;
		this.timeSpanSec = ori.timeSpanSec;
		this.newFlag = ori.newFlag;
	}

	public float x;
	public float y;
	public int timeSpanSec;
	public bool newFlag;
}

