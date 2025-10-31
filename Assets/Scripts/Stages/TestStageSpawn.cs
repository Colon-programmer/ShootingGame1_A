using UnityEngine;
using System.Collections;
// テストステージでの敵のスポーンを管理するスクリプト

public class TestStageSpawn : MonoBehaviour
{
    private float stageTimer; // ステージが始まってからの時間を計り、敵の出現タイミングを制御するために使う変数

    private bool[] spawnflag = new bool[1];

    [SerializeField] private GameObject bats;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stageTimer = 20.0f;
        spawnflag[0] = true;
    }

    void Update()
    {
        stageTimer -= Time.deltaTime;
        // タイマーが〇になったら敵が出るようにする
        if (stageTimer <= 19.0f && spawnflag[0])
        {
            StartCoroutine("TestSpawing");
            spawnflag[0] = false;
        }
    }

    IEnumerator TestSpawing()
    {
        for (int i = 0; i < 10; i++)
        {
            // コウモリ敵を生成
            GameObject testbat;
            testbat = Instantiate(bats, new Vector2(5.0f, 5.0f), Quaternion.identity);
            // 体力設定
            testbat.GetComponent<MobEnemiesManager>().enemyHp = 50;
            // 進行方向設定
            testbat.transform.eulerAngles = new Vector3(0, 0, 120);

            yield return new WaitForSeconds(0.5f);
        }

        for (int i = 0; i < 10; i++)
        {
            GameObject testbat;
            testbat = Instantiate(bats, new Vector2(-5.0f, 5.0f), Quaternion.identity);
            testbat.GetComponent<MobEnemiesManager>().enemyHp = 50;
            testbat.transform.eulerAngles = new Vector3(0, 0, -120);

            yield return new WaitForSeconds(0.5f);
        }
    }
}
