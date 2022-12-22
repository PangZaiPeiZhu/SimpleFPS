using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //敌人死亡预制体
    public GameObject prefabBoomEffect;

    public float speed = 6;
    public float fireTime = 1f;
    public float maxHp=4;
    float m_time ;

    Vector3 input;

    Transform player;
    float hp;
    bool lifeState = true;

    Weapon weapon;


   
    void Start()
    {
        player = GameObject.Find("Player").transform;
        weapon = gameObject.GetComponent<Weapon>();
        m_time = Time.time;
        hp = maxHp;
        prefabBoomEffect = GameObject.Find("prefabDeath");
    }

  
    void Update()
    {
        

        EnemyMove();
        endFire();
        Debug.Log("敌人还剩血量：" + hp);
        if (hp <= 0)
        {
           // Instantiate(prefabBoomEffect, transform.position, transform.rotation);
            Destroy(gameObject);
           
                Debug.Log("恭喜你赢了！重要的事情说三遍");
                Debug.Log("恭喜你赢了！重要的事情说三遍");
                Debug.Log("恭喜你赢了！重要的事情说三遍");
          
           
        }

       
    }

    //敌人的移动机制
    void EnemyMove()
    {
        //敌人的移动是始终朝向玩家的，他想干掉我们
        input = player.position - transform.position;
        input = input.normalized;

        transform.position += input * speed * Time.deltaTime;
        if (input.magnitude > 0.1f)
        {
            transform.forward = input;
        }
    }

    //控制攻击间隔
    void endFire()
    {
        if (m_time + fireTime < Time.time)
        {
            Fire();
            m_time = Time.time;
        }

    }

    //敌人的攻击机制
    void Fire()
    {
        weapon.Fire(true, true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerBullet"))
        {
            if (hp <= 0) { return; }
            hp--;
            if (hp == 0) { lifeState = false; }
        }
    }
}
