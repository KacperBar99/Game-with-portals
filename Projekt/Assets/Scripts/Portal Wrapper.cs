using System.Collections;
using System.Collections.Generic;
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
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setOffset(Vector3 offset)
    {
        this._camera.setOffset(offset);
    }
}
