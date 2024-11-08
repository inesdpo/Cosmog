using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
   //[SerializeField] private GameObject[] items;// all the items in the inventory

   [SerializeField] private GameObject itemToActivate; //items in inventory that need to become activated
   [SerializeField] private GameObject itemToDeactivate; //items in the game that need to be deactivated

   public void Update()
   {
        // Check if the left mouse button (button index 0) was pressed this frame
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit raycastHit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out raycastHit, 100f))
            {
                if (raycastHit.transform != null)
                { 
                    CurrentClickedGameObject(raycastHit.transform.gameObject);
                }
            }
            
        }
   }

    public void TriggerEvent()
    {
        // what happens on mouse click
        itemToActivate.SetActive(true);
        itemToDeactivate.SetActive(false);
    }

    public void CurrentClickedGameObject(GameObject gameObject)
    {
        if (gameObject.tag == "item")
        {
            // Call the trigger function
            TriggerEvent();
        }
    }
}
