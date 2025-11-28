using UnityEngine;
using System.Collections;
// コウモリ雑魚の行動パターンのスクリプト
public class BatsMovePattern : EnemyMoveManager
{
    public override void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();
    }

    public override void FixedUpdate()
    {
        
    }

    IEnumerator BatsMove_01()
    {

        yield return new WaitForSeconds(0.5f);

        yield return new WaitForSeconds(10.0f);
    }
}
