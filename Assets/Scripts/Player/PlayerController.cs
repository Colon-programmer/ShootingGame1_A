using UnityEngine;
// 自機の操作を管理するスクリプト

public class PlayerController : MonoBehaviour
{
    [SerializeField] private ControllerManager controllerManager;

    [SerializeField] protected GameObject meinshottingPrefab;
    protected float meinshotinterval = 0.1f; // メインショットのインターバル
    protected float meinshottime = 0.0f; // メインショットを撃つタイミングを測る

    protected float playerHighSpeed; // 自機の高速時移動速度
    protected float playerSlowSpeed = 2.0f; // 自機の低速時移動速度

    [SerializeField] protected float shotPower; // 1.0につき1つサブショットを開放することができるショットパワーの変数

    [SerializeField] protected GameObject shotsField; // キャラクターと弾の動きを連動させない
                                                      // ようにするため弾専用の座標を用意する
    protected Vector2 playerposition;
    protected Rigidbody2D playerRb;

    protected Vector2[,] subshooterLayout = new Vector2[4, 2]; // サブショットの発射口の配置を格納する変数
    [SerializeField] protected GameObject subshooter; // サブショットの発射口の画像

    protected GameObject[] subshooterObj = new GameObject[4]; // 生成したサブショットの発射口を変数として持たせる
    protected Rigidbody2D[] subshooterRigidbody = new Rigidbody2D[4]; // サブショットの発射口のRigidbody
    [SerializeField] protected GameObject subShooterCenter; // サブショットの発射口の移動を自機と別にするためのオブジェクト

    public void Start()
    {
        // プレイヤーのRigidbodyを取得する
        playerRb = GetComponent<Rigidbody2D>();
        shotPower = 0.0f;
        // 自機の移動速度を設定する
        CharacterDefaultSetting();
        // サブショットの発射口を配置する
        SubShooterSetting();
    }
    public virtual void Update()
    {
        ShotPowerCheck();
        PlayerMove();
        // ショット
        playerposition = this.transform.position;
        if (controllerManager.shottingAction.IsPressed())
        {
            meinshottime += Time.deltaTime;
            if (meinshottime >= meinshotinterval)
            {
                // メインショットを撃つ
                Instantiate(meinshottingPrefab,
                    new Vector2(playerposition.x - 0.25f, playerposition.y), Quaternion.identity,
                    shotsField.transform);
                Instantiate(meinshottingPrefab,
                    new Vector2(playerposition.x + 0.25f, playerposition.y), Quaternion.identity,
                    shotsField.transform);
                meinshottime = 0.0f; // 時間をリセットする
            }
        }
        else
        {
            meinshottime = meinshotinterval; // 次に押した時すぐに発射されるようにする
        }
    }
    /// <summary>
    /// 溜まったパワーの量に応じてサブショットを開放したり閉鎖したりする関数
    /// </summary>
    public void ShotPowerCheck()
    {
        // パワーが4.0以上の時
        if (shotPower >= 4.0f)
        {
            subshooterObj[3].SetActive(true);
        }
        // パワーが足りていないときは対応したサブショットを閉鎖する
        else
        {
            subshooterObj[3].SetActive(false);
        }
        // パワーが3.0以上の時
        if (shotPower >= 3.0f)
        {
            subshooterObj[2].SetActive(true);
        }
        else
        {
            subshooterObj[2].SetActive(false);
        }
        // パワーが2.0以上の時
        if (shotPower >= 2.0f)
        {
            subshooterObj[1].SetActive(true);
        }
        else
        {
            subshooterObj[1].SetActive(false);
        }
        // パワーが1.0以上の時
        if (shotPower >= 1.0f)
        {
            subshooterObj[0].SetActive(true);
        }
        else
        {
            subshooterObj[0].SetActive(false);
        }
    }
    // プレイヤーの移動を行う関数
    public virtual void PlayerMove()
    {

        var playermoveValue = controllerManager.moveingAction.ReadValue<Vector2>();
        
        if (controllerManager.slowAction.IsPressed())
        {
            // 低速移動
            // 本体
            playerRb.linearVelocity =
            new Vector2(playermoveValue.x * playerSlowSpeed, playermoveValue.y * playerSlowSpeed);
        }
        else
        {
            // 高速移動
            // 本体
            playerRb.linearVelocity =
            new Vector2(playermoveValue.x * playerHighSpeed, playermoveValue.y * playerHighSpeed);
        }
        subShooterCenter.transform.position = new Vector2(playerRb.position.x,
                                                            playerRb.position.y);
    }
    /// <summary>
    /// ゲームスタート時の自機の初期設定をする関数
    /// </summary>
    public virtual void CharacterDefaultSetting()
    {
        
    }
    /// <summary>
    /// サブショットの発射口の配置を設定する関数
    /// 1次元目がどの発射口か(開放順)
    /// 2次元目が開放されてる発射口が奇数が偶数か(0が偶数,1が奇数とします。)
    /// </summary>
    public virtual void SubShooterSetting()
    {
        
    }
}
