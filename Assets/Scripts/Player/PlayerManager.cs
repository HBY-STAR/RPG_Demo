using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, ISaveManager
{
    public static PlayerManager Instance;
    public GameData gameData;
    public Player player;
    private void Awake()
    {
        if (Instance != null)
            Destroy(Instance.gameObject);
        else
            Instance = this;
    }

    public void LoadData(GameData gameData)
    {
        this.gameData = gameData;
    }

    public void SaveData(ref GameData gameData)
    {
        gameData = this.gameData;
    }
}
