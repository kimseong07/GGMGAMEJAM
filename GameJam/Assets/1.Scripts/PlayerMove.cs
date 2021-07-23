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
    public GameObject AngleBar;

    Rigidbody2D rigid;
    public LeanJoystick leanJoystick;
    public LeanJoystick verticalJoy;

    bool isleft = false;
    bool isright = false;
    public bool isjump = false;

    RaycastHit2D hittted;
    public bool grabb;
    public float distance = 2f;
    public Transform holdpoint;
    public float throwpower;
    //vector2ÀÇ y°ª
    public float throwAngle = 1;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(jumpPoint.transform.position, Vector2.down, 0.2f);
        if (hit)
        {
            isjump = true;
        }
        else if (!hit)
        {
            isjump = false;
        }

        if (grabb)
        {
            hittted.collider.gameObject.transform.position = holdpoint.position;
            verticalJoy.gameObject.SetActive(true);
            AngleBar.SetActive(true);
        }

    }

    private void FixedUpdate()
    {
        Move();
        ThrowAng();
    }

    void Move()
    {
        Vector3 moveVelo = Vector3.zero;

        float x = transform.localScale.x;

        if (leanJoystick.ScaledValue.x < 0)
        {
            if (isright)
            {
                isright = false;

                moveVelo = Vector3.left;

                isleft = true;
                x *= -1;
            }
            isright = false;

            moveVelo = Vector3.left;

            isleft = true;
        }
        else if (leanJoystick.ScaledValue.x > 0)
        {
            if (isleft)
            {
                isleft = false;

                moveVelo = Vector3.right;

                isright = true;
                x *= -1;
            }
            isleft = false;

            moveVelo = Vector3.right;

            isright = true;
        }



        transform.position += moveVelo * speed * Time.deltaTime;
        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
    }

    void ThrowAng()
    {
        if (verticalJoy.ScaledValue.y < 0)
        {
            throwAngle -= 0.02f;

        }
        else if (verticalJoy.ScaledValue.y > 0)
        {
            throwAngle += 0.02f;
        }

        if(throwAngle < 0)
        {
            throwAngle = 0;
        }
        else if(throwAngle > 2)
        {
            throwAngle = 2;
        }
    }

    public void Jump()
    {
        if (isjump)
        {
            isjump = false;

            rigid.velocity = new Vector2(rigid.velocity.x, 0);
            rigid.velocity += Vector2.up * jump;
        }
    }

    public void Grab()
    {
        if (!grabb)
        {
            Physics2D.queriesStartInColliders = false;

            hittted = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, distance);

            if (hittted.collider != null)
            {
                grabb = true;
            }

        }
        else
        {
            grabb = false;

            if (hittted.collider.gameObject.GetComponent<Rigidbody2D>() != null)
            {
                hittted.collider.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x, throwAngle) * throwpower; ;
            }
            verticalJoy.gameObject.SetActive(false);
            AngleBar.SetActive(false);
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector2.down);
    }

}