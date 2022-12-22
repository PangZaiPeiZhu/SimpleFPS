using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    //子弹的预制体
    public GameObject prefabBullet;

    //几种武器的CD长度
    public float pistolFireCD = 0.2f;
    public float shotgunFireCD = 0.5f;
    public float rifleFireCD = 0.1f;

    //上次开火时间
    float lastFireTime;

    //当前的武器
    public int curGun    //  0:手枪，1：霰弹枪，2：自动步枪
    {
        get;
        private set;
    }

    //设置开火函数
    //keyDown指代单射，keyPressed指代连射
    public void Fire(bool keyDown,bool keyPressed)
    {
        switch (curGun)
        {
            case 0:
            if (keyDown)
                {
                    PistolFire();
                }
                break;
            case 1:
                if (keyDown)
                {
                    ShotgunFire();
                }
                break;
            case 2:
                if(keyPressed)
                {
                    RifleFire();
                }
                break;

        }

    }

    //更换武器
    public int Change()
    {
        if (curGun != 3)
        {
            curGun++;
        }
        else
        {
            curGun = 0;
        }
        return curGun;
    }

    //手枪射击专用函数
    public void PistolFire()
    {
        if (lastFireTime + pistolFireCD > Time.time)
            return;
        lastFireTime = Time.time;

        GameObject bullet = Instantiate(prefabBullet, null);
        bullet.transform.position = transform.position + transform.forward * 1f;
        bullet.transform.forward = transform.forward;
    }
    //霰弹射击专用函数
    public void ShotgunFire()
    {
        if (lastFireTime + shotgunFireCD > Time.time)
            return;
        lastFireTime = Time.time;

       //创建5颗子弹，相互间间隔10度，分布于前方扇形区域
       for(int i = -2; i <= 2; i++)
        {
            GameObject bullet = Instantiate(prefabBullet, null);
            Vector3 dir = Quaternion.Euler(0, i * 10, 0) * transform.forward;

            bullet.transform.position = transform.position + dir * 1f;
            bullet.transform.forward = dir;

            //霰弹枪的子弹射击距离很短，因此修改子弹的生命周期
            Bullet b = bullet.GetComponent<Bullet>();
            b.lifeTime = 0.3f;
        }
    }
    //自动步枪射击专用函数
    public void RifleFire()
    {
        if (lastFireTime + rifleFireCD > Time.time)
            return;
        lastFireTime = Time.time;

        GameObject bullet = Instantiate(prefabBullet, null);
        bullet.transform.position = transform.position + transform.forward * 1.2f;
        bullet.transform.forward = transform.forward;
    }
    void Start()
    {
        
    }

   
    void Update()
    {
        
    }
}
