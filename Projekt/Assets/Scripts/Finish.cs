using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            statics.Finished = true;
            SceneManager.LoadScene("Menu", LoadSceneMode.Single);
        }
    }
}
