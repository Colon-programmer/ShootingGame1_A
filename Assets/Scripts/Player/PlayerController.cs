using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
// 自機の操作を管理するスクリプト

public class PlayerController : MonoBehaviour
{
    private InputAction shottingAction; // ショットボタンの入力判定
    private InputAction moveingAction; // 移動の入力判定
    private InputAction slowAction; // 低速移動切り替えボタン

    [SerializeField] private GameObject meinshottingPrefab_1; // メインショットの画像(1人目のキャラの)
    [SerializeField] private GameObject meinshottingPrefab_2; // メインショットの画像(2人目のキャラの)
    private float meinshotinterval = 0.1f; // メインショットのインターバル
    private float meinshottime = 0.0f; // メインショットを撃つタイミングを測る

    private float playerHighSpeed = 5.0f; // 自機の高速時移動速度
    private float playerSlowSpeed = 2.0f; // 自機の低速時移動速度

    private Vector2 playerposition;
    private Rigidbody2D playerRb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        shottingAction = InputSystem.actions.FindAction("Attack");
        moveingAction = InputSystem.actions.FindAction("Move");
        slowAction = InputSystem.actions.FindAction("Slow");
        playerRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        var playermoveValue = moveingAction.ReadValue<Vector2>();
        // 移動
        if (slowAction.IsPressed())
        {
            // 低速移動
            playerRb.linearVelocity =
            new Vector2(playermoveValue.x * playerSlowSpeed, playermoveValue.y * playerSlowSpeed);
        }
        else
        {
            // 高速移動
            playerRb.linearVelocity =
            new Vector2(playermoveValue.x * playerHighSpeed, playermoveValue.y * playerHighSpeed);
        }
        // ショット
        playerposition = this.transform.position;
        if (shottingAction.IsPressed())
        {
            meinshottime += Time.deltaTime;
            if (meinshottime >= meinshotinterval)
            {
                // メインショットを撃つ
                Instantiate(meinshottingPrefab_1,
                    new Vector2(playerposition.x - 0.25f, playerposition.y), Quaternion.identity);
                Instantiate(meinshottingPrefab_1,
                    new Vector2(playerposition.x + 0.25f, playerposition.y), Quaternion.identity);
                meinshottime = 0.0f;
            }
        }
        else
        {
            meinshottime = 0.0f;
        }
    }
}
