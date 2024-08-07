using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public Stat maxHeath;
    public Stat damage;
    
    public int currentHeath;

    public System.Action OnHealthChanged;
    
    void Start()
    {
        currentHeath = maxHeath.GetValue();
    }

    public virtual void TakeDamage(int _damage)
    {
        currentHeath -= _damage;
        if(OnHealthChanged != null)
            OnHealthChanged();
        CheckDie();
    }

    protected virtual void CheckDie()
    {
        if (currentHeath <= 0)
        {
            Destroy(gameObject, 0.1f);
        }

    }

    public virtual void Cure(int _cure)
    {
        currentHeath += _cure;
        if (currentHeath > maxHeath.GetValue())
        {
            currentHeath = maxHeath.GetValue();
        }
        
        if(OnHealthChanged != null)
            OnHealthChanged();
    }
}
