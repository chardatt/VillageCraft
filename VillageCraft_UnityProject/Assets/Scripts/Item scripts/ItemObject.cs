using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ce scripte permet de créer des items que l'on aura besoin
// on y ajoute un prefab d'objet, une description et son type d'item 
public enum ItemType
{
    Food,
    Equipement,
    Default
}
public abstract class ItemObject : ScriptableObject
{
    public GameObject prefab;
    public ItemType type;
    [TextArea(15,20)]
    public string description; 
}
