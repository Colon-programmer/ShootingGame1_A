using UnityEngine;
// ミーシャ選択時のサブショットを制御するスクリプト
public class MisyaSubShots : SubShooters
{
    public enum LeftAndRight
    {
        Left = 0,
        Right = 1
    }
    [Header("自機の左右どちら側に配置されるのかを設定する")]
    public LeftAndRight subPosition;

    [SerializeField] private GameObject slowsubBullet; // 低速時ショットの弾

    public bool changeshotangle = false; // 弾の飛ばし方を変えるフラグ

    public override void SubShotting()
    {
        // 低速時
        if (controllerManager.GetComponent<ControllerManager>().slowAction.IsPressed())
        {
            subshotinterval = 0.1f;
            
            // フラグが立っていた時
            if (changeshotangle)
            {
                Instantiate(slowsubBullet,
                    new Vector2(this.transform.position.x - 0.5f, this.transform.position.y),
                    Quaternion.identity, ShotsField.transform);
                Instantiate(slowsubBullet,
                    new Vector2(this.transform.position.x + 0.5f, this.transform.position.y),
                    Quaternion.identity, ShotsField.transform);
            }
            // そうでない時
            else
            {
                GameObject slowBullets_1; // 少し斜めに飛ばす弾
                GameObject slowBullets_2; // 斜め飛ばす弾
                
                if (subPosition == LeftAndRight.Left) // 自機より左のサブショット
                {
                    // 少し斜めに飛ばす弾を生成
                    slowBullets_1 = Instantiate(slowsubBullet,
                            new Vector2(this.transform.position.x + 0.25f, this.transform.position.y),
                            Quaternion.identity, ShotsField.transform);
                    slowBullets_1.transform.eulerAngles = new Vector3(0, 0, 5);
                    // 斜め飛ばす弾を生成
                    slowBullets_2 = Instantiate(slowsubBullet,
                        this.transform.position, Quaternion.identity, ShotsField.transform);
                    slowBullets_2.transform.eulerAngles = new Vector3(0, 0, 10);
                }
                if (subPosition == LeftAndRight.Right) // 自機より右のサブショット
                {
                    // 少し斜めに飛ばす弾を生成
                    slowBullets_1 = Instantiate(slowsubBullet,
                            new Vector2(this.transform.position.x - 0.25f, this.transform.position.y),
                            Quaternion.identity, ShotsField.transform);
                    slowBullets_1.transform.eulerAngles = new Vector3(0, 0, -5);
                    // 斜め飛ばす弾を生成
                    slowBullets_2 = Instantiate(slowsubBullet,
                        this.transform.position, Quaternion.identity, ShotsField.transform);
                    slowBullets_2.transform.eulerAngles = new Vector3(0, 0, -10);
                }
            }
            
            subshottime = 0.0f;
        }
        // 高速時
        else
        {
            subshotinterval = 0.15f;
            Instantiate(subBullet,
                new Vector2(this.transform.position.x - 0.1f, this.transform.position.y), Quaternion.identity, ShotsField.transform);
            Instantiate(subBullet,
                new Vector2(this.transform.position.x + 0.1f, this.transform.position.y), Quaternion.identity, ShotsField.transform);
            subshottime = 0.0f;
        }
    }
}
