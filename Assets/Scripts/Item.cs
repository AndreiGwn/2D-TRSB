using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemName;
    public int quantity;
    public Sprite sprite;
    [TextArea]
    public string itemDescription;

    private InventoryManager InventoryManager;

    void Start()
    {
        // Find the InventoryManager in the scene
        InventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"Collided with {collision.gameObject.name}");

        // If the player collides with the item, attempt to pick it up
        if (collision.gameObject.CompareTag("Player"))
        {
            int leftOverItems = InventoryManager.AddItem(itemName, quantity, sprite, itemDescription);
            if (leftOverItems <= 0)
            {
                Debug.Log("Item picked up: " + itemName);
                Destroy(gameObject); // Remove item from the world
            }
            else
            {
                Debug.Log("Items left over: " + leftOverItems);
                quantity = leftOverItems; // Update remaining quantity
            }
        }
    }
}
