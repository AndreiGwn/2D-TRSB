using UnityEngine;

// Scriptable object for defining item properties and effects
[CreateAssetMenu(fileName = "ItemSO", menuName = "Scriptable Objects/ItemSO")]
public class ItemSO : ScriptableObject
{
    public string itemName; // Name of the item
    public StatToChange statToChange = new StatToChange(); // Stat affected by this item
    public int amountToChangeStat; // Amount by which the stat changes

    public AttributeToChange attributeToChange = new AttributeToChange(); // Attribute affected by this item
    public int amountToChangeAttribute; // Amount by which the attribute changes

    // Uses the item and applies its effect
    public bool UseItem()
    {
        if (statToChange == StatToChange.health)
        {
            PlayerHealth playerHealth = GameObject.Find("HealthManager").GetComponent<PlayerHealth>();
            if (playerHealth.health == playerHealth.maxHealth)
            {
                return false; // Can't use if at max health
            }
            else
            {
                playerHealth.ChangeHealth(amountToChangeStat);
                return true;
            }
        }
        if (statToChange == StatToChange.mana)
        {
            PlayerMana playerMana = GameObject.Find("ManaManager").GetComponent<PlayerMana>();
            if (playerMana.mana == playerMana.maxMana)
            {
                return false; // Can't use if at max mana
            }
            else
            {
                playerMana.ChangeMana(amountToChangeStat);
                return true;
            }
        }
        return false;
    }

    // Enumeration of possible stats an item can affect
    public enum StatToChange
    {
        none,
        health,
        mana,
        stamina
    };

    // Enumeration of possible attributes an item can affect
    public enum AttributeToChange
    {
        none,
        strength,
        defense,
        intelligence,
        agility
    };
}
