using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ItemManager : MonoBehaviour
{
    [SerializeField] private List<Item> items;
    [SerializeField] private List<InventoryItem> inventoryItems;
    [SerializeField] private GameObject objectToActivate;
 

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] itemGameObjects = GameObject.FindGameObjectsWithTag("item");

        foreach (GameObject itemGameObject in itemGameObjects)
        {
            Item item = itemGameObject.GetComponent<Item>();
            item.SetItemManager(this);
            items.Add(item);
        }


        GameObject[] inventoryItemGameObjects = GameObject.FindGameObjectsWithTag("inventoryItem");

        foreach (GameObject itemGameObject in inventoryItemGameObjects)
        {
            InventoryItem inventoryItem = itemGameObject.GetComponent<InventoryItem>();
            inventoryItems.Add(inventoryItem);
        }

        Cursor.lockState = CursorLockMode.Locked;  
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyUp(KeyCode.Space))
        {
            CheckEveryItem();
        }*/

        /*foreach (GameObject item in item)
        {
            // Check if the item is inactive
            if (item == true)
            {
                Debug.Log("EventToBeTriggered");
                itemComponent.TriggerEvent();
                Debug.Log("Event Triggered");
            }
        }*/
        ActivateInventory();
    }

    public List<InventoryItem> GetInventoryItems()
    {
        return inventoryItems;
    }

    public void ItemClick(GameObject clickedGO, Item item)
    {
        Debug.Log("The game object " + clickedGO.name + " was clicked");
        item.SetHasBeenFound(true);

        foreach (InventoryItem inventoryItem in inventoryItems)
        {
            if (item.GetItemName() == inventoryItem.GetItemName())
            {
                inventoryItem.ToggleInventoryItem(true);
                item.gameObject.SetActive(false);
                //objectToActivate.SetActive(true);
         
                break;
            }
        }
    }

    public void ActivateInventory()
    {
        if (Input.GetKeyUp(KeyCode.V))
        {
            
            objectToActivate.SetActive(!objectToActivate.activeSelf);
            
        }
    }

    /*private void CheckEveryItem()
    {
        foreach (Item item in items)
        {
            Debug.Log("The item " + item.GetItemName() + " has been found? " + item.GetHasBeenFound());
        }
    }*/


}
