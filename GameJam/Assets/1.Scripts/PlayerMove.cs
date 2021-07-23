using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Gui;

public class PlayerMove : MonoBehaviour
{
    public GameObject player;

    public float speed = 1f;
    public float jump = 1f;
    public LayerMask WhatIsGround;
    public GameObject jumpPoint;

    Rigidbody2D rigid;
    public LeanJoystick leanJoystick;

    bool isleft = false;
    bool isright = false;
    public bool isjump = false;


    BoxCollider2D box;

    PlayerGrab grab;

    public Transform groundChkFront;  // 바닥 체크 position 
    public Transform groundChkBack;   // 바닥 체크 position 
    public Transform groundChkcenter;

    private bool isGround;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
        grab = FindObjectOfType<PlayerGrab>();
    }

    void Update()
    {
        bool ground_front = Physics2D.Raycast(groundChkFront.position, Vector2.down, 0.2f, WhatIsGround);
        bool ground_back = Physics2D.Raycast(groundChkBack.position, Vector2.down, 0.2f, WhatIsGround);
        bool ground_center = Physics2D.Raycast(groundChkcenter.position, Vector2.down, 0.2f, WhatIsGround);

        if (ground_front || ground_back || ground_center)
        {
            isGround = true;
        }
        else
            isGround = false;

        if (isGround)
        {
            isjump = true;
        }
        else if (!isGround)
        {
            isjump = false;
        }

        Jump();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        if (leanJoystick.ScaledValue.x < 0)
        {
            rigid.AddForce(new Vector2(leanJoystick.ScaledValue.x * speed , 0));

            player.transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (leanJoystick.ScaledValue.x > 0)
        {
            rigid.AddForce(new Vector2(leanJoystick.ScaledValue.x * speed , 0));

            player.transform.localScale = new Vector3(1, 1, 1);
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

    

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(groundChkFront.position, Vector2.down * 0.2f);
        Gizmos.DrawRay(groundChkBack.position, Vector2.down * 0.2f);
        Gizmos.DrawRay(groundChkcenter.position, Vector2.down * 0.2f);
    }
}