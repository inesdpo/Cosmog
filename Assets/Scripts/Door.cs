using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private float finalZRotation;

    private float initialZRotation;
    [SerializeField] private GameObject player;
    private bool isRotated = false;
    [SerializeField] private bool playerInCollider = false;
    [SerializeField] private AudioSource opening;
    [SerializeField] private AudioSource closing;

    // Start is called before the first frame update
    void Start()
    {
        initialZRotation = transform.eulerAngles.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && (playerInCollider == true))
        {
            if (isRotated)
            {
                Rotation(initialZRotation);
                opening.gameObject.GetComponent<AudioSource>().enabled = false;
                closing.gameObject.GetComponent<AudioSource>().enabled = true;
                closing.Play();
            }
            else
            {
                Rotation(finalZRotation);
                closing.gameObject.GetComponent<AudioSource>().enabled = false;
                opening.gameObject.GetComponent<AudioSource>().enabled = true;
                opening.Play();
            }
            isRotated = !isRotated;


        }

      
    }

    public void Rotation(float zRotation)
    {
    Vector3 currentRotation = transform.eulerAngles;

    currentRotation.z = zRotation;

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
