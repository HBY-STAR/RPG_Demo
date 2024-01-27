using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCure : MonoBehaviour
{
    [SerializeField] private ItemData itemData;
    private SpriteRenderer sr;

    [SerializeField] private int cureVolume;
    
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        sr.sprite = itemData.icon;
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.GetComponent<Player>() != null)
        {
            collider2D.GetComponent<CharacterStats>().Cure(cureVolume);
            Destroy(gameObject);
        }
    }
}
