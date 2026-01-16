using UnityEngine;
// 各ステージでの敵のスポーンを管理するスクリプト

public class EnemySpawnManager : MonoBehaviour
{
    protected float stageTimer; // ステージが始まってからの時間を計り、敵の出現タイミングを制御するために使う変数
    protected bool timercountstoper; // trueならカウントしない

    protected bool[] spawnflag; // 雑魚敵の出現フラグ

    [SerializeField] protected GameObject bats;

    [SerializeField] protected GameObject mid_boss; // 中ボス(途中に出るボス)
    //[SerializeField] protected GameObject big_boss; // ボス(ステージ最後に出るボス)

    [SerializeField] protected GameObject[] powerItmeobj; // パワーアイテムのオブジェクト
    [SerializeField] protected GameObject[] scoreItmeobj; // スコアアイテムのオブジェクト

    protected bool[] bossflag = new bool[2]; // ボスの出現フラグ
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int flag = 0; flag < bossflag.Length; flag++)
        {
            bossflag[flag] = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool gettimercountstoper
    {
        get { return this.timercountstoper; }
        set { this.timercountstoper = value; }
    }
}
