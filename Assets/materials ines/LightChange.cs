using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightChange : MonoBehaviour
{
    [SerializeField] private float secondaryState;
    [SerializeField] private GameObject villain;

    Light myLight;

    void Start()
    {
        myLight = GetComponent<Light>();
    }

    void Update()
    {
        if (!villain.activeInHierarchy)
        {
            ChangeState();
        }
        
    }

    public void ChangeState()
    {
        myLight.intensity = secondaryState;
    }
}
