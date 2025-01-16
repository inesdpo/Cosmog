using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VRTemplate;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Item : MonoBehaviour
{
    [SerializeField] private string itemName;
    [SerializeField] private bool hasBeenFound;


    public string GetItemName() { return itemName; }
    public bool GetHasBeenFound() { return hasBeenFound; }
    public void SetHasBeenFound(bool hasItBeenFound) { hasBeenFound = hasItBeenFound; }

    private ItemManager itemManager;

    private XRGrabInteractable grabInteractable;

    void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();

        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnGrab);
            grabInteractable.selectExited.AddListener(OnRelease);
        }
    }

    private void OnDestroy()
    {
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.RemoveListener(OnGrab);
            grabInteractable.selectExited.RemoveListener(OnRelease);
        }
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        Debug.Log("Object grabbed!");
        Debug.Log("Click!");
        itemManager.ItemClick(this.gameObject, this);
    }
    private void OnRelease(SelectExitEventArgs args)
    {
        Debug.Log("Object released!");
        
    }

    public void SetItemManager(ItemManager newItemManager)
    {
        itemManager = newItemManager;
    }

    /*public void OnMouseUp()
    {
        Debug.Log("Click!");
        itemManager.ItemClick(this.gameObject, this);
    }*/


}
