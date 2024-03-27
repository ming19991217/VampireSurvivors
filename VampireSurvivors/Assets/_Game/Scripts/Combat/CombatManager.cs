using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager
{

    public CalculateResult TryCalculateHp(AttackInfo attacker, DefendInfo defender)
    {
        return new(true, 50);
    }

    public record CalculateResult
    {
        public bool Success { get; }
        public int Hp { get; }

        public CalculateResult(bool success, int hp)
        {
            Success = success;
            Hp = hp;
        }
    }
}
public class AttackInfo
{
    public int Damage { get; private set; }

    public AttackInfo(int damage)
    {
        Damage = damage;
    }
}

public class DefendInfo
{
    public int Hp { get; private set; }

    public DefendInfo(int hp)
    {
        Hp = hp;
    }
}

public interface IDender
{
    DefendInfo defendInfo { get; }
}


