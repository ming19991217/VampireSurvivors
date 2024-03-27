using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP
{
    public int CurrentHP { get; private set; }
    public int MaxHP { get; private set; }

    public HP(int maxHP)
    {
        MaxHP = maxHP;
        CurrentHP = MaxHP;
    }

    public void TakeDamage(int damage)
    {
        CurrentHP -= damage;
        if (CurrentHP < 0)
        {
            CurrentHP = 0;
            Debug.Log("Dead");
        }
    }
}
