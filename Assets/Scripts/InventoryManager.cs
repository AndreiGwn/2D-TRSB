using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventoryMenu; // Reference to the inventory UI menu
    private bool menuActivated; // Tracks whether the inventory menu is opem
    private KeyCode inventoryKey = KeyCode.I; // Key used to toggle inventory
    public ItemSlot[] itemSlot; // Array of item slots in the inventory

    public ItemSO[] itemSOs; // Array of item scriptable objects

    void Update()
    {
        // Toggle inventory menu when pressing the inventory key
        if (Input.GetKeyDown(inventoryKey) && menuActivated)
        {
            Time.timeScale = 1; // Resume game time
            inventoryMenu.SetActive(false); // Hide inventory
            menuActivated = false;
        }

        else if (Input.GetKeyDown(inventoryKey) && !menuActivated)
        {
            Time.timeScale = 0; // Pause game time
            inventoryMenu.SetActive(true); // Show inventory
            menuActivated = true;
        }
    }

    // Attempts to use an item by its name
    public bool UseItem(string itemName)
    {
        for (int i = 0; i < itemSOs.Length; i++)
        {
            if (itemSOs[i].itemName == itemName)
            {
                bool usable = itemSOs[i].UseItem();
                return usable;
            }
        }

        return false;
    }

    // Adds an item to the inventory and returns leftover quantity if slots are full
    public int AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription)
    {
        // First, try adding to an existing stack of the same item
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (itemSlot[i].itemName == itemName && itemSlot[i].itemDescription == itemDescription && !itemSlot[i].isFull)
            {
                int leftOverItems = itemSlot[i].AddItem(itemName, quantity, itemSprite, itemDescription);
                return leftOverItems;
            }
        }

        // If no stack eists, add it to an empty slot
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (itemSlot[i].isFull == false && string.IsNullOrEmpty(itemSlot[i].itemName))
            {
                int leftOverItems = itemSlot[i].AddItem(itemName, quantity, itemSprite, itemDescription);
                return leftOverItems;
            }
        }

        return quantity; // If inventory is full, return the remaining quantity
    }

    // Deselects all item slots in the inventory UI
    public void DeselectAllSlots()
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            itemSlot[i].selectedShader.SetActive(false);
            itemSlot[i].thisItemSelected = false;
        }
    }
}
