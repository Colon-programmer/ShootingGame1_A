using UnityEngine;
// 敵の出す自機狙い弾のスクリプト

public class EnemyPlayerSearch : EnemyBullets
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start()
    {
        base.Start();
        // 自機を探す
        playerposition = GameObject.FindWithTag("Player");
        // 自機の位置から移動方向を決める
        enemybulletVec = (playerposition.transform.position - this.transform.position);
    }
}
