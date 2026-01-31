using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    
    [SerializeField] List<AudioClip> audioClips = new List<AudioClip>();
    [SerializeField] AudioSource audioSource;

    public void Awake()
    {
        if (instance == null) instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ContextMenu("CallSpellWASD")]
    private void CallSpellWASD()
    {
        StartCoroutine(SpellWASD());
    }

    private IEnumerator SpellWASD()
    {
        for (int i = 0; i < audioClips.Count; i++)
        {
            audioSource.PlayOneShot(audioClips[i]);
            yield return new WaitUntil(() => !audioSource.isPlaying);
        }
    }

    public void PlaySound(Sounds sound)
    {
        audioSource.PlayOneShot(audioClips[(int)sound]);
    }
    
    public enum Sounds
    {
        W,
        A,
        S,
        D
    }
}
