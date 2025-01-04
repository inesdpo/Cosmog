using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenComputer : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Animator computerAnim;

    private bool playerInCollider = false;

    // Start is called before the first frame update
    void Start()
    {
        computerAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.C) && (playerInCollider == true))
        {
            computerAnim.gameObject.GetComponent<Animator>().enabled = true;
            computerAnim.Play("Computer");
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
