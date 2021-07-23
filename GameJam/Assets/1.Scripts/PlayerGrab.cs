using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Gui;

public class PlayerGrab : MonoBehaviour
{

    Rigidbody2D rigid;

    RaycastHit2D hittted;

    public GameObject AngleBar;

    public LeanJoystick verticalJoy;

    public bool grabb;
    public float distance = 0.9f;
    public Transform holdpoint;
    public float throwpower;
    public float throwAngle = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (grabb)
        {
            hittted.collider.gameObject.transform.position = holdpoint.position;
            verticalJoy.gameObject.SetActive(true);
            AngleBar.SetActive(true);
        }
    }

    private void FixedUpdate()
    {
        ThrowAng();
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

        if (throwAngle < 0)
        {
            throwAngle = 0;
        }
        else if (throwAngle > 2)
        {
            throwAngle = 2;
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
}
