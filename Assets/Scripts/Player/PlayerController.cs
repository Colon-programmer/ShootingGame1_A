using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private InputAction shottingAction; // �V���b�g�{�^���̓��͔���
    private InputAction moveingAction; // �ړ��̓��͔���
    [SerializeField] private GameObject meinshottingPrefab; // ���C���V���b�g�̉摜
    private float meinshotinterval = 0.1f; // ���C���V���b�g�̃C���^�[�o��
    private float meinshottime = 0.0f; // ���C���V���b�g�����^�C�~���O�𑪂�
    private Vector2 playerposition;
    private Rigidbody2D playerRb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        shottingAction = InputSystem.actions.FindAction("Attack");
        moveingAction = InputSystem.actions.FindAction("Move");
        playerRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // �ړ�
        var playermoveValue = moveingAction.ReadValue<Vector2>();
        playerRb.linearVelocity = new Vector2(playermoveValue.x, playermoveValue.y);
        // �V���b�g
        playerposition = this.transform.position;
        if (shottingAction.IsPressed())
        {
            meinshottime += Time.deltaTime;
            if (meinshottime >= meinshotinterval)
            {
                Instantiate(meinshottingPrefab,
                    new Vector2(playerposition.x - 0.25f, playerposition.y), Quaternion.identity);
                Instantiate(meinshottingPrefab,
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
