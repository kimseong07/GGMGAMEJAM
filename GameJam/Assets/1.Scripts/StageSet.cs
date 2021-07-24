using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSet : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> stageList = new List<GameObject>();
    [SerializeField]
    private List<GameObject> stageRock = new List<GameObject>();
    private bool stageSet = true;
    private int stageCount;
    private int stageCountStart;

    void Start()
    {
        stageCount = PlayerPrefs.GetInt("stageNum", 1);
        stageCountStart = stageCount;
        for (int a = 0; stageCount > a; a++)
        {
            stageList[a].gameObject.SetActive(stageSet);
            stageRock[a].gameObject.SetActive(!stageSet);
            stageCountStart = stageCount;
        }
    }
    void Update()
    {
        stageCount = PlayerPrefs.GetInt("stageNum", 1);
        if (stageCount > stageCountStart)
        {
            for (int a = 0; stageCount > a; a++)
            {
                stageList[a].gameObject.SetActive(stageSet);
                stageRock[a].gameObject.SetActive(!stageSet);
                stageCountStart = stageCount;
            }
        }
    }
}
