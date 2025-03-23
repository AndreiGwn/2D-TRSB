using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    // Item data for this particular slot
    [Header("Item Data")]
    public string itemName; // Name of the item in the slot
    public int quantity; // Quantity of the item in the slot
    public Sprite itemSprite; // Sprite representing the item in the slot
    public bool isFull; // Flag to check if the slot is full (cannot hold more items)
    public string itemDescription; // Description of the item
    public Sprite emptySprite; // Sprite to represent an empty slot

    [SerializeField] private int maxNumberOfItems; // Maximum nmber of items allowed in the slot

    // UI elements for displaying the item in the slot
    [Header("Item Slot UI Elements")]
    [SerializeField] private TMP_Text quantityText; // Text component to show the quantity of items
    [SerializeField] private Image itemImage; // Image component to display the item sprite

    // UI elements for displaying the item description when the item is selected
    [Header("Item Description Slot")]
    public Image itemDescriptionImage; // Image component for the item's description image
    public TMP_Text ItemDescriptionNameText; // Text component to show the item name in the description UI
    public TMP_Text ItemDescriptionText; // Text component to show the item description in the UI

    // Shader and selection state for visual selection of the item slot
    public GameObject selectedShader; // Shader effect that highlights the selected item slot
    public bool thisItemSelected; // Flag to check if this item is currently selected

    // Reference to the inventory manager for managing the inventory system
    private InventoryManager inventoryManager;

    void Start()
    {
        // Fetch the InventoryManager from the scene to manage item interactions
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
    }

    // Adds an item to the slot, returning any leftover quantity if the max limit is exceeded
    public int AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription)
    {
        // If the slot is already full, return the quantity unchanged
        if (isFull)
            return quantity;

        // Assign the item data to this slot
        this.itemName = itemName;
        this.itemSprite = itemSprite;
        itemImage.sprite = itemSprite; // Update the slot image with the item's sprite
        this.itemDescription = itemDescription;

        // Add the quantity of items to this slot
        this.quantity += quantity;

        // Check if the new quantity exceeds the maximum number of items allowed
        if (this.quantity > +maxNumberOfItems)
        {
            // Display the updated quantity in the slot's text
            quantityText.text = quantity.ToString();
            quantityText.enabled = true; // Enable quantity text visibility

            // Mark the slot as full
            isFull = true;

            // Calculate the leftover items that couldn't fit into the slot
            int extraItems = this.quantity - maxNumberOfItems;

            // Set the quantity to the max limit
            this.quantity = maxNumberOfItems;

            // Return the leftover items
            return extraItems;
        }

        // Update the slot with the current item quantity
        quantityText.text = this.quantity.ToString();
        quantityText.enabled = true; // Ensure the quantity text is visible

        // No leftover items, so return 0
        return 0;
    }

    // Handles user interaction with the item slot when it's clicked (left or right click)
    public void OnPointerClick(PointerEventData eventData)
    {
        // Check if the left mouse button was clicked
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            // Call the method for left-click interactions
            OnLeftClick();
        }

        // Check if the right mouse button was clicked
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            // Call the method for right-click interactions
            OnRightClick();
        }
    }

    // Handles the behavior when the player left-clicks on the item slot
    public void OnLeftClick()
    {
        // If the item is already selected, attempt to use the item
        if (thisItemSelected)
        {
            // Try using the item via the inventory manager
            bool usable = inventoryManager.UseItem(itemName);
            if (usable)
            {
                // If the item was used successfully, decrement the quantity
                this.quantity -= 1;
                quantityText.text = this.quantity.ToString();

                // If no items are left, empty the slot
                if (this.quantity <= 0)
                    EmptySlot();
            }
        }
        else
        {
            // If the item is not selected, select it and show its description
            inventoryManager.DeselectAllSlots(); // Deselect all previously selected slots
            selectedShader.SetActive(true); // Highlight the current item slot
            thisItemSelected = true; // Mark this item as selected

            // Display item details in the description UI
            ItemDescriptionNameText.text = itemName;
            ItemDescriptionText.text = itemDescription;
            itemDescriptionImage.sprite = itemSprite;

            // If the sprite is null, set it to an empty sprite
            if (itemDescriptionImage.sprite == null)
                itemDescriptionImage.sprite = emptySprite;
        }
    }

    // Clears the item from the slot when it's used up or dropped
    private void EmptySlot()
    {
        quantityText.enabled = false; // Hide the quantity text
        itemImage.sprite = emptySprite; // Set the item sprite to the empty sprite

        // Clear the item description UI
        ItemDescriptionNameText.text = "";
        ItemDescriptionText.text = "";
        itemDescriptionImage.sprite = emptySprite;
    }

    // Handles the behavior when the player right-clicks on the item slot (to drop the item)
    public void OnRightClick()
    {
        // Create a new GameObject for the dropped item
        GameObject itemToDrop = new GameObject(itemName);
        Item newItem = itemToDrop.AddComponent<Item>(); // Add the Item component to the new GameObject
        newItem.quantity = 1; // Set the quantity of the dropped item to 1
        newItem.itemName = itemName;
        newItem.sprite = itemSprite;
        newItem.itemDescription = itemDescription;

        // Add and configure a SpriteRenderer for the dropped item
        SpriteRenderer sr = itemToDrop.AddComponent<SpriteRenderer>();
        sr.sprite = itemSprite;
        sr.sortingOrder = 5; // Ensure the dropped item is rendered above other objects
        sr.sortingLayerName = "Ground"; // Set the layer to the ground

        // Add a BoxCollider2D so the item cna interact with the physics engine
        BoxCollider2D collider = itemToDrop.AddComponent<BoxCollider2D>();

        // Add a Rigidbody2D to the item so it can fall with gravity
        Rigidbody2D rb = itemToDrop.AddComponent<Rigidbody2D>();
        rb.gravityScale = 1; // Enable gravity for the dropped item
        rb.freezeRotation = true; // Prevent the item from rotating

        // Set the position where the item will be dropped near the player
        Vector3 spawnPosition = GameObject.FindWithTag("Player").transform.position + new Vector3(1, 1, 0);
        itemToDrop.transform.position = spawnPosition;

        // Scale the item to be larger when dropped
        itemToDrop.transform.localScale = new Vector3(2f, 2f, 2f);

        // Decrease the quantity of the item in the slot
        this.quantity -= 1;
        quantityText.text = this.quantity.ToString();

        // If no items are left, empty the slot
        if (this.quantity <= 0)
            EmptySlot();
    }
}
