using UnityEngine;

public class MisyaSubShots : SubShooters
{
    public override void Start()
    {
        subshotinterval = 0.2f;

        base.Start();
    }

    public override void SubShotting()
    {
        Instantiate(subBullet, 
            new Vector2(this.transform.position.x - 0.2f, this.transform.position.y), Quaternion.identity, ShotsField.transform);
        Instantiate(subBullet,
            new Vector2(this.transform.position.x + 0.2f, this.transform.position.y), Quaternion.identity, ShotsField.transform);
        subshottime = 0.0f;
    }
}
