using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PortalWrapper : MonoBehaviour
{
    [SerializeField]
    private PortalCamera _camera;
    [SerializeField]
    private GameObject renderPlane;
    [SerializeField]
    private Portal colliderPlane;
    [SerializeField]
    private GameObject frame;

    private TunnelPortal wrapper;


    // Start is called before the first frame update
    void Start()
    {
        this.wrapper = this.gameObject.GetComponentInParent<TunnelPortal>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setOffset(Vector3 offset)
    {
        this._camera.setOffset(offset);
    }

    public void setEvent(bool offsetChgange)
    {
        this.wrapper.checkPlayerPosition(offsetChgange);
    }

    public void setUseOffset(bool useOffset)
    {
        this._camera.setUseOffset(useOffset);
    }
    public void setCameraStatus(bool status)
    {
        this._camera.setCameraStatus(status);
    }
    public bool getCameraStatus()
    {
        return this._camera.getCameraStatus();
    }
    public Portal GetPortal()
    {
        return this.colliderPlane;
    }
    public Vector3 getPosition()
    {
        return this.colliderPlane.getPosition();
    }
    public Vector3 getLocalUp()
    {
        return this.colliderPlane.getLocalUp();
    }
}
