using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon
{
    Bullet bulletPrefab;
    float attackSpeed = 1;
    float currentAttackSpeed;

    public Weapon(Bullet bulletPrefab)
    {
        this.bulletPrefab = bulletPrefab;
    }


    public void Attack()
    {
        currentAttackSpeed -= Time.deltaTime;
        if (currentAttackSpeed > 0)
            return;

        var bullet = GameObject.Instantiate(bulletPrefab);
        bullet.Init(Vector3.left, 10, onHitEnemy: (enemys) =>
        {
            foreach (var enemy in enemys)
            {
                enemy.HP.TakeDamage(10);
            }
            GameObject.Destroy(bullet.gameObject);
        });

        currentAttackSpeed = attackSpeed;
    }




}
