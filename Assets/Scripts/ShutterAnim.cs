using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShutterAnim : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject key;
    [SerializeField] private Animator shutterAnim;
    [SerializeField] private bool playerInCollider=false;

    // Start is called before the first frame update
    void Start()
    {
        
        shutterAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CanOpen();
    }
    public void CanOpen()
    {
        if ((key.activeInHierarchy) && (playerInCollider = true))
        {
            shutterAnim.gameObject.GetComponent<Animator>().enabled = true;
            shutterAnim.Play("Armature|gartentor");
        }

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInCollider = true;
        }
    }


}
