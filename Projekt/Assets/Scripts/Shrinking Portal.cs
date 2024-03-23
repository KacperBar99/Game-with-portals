using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkingPortal : MonoBehaviour
{

    [SerializeField]
    private Portal tunnelA;
    [SerializeField]
    private Portal tunnelB;
    [SerializeField]
    private Portal fakeA;
    [SerializeField]
    private Portal fakeB;


    private float entryScale;
    private float exitScale;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

    }

}
