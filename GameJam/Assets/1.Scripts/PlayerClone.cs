using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerClone : MonoBehaviour
{
    public Slider gauge;
    private bool button;
    private float cloneScale;
    public GameObject player;
    public GameObject cloneParent;
    Vector2 fwd;
    float x;
    float direction;
    private void Start()
    {
        fwd = transform.TransformDirection(Vector2.down);
    }
    void Update()
    {
        gauge.value = gameObject.transform.localScale.x - 0.2f;
        if (button && gameObject.transform.localScale.x > 0.2f)
        {
            cloneScale += 1 * Time.deltaTime;
            Clone();
        }
        if (transform.rotation.y < 0)
        {
            direction = -1;
        }
        else if (transform.rotation.y > -1)
        {
            direction = 1;
        }
    }
    void Damage()
    {
        float playerScale = cloneScale * 0.0005f;
        gameObject.transform.localScale = new Vector3(transform.localScale.x - playerScale, transform.localScale.y - playerScale, transform.localScale.z - playerScale);
    }
    void Clone()
    {
        x += 0.575f * Time.deltaTime;
        cloneParent.transform.GetChild(cloneParent.transform.childCount - 1).transform.localScale = new Vector3(cloneScale, cloneScale, cloneScale);
        cloneParent.transform.GetChild(cloneParent.transform.childCount - 1).transform.position = new Vector3(gameObject.transform.position.x + direction + x * direction, gameObject.transform.position.y + x - 0.5f, 0);
        Damage();
    }
    public void ButtonDown()
    {
        if (gameObject.transform.localScale.x > 0.2f)
        {
            Instantiate(player, cloneParent.transform);
            cloneParent.transform.GetChild(cloneParent.transform.childCount - 1).transform.position = new Vector3(gameObject.transform.position.x + direction, gameObject.transform.position.y, gameObject.transform.position.z);
            button = true;
        }
        x = 0;
    }
    public void ButtonUp()
    {
        cloneScale = 0;
        button = false;
    }
}
