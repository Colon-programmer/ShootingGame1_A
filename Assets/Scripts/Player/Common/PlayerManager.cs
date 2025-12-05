using UnityEngine;
// プレイヤーのゲーム中に必要な情報を管理するスクリプト


public class PlayerManager : GameManager<PlayerManager>
{
    // 自機の種類の型
    public enum PLAYER
    {
        MISYA = 1,
        POPPY = 2
    }

    public PLAYER playerSelect;
    public sbyte lifedefault = 2; // 初期残機数
    public sbyte bombdefault = 2; // 初期ボム数
    public int powerdefault = 100; // 現在のパワーの数値(ステージ間で引き継ぐ)

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
