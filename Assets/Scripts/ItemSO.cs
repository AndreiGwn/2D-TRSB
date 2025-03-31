using System;
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
        // Check if the item affects a stat
        if (statToChange != StatToChange.none)
        {
            // Apply the stat change
            ApplyStatChange();
            return true;
        }
        // Check if the item affects an attribute
        if (attributeToChange != AttributeToChange.none)
        {
            // Apply the attribute change
            ApplyAttributeChange();
            return true;
        }
        return false;
    }

    private void ApplyStatChange()
    {
        Debug.Log($"Modifying {statToChange} by  {amountToChangeStat}");
    }

    private void ApplyAttributeChange()
    {
        Debug.Log($"Modifying {attributeToChange} by  {amountToChangeAttribute}");
    }

    // Enumeration of possible stats an item can affect
    public enum StatToChange
    {
        none,
        health,
    };

    // Enumeration of possible attributes an item can affect
    public enum AttributeToChange
    {
        none,
        strength,
        defense,
        stealth,
        agility
    };
}
