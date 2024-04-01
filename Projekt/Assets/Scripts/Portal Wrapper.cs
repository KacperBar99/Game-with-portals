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

    private ShrinkingPortal wrapper;

    
    // Start is called before the first frame update
    void Start()
    {
        this.wrapper = this.gameObject.GetComponentInParent<ShrinkingPortal>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setOffset(Vector3 offset)
    {
        this._camera.setOffset(offset);
    }

    public void setEvent()
    {
        string[] ret = { name, this.gameObject.transform.parent.name };
        this.wrapper.checkPlayerPosition(ret);
    }

    public void setUseOffset(bool useOffset)
    {
        this._camera.setUseOffset(useOffset);
    }
}
