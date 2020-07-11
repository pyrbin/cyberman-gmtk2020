using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFire : MonoBehaviour
{
    public float BulletForce = 5f;
    public Transform BulletContainer;
    public Transform FirePoint;
    public Bullet Bullet;

    public void Fire()
    {
        var proj = Instantiate(Bullet, BulletContainer) as Bullet;
        proj.transform.position = FirePoint.transform.position;
        proj.Rbody.AddForce(Vector2.right * BulletForce, ForceMode2D.Impulse);
        Destroy(proj, 5f);
    }
}
