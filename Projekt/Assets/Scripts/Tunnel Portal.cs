using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelPortal : MonoBehaviour
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
    [SerializeField]
    private bool alternativeOffset=false;
    [SerializeField, Tooltip("If player vertical position difference to portal is above that, portal will not render.")]
    private float HeightDiffernceThreshold = 50.0f;

    private Transform player;
    private Camera mainCamera;
    private Plane[] cameraFrustum;
    private Bounds[] bounds;
    private Vector3 distanceReal;
    private Vector3 distanceFake;

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

        this.distanceReal = this.tunnelB.transform.position - this.tunnelA.transform.position;
        this.distanceFake = this.fakeB.transform.position - this.fakeA.transform.position;
        Vector3 difference = (this.distanceReal - this.distanceFake)/2.0f;

        differenceA += difference;
        differenceB -= difference;

        if (alternativeOffset)
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

            if (dotProduct < 0 || this.isAboveOrBelow())
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

            if (dotProduct < 0 || this.isAboveOrBelow())
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
            if (dotProduct < 0 || this.isAboveOrBelow())
            {
               this.tunnelB.setCameraStatus(false);
               this.fakeA.setCameraStatus(false);
            }
            else
            {
                if (!fakeA.getCameraStatus() && GeometryUtility.TestPlanesAABB(cameraFrustum, bounds[0]))
                {
                    this.tunnelB.setCameraStatus(true);
                    this.fakeA.setCameraStatus(true);
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
            if (dotProduct < 0 || this.isAboveOrBelow())
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

    private bool isAboveOrBelow()
    {
        if (this.inside)
        {
            if (Mathf.Abs(this.fake.position.y - this.player.position.y) >= this.HeightDiffernceThreshold) return true;
            else return false;
        }
        else
        {
            if (Mathf.Abs(this.real.position.y - this.player.position.y) >= this.HeightDiffernceThreshold) return true;
            else return false;
        }
        
    }

}
