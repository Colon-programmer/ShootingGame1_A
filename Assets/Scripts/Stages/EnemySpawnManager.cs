using UnityEngine;
// 各ステージでの敵のスポーンを管理するスクリプト

public class EnemySpawnManager : MonoBehaviour
{
    protected float stageTimer; // ステージが始まってからの時間を計り、敵の出現タイミングを制御するために使う変数

    protected bool[] spawnflag;

    [SerializeField] protected GameObject bats;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
