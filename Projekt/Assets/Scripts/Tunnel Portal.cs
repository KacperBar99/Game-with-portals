using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelPortal : MonoBehaviour
{
    private enum direction
    {
        x=0, y=1, z=2
    }

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

    [SerializeField]
    private direction orientation;

    [SerializeField]
    private bool reverseTunnelOrientation=false;


    private float entryScale;
    private float exitScale;
    private Transform player;


    private float distanceReal;
    private float distanceFake;

    // Start is called before the first frame update
    void Start()
    {

        Vector3 differenceA = this.fake.position - this.real.position;
        Vector3 differenceB = differenceA;
        this.distanceReal = Vector3.Distance(this.tunnelA.transform.position, this.tunnelB.transform.position);
        this.distanceFake = Vector3.Distance(this.fakeA.transform.position, this.fakeB.transform.position);
        float lengthDifference = Mathf.Abs(distanceReal - distanceFake)/2.0f;


        switch (orientation)
        {
            case direction.x:
                {
                    differenceA.x += lengthDifference;
                    differenceB.x -= lengthDifference;
                    break;
                }
            case direction.y:
                {
                    differenceA.y += lengthDifference;
                    differenceB.y -= lengthDifference;
                    break;
                }
            case direction.z:
                {
                    differenceA.z += lengthDifference;
                    differenceB.z -= lengthDifference;
                    break;
                }
        }
        if (reverseTunnelOrientation)
        {
            this.tunnelB.setOffset(differenceA);
            this.tunnelA.setOffset(differenceB);
        }
        else
        {
            this.tunnelB.setOffset(differenceB);
            this.tunnelA.setOffset(differenceA);
        }
        







        this.player = GameObject.FindGameObjectWithTag("Player").transform;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void checkPlayerPosition(bool offsetChange)
    {

        if (offsetChange)
        {
            this.tunnelA.setUseOffset(false);
            this.tunnelB.setUseOffset(false);
        }
        else {
            this.tunnelB.setUseOffset(true);
            this.tunnelA.setUseOffset(true);
        }
        
    }

}
