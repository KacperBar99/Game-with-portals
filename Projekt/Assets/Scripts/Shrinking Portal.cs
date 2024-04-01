using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkingPortal : MonoBehaviour
{

    [SerializeField]
    private PortalWrapper tunnelA;
    [SerializeField]
    private PortalWrapper tunnelB;
    [SerializeField]
    private PortalWrapper fakeA;
    [SerializeField]
    private PortalWrapper fakeB;

    [SerializeField]
    private Transform fake;
    [SerializeField]
    private Transform real;


    private float entryScale;
    private float exitScale;
    private Transform player;


    private float distanceReal;
    private float distanceFake;

    // Start is called before the first frame update
    void Start()
    {
       
        Vector3 difference = this.fake.position - this.real.position;
        this.tunnelB.setOffset(difference);
        this.player = GameObject.FindGameObjectWithTag("Player").transform;
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void checkPlayerPosition(string[] id)
    {

        Debug.Log(id[0]);
        Debug.Log(id[1]);

        if (id[0] == "Portal" && id[1] == "Real")
        {
            this.tunnelB.setUseOffset(false);
        }
        else this.tunnelB.setUseOffset(true);
    }

}
