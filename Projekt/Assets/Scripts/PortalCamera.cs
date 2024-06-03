using System.Collections.Generic;
using UnityEngine;

public class PortalCamera : MonoBehaviour
{
    [SerializeField,Tooltip("Setting to copy main Camera setting instead of using already set ones.")]
    private bool setCamera=true;
    [SerializeField,Tooltip("Which culling layer should not be visible to this camera.")]
    private List<string> cullingSkip;
    [SerializeField]
    private MeshRenderer renderPlane;
    [SerializeField]
    private Vector3 CameraOffset;
    [SerializeField]
    private bool useOffset;

   
    private Camera objectCamera;
    private Transform playerCamera;
    public Transform portal;
    public Transform Bportal;

    private void Start()
    {
        this.playerCamera = GameObject.FindGameObjectWithTag("MainCamera").transform;
        objectCamera = GetComponent<Camera>();

        if (setCamera && objectCamera != null)
        {
            this.objectCamera.CopyFrom(Camera.main);
            foreach (string skip in cullingSkip)
            {
                this.objectCamera.cullingMask &= ~(1 << LayerMask.NameToLayer(skip));
            }
            this.objectCamera.cullingMask &= ~(1 << LayerMask.NameToLayer("UI"));
        }

        objectCamera.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        renderPlane.material.SetTexture("_Texture", objectCamera.targetTexture);
    }


    void LateUpdate()
    {
        float angularDifference = -Vector3.SignedAngle(this.portal.forward,this.Bportal.forward,this.transform.up);

        Quaternion portalRotation = Quaternion.AngleAxis(angularDifference, Vector3.up);
        Vector3 newCameraDirection = portalRotation * this.playerCamera.forward;
        this.transform.rotation = Quaternion.LookRotation(newCameraDirection, Vector3.up);

        Vector3 playerOffsetFromPortal = this.playerCamera.position - this.Bportal.position;
        transform.position = this.portal.position + Quaternion.Euler(0f, angularDifference, 0f) * playerOffsetFromPortal;

        if (useOffset)
        {
            this.transform.position += this.CameraOffset;
        }
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
            if(this.objectCamera)
            this.objectCamera.enabled = true;
        }
        else
        {
            if(this.objectCamera)
            this.objectCamera.enabled = false;
        }
    }
    public bool getCameraStatus()
    {
        return this.objectCamera.enabled;
    }
}
