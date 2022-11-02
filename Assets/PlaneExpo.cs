using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneExpo : MonoBehaviour
{
    public AudioSource source;
    public AudioClip clip;
    float timer;
    void Start()
    {
        source.Play();
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= clip.length)
            Destroy(gameObject);
    }

}
