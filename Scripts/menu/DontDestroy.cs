using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestr : MonoBehaviour
{
    void Start()
    {
        print(GameObject.FindGameObjectsWithTag("Audio").Length);
        if (GameObject.FindGameObjectsWithTag("Audio").Length <= 1) {
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
        
    }
}
