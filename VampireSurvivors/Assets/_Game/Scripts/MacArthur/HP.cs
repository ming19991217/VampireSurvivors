using System;
using UnityEngine;


namespace MacArthur
{
    public class HP
    {
        int hp;
        public int HPValue => hp;
        Action onDie;
        Action<int> onDamage;

        public HP(int hp, Action<int> onDamage, Action onDie)
        {
            this.hp = hp;
            this.onDie = onDie;
            this.onDamage = onDamage;
        }

        public void TakeDamage(int damage)
        {
            hp -= damage;
            onDamage?.Invoke(hp);

            if (hp <= 0)
            {
                onDie?.Invoke();
            }
        }


    }
}