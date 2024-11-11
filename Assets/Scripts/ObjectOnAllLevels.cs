using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectOnAllLevels : MonoBehaviour
{
    private static ObjectOnAllLevels instance;

    private void Awake()
    {
        // Ensure only one instance of this GameObject exists across scenes
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Make this object persistent across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy any duplicate instances
        }
    }
}
