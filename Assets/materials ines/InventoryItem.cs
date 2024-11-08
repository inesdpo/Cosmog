using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    [SerializeField] private string itemName;
    [SerializeField] private GameObject inventoryItemGameObject;

    public void ToggleInventoryItem(bool toggle)
    {
        inventoryItemGameObject.SetActive(toggle);
    }

    public string GetItemName()
    {
        return itemName;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
