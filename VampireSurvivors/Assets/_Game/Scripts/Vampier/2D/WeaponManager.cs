using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum WeaponType
{
	Normal,
}

public class WeaponManager
{
	Player player;
	List<Weapon> weapons = new();
	List<Bullet> bulletPrefabs;
	Action<Weapon, Bullet> onBulletSpawn;

	public WeaponManager(Player player, List<Bullet> bulletPrefabs, Action<Weapon, Bullet> onBulletSpawn)
	{
		this.player = player;
		this.bulletPrefabs = bulletPrefabs;
		this.onBulletSpawn = onBulletSpawn;
	}

	public void AddWeapon(WeaponType weapon)
	{
		weapons.Add(new Weapon(bulletPrefabs[0], player, Vector3.right));
		weapons.Add(new Weapon(bulletPrefabs[0], player, Vector3.up));
		weapons.Add(new Weapon(bulletPrefabs[0], player, Vector3.left));
		weapons.Add(new Weapon(bulletPrefabs[0], player, Vector3.down));

		// weapons.Add(new Weapon(bulletPrefabs[0], player, new Vector3(1, 1, 0)));
		// weapons.Add(new Weapon(bulletPrefabs[0], player, new Vector3(1, -1, 0)));
		// weapons.Add(new Weapon(bulletPrefabs[0], player, new Vector3(-1, 1, 0)));
		// weapons.Add(new Weapon(bulletPrefabs[0], player, new Vector3(-1, -1, 0)));
	}


	public void WeaponUpdate()
	{
		foreach (var weapon in weapons)
		{
			if (weapon.TryFire(out var bullet) == false)
				continue;

			onBulletSpawn?.Invoke(weapon, bullet);
		}

	}
}
