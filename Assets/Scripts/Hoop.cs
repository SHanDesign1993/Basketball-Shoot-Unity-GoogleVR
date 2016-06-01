using UnityEngine;
using System.Collections;

public class Hoop : MonoBehaviour {
    public AudioClip hoopSound;
    public AudioClip peopleSound;
    private AudioSource source;
    private float volLowRange = .7f;
    private float volHighRange = 1.0f;
    void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider col)
    { 
        if (col.gameObject.tag == "ball")
        {
            float vol = Random.Range(volLowRange, volHighRange);
            source.PlayOneShot(hoopSound, vol);
            source.PlayOneShot(peopleSound, vol);
        }
    }
}
