using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item/Create New Item")]
public class Item : ScriptableObject
{
    public int ID;
    public string ItemName;
    public string Description;
    public Sprite ItemIcon;
    public GameObject Prefab;
    public Type itemType;

    public enum Type
    {
        Apple,
        Fish,
        Key,
    }
}
