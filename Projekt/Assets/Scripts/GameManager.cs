using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField,Tooltip("Quick test option")]
    private bool maxFPS;
    // Start is called before the first frame update
    void Start()
    {
        if (maxFPS)
        {
            Application.targetFrameRate = -1;
            QualitySettings.vSyncCount = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
