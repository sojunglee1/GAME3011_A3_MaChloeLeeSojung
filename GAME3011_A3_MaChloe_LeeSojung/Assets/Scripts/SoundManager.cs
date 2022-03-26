using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void MatchFound(AudioClip clip)
    {
        source.PlayOneShot(clip);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
