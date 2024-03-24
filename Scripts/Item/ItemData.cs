using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "New item Data", menuName = "Data/item")]
public class ItemData : ScriptableObject
{
    public String itemName;
    public Sprite icon;
    public string itemId;


    private void OnValidate()
    {
        #if UNITY_EDITOR
        string path = AssetDatabase.GetAssetPath(this);
        itemId = AssetDatabase.AssetPathToGUID(path);
        #endif
    }
}
