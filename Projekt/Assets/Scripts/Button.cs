using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField]
    private GameObject sentClick;
    [SerializeField]
    private Animator animator;


    private GameObject Icon;
    private bool playerIn = false;
    private Transform player;
    private Camera mainCamera;
    private Plane[] cameraFrustum;
    private Bounds bounds;
    // Start is called before the first frame update
    void Start()
    {
        var objects = GameObject.FindGameObjectsWithTag("UI");
        foreach (var obj in objects)
        {
            if (obj.name == "Click Button") 
            {
                this.Icon = obj;
                break;
            }
        }
        this.Icon.gameObject.SetActive(false);
        this.player = GameObject.FindGameObjectWithTag("Player").transform;
        mainCamera = Camera.main;
        bounds = GetComponent<Collider>().bounds;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerIn)
        {
            Vector3 buttonToPlayer = player.position - transform.position;
            float dotProduct = Vector3.Dot(transform.up, buttonToPlayer);

            
            if(dotProduct < 0)
            {
                if (this.Icon.activeSelf)
                {
                    this.Icon.SetActive(false);
                }
            }else
            {
                cameraFrustum = GeometryUtility.CalculateFrustumPlanes(this.mainCamera);
                if(!this.Icon.activeSelf && GeometryUtility.TestPlanesAABB(this.cameraFrustum,this.bounds)) 
                {
                    this.Icon.SetActive(true);
                }else if (this.Icon.activeSelf && !GeometryUtility.TestPlanesAABB(this.cameraFrustum,this.bounds)) 
                {
                    this.Icon.SetActive(false);
                }
            }
            
        }
        if (Input.GetKeyDown(KeyCode.E) && this.Icon.activeSelf)
        {
            animator.SetTrigger("Press");
            this.sentClick.GetComponent<Wall>().changeState();
        }
    }
    private void OnTriggerEnter(Collider other)
    {     
        this.playerIn = true;
    }
    private void OnTriggerExit(Collider other)
    { 
        this.playerIn = false;
        this.Icon.SetActive(false);
    }
}
