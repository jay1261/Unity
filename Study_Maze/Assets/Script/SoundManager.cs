using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    #region 변수
    public AudioClip AudioClip;
    public List<AudioClip> AudioClip_List;
    public AudioSource AudioSource;
    #endregion

    #region Singleton
    private static SoundManager Instance;
    public static SoundManager _Instance
    {
        get { return Instance; }
    }
    #endregion

    private void Awake()
    {
        //Instance = GetComponent<SoundManager>();
        Instance = this;

        AudioSource = GetComponent<AudioSource>();
        //AudioClip_List = new List<AudioClip>(5);

    }

    public void JumpSound()
    {
        //AudioSource.PlayOneShot(AudioClip);
        AudioSource.PlayOneShot(AudioClip_List[0]);
    }
    public void GameOverSound()
    {
        AudioSource.PlayOneShot(AudioClip_List[1]);
    }
}
