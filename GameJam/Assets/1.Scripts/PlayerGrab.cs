using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Gui;

public class PlayerGrab : MonoBehaviour
{
    public LeanJoystick verticalJoy;

    public bool grabb;
    public float distance = 0.9f;

    public float throwpower = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (grabb)
        {
            verticalJoy.gameObject.SetActive(true);
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
            throwpower -= 0.5f;

        }
        else if (verticalJoy.ScaledValue.y > 0)
        {
            throwpower += 0.5f;
        }
    }

    public void Grab()
    {
        if (!grabb)
        {
            grabb = true;
        }
        else
        {
            grabb = false;
            verticalJoy.gameObject.SetActive(false);
        }

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "able" && grabb == true)
        {
            Debug.Log(collision.gameObject.tag);
        }
    }
}
