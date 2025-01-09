using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Door : MonoBehaviour
{
    [SerializeField] private float finalZRotation;
    private float initialZRotation;
    [SerializeField] private float finalXRotation;
    private float initialXRotation;
    [SerializeField] private GameObject player;
    private bool isRotated = false;
    [SerializeField] private bool playerInCollider = false;
    [SerializeField] private AudioSource opening;
    [SerializeField] private AudioSource closing;
    [SerializeField] private InputActionProperty triggerAction;

    // Start is called before the first frame update
    void Start()
    {
        initialZRotation = transform.eulerAngles.z;
        initialXRotation = transform.eulerAngles.x;
        triggerAction.action.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if (triggerAction.action.WasPressedThisFrame() && playerInCollider)
        {
            Debug.Log("Trigger pressed!");
            if (isRotated)
            {
                RotationZ(initialZRotation);
                RotationX(initialXRotation);
                opening.gameObject.GetComponent<AudioSource>().enabled = false;
                closing.gameObject.GetComponent<AudioSource>().enabled = true;
                closing.Play();
            }
            else
            {
                RotationZ(finalZRotation);
                RotationX(finalXRotation);
                closing.gameObject.GetComponent<AudioSource>().enabled = false;
                opening.gameObject.GetComponent<AudioSource>().enabled = true;
                opening.Play();
            }
            isRotated = !isRotated;
        }
    }

    public void RotationZ(float zRotation)
    {
        Vector3 currentRotation = transform.eulerAngles;
        currentRotation.z = zRotation;
        transform.eulerAngles = currentRotation;
    }

    public void RotationX(float xRotation)
    {
        Vector3 currentRotation = transform.eulerAngles;
        currentRotation.x = xRotation;
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
