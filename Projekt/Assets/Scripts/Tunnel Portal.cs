using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelPortal : MonoBehaviour
{
    private enum direction
    {
        x=0, y=1, z=2
    }

    private Camera mainCamera;
    private Plane[] cameraFrustum;
    private Bounds[] bounds;

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


    private Transform player;


    private float distanceReal;
    private float distanceFake;

    private bool inside = false;


    private void Awake()
    {
        this.player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    // Start is called before the first frame update
    void Start()
    {
        this.mainCamera = Camera.main;
        bounds = new Bounds[4];
        bounds[0] = tunnelA.GetPortal().GetComponent<Collider>().bounds;
        bounds[1] = tunnelB.GetPortal().GetComponent<Collider>().bounds;
        bounds[2] = fakeA.GetPortal().GetComponent<Collider>().bounds;
        bounds[3] = fakeB.GetPortal().GetComponent<Collider>().bounds;
        tunnelA.setCameraStatus(false);
        tunnelB.setCameraStatus(false);
        fakeA.setCameraStatus(false);
        fakeB.setCameraStatus(false);

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




    }

    private void LateUpdate()
    {
        Vector3 portalToPlayer;
        float dotProduct;
        cameraFrustum = GeometryUtility.CalculateFrustumPlanes(mainCamera);
        if (inside)
        {
            

            //fake A
            portalToPlayer = player.position - fakeA.transform.position;
            dotProduct = Vector3.Dot(transform.up, portalToPlayer);

            if (dotProduct < 0)
            {
                if (tunnelA.getCameraStatus())
                {
                    tunnelA.setCameraStatus(false);
                }
            }
            else
            {
                if(!tunnelA.getCameraStatus() && GeometryUtility.TestPlanesAABB(cameraFrustum, bounds[2]))
                {
                    tunnelA.setCameraStatus(true);
                }else if(tunnelA.getCameraStatus() && !GeometryUtility.TestPlanesAABB(cameraFrustum, bounds[2]))
                {
                    tunnelA.setCameraStatus(false);
                }
            }


            //fake B
            portalToPlayer = player.position - fakeB.transform.position;
            dotProduct = Vector3.Dot(transform.up, portalToPlayer);

            if (dotProduct < 0)
            {
                if (tunnelB.getCameraStatus())
                {
                    tunnelB.setCameraStatus(false);
                }
            }
            else
            {
                if (!tunnelB.getCameraStatus() && GeometryUtility.TestPlanesAABB(cameraFrustum, bounds[3]))
                {
                    tunnelB.setCameraStatus(true);
                }
                else if (tunnelB.getCameraStatus() && !GeometryUtility.TestPlanesAABB(cameraFrustum, bounds[3]))
                {
                    tunnelB.setCameraStatus(false);
                }
            }
        }
        else
        {
            //outside


            //tunnel A

            portalToPlayer = player.position - tunnelA.getPosition();
            dotProduct = Vector3.Dot(this.tunnelA.getLocalUp(), portalToPlayer);
            if (dotProduct < 0)
            {
               this.tunnelB.setCameraStatus(false);
               this.fakeA.setCameraStatus(false);
            }
            else
            {
                if (!fakeA.getCameraStatus() && GeometryUtility.TestPlanesAABB(cameraFrustum, bounds[0]))
                {
                    this.tunnelB.setCameraStatus(true);
                    fakeA.setCameraStatus(true);
                }
                else if (fakeA.getCameraStatus() && !GeometryUtility.TestPlanesAABB(cameraFrustum, bounds[0]))
                {
                    this.tunnelB.setCameraStatus(false);
                    this.fakeA.setCameraStatus(false);
                }
            }




            //tunnelB
            portalToPlayer = player.position - tunnelB.getPosition();
            dotProduct = Vector3.Dot(this.tunnelB.getLocalUp(), portalToPlayer);
            if (dotProduct < 0)
            {
                this.tunnelA.setCameraStatus(false);
                this.fakeB.setCameraStatus(false);
            }
            else
            {
                if (!fakeB.getCameraStatus() && GeometryUtility.TestPlanesAABB(cameraFrustum, bounds[1]))
                {
                    this.tunnelA.setCameraStatus(true);
                    fakeB.setCameraStatus(true);

                }
                else if (fakeB.getCameraStatus() && !GeometryUtility.TestPlanesAABB(cameraFrustum, bounds[1]))
                {
                    this.tunnelA.setCameraStatus(false);
                    this.fakeB.setCameraStatus(false);

                }
            }
           
        }
    }
   

    public void checkPlayerPosition(bool offsetChange)
    {

        if (offsetChange)
        {
            this.inside = true;
            this.tunnelA.setUseOffset(false);
            this.tunnelB.setUseOffset(false);
            this.tunnelA.setCameraStatus(true);
            this.tunnelB.setCameraStatus(true);
            this.fakeA.setCameraStatus(false);
            this.fakeB.setCameraStatus(false);
        }
        else {
            this.inside = false;
            this.tunnelB.setUseOffset(true);
            this.tunnelA.setUseOffset(true);
        }
        
    }

}
