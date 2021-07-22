using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerClone : MonoBehaviour
{
    public Slider gauge;
    private bool button;
    public GameObject player;
    public GameObject cloneParent;
    Vector2 fwd;
    private void Start()
    {
        fwd = transform.TransformDirection(Vector2.down);
    }
    void Update()
    {

        gauge.value = gameObject.transform.localScale.x - 0.2f;
        if (button && gameObject.transform.localScale.x > 0.2f)
        {
            DataManager.Instance.cloneScale += 1 * Time.deltaTime;
            Clone();
        }
    }
    void Damage()
    {
        float playerScale = DataManager.Instance.cloneScale * 0.0005f;
        gameObject.transform.localScale = new Vector3(transform.localScale.x - playerScale, transform.localScale.y - playerScale, transform.localScale.z - playerScale);
    }
    void Clone()
    {
        cloneParent.transform.GetChild(cloneParent.transform.childCount - 1).transform.localScale = new Vector3(DataManager.Instance.cloneScale, DataManager.Instance.cloneScale, DataManager.Instance.cloneScale);
        cloneParent.transform.GetChild(cloneParent.transform.childCount - 1).transform.position += new Vector3((gameObject.transform.position.x+0.5f + Time.deltaTime)*Time.deltaTime, 1 * Time.deltaTime, 0);
        Damage();
    }
    public void ButtonDown()
    {
        if (gameObject.transform.localScale.x > 0.2f)
        {
            Instantiate(player, cloneParent.transform);
            cloneParent.transform.GetChild(cloneParent.transform.childCount - 1).transform.position = new Vector3(gameObject.transform.position.x + 1, gameObject.transform.position.y, gameObject.transform.position.z);
            button = true;
        }
    }
    public void ButtonUp()
    {
        DataManager.Instance.cloneScale = 0;
        button = false;
    }
}
