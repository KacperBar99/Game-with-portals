using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RenderingControl : MonoBehaviour
{
    [SerializeField, Tooltip("If player vertical position difference to portal is above that, portal will not render.")]
    private float HeightDiffernceThreshold = 50.0f;
    [SerializeField, Tooltip("Camera that will be disabled")]
    private Camera cameraToDisable;
    private Transform player;
    private Camera mainCamera;
    private Plane[] cameraFrustum;
    private Bounds bounds;


    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.FindGameObjectWithTag("Player").transform;
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
            if (this.cameraToDisable.enabled)
            {
                this.cameraToDisable.enabled = false;
            }
        }
        else
        {
            cameraFrustum = GeometryUtility.CalculateFrustumPlanes(mainCamera);
            bool testPlanes = GeometryUtility.TestPlanesAABB(cameraFrustum, this.bounds);

            if (!this.cameraToDisable.enabled && testPlanes)
            {
                this.cameraToDisable.enabled = true;
            }
            else if (cameraToDisable.enabled && !testPlanes)
            {
                this.cameraToDisable.enabled = false;
            }
        }
    }
    private bool isAboveOrBelow()
    {
        if (Mathf.Abs(this.transform.position.y - this.player.position.y) >= this.HeightDiffernceThreshold) return true;
        else return false;
    }
}
