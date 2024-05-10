using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Renderingcontrol : MonoBehaviour
{
    [SerializeField, Tooltip("Camera that will be disabled")]
    private Camera cameraToDisable;
    private Transform player;
    private Camera mainCamera;
    private Plane[] cameraFrustum;
    private Bounds bounds;




    private void Awake()
    {
        this.player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        bounds = GetComponent<Collider>().bounds;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        Vector3 portalToPlayer = player.position - transform.position;
        float dotProduct = Vector3.Dot(transform.up, portalToPlayer);

        if (dotProduct < 0 || this.isAboveOrBelow())
        {
            if (cameraToDisable.enabled)
            {
                this.cameraToDisable.enabled = false;
            }
        }
        else
        {
            cameraFrustum = GeometryUtility.CalculateFrustumPlanes(mainCamera);

            if (!cameraToDisable.enabled && GeometryUtility.TestPlanesAABB(cameraFrustum, bounds))
            {
                this.cameraToDisable.enabled = true;
            }
            else if (cameraToDisable.enabled && (!GeometryUtility.TestPlanesAABB(cameraFrustum, bounds)))
            {
                this.cameraToDisable.enabled = false;
            }
        }
    }
    private bool isAboveOrBelow()
    {
        if (Mathf.Abs(this.transform.position.y - this.player.position.y) >= 50) return true;
        else return false;
    }
}
