using UnityEngine;
using System.Collections;

public class Bounce : MonoBehaviour {

    public AudioClip bounceSound;
    public AudioClip boardSound;
    private AudioSource source;
    private float volLowRange = .7f;
    private float volHighRange = 1.0f;
    void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision col)
    {
        float vol = Random.Range(volLowRange, volHighRange);
        if (col.gameObject.name == "Plane" )
        {
            source.PlayOneShot(bounceSound, vol);
        }
        if (col.gameObject.name == "Cylinder01")
        {
            source.PlayOneShot(boardSound, vol);
        }
    }
}
