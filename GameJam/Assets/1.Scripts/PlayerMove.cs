using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Gui;

public class PlayerMove : MonoBehaviour
{
    public float speed = 1f;
    public float jump = 1f;
    public LayerMask WhatIsGround;
    public GameObject jumpPoint;

    Rigidbody2D rigid;
    public LeanJoystick leanJoystick;

    bool isleft = false;
    bool isright = false;
    bool isjump = false;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        RaycastHit2D hit =  Physics2D.Raycast(jumpPoint.transform.position, Vector2.down, 0.2f, WhatIsGround);
        if (hit)
        {
            isjump = true;
        }
        else if(!hit)
        {
            isjump = false;
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        Vector3 moveVelo = Vector3.zero;

        float y = transform.rotation.y;
        
        if (leanJoystick.ScaledValue.x < 0)
        {
            isright = false;

            moveVelo = Vector3.left;

            isleft = true;
        }
        else if (leanJoystick.ScaledValue.x > 0)
        {
            isleft = false;

            moveVelo = Vector3.right;

            isright = true;
        }

        if(isright)
        {
            y = 0;
        }
        else if(isleft)
        {
            y = 180;
        }

        transform.position += moveVelo * speed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0, y, 0);
    }

    public void Jump()
    {
        if (isjump)
        {
            isjump = false;

            rigid.velocity = Vector2.zero;

            Vector2 jumpVelo = new Vector2(0, jump);

            rigid.AddForce(jumpVelo, ForceMode2D.Impulse);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(jumpPoint.transform.position, Vector2.down * 0.2f);
    }
}
