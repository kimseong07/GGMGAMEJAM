using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Music : MonoBehaviour
{
    public Slider mainbackVolume;
    public AudioSource mainaudio;
    private static float mainbackVol = 1f;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        SoundSlider();
    }
    public void SoundSlider()
    {
        mainaudio.volume = mainbackVolume.value;
        mainbackVol = mainbackVolume.value;
        PlayerPrefs.SetFloat("mainbackvol", mainbackVol);
    }
    private void Init()
    {
        mainbackVol = PlayerPrefs.GetFloat("mainbackvol", 1f);
        mainbackVolume.value = mainbackVol;
        mainaudio.volume = mainbackVolume.value;
    }
}
