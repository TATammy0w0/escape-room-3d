using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item/Create New Item")]
public class Item : ScriptableObject
{
    public int id;
    public string itemName;
    public string description;
    public Sprite itemIcon;
    // public GameObject prefab;
    public Type itemType;

    public enum Type
    {
        Apple,
        Fish,
        Key,
    }
}
