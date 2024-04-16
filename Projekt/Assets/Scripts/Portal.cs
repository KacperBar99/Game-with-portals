using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Transform receiver;
    public PortalWrapper wrapper;


    private Transform player;
    private bool playerIsOverlapping = false;

    

    private void Awake()
    {
        this.player = GameObject.FindGameObjectWithTag("Player").transform;
        
    }

    [SerializeField]
    private bool isDisablingOffset;

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
                
                if(this.wrapper != null)
                this.wrapper.setEvent(isDisablingOffset);
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
    public Vector3 getPosition()
    {
        return this.transform.position;
    }
    public Vector3 getLocalUp()
    {
        return this.transform.up;
    }
}
