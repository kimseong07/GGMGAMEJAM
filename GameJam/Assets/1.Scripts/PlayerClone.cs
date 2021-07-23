using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerClone : MonoBehaviour
{
    public float itemHeal = 0.25f;
    private Vector3 scaleSave;
    public Slider gauge;
    private bool button;
    private float cloneScale;
    public GameObject clone;
    public GameObject cloneParent;
    float x;
    float direction;

    void Update()
    {
        gauge.value = gameObject.transform.localScale.y - 0.2f;
        if (button && gameObject.transform.localScale.y > 0.2f)
        {
            cloneScale += 1 * Time.deltaTime;
            Clone();
        }
        if (transform.localScale.x < 0)
        {
            direction = -1;
        }
        else if (transform.localScale.x > 0)
        {
            direction = 1;
        }
        DClone();
    }
    public void ButtonDown()
    {
        if (gameObject.transform.localScale.y > 0.2f)
        {
            Instantiate(clone, cloneParent.transform);
            cloneParent.transform.GetChild(cloneParent.transform.childCount - 1).transform.position = new Vector3(gameObject.transform.position.x + direction, gameObject.transform.position.y, gameObject.transform.position.z);
            button = true;
        }
        scaleSave = gameObject.transform.localScale;
        x = 0;
    }
    public void ButtonUp()
    {
        cloneScale = 0;
        button = false;
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
        cloneParent.transform.GetChild(cloneParent.transform.childCount - 1).transform.localScale = new Vector3(cloneScale, cloneScale, cloneScale);
        cloneParent.transform.GetChild(cloneParent.transform.childCount - 1).transform.position = new Vector3(gameObject.transform.position.x + direction + x * direction, gameObject.transform.position.y + x - 0.5f, 0);
        //Damage();
    }
    public void DClone()
    {
        if (cloneParent.transform.childCount > 0 && cloneParent.transform.GetChild(cloneParent.transform.childCount - 1).transform.localScale.x > 2.5f)
        {
            Destroy(cloneParent.transform.GetChild(cloneParent.transform.childCount - 1).gameObject);
            button = false;
            gameObject.transform.localScale = scaleSave;
        }
    }

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
}