using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerClone : MonoBehaviour
{
    public float itemHeal = 0.25f;
    public Slider gauge;
    private float cloneScale;
    public GameObject clone;
    public GameObject cloneParent;
    float x;
    float direction;
    float amount = 100;

    private float coolTime = 1f;
    private float curCool = 0f;


    private void Start()
    {
        curCool = 0;
    }

    void Update()
    {
        gauge.value = amount / 100;
        if (transform.localScale.x < 0)
        {
            direction = -1.5f;
        }
        else if (transform.localScale.x > 0)
        {
            direction = 1.5f;
        }

        curCool -= Time.deltaTime;
    }
    public void ButtonDown()
    {
        
        if(amount > 0 && curCool <= 0)
        {
            amount -= 10;
            Instantiate(clone, cloneParent.transform);
            cloneParent.transform.GetChild(cloneParent.transform.childCount - 1).transform.position = new Vector3(gameObject.transform.position.x + direction, gameObject.transform.position.y, gameObject.transform.position.z);

            x = 0;
            Clone();

            curCool = coolTime;
        }
        else
        {
            
        }
        
        
    }
    public void Damage()
    {
        float playerScale = cloneScale * 0.0005f;
        if (transform.localScale.x < 0)
        {
            gameObject.transform.localScale = new Vector3(transform.localScale.x + playerScale, transform.localScale.y - playerScale, transform.localScale.z - playerScale);
        }
        else if (transform.localScale.x > 0)
        {
            gameObject.transform.localScale = new Vector3(transform.localScale.x - playerScale, transform.localScale.y - playerScale, transform.localScale.z - playerScale);
        }
    }
    public void Clone()
    {
        x += 0.575f * Time.deltaTime;
        //cloneParent.transform.GetChild(cloneParent.transform.childCount - 1).transform.localScale = new Vector3(cloneScale, cloneScale, cloneScale);
        cloneParent.transform.GetChild(cloneParent.transform.childCount - 1).transform.position = new Vector3(gameObject.transform.position.x + direction + x * direction, gameObject.transform.position.y + x - 0.5f, 0);
        //Damage();
    }
    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Item")
        {
            if (gameObject.transform.localScale.x < 1)
            {
                gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x + itemHeal, gameObject.transform.localScale.y + itemHeal, gameObject.transform.localScale.z + itemHeal);
                if (gameObject.transform.localScale.x > 1)
                {
                    gameObject.transform.localScale = new Vector3(1, 1, 1);
                    Destroy(collision.gameObject);
                }
            }
        }
    }
    */
}