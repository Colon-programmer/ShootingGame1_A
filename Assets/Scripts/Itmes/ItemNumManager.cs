using UnityEngine;
using System.Globalization; // CultureInfoを使うのに必要
using TMPro;
// パワー、残機、ボムの所持数を管理するスクリプト

public class ItemNumManager : MonoBehaviour
{
    private float powerNum; // 現在のパワー数
    private sbyte lifeNum; // 現在の残機数
    private sbyte bombNum; // 現在のボム数
    private GameObject playermanager; // シングルトンを持ったオブジェクト

    [SerializeField] private TextMeshProUGUI powerText; // 現在のパワー数を表示するテキスト
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // シングルトンからゲーム開始時の初期値を設定
        playermanager = GameObject.Find("PlayerManager");
        powerNum = playermanager.GetComponent<PlayerManager>().powerdefault;
        lifeNum = playermanager.GetComponent<PlayerManager>().lifedefault;
        bombNum = playermanager.GetComponent<PlayerManager>().bombdefault;
        PrintpowerText();
    }

    // Update is called once per frame
    void Update()
    {
        PrintpowerText();
    }
    /// <summary>
    /// 現在のパワーを表示する関数
    /// </summary>
    void PrintpowerText()
    {
        powerText.text = "POWER : " + 
            powerNum.ToString("F2", CultureInfo.CurrentCulture); // 小数点第2まで表示する
    }

    public float getpowerNum
    {
        get { return this.powerNum; }
        set { this.powerNum = value; }
    }

    public sbyte getlifeNum
    {
        get { return this.lifeNum; }
        set { this.lifeNum = value; }
    }

    public sbyte getbombNum
    {
        get { return this.bombNum; }
        set { this.bombNum = value; }
    }

}
