using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable object/Weapon")]
public class Gun : ScriptableObject
{
    public Sprite image;
    public ItemType type;
    public ActionType actionType;
    public int cost;

}

public enum ItemType {
    Guns
}

public enum ActionType {
    AK,
    Pistol,
    MP
}