using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
        box.enabled = false;
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
        StartCoroutine(force());
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "able")
        {
            Vector2 force = new Vector2(throwpower, 0);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(force * -box.offset.y, ForceMode2D.Impulse);
        }
    }

    IEnumerator force()
    {
        box.enabled = true;
        yield return new WaitForSeconds(0.5f);
        box.enabled = false;
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
