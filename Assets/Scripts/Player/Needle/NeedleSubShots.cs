using UnityEngine;
//針装備選択時のサブショットを制御するスクリプト

public class NeedleSubShots : SubShooters
{
    private bool shotflag = true;

    SpriteRenderer subshooterSprite;

    public override void Start()
    {
        base.Start();

        subshooterSprite = GetComponent<SpriteRenderer>();
    }
    public override void SubShotting()
    {
        if (shotflag)
        {
            // 低速時
            if (controllerManager.GetComponent<ControllerManager>().slowAction.IsPressed())
            {
                subshotinterval = 0.1f;
                Instantiate(subBullet,
                    this.transform.position, Quaternion.identity, ShotsField.transform);
                subshottime = 0.0f;
            }
            // 高速時
            else
            {
                subshotinterval = 0.1f;
                Instantiate(subBullet,
                    this.transform.position, Quaternion.identity, ShotsField.transform);
                subshottime = 0.0f;
            }
        }
    }
}
