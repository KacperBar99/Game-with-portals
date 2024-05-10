using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField]
    private bool isDisablingOffset;

    [SerializeField]
    private Transform receiver;
    [SerializeField]
    private PortalWrapper wrapper;


    private Transform player;
    private bool playerIsOverlapping = false;
    private float lastDotProduct = 0;
    private Transform portal;
    private Transform Bportal;


    private void Awake()
    {
        this.player = GameObject.FindGameObjectWithTag("Player").transform;
        this.portal = this.transform.parent;
        this.Bportal = this.receiver.parent;
    }

    private void LateUpdate()
    {
        Vector3 portalToPlayer = this.player.position - this.transform.position;
        float dotProduct = Vector3.Dot(this.transform.up, portalToPlayer);

        if (this.playerIsOverlapping && dotProduct < 0f && lastDotProduct > 0)
        {
            var pl = player.GetComponent<CharacterController>();
            pl.enabled = false;
            float rotationDiff = -Vector3.SignedAngle(this.portal.forward, this.Bportal.forward,this.transform.up);
            this.player.Rotate(Vector3.up, rotationDiff);
            Vector3 positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * portalToPlayer;
            player.position = receiver.position + positionOffset;
            pl.enabled = true;
            playerIsOverlapping = false;

            if (this.wrapper != null)
                this.wrapper.setEvent(isDisablingOffset);
        }
        this.lastDotProduct = dotProduct;
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
