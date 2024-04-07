using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PortalCamera : MonoBehaviour
{
    [SerializeField,Tooltip("Setting to copy main Camera setting instead of using already set ones.")]
    private bool copyCamera;
    [SerializeField,Tooltip("Which culling layer should not be visible to this camera.")]
    private string cullingSkip;
    [SerializeField]
    private Renderer renderPlane;

    [SerializeField]
    private Vector3 CameraOffset;
    [SerializeField]
    private bool useOffset;

   
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
            this.objectCamera.cullingMask &= ~(1 << LayerMask.NameToLayer("UI"));
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
        if (useOffset)
        {
            this.transform.position = CameraOffset+portal.position+playerOffsetFromPortal;
        }
        else
        {
            transform.position = portal.position + playerOffsetFromPortal;
        }
        

       

        float angularDifference = Quaternion.Angle(portal.rotation, Bportal.rotation);

        Quaternion portalRotation = Quaternion.AngleAxis(angularDifference, Vector3.up);
        Vector3 newCameraDirection = portalRotation * playerCamera.forward;
        transform.rotation = Quaternion.LookRotation(newCameraDirection, Vector3.up);
    }

    public void setOffset(Vector3 offset)
    {
        this.useOffset = true;
        this.CameraOffset = offset;
    }
    public void setUseOffset(bool useOffset)
    {
        this.useOffset = useOffset;
    }
    public void setCameraStatus(bool status)
    {
        if (status)
        {
            this.objectCamera.enabled = true;
        }
        else
        {
            this.objectCamera.enabled = false;
        }
    }
    public bool getCameraStatus()
    {
        return this.objectCamera.enabled;
    }
}
