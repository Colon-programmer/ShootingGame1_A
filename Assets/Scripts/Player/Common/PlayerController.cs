using UnityEngine;
using UnityEngine.UI;
// 自機の操作を管理するスクリプト

public class PlayerController : MonoBehaviour
{
    private GameObject controllerObj; // ヒエラルキー内のControllerオブジェクト
    protected ControllerManager controllerManager;

    [SerializeField] protected GameObject meinshottingPrefab;
    protected float meinshotinterval = 0.1f; // メインショットのインターバル
    protected float meinshottime = 0.0f; // メインショットを撃つタイミングを測る

    protected float playerHighSpeed; // 自機の高速時移動速度
    protected float playerSlowSpeed = 2.0f; // 自機の低速時移動速度

    [SerializeField] protected int shotPower; // 1.0につき1つサブショットを開放することができるショットパワーの変数
    private int shotMaxPower = 400; // 溜められるパワーの上限値

    protected GameObject shotsField; // 弾専用の親オブジェクト

    protected Vector2 playerposition;
    protected Rigidbody2D playerRb;

    protected Vector2[,] fastsubshooterLayout = new Vector2[4, 2]; // 高速時サブショットの発射口の配置を格納する変数
    protected Vector2[,] slowsubshooterLayout = new Vector2[4, 2]; // 低速時サブショットの発射口の配置を格納する変数
    [SerializeField] protected GameObject subshooter; // サブショットの発射口の画像

    protected GameObject[] subshooterObj = new GameObject[4]; // 生成したサブショットの発射口を変数として持たせる
    protected Rigidbody2D[] subshooterRigidbody = new Rigidbody2D[4]; // サブショットの発射口のRigidbody
    protected GameObject subShooterCenter; // サブショットの発射口の移動を自機と別にするためのオブジェクト

    private sbyte playerlife; // 残機数
    private sbyte playerbomb; // 所持ボム数
    [SerializeField] protected GameObject bombeffect; // ボム攻撃の見た目

    private GameObject itemDisplayer; // アイテムの所持数を表示するオブジェクト

    // それぞれの方向の移動制限
    protected float maxMoveX = 4.2f;
    protected float minMoveX = -4.3f;
    protected float maxMoveY = 4.2f;
    protected float minMoveY = -4.3f;

    [SerializeField] PlayerDeadPoint deadpoint; // やられ判定のスクリプト
    float respawnTime = 0.0f; // 復活までの時間を測る
    float respawnInterval = 1.0f; // 復活までに掛かる時間
    [SerializeField] SpriteRenderer playerSprite; // 自機のスプライト

    [SerializeField] private GameObject slowEffect; // 低速移動時に表示されるマーク

    private GameObject itmecollector; // アイテム自動回収判定
    private float itmecollectline = 2.5f; // アイテム自動回収ライン

    public void Start()
    {
        // プレイヤーのRigidbodyを取得する
        playerRb = GetComponent<Rigidbody2D>();
        // ゲーム開始時の初期値を設定
        itemDisplayer = GameObject.Find("ItemDisplayer");
        
        shotPower = itemDisplayer.GetComponent<ItemNumManager>().getpowerNum;
        playerlife = itemDisplayer.GetComponent<ItemNumManager>().getlifeNum;
        playerbomb = itemDisplayer.GetComponent<ItemNumManager>().getbombNum;
        // ControllerManagerを取得する
        controllerObj = GameObject.Find("Controller");
        controllerManager = controllerObj.GetComponent<ControllerManager>();
        //弾専用の親オブジェクトを取得する
        shotsField = GameObject.Find("ShotsField");
        // サブショットの発射口の座標用オブジェクトを取得する
        subShooterCenter = GameObject.Find("subWeapon");
        // アイテム自動回収判定を取得
        itmecollector = GameObject.Find("ItmeCollector");
        // アイテム自動回収判定を無効化
        itmecollector.SetActive(false);

        slowEffect.SetActive(false);
        // 自機の移動速度を設定する
        CharacterDefaultSetting();
        // サブショットの発射口を配置する
        SubShooterSetting();
    }
    public virtual void Update()
    {
        // 敵に倒されたとき
        if (deadpoint.getdeadmotionflag)
        {
            // 残機を減らす
            itemDisplayer.GetComponent<ItemNumManager>().getlifeNum = playerlife;
            // 自機の移動を止める
            playerRb.linearVelocity = Vector2.zero;
            // 少し時間を経過してから復活する
            respawnTime += Time.deltaTime;
            if (respawnTime >= respawnInterval)
            {
                playerRb.transform.position = new Vector2(0.0f, -3.0f); // 初期位置に戻す
                respawnTime = 0.0f;
                deadpoint.getdeadmotionflag = false;
            }
        }
        // 通常時は操作可能にする
        else
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
                // 連打でインターバル以上に弾を出さないようにしつつ、押しなおしたらすぐに弾が出るようにする
                if (meinshottime < meinshotinterval)
                {
                    meinshottime += Time.deltaTime;
                }
            }
            // 自機が指定の数値より上の位置にいるならアイテムを自動回収する
            if (playerposition.y >= itmecollectline)
            {
                itmecollector.SetActive(true);
            }
            // そうでないならアイテムを自動回収しない
            else
            {
                itmecollector.SetActive(false);
            }
            // ボムを持っている時にボムボタンを押したらボムを発動する
            if (controllerManager.bombAction.triggered && playerbomb > 0 && deadpoint.getinvicibleflag == false)
            {
                Instantiate(bombeffect, playerposition, Quaternion.identity);
                playerbomb -= 1;
                itemDisplayer.GetComponent<ItemNumManager>().getbombNum = playerbomb;
                deadpoint.getinvicibleflag = true;
            }
        }
    }
    /// <summary>
    /// 溜まったパワーの量に応じてサブショットを開放したり閉鎖したりする関数
    /// </summary>
    public virtual void ShotPowerCheck()
    {
        // パワーが4.0以上の時
        if (shotPower >= 400)
        {
            subshooterObj[3].SetActive(true);
        }
        // パワーが足りていないときは対応したサブショットを閉鎖する
        else
        {
            subshooterObj[3].SetActive(false);
        }
        // パワーが3.0以上の時
        if (shotPower >= 300)
        {
            subshooterObj[2].SetActive(true);
        }
        else
        {
            subshooterObj[2].SetActive(false);
        }
        // パワーが2.0以上の時
        if (shotPower >= 200)
        {
            subshooterObj[1].SetActive(true);
        }
        else
        {
            subshooterObj[1].SetActive(false);
        }
        // パワーが1.0以上の時
        if (shotPower >= 100)
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

        // 移動範囲の制限を超えていない時のみ移動する
        // 左の移動制限
        if (playermoveValue.x <= -0.1f && playerposition.x <= minMoveX)
        {
            playermoveValue.x = 0.0f;
        }
        // 右の移動制限
        if (playermoveValue.x >= 0.1f && playerposition.x >= maxMoveX)
        {
            playermoveValue.x = 0.0f;
        }
        // 下の移動制限
        if (playermoveValue.y <= -0.1f && playerposition.y <= minMoveY)
        {
            playermoveValue.y = 0.0f;
        }
        // 上の移動制限
        if (playermoveValue.y >= 0.1f && playerposition.y >= maxMoveY)
        {
            playermoveValue.y = 0.0f;
        }

        // 低速移動
        if (controllerManager.slowAction.IsPressed())
        {
            playerRb.linearVelocity = new Vector2(playermoveValue.x * playerSlowSpeed, playermoveValue.y * playerSlowSpeed);
            slowEffect.SetActive(true);
        }
        // 高速移動
        else
        {
            playerRb.linearVelocity = new Vector2(playermoveValue.x * playerHighSpeed, playermoveValue.y * playerHighSpeed);
            slowEffect.SetActive(false);
        }

        subShooterCenter.transform.position = new Vector2(playerRb.position.x,
                                                            playerRb.position.y);
        SubShooterPositionning();
    }
    /// <summary>
    /// X方向への移動を行う関数
    /// </summary>
    /// <param name="moveX"></param>
    public void PlayerMovementX(Vector2 moveX)
    {
        // 低速移動
        if (controllerManager.slowAction.IsPressed())
        {
            playerRb.linearVelocityX = moveX.x * playerSlowSpeed;
        }
        // 高速移動
        else
        {
            playerRb.linearVelocityX = moveX.x * playerHighSpeed;
        }
    }
    /// <summary>
    /// Y方向への移動を行う関数
    /// </summary>
    /// <param name="moveY"></param>
    public void PlayerMovementY(Vector2 moveY)
    {
        // 低速移動
        if (controllerManager.slowAction.IsPressed())
        {
            playerRb.linearVelocityY = moveY.y * playerSlowSpeed;
        }
        // 高速移動
        else
        {
            playerRb.linearVelocityY = moveY.y * playerHighSpeed;
        }
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
    /// <summary>
    /// サブショットの発射口の配置を変更する関数
    /// </summary>
    public virtual void SubShooterPositionning()
    {

    }

    public sbyte getlifeNum
    {
        get { return this.playerlife; }
        set { this.playerlife = value; }
    }

    public int getshotPower
    {
        get { return this.shotPower; }
        set { this.shotPower = value; }
    }

    public sbyte getplayerbomb
    {
        get { return this.playerbomb; }
        set { this.playerbomb = value; }
    }

    public int getshotMaxPower
    {
        get { return this.shotMaxPower; }
        private set { this.shotMaxPower = value; }
    }

    public GameObject getitemDisplayer
    {
        get { return this.itemDisplayer; }
        set { this.itemDisplayer = value; }
    }
}
