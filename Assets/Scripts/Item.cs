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
        InventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            int leftOverItems = InventoryManager.AddItem(itemName, quantity, sprite, itemDescription);
            if (leftOverItems <= 0) 
                Destroy(gameObject);
            else
                quantity = leftOverItems;
        }
    }
}
