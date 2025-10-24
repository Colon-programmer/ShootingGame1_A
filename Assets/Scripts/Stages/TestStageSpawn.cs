using UnityEngine;
// テストステージでの敵のスポーンを管理するスクリプト

public class TestStageSpawn : MonoBehaviour
{
    private float stageTimer; // ステージが始まってからの時間を計り、敵の出現タイミングを制御するために使う変数

    private bool spawnflag = true;

    [SerializeField] private GameObject bats;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stageTimer = 20.0f;
    }

    void FixedUpdate()
    {
        stageTimer -= Time.deltaTime;
        // タイマーが〇になったら敵が出るようにする
        if (stageTimer <= 19.0f && spawnflag)
        {
            GameObject testbat;
            spawnflag = false;
            testbat = Instantiate(bats, new Vector2(0.0f, 6.0f), Quaternion.identity);
        }
    }
}
