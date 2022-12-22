using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float c_bulletSpeed = 10f;
    //设置子弹的生命周期
    public float lifeTime = 2;
    //子弹生成的时间
    float startTime;
   
    void Start()
    {
        startTime = Time.time;
    }

   
    void Update()
    {
        //子弹移动
        transform.position+= c_bulletSpeed * transform.forward * Time.deltaTime;
        //自毁装置
        if (startTime + lifeTime < Time.time)
            Destroy(gameObject);
    }

    //中弹事件
    private void OnTriggerEnter(Collider other)
    {
        //同类子弹碰撞不销毁
        if (CompareTag(other.tag))
            return;

        Destroy(gameObject);
    }
}
