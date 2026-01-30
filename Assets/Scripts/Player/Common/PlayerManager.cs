using UnityEngine;
using UnityEngine.SceneManagement;
// プレイヤーのゲーム中に必要な情報を管理するスクリプト


public class PlayerManager : GameManager<PlayerManager>
{
    // 自機の種類の型
    public enum PLAYER
    {
        MISYA = 1,
        NEEDLE = 2
    }

    public PLAYER playerSelect;
    public sbyte lifedefault; // 初期残機数
    public sbyte bombdefault; // 初期ボム数
    public int powerdefault; // 現在のパワーの数値(ステージ間で引き継ぐ)
    public int scorenum; // 獲得したスコアを格納する変数
    public sbyte stagenumber; // 現在のステージの番号を格納する変数
    // ステージを跨ぐ際に使う変数
    public sbyte lifenow;
    public sbyte bombnow;
    public int powernow;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// スコアを0にする関数
    /// </summary>
    void DefaultSeter()
    {
        lifenow = lifedefault;
        bombnow = bombdefault;
        powernow = powerdefault;
        scorenum = 0;
    }
    /// <summary>
    /// ゲームシーンに移行する関数
    /// </summary>
    /// <param name="num">行きたいステージ数を指定する</param>
    public void GameSceneMove(sbyte num)
    {
        // ゲームシーンに行く前にスコアをリセットする
        DefaultSeter();

        switch (num)
        {
            case 1:
                stagenumber = 1;
                SceneManager.LoadScene("Stage_01");
                break;
            default:
                break;
        }
    }
}
