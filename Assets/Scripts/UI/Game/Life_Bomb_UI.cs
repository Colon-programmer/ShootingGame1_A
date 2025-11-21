using UnityEngine;
using UnityEngine.UI;
// 残機数とボム数を表示するスクリプト

public class Life_Bomb_UI : MonoBehaviour
{
    private sbyte life; // 残機数(取得用)
    private sbyte bomb; // ボム数(取得用)
    // 2つ共初期値を0とする
    private sbyte nowlife = 0; // 今の残機数を覚える
    private sbyte nowbomb = 0; // 今のボム数を覚える

    [SerializeField] private Image[] lifeimage; // 残機の画像
    [SerializeField] private Image[] bombimage; // ボムの画像
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
        }
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
        }
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
