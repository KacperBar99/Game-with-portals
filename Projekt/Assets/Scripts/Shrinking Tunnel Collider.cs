using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkingTunnelCollider : MonoBehaviour
{

    [SerializeField]
    private bool isStart = false; 
    private ShrinkingTunnel ShrinkingTunnel;


    private void Start()
    {
        this.ShrinkingTunnel=this.transform.parent.GetComponent<ShrinkingTunnel>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            this.ShrinkingTunnel.setInside(isStart);
        }
        
    }
}
