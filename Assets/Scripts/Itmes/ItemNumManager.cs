using UnityEngine;
using System.Globalization; // CultureInfoを使うのに必要
using TMPro;
// パワー、残機、ボムの所持数を管理するスクリプト

public class ItemNumManager : MonoBehaviour
{
    private int powerNum; // 現在のパワー数
    private sbyte lifeNum; // 現在の残機数
    private sbyte bombNum; // 現在のボム数
    private GameObject playermanager; // シングルトンを持ったオブジェクト
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // シングルトンからゲーム開始時の初期値を設定
        playermanager = GameObject.Find("PlayerManager");
        powerNum = playermanager.GetComponent<PlayerManager>().powernow;
        lifeNum = playermanager.GetComponent<PlayerManager>().lifenow;
        bombNum = playermanager.GetComponent<PlayerManager>().bombnow;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int getpowerNum
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
