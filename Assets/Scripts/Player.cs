using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //移动速度
    public float c_speed;

    //最大血量
    public float m_maxHp ;

    //输入方向变量
    Vector3  c_input;

    //判断是否死亡
    bool LifeState=true;

    //当前血量
    private float c_Hp;

    Weapon weapon;

    //刚体
    private Rigidbody playerRigidbody;
    public float force=15;
    
    void Start()
    {
        //获取刚体组件
        playerRigidbody = GetComponent<Rigidbody>();
        //刚开始满血
        c_Hp = m_maxHp;
        //给武器类对象变量引用脚本
        weapon = GetComponent<Weapon>();
    }



   
    void Update()
    {
        //控制跳跃
        if (transform.position.y <= 1.89)
        {
            if (Input.GetKeyDown(KeyCode.Space))

            {
                Debug.Log("空格键有用");
                playerRigidbody.AddForce(0, force, 0);
            }
        }
        
        //将键盘的横向、纵向输入，保存在input中
        c_input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        bool fireKeyDown = Input.GetKeyDown(KeyCode.J);
        bool fireKeyPressed = Input.GetMouseButton(0);
        bool changeWeapon = Input.GetKeyDown(KeyCode.Q);


        //判断是否dead，还活着就可以动

        Debug.Log("现在还剩血量：" + c_Hp);
        if (LifeState==true )
        {

            //rotate();
            Move();
            weapon.Fire(fireKeyDown, fireKeyPressed);
            if (changeWeapon)
            {
                weapon.Change();
            }
            

        }
        else { Time.timeScale = 0; }

        void Move()
        {
            
            //先归一化输入变量，让输入更直接，避免同时斜向移动时速度超过最大值
            c_input =c_input.normalized;
            transform.position += c_input * c_speed * Time.deltaTime;
            //令角色前方与移动方向一致
            if (c_input.magnitude > 0.1f)
            {
                transform.forward = c_input;
            }
            
            




            //限制移动范围
            Vector3 c_VectorTemp=transform.position ;
            const float m_border = 20f;
            if(c_VectorTemp.x < -m_border)
            {
                c_VectorTemp.x = -m_border;

            }
            if (c_VectorTemp.x > m_border)
            {
                c_VectorTemp.x = m_border;

            }
            if (c_VectorTemp.z < -m_border)
            {
                c_VectorTemp.z = -m_border;

            }
            if (c_VectorTemp.z > m_border)
            {
                c_VectorTemp.z = m_border;

            }

            transform.position = c_VectorTemp;




        }
        /*void rotate()
        {
            float mouse_x = Input.GetAxis("Mouse X");
            float mouse_y = Input.GetAxis("Mouse Y");
            Quaternion qx = Quaternion.Euler(0, mouse_x, 0);
            Quaternion qy = Quaternion.Euler(-mouse_y, 0, 0);

            //记住当前乘积顺序
            transform.rotation = qx*transform.rotation ;//绕世界y坐标旋转

            transform.rotation = transform.rotation*qy;//绕自身坐标x旋转
        
        }*/



    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyBullet"))
        {
            if (c_Hp <= 0) { return; }
            c_Hp-=1;
            if (c_Hp == 0) { LifeState = false; }
        }
    }
}
