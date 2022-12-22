using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform followTaeget;
    Vector3 offset;

    
    void Start()
    {
        //…Ë÷√∆´“∆¡ø
        offset = transform.position - followTaeget.position;
    }

   
    void LateUpdate()
    {
        
        transform.position =  followTaeget.position + offset;
        
    }
}
