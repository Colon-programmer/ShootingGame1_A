using UnityEngine;
using System.Collections;
// ステージ1のスポーンを管理するスクリプト

public class Stage1EnemySpawn : EnemySpawnManager
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stageTimer = 0.0f;
        spawnflag = new bool[7];
        for (int flag = 0; flag < spawnflag.Length; flag++)
        {
            spawnflag[flag] = true;
        }
        for (int flag = 0; flag < bossflag.Length; flag++)
        {
            bossflag[flag] = true;
        }
        timercountstoper = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!timercountstoper)
        {
            stageTimer += Time.deltaTime;
        }
        // タイマーが指定秒数になったら敵が出るようにする
        if (stageTimer >= 3.0f && spawnflag[0])
        {
            StartCoroutine("EnemySpawn_1_0");
            spawnflag[0] = false;
        }
        if (stageTimer >= 10.0f && spawnflag[1])
        {
            StartCoroutine("EnemySpawn_1_2");
            spawnflag[1] = false;
        }
        if (stageTimer >= 15.0f && spawnflag[2])
        {
            StartCoroutine("EnemySpawn_1_1");
            spawnflag[2] = false;
        }
        if (stageTimer >= 20.0f && spawnflag[3])
        {
            StartCoroutine("EnemySpawn_1_1");
            StartCoroutine("EnemySpawn_1_2");
            spawnflag[3] = false;
        }

        if (stageTimer >= 25.0f && bossflag[0])
        {
            Mid_Boss_1();
            bossflag[0] = false;
            timercountstoper = true;
        }

        if (stageTimer >= 27.0f && spawnflag[4])
        {
            spawnNum = 4;
            spawn_Set_X = 3.0f;
            StartCoroutine("EnemySpawn_2_1");
            spawnflag[4] = false;
        }

        if (stageTimer >= 32.0f && spawnflag[5])
        {
            spawnNum = 4;
            spawn_Set_X = -3.0f;
            StartCoroutine("EnemySpawn_2_2");
            spawnflag[5] = false;
        }
    }

    IEnumerator EnemySpawn_1_0()
    {
        float spawnX = -3.0f;
        float spawnY = 5.0f;
        for (int i = 0; i < 10; i++)
        {
            // コウモリ敵を生成
            GameObject firstbats;
            firstbats = Instantiate(bats, new Vector2(spawnX, spawnY), Quaternion.identity);

            // 体力設定
            firstbats.GetComponent<MobEnemiesManager>().enemyHp = 50;

            // ドロップアイテムを設定
            firstbats.GetComponent<MobEnemiesManager>().getdropitme = powerItmeobj[0];

            // 行動パターンを設定
            firstbats.GetComponent<EnemyMoveManager>().getmovepattern = 1;

            spawnX += 0.5f;

            yield return new WaitForSeconds(0.2f);
        }

    }

    IEnumerator EnemySpawn_1_1()
    {
        float spawnX = -4.0f;
        float spawnY = 5.0f;
        for (int i = 0; i < 5; i++)
        {
            // コウモリ敵を生成
            GameObject firstbats;
            firstbats = Instantiate(bats, new Vector2(spawnX, spawnY), Quaternion.identity);

            // 体力設定
            firstbats.GetComponent<MobEnemiesManager>().enemyHp = 50;

            // ドロップアイテムを設定
            firstbats.GetComponent<MobEnemiesManager>().getdropitme = scoreItmeobj[0];

            // 行動パターンを設定
            firstbats.GetComponent<EnemyMoveManager>().getmovepattern = 1;

            spawnX += 0.5f;

            yield return new WaitForSeconds(0.2f);
        }
    }

    IEnumerator EnemySpawn_1_2()
    {
        float spawnX = 4.0f;
        float spawnY = 5.0f;
        for (int i = 0; i < 5; i++)
        {
            // コウモリ敵を生成
            GameObject firstbats;
            firstbats = Instantiate(bats, new Vector2(spawnX, spawnY), Quaternion.identity);

            // 体力設定
            firstbats.GetComponent<MobEnemiesManager>().enemyHp = 50;

            // ドロップアイテムを設定
            firstbats.GetComponent<MobEnemiesManager>().getdropitme = powerItmeobj[0];

            // 行動パターンを設定
            firstbats.GetComponent<EnemyMoveManager>().getmovepattern = 2;

            spawnX -= 0.5f;

            yield return new WaitForSeconds(0.2f);
        }
    }

    IEnumerator EnemySpawn_2_1()
    {
        float spawnY = 5.0f;
        for (int i = 0; i < spawnNum; i++)
        {
            // コウモリ敵を生成
            GameObject firstbats;
            firstbats = Instantiate(bats, new Vector2(spawn_Set_X, spawnY), Quaternion.identity);

            // 体力設定
            firstbats.GetComponent<MobEnemiesManager>().enemyHp = 50;

            // ドロップアイテムを設定
            firstbats.GetComponent<MobEnemiesManager>().getdropitme = powerItmeobj[0];

            // 行動パターンを設定
            firstbats.GetComponent<EnemyMoveManager>().getmovepattern = 3;

            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator EnemySpawn_2_2()
    {
        float spawnY = 5.0f;
        for (int i = 0; i < spawnNum; i++)
        {
            // コウモリ敵を生成
            GameObject firstbats;
            firstbats = Instantiate(bats, new Vector2(spawn_Set_X, spawnY), Quaternion.identity);

            // 体力設定
            firstbats.GetComponent<MobEnemiesManager>().enemyHp = 50;

            // ドロップアイテムを設定
            firstbats.GetComponent<MobEnemiesManager>().getdropitme = scoreItmeobj[0];

            // 行動パターンを設定
            firstbats.GetComponent<EnemyMoveManager>().getmovepattern = 4;

            yield return new WaitForSeconds(0.5f);
        }
    }


    void Mid_Boss_1()
    {
        Instantiate(mid_boss, new Vector2(5.0f, 5.0f), Quaternion.identity);
    }
}
