using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private GameObject sentClick;
    [SerializeField]
    private GameObject Icon;
    private bool playerIn = false;
    private Transform player;
    private Camera mainCamera;
    private Plane[] cameraFrustum;
    private Bounds bounds;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        this.audioSource = GetComponent<AudioSource>();
        this.player = GameObject.FindGameObjectWithTag("Player").transform;
        mainCamera = Camera.main;
        bounds = GetComponent<Collider>().bounds;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0) return;
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
            }
            else
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
        if (Input.GetKeyDown(KeyCode.E) && this.playerIn && this.Icon.activeSelf)
        {
            this.animator.SetTrigger("Press");
            this.sentClick.GetComponent<Wall>().changeState();
            this.audioSource.Play();
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
