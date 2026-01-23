using UnityEngine;
// サブショットの制御をするスクリプト
public class SubShooters : MonoBehaviour
{
    [SerializeField] protected  GameObject subBullet;

    protected float subshotinterval; // サブショットのインターバル
    protected float subshottime = 0.0f; // サブショットを撃つタイミングを測る

    protected GameObject ShotsField;

    protected GameObject controllerManager;

    [SerializeField] private GameObject playerdead;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public virtual void Start()
    {
        ShotsField = GameObject.Find("ShotsField");
        controllerManager = GameObject.Find("Controller");
        playerdead = GameObject.Find("PlayerDeadPoint");
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (controllerManager.GetComponent<ControllerManager>().shottingAction.IsPressed() && 
            !playerdead.GetComponent<PlayerDeadPoint>().getdeadmotionflag)
        {
            subshottime += Time.deltaTime;
            if (subshottime >= subshotinterval)
            {
                SubShotting();
            }
        }
        else
        {
            // 連打でインターバル以上に弾を出さないようにしつつ、押しなおしたらすぐに弾が出るようにする
            if (subshottime < subshotinterval)
            {
                subshottime += Time.deltaTime;
            }
        }
        
    }

    public virtual void SubShotting()
    {
        Instantiate(subBullet, this.transform.position, Quaternion.identity, ShotsField.transform);
        subshottime = 0.0f;
    }

    public float getsubshotinterval
    {
        get { return this.subshotinterval; }
        set { this.subshotinterval = value; }
    }
}
