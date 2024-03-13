using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCamera : MonoBehaviour
{
    [SerializeField,Tooltip("Setting to copy main Camera setting instead of using already set ones.")]
    private bool copyCamera;
    [SerializeField,Tooltip("Which culling layer should not be visible to this camera.")]
    private string cullingSkip;
    [SerializeField]
    private Renderer renderPlane;

   
    private Camera objectCamera;
    private Transform playerCamera;
    public Transform portal;
    public Transform Bportal;

    private void Awake()
    {
        this.playerCamera = GameObject.FindGameObjectWithTag("MainCamera").transform;
        objectCamera = GetComponent<Camera>();
        
        if (copyCamera && objectCamera != null)
        {
            this.objectCamera.CopyFrom(Camera.main);
            this.objectCamera.cullingMask &= ~(1 << LayerMask.NameToLayer(cullingSkip));
        }
    }

    private void Start()
    {
        objectCamera.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        renderPlane.material.mainTexture=objectCamera.targetTexture;
    }


    void LateUpdate()
    {
        Vector3 playerOffsetFromPortal = playerCamera.position - Bportal.position;
        transform.position = portal.position + playerOffsetFromPortal;
        
        

        float angularDifference = Quaternion.Angle(portal.rotation, Bportal.rotation);

        Quaternion portalRotation = Quaternion.AngleAxis(angularDifference, Vector3.up);
        Vector3 newCameraDirection = portalRotation * playerCamera.forward;
        transform.rotation = Quaternion.LookRotation(newCameraDirection, Vector3.up);
    }
}
