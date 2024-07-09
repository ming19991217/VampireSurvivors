using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon
{
    Bullet bulletPrefab;
    Player player;
    float attackSpeed = .05f;
    float currentAttackSpeed;


    public Vector3 Direction { get; private set; }


    public Weapon(Bullet bulletPrefab, Player player, Vector3 direction)
    {
        this.bulletPrefab = bulletPrefab;
        this.player = player;
        this.Direction = direction;
    }


    public bool TryFire(out Bullet bullet)
    {
        bullet = null;
        currentAttackSpeed -= Time.deltaTime;
        if (currentAttackSpeed > 0)
            return false;

        bullet = GameObject.Instantiate(bulletPrefab, player.transform.position, Quaternion.identity);
        currentAttackSpeed = attackSpeed;
        return true;
    }





}
