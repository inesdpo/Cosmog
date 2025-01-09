using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OpenComputer : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Animator computerAnim;
    [SerializeField] private GameObject item;

    private bool playerInCollider = false;
    [SerializeField] private InputActionProperty triggerAction;

    // Start is called before the first frame update
    void Start()
    {
        computerAnim = GetComponent<Animator>();
        triggerAction.action.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if (triggerAction.action.WasPressedThisFrame() && (playerInCollider == true))
        {
            computerAnim.gameObject.GetComponent<Animator>().enabled = true;
            computerAnim.Play("Computer");
            StartCoroutine(ActivateItemAfterDelay(0.1f));
        }
    }

    private IEnumerator ActivateItemAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); 
        item.SetActive(true); 
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInCollider = true;
        }
    }
}
