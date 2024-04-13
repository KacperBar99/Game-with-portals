using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Renderingcontrol : MonoBehaviour
{

    private Transform player;
    [SerializeField,Tooltip("Camera that will be disabled")]
    private Camera cameraToDisable;
    private Camera mainCamera;
    private Plane[] cameraFrustum;
    private Bounds bounds;


    public bool showdot=false;


    private void Awake()
    {
        this.player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        bounds = GetComponent<Collider>().bounds;
        cameraToDisable.enabled = false;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        Vector3 portalToPlayer = player.position - transform.position;
        float dotProduct = Vector3.Dot(transform.up, portalToPlayer);

        cameraFrustum = GeometryUtility.CalculateFrustumPlanes(mainCamera);

        if(showdot)Debug.Log(dotProduct);
        if (dotProduct < 0)
        {
            if (cameraToDisable.enabled)
            {
                cameraToDisable.enabled = false;
            }
        }
        else
        {
            if (!cameraToDisable.enabled && GeometryUtility.TestPlanesAABB(cameraFrustum, bounds))
            {
                cameraToDisable.enabled = true;
            }
            else if (cameraToDisable.enabled && !GeometryUtility.TestPlanesAABB(cameraFrustum, bounds))
            {
                cameraToDisable.enabled = false;
            }
        }
    }
}
