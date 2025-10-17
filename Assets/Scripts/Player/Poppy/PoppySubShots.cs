using UnityEngine;

public class PoppySubShots : SubShooters
{
    public enum SubSetNumber
    { 
        First = 1,
        Second = 2
    }
    [Header("サブショットが解禁される順番を設定する")]
    public SubSetNumber subNumber;

    public float getsubshotinterval
    {
        get { return this.subshotinterval; }
        set { this.subshotinterval = value; }
    }

}
