using UnityEngine;

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
            
            // 2発共にまっすぐ飛ばすフラグが立っていた時
            if (changeshotangle)
            {
                Instantiate(slowsubBullet,
                    new Vector2(this.transform.position.x - 0.25f, this.transform.position.y),
                    Quaternion.identity, ShotsField.transform);
                Instantiate(slowsubBullet,
                    new Vector2(this.transform.position.x + 0.25f, this.transform.position.y),
                    Quaternion.identity, ShotsField.transform);
            }
            // そうでない時
            else
            {
                // まっすぐ飛ばす弾
                Instantiate(slowsubBullet,
                        this.transform.position, Quaternion.identity, ShotsField.transform);
                // 斜め飛ばす弾
                if (subPosition == LeftAndRight.Left) // 自機より左のサブショット
                {
                    GameObject slowBullets;
                    slowBullets = Instantiate(slowsubBullet,
                        this.transform.position, Quaternion.identity, ShotsField.transform);
                    slowBullets.transform.eulerAngles = new Vector3(0, 0, 15);
                }
                if (subPosition == LeftAndRight.Right) // 自機より右のサブショット
                {
                    GameObject slowBullets;
                    slowBullets = Instantiate(slowsubBullet,
                        this.transform.position, Quaternion.identity, ShotsField.transform);
                    slowBullets.transform.eulerAngles = new Vector3(0, 0, -15);
                }
            }
            
            subshottime = 0.0f;
        }
        // 高速時
        else
        {
            subshotinterval = 0.2f;
            Instantiate(subBullet,
                new Vector2(this.transform.position.x - 0.2f, this.transform.position.y), Quaternion.identity, ShotsField.transform);
            Instantiate(subBullet,
                new Vector2(this.transform.position.x + 0.2f, this.transform.position.y), Quaternion.identity, ShotsField.transform);
            subshottime = 0.0f;
        }
    }
}
