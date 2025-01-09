using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewDoor : MonoBehaviour
{
    [SerializeField] private float finalYRotation;
    private float initialYRotation;
    [SerializeField] private GameObject player;
    private bool isRotated = false;
    [SerializeField] private bool playerInCollider = false;
    [SerializeField] private AudioSource opening;
    [SerializeField] private AudioSource closing;
    [SerializeField] private InputActionProperty triggerAction;

    // Start is called before the first frame update
    void Start()
    {
        initialYRotation = transform.eulerAngles.y;
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
                RotationY(initialYRotation);
                opening.gameObject.GetComponent<AudioSource>().enabled = false;
                closing.gameObject.GetComponent<AudioSource>().enabled = true;
                closing.Play();
            }
            else
            {
                RotationY(finalYRotation);
                closing.gameObject.GetComponent<AudioSource>().enabled = false;
                opening.gameObject.GetComponent<AudioSource>().enabled = true;
                opening.Play();
            }
            isRotated = !isRotated;
        }
    }

    public void RotationY(float yRotation)
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
