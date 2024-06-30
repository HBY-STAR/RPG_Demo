using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
   private Entity entity;
   private CharacterStats stats;
   private Transform myTransform;
   private Slider slider;

   private void Start()
   {
      entity = GetComponentInParent<Entity>();
      stats = GetComponentInParent<CharacterStats>();
      myTransform = GetComponent<Transform>();
      slider = GetComponentInChildren<Slider>();

      entity.OnFilped += FlipUI;
      stats.OnHealthChanged += UpdateHealthUI;
   }

   private void FlipUI()
   {
      myTransform.Rotate(0,180,0);
   }

   private void UpdateHealthUI()
   {
      slider.maxValue = stats.maxHeath.GetValue();
      slider.value = stats.currentHeath;
   }
}
