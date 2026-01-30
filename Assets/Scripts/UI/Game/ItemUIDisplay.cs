using UnityEngine;
using UnityEngine.UI;
using System.Globalization; // CultureInfoを使うのに必要
using TMPro;
// 所持しているアイテム数を表示するスクリプト

public class ItemUIDisplay : MonoBehaviour
{
    private sbyte life; // 残機数(取得用)
    private sbyte bomb; // ボム数(取得用)
    // 2つ共初期値を10とする
    private sbyte nowlife = 10; // 今の残機数を覚える
    private sbyte nowbomb = 10; // 今のボム数を覚える

    [SerializeField] private Image[] lifeimage; // 残機の画像
    [SerializeField] private Image[] bombimage; // ボムの画像

    [SerializeField] private TextMeshProUGUI powerText; // 現在のパワー数を表示するテキスト
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PrintpowerText();
    }

    // Update is called once per frame
    void Update()
    {
        life = GetComponent<ItemNumManager>().getlifeNum;
        // 残機数が覚えていた数と違う時
        if (life != nowlife)
        {
            // 残機の表示数を変える
            for (int i = 0; i < life; i++)
            {
                // 持っている分を表示する
                lifeimage[i].enabled = true;
            }
            for (int inoon = life; inoon < 10; inoon++)
            {
                // 持っていない分を非表示する
                lifeimage[inoon].enabled = false;
            }
            // 変わった残機の数を覚える
            nowlife = life;
        }
        bomb = GetComponent<ItemNumManager>().getbombNum;
        // ボム数が覚えていた数と違う時
        if (bomb != nowbomb)
        {
            // ボムの表示数を変える
            for (int b = 0; b < bomb; b++)
            {
                // 持っている分を表示する
                bombimage[b].enabled = true;
            }
            for (int bnoon = bomb; bnoon < 10; bnoon++)
            {
                // 持っていない分を非表示する
                bombimage[bnoon].enabled = false;
            }
            // 変わったボムの数を覚える
            nowbomb = bomb;
        }
        PrintpowerText();
    }

    /// <summary>
    /// 現在のパワーを表示する関数
    /// </summary>
    void PrintpowerText()
    {
        // 表示する時だけ浮動小数点にする
        float powerNumFloat = (float)GetComponent<ItemNumManager>().getpowerNum / 100.0f;

        powerText.text = "POWER : " +
            powerNumFloat.ToString("F2", CultureInfo.CurrentCulture); // 小数点第2まで表示する
    }

    public sbyte getlife
    {
        get { return this.life; }
        set { this.life = value; }
    }

    public sbyte getbomb
    {
        get { return this.bomb; }
        set { this.bomb = value; }
    }
}
