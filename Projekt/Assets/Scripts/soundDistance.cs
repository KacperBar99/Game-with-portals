using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static statics;

public class soundDistance : MonoBehaviour
{
    [SerializeField]
    private float MaxDistance = 150f;

    private AudioSource AudioSource;
    private Transform player;
    private bool inRange;

    void Start()
    {
        this.AudioSource = GetComponent<AudioSource>();
        this.player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange)
        {
            if (Vector3.Distance(this.transform.position, this.player.position) > this.MaxDistance)
            {
                this.inRange = false;
                StartCoroutine(AudioFadeOut.FadeOut(this.AudioSource, 1f));
            }
        }
        else
        {
            if (Vector3.Distance(this.transform.position, this.player.position) <= this.MaxDistance)
            {
                this.inRange = true;
                this.AudioSource.Play();
            }
        }
    }
}
