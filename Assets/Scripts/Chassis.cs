using UnityEngine;

[CreateAssetMenu(fileName = "NewChassis", menuName = "Scriptable Objects/New Chassis")]
public class Chassis : ScriptableObject
{
    public float speed;
    public float jumpHeight;
    public float maxHealth;
    public float maxCharge;
    public float strength;
    public float hackSpeed;
    public bool arms;
    public bool canCrouch;
    public bool canCrouchWalk;
}