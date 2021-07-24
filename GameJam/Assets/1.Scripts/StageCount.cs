using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageCount : MonoBehaviour
{
    public int stageNum;
    private int stageCount;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            stageCount = PlayerPrefs.GetInt("stageNum", 1);
            if (stageNum == stageCount)
            {
                stageCount++;
                PlayerPrefs.SetInt("stageNum", stageCount);
            }
        }
    }
}
