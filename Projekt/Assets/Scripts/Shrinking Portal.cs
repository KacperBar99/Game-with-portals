using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkingPortal : MonoBehaviour
{
    public bool doStuff;

    [SerializeField]
    private Transform portal1;
    [SerializeField]
    private Transform portal2;

    private float scaleDifference;
    private GameObject player;
    private float portalDistance;
    private float initScale;
    private float maxScale;
    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.FindGameObjectWithTag("Player");
        this.portalDistance = Vector3.Distance(portal1.position, portal2.position);
        this.scaleDifference = portal1.localScale.x / portal2.localScale.x;
        this.initScale = this.player.transform.localScale.x;
        this.maxScale = this.player.transform.localScale.x/this.scaleDifference;

    }

    // Update is called once per frame
    void Update()
    {
        if (!doStuff) return;

        if(this.player.transform.position.z>portal1.position.z && this.player.transform.position.z <= portal2.position.z)
        {
            float currentDistance=this.player.transform.position.z-this.portal1.position.z;

            Debug.Log(currentDistance / this.portalDistance);
            SetPlayerScale(initScale-(currentDistance/this.portalDistance)*maxScale);
        }
    }

    private void SetPlayerScale(float scale)
    {
        Debug.Log("Skala "+scale);
        this.player.transform.localScale = new Vector3(scale, scale, scale);
    }
}
