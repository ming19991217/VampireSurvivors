using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    Player target;
    EnemyMovement enemyMovement;
    Grid enemySpatialGroups;
    bool isReady = false;
    Vector2Int gridPosition;
    Action<Enemy> onMoveAction;
    public HP HP { get; private set; }


    public void Init(Player player, float speed, Action<Enemy> onMove)
    {
        enemyMovement = new(this.transform, player.transform, speed);
        isReady = true;
        target = player;
        this.onMoveAction = onMove;
        HP = new HP(5);
    }


    public void UpdateEnemey()
    {
        if (!isReady)
            return;

        var movementType = enemyMovement.TryMove();

        onMoveAction(this);

        if (movementType == EnemyMovement.MovementType.Arrive)
        {
            Attack();
        }
    }

    void Attack()
    {
        target.HP.TakeDamage(10);
    }

    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }




}
