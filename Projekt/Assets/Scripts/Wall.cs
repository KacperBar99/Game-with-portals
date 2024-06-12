using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField]
    private Vector3 moveOffset = Vector3.zero;

    [SerializeField]
    private float time = 1.0f;

    private bool isDefault = true;
    private Vector3 defaultPosition;
    private AudioSource audioSource;

    private void Start()
    {
        this.audioSource = GetComponent<AudioSource>();
        this.defaultPosition = this.transform.position;
    }
    public void Update()
    {
        if (this.isDefault)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, this.defaultPosition, Time.deltaTime/this.time);
        }
        else
        {
            this.transform.position = Vector3.Lerp(this.transform.position, this.defaultPosition + moveOffset, Time.deltaTime/this.time);
        }
        
    }

    public void changeState()
    {
        this.isDefault=!this.isDefault;
        this.audioSource.Play();
    }
}
