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

    }

    
}
