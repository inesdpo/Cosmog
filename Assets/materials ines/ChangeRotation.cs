using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRotation : MonoBehaviour
{
    [SerializeField] private float targetYRotation = 100f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Rotation();
    }

    public void Rotation()
    {
        if(Input.GetKey(KeyCode.C))
        {
            Vector3 currentRotation = transform.eulerAngles;

            currentRotation.y = targetYRotation;

            transform.eulerAngles = currentRotation;
        } 
    }
}
