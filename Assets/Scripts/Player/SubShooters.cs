using UnityEngine;
// サブショットの制御をするスクリプト
public class SubShooters : MonoBehaviour
{
    [SerializeField] protected  GameObject subBullet;

    protected float subshotinterval; // サブショットのインターバル
    protected float subshottime = 0.0f; // サブショットを撃つタイミングを測る

    protected GameObject ShotsField;

    private GameObject controllerManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public virtual void Start()
    {
        ShotsField = GameObject.Find("ShotsField");
        controllerManager = GameObject.Find("Controller");
    }

    // Update is called once per frame
    public void Update()
    {
        if (controllerManager.GetComponent<ControllerManager>().shottingAction.IsPressed())
        {
            subshottime += Time.deltaTime;
            if (subshottime >= subshotinterval)
            {
                SubShotting();
            }
        }
        else
        {
            subshottime = subshotinterval; // 次に押した時すぐに発射されるようにする
        }
        
    }

    public virtual void SubShotting()
    {
        Instantiate(subBullet, this.transform.position, Quaternion.identity, ShotsField.transform);
        subshottime = 0.0f;
    }
}
