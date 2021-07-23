using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Gui;

public class PlayerGrab : MonoBehaviour
{
    public bool cat = false;

    public float throwpower = 0;

    public BoxCollider2D box;

    PlayerMove player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        
    }

    public void Grab()
    {
        cat = true;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "able")
        {
            Debug.Log(collision.gameObject.tag);

            Vector2 force = new Vector2(player.leanJoystick.ScaledValue.x * throwpower, 0);
            if (cat == true)
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
            }
        }
    }
    /*
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "able")
        {
            cat = false;
        }
    }
    */
}
