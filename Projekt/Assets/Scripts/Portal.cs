using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private Transform player;
    public Transform receiver;

    private Camera mainCamera;


    private bool playerIsOverlapping = false;

    private void Awake()
    {
        this.player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Start()
    {
        mainCamera = Camera.main;
    }


    private void LateUpdate()
    {
        Vector3 portalToPlayer = player.position - transform.position;
        float dotProduct = Vector3.Dot(transform.up, portalToPlayer);


        if (this.playerIsOverlapping)
        {
            if(dotProduct < 0f)
            {
                float rotationDiff = -Quaternion.Angle(transform.rotation, receiver.rotation);
                rotationDiff += 180;
                player.Rotate(Vector3.up, rotationDiff);
                var pl = player.GetComponent<CharacterController>();
                pl.enabled = false;
                Vector3 positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * portalToPlayer;
                player.position = receiver.position + positionOffset;
                pl.enabled = true;
                playerIsOverlapping = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            this.playerIsOverlapping = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            this.playerIsOverlapping = false;
        }
    }
}
