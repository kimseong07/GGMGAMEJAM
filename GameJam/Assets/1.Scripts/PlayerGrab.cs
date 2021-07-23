using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Gui;

public class PlayerGrab : MonoBehaviour
{
    public bool grabb;

    public bool cat = false;

    public float throwpower = 0;

    public GameObject playerMg;
    // Start is called before the first frame update
    void Start()
    {
        
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

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "able")
        {
            Debug.Log(collision.gameObject.tag);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(1 * throwpower, 0));
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "able")
        {
            cat = false;
        }
    }
}
