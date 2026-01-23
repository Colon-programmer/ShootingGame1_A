using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Threading;
// ステージ1のステージボスのスクリプト

public class Big_Boss_Stage_1 : Big_BossManager
{
    public override void Start()
    {
        base.Start();

        big_boss_maxhp = big_boss_hp;
    }

    private void Update()
    {
        if (big_boss_hp <= 0)
        {
            // 攻撃パターン切り替え時に敵弾を全て消す
            Instantiate(shotEraserobj, new Vector2(0, 0), Quaternion.identity);
            switch (big_boss_patten)
            {
                case 4:
                    big_boss_patten -= 1; // 攻撃パターンを変える
                    big_boss_hp = 5000; // 新しい体力を設定する
                    big_boss_maxhp = big_boss_hp; // 新しく設定した体力を最大体力として設定する
                    break;
                case 3:
                    big_boss_patten -= 1; // 攻撃パターンを変える
                    big_boss_hp = 3000; // 新しい体力を設定する
                    big_boss_maxhp = big_boss_hp; // 新しく設定した体力を最大体力として設定する
                    break;
                case 2:
                    big_boss_patten -= 1; // 攻撃パターンを変える
                    big_boss_hp = 5000; // 新しい体力を設定する
                    big_boss_maxhp = big_boss_hp; // 新しく設定した体力を最大体力として設定する
                    break;
                case 1:
                    scorecountobj.GetComponent<ScoreGetter>().getscore += 100;
                    enemySpawnobj.GetComponent<EnemySpawnManager>().gettimercountstoper = false;
                    Destroy(this.gameObject);
                    break;
                default:
                    break;
            }
        }
    }
}
