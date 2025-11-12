using UnityEngine;
// ポピー選択時のサブショットを制御するスクリプト

public class PoppySubShots : SubShooters
{
    public enum SubSetNumber
    { 
        First = 1,
        Second = 2
    }
    [Header("サブショットが解禁される順番を設定する")]
    public SubSetNumber subNumber;

    [SerializeField] public Color32 poppyhighShooterColor; // 高速ショット時のサブショットの発射口の色
    [SerializeField] public Color32 poppyslowShooterColor; // 低速ショット時のサブショットの発射口の色

    public float getsubshotinterval
    {
        get { return this.subshotinterval; }
        set { this.subshotinterval = value; }
    }

}
