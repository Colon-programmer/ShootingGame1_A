using UnityEngine;
using System.Collections;
// ステージ1のスポーンを管理するスクリプト

public class Stage1EnemySpawn : EnemySpawnManager
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnflag = new bool[2];
        for (int flag = 0; flag < spawnflag.Length; flag++)
        {
            spawnflag[flag] = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        stageTimer -= Time.deltaTime;
        // タイマーが指定秒数になったら敵が出るようにする
        if (stageTimer <= 19.0f && spawnflag[0])
        {
            StartCoroutine("EnemySpawn");
            spawnflag[0] = false;
        }
    }

    IEnumerator EnemySpawn()
    {
        float spawnX = -3.0f;
        float spawnY = 5.0f;
        for (int i = 0; i < 5; i++)
        {
            // コウモリ敵を生成
            GameObject firstbats;
            firstbats = Instantiate(bats, new Vector2(spawnX, spawnY), Quaternion.identity);

            // 体力設定
            firstbats.GetComponent<MobEnemiesManager>().enemyHp = 50;

            spawnX += 0.3f;

            yield return new WaitForSeconds(0.2f);
        }
    }
}
