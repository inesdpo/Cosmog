using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRotation : MonoBehaviour
{
    [SerializeField] private float finalYRotation;

    private float initialYRotation;     
    private bool isRotated = false;
    [SerializeField] private bool playerInCollider=false;

    // Start is called before the first frame update
    void Start()
    {
        initialYRotation = transform.eulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.C) && (playerInCollider == true))
        {
            if (isRotated)
            {
                Rotation(initialYRotation);
            }
            else
            {
                Rotation(finalYRotation);
            }
            isRotated = !isRotated;


        }
    }

    public void Rotation(float yRotation)
    {
        Vector3 currentRotation = transform.eulerAngles;

        currentRotation.y = yRotation;

        transform.eulerAngles = currentRotation;
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInCollider = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInCollider = false;
        }
    }
}
