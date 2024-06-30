using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
   [SerializeField] protected float coolDown;
   [SerializeField] protected float coolDownTimer;

   protected Player player;

   protected virtual void Start()
   {
      player = PlayerManager.Instance.player;
   }

   protected virtual void Update()
   {
      coolDownTimer -= Time.deltaTime;
   }

   public virtual bool CanUseSkill()
   {
      return coolDownTimer < 0;
   }

   public virtual void UseSkill()
   {
      coolDownTimer = coolDown;
      //skill start
   }
}
