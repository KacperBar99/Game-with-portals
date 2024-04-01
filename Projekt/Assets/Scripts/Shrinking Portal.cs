using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkingPortal : MonoBehaviour
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


    private float entryScale;
    private float exitScale;
    [SerializeField]
    private Vector3 offset;


    // Start is called before the first frame update
    void Start()
    {
        Vector3 difference = this.fake.position - this.real.position;
        this.tunnelB.setOffset(difference);
    }

    // Update is called once per frame
    void Update()
    {

    }

}
