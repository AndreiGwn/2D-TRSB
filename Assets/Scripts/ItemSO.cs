using UnityEngine;

[CreateAssetMenu(fileName = "ItemSO", menuName = "Scriptable Objects/ItemSO")]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public StatToChange statToChange = new StatToChange();
    public int amountToChangeStat;

    public AttributeToChange attributeToChange = new AttributeToChange();
    public int amountToChangeAttribute;

    public bool UseItem()
    {
        if (statToChange == StatToChange.health)
        {
            PlayerHealth playerHealth = GameObject.Find("HealthManager").GetComponent<PlayerHealth>();
            if (playerHealth.health == playerHealth.maxHealth)
            {
                return false;
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
                return false;
            }
            else
            {
                playerMana.ChangeMana(amountToChangeStat);
                return true;
            }
        }
        return false;
    }

    public enum StatToChange
    {
        none,
        health,
        mana,
        stamina
    };

    public enum AttributeToChange
    {
        none,
        strength,
        defense,
        intelligence,
        agility
    };
}
