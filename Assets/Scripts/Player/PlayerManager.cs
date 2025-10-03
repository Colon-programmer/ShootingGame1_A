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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerSelect = PLAYER.MISYA;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
