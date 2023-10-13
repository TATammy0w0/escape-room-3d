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
    public Type ItemType;

    public enum Type
    {
        IncreaseHealth,
        DecreaseHealth,
        Key,
    }
}
