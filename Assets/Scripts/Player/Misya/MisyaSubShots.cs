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

    private bool changeshotangle = false; // 弾の飛ばし方を変えるフラグ

    private bool firstshooterflag = false; // 前半の1,2個目のサブシューターかどうかのフラグ

    private bool maxpowerflag = false; // パワーが最大まで溜まったかどうかのフラグ

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
                    new Vector2(this.transform.position.x, this.transform.position.y),
                    Quaternion.identity, ShotsField.transform);
            }
            // そうでない時
            else
            {
                GameObject slowBullets; // 少し斜めに飛ばす弾

                // パワーが最大の時、斜め飛ばす弾を生成する
                if (maxpowerflag && !firstshooterflag)
                {
                    if (subPosition == LeftAndRight.Left) // 自機より左のサブショット
                    {
                        // 斜めに飛ばす弾を生成
                        slowBullets = Instantiate(slowsubBullet,
                                new Vector2(this.transform.position.x, this.transform.position.y),
                                Quaternion.identity, ShotsField.transform);
                        slowBullets.transform.eulerAngles = new Vector3(0, 0, 10);
                    }
                    if (subPosition == LeftAndRight.Right) // 自機より右のサブショット
                    {
                        // 斜めに飛ばす弾を生成
                        slowBullets = Instantiate(slowsubBullet,
                                new Vector2(this.transform.position.x, this.transform.position.y),
                                Quaternion.identity, ShotsField.transform);
                        slowBullets.transform.eulerAngles = new Vector3(0, 0, -10);
                    }

                }
                // そうでないときは少し斜めに飛ばす弾を生成する
                else
                {
                    if (subPosition == LeftAndRight.Left) // 自機より左のサブショット
                    {
                        // 少し斜めに飛ばす弾を生成
                        slowBullets = Instantiate(slowsubBullet,
                                new Vector2(this.transform.position.x, this.transform.position.y),
                                Quaternion.identity, ShotsField.transform);
                        slowBullets.transform.eulerAngles = new Vector3(0, 0, 5);
                    }
                    if (subPosition == LeftAndRight.Right) // 自機より右のサブショット
                    {
                        // 少し斜めに飛ばす弾を生成
                        slowBullets = Instantiate(slowsubBullet,
                                new Vector2(this.transform.position.x, this.transform.position.y),
                                Quaternion.identity, ShotsField.transform);
                        slowBullets.transform.eulerAngles = new Vector3(0, 0, -5);
                    }
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

    public bool getchangeshotangle
    {
        get { return this.changeshotangle; }
        set { this.changeshotangle = value; }
    }

    public bool getfirstshooterflag
    {
        get { return this.firstshooterflag; }
        set { this.firstshooterflag = value; }
    }

    public bool getmaxpowerflag
    {
        get { return this.maxpowerflag; }
        set { this.maxpowerflag = value; }
    }
}
