using UnityEngine;
using UnityEngine.InputSystem;
// コントローラーの入力を受け付けるスクリプト
public class ControllerManager : MonoBehaviour
{
    public InputAction shottingAction; // ショットボタンの入力判定
    public InputAction moveingAction; // 移動の入力判定
    public InputAction slowAction; // 低速移動切り替えボタン
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 入力を受け付けるようにする
        shottingAction = InputSystem.actions.FindAction("Attack");
        moveingAction = InputSystem.actions.FindAction("Move");
        slowAction = InputSystem.actions.FindAction("Slow");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
