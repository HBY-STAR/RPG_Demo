using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class Inventory : MonoBehaviour, ISaveManager
{
    public static Inventory instance;

    public List<InventoryItem> inventory;

    public Dictionary<ItemData, InventoryItem> inventoryDictionary;

    [Header("Item effects")]
    [SerializeField] private int hpUp;
    [SerializeField] private int attackUp;

    [Header("Inventory UI")]
    [SerializeField] private Transform inventorySlotParent;
    [SerializeField] private int maxItem;
    private UI_ItemSlot[] itemSlots;

    private List<InventoryItem> loadedItems;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
        itemSlots = inventorySlotParent.GetComponentsInChildren<UI_ItemSlot>();
        inventory = new List<InventoryItem>();
        inventoryDictionary = new Dictionary<ItemData, InventoryItem>();
        loadedItems = new List<InventoryItem>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            foreach (var item in inventory)
            {
                if(item.data.itemName == "HP_cure")
                {
                    if(item.stackSize > 0)
                    {
                        item.RemoveStack();
                        PlayerManager.Instance.player.stats.Cure(20);
                        if(item.stackSize <= 0)
                        {
                            inventory.Remove(item);
                            inventoryDictionary.Remove(item.data);
                            break;
                        }
                    }
                }
            }
            UpdateSlotUI();
        }
    }

    private void AddLoadedItems()
    {
        foreach(var item in loadedItems)
        {
            for (int i = 0; i < item.stackSize; i++)
            {
                AddItem(item.data);
            }
        }
    }

    private void UpdateSlotUI()
    {
        for(int i = 0; i < inventory.Count; i++)
        {
            itemSlots[i].UpdateSlot(inventory[i]);
        }
        for(int i = inventory.Count; i < itemSlots.Length; i++)
        {
            itemSlots[i].UpdateSlot(null);
        }
    }

    public void AddItem(ItemData item)
    {
        if(inventory.Count >= maxItem)
        {
            return;
        }

        if(item && inventoryDictionary.TryGetValue(item, out InventoryItem inventoryItem))
        {
            inventoryItem.AddStack();
        }
        else
        {
            InventoryItem newItem = new InventoryItem(item);
            inventory.Add(newItem);
            inventoryDictionary.Add(item, newItem);
        }

        UpdateSlotUI();
    }

    public void RemoveItem(ItemData item)
    {
        if(inventory.Count <= 0)
        {
            return;
        }

        if(inventoryDictionary.TryGetValue(item, out InventoryItem inventoryItem))
        {
            inventoryItem.RemoveStack();
            if(inventoryItem.stackSize <= 0)
            {
                inventory.Remove(inventoryItem);
                inventoryDictionary.Remove(item);
            }
        }

        UpdateSlotUI();
    }

    public void UpdatePlayerStats(ItemData item, bool add)
    {
        if (add)
        {
            if(item.itemName == "HP_up")
            {
                PlayerManager.Instance.player.stats.maxHeath.AddModifier(hpUp);
            }
            else if(item.itemName == "Attack_up")
            {
                PlayerManager.Instance.player.stats.damage.AddModifier(attackUp);
            }
        }else
        {
            if (item.itemName == "HP_up")
            {
                PlayerManager.Instance.player.stats.maxHeath.RemoveModifier(hpUp);
            }
            else if (item.itemName == "Attack_up")
            {
                PlayerManager.Instance.player.stats.damage.RemoveModifier(attackUp);
            }
        }
    }

    public void LoadData(GameData gameData)
    {
        foreach(KeyValuePair<string, int> pair in gameData.inventory)
        {
            foreach(var item in GetItemDataBase())
            {
                if(item != null && item.itemId == pair.Key)
                {
                    InventoryItem newItem = new InventoryItem(item);
                    newItem.stackSize = pair.Value;
                    loadedItems.Add(newItem);
                }
            }
        }

        AddLoadedItems();
    }

    public void SaveData(ref GameData gameData)
    {
        gameData.inventory.Clear();

        foreach(KeyValuePair<ItemData, InventoryItem> pair in inventoryDictionary)
        {
            gameData.inventory.Add(pair.Key.itemId, pair.Value.stackSize);
        }
    }

    private List<ItemData> GetItemDataBase()
    {
        List<ItemData> itemDataBase = new List<ItemData>();
        string[] assetNames = AssetDatabase.FindAssets("", new[] { "Assets/Item" });

        foreach (var assetName in assetNames)
        {
            string path = AssetDatabase.GUIDToAssetPath(assetName);
            ItemData item = AssetDatabase.LoadAssetAtPath<ItemData>(path);
            itemDataBase.Add(item);
        }

        return itemDataBase;
    }
}
