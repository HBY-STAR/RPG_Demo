using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New item Data", menuName = "Data/item")]
public class ItemData : ScriptableObject
{
    public String itemName;
    public Sprite icon;
}
