using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    Player target;
    EnemyMovement enemyMovement;
    EnemySpatialGroups enemySpatialGroups;
    bool isReady = false;
    Vector2Int gridPosition;

    public HP HP { get; private set; }


    public void Init(Player player, float speed)
    {
        enemyMovement = new(this.transform, player.transform, speed);
        isReady = true;
        target = player;
        HP = new HP(100);

        this.enemySpatialGroups = EnemySpatialGroups.instance;
        gridPosition = enemySpatialGroups.GetGridIndex(this.transform.position);
        enemySpatialGroups.AddEnemy(gridPosition, this);
    }


    private void FixedUpdate()
    {
        if (!isReady)
            return;

        var movementType = enemyMovement.TryMove();

        var newPos = enemySpatialGroups.GetGridIndex(this.transform.position);
        if (newPos != gridPosition)
        {
            enemySpatialGroups.RemoveEnemy(gridPosition, this);
            gridPosition = newPos;
            enemySpatialGroups.AddEnemy(gridPosition, this);
        }

        if (movementType == EnemyMovement.MovementType.Arrive)
        {
            Attack();
        }
    }

    void Attack()
    {
        target.HP.TakeDamage(10);
    }





}
