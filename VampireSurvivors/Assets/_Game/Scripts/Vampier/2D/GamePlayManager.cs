using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GamePlayManager : MonoBehaviour
{
    [SerializeField]
    Player player;

    [SerializeField]
    Enemy enemyPrefab;
    [SerializeField]
    List<Bullet> bulletPrefabs;

    WeaponManager playerWeaponManager;
    BulletManager bulletManager;
    EnemyManager enemyManager;
    EnemySpawner enemySpawner;

    static float spawnInterval = .5f;
    float currentSpawnInterval = spawnInterval;

    private void Awake()
    {
        Init();
    }

    void Init()
    {
        var enemyGridSystem = new EnemyGridSystem(100, 100, 2, transform);
        enemyManager = new EnemyManager(enemyGridSystem, player);
        enemySpawner = new EnemySpawner(player, enemyPrefab, enemyManager.RegisterEnemy);

        player.Init();
        bulletManager = new BulletManager(enemyManager, enemyGridSystem);
        playerWeaponManager = new WeaponManager(player, bulletPrefabs, bulletManager.AddBullet);

        playerWeaponManager.AddWeapon(WeaponType.Normal);
    }


    private void Update()
    {
        bulletManager.BulletUpdate();
        enemyManager.EnemyUpdate();
        enemySpawner.EnemySpawnUpdate();
        playerWeaponManager.WeaponUpdate();

    }



}