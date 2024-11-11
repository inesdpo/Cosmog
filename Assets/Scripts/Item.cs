using System.Collections;
using System.Collections.Generic;
using Unity.VRTemplate;
using UnityEditor.Search;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private string itemName;
    [SerializeField] private bool hasBeenFound;


    public string GetItemName() { return itemName; }
    public bool GetHasBeenFound() { return hasBeenFound; }
    public void SetHasBeenFound(bool hasItBeenFound) { hasBeenFound = hasItBeenFound; }

    private ItemManager itemManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetItemManager(ItemManager newItemManager)
    {
        itemManager = newItemManager;
    }

    public void OnMouseUp()
    {
        Debug.Log("Click!");
        itemManager.ItemClick(this.gameObject, this);
    }


}
