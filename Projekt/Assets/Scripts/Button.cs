using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    private GameObject Icon;
    private bool playerIn = false;
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
    }

    // Update is called once per frame
    void Update()
    {
        if (playerIn)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                animator.SetTrigger("Press");
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        this.Icon?.SetActive(true);
        this.playerIn = true;
    }
    private void OnTriggerExit(Collider other)
    {
        this.Icon?.SetActive(false);
        this.playerIn = false;
    }
}
