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
    public LeanJoystick verticalJoy;

    bool isleft = false;
    bool isright = false;
    bool isjump = false;

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
            leanJoystick.gameObject.SetActive(false);
            verticalJoy.gameObject.SetActive(true);
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

        if (isright)
        {
            x = 1;
        }
        else if (isleft)
        {
            x = -1;
        }

        transform.position += moveVelo * speed * Time.deltaTime;
        transform.localScale = new Vector3(x,1,1);
    }

    void ThrowAng()
    {
        if(verticalJoy.ScaledValue.y < 0)
        {
            throwAngle -= 0.02f;
        }
        else if(verticalJoy.ScaledValue.y > 0)
        {
            throwAngle += 0.02f;
        }
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
            leanJoystick.gameObject.SetActive(true);
            verticalJoy.gameObject.SetActive(false);

            Debug.Log(transform.localScale.x);
        }
    }
}
