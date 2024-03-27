using UnityEngine;

public class EnemyMovement
{
	public enum MovementType
	{
		FollowPlayer,
		Arrive
	}

	Transform self, player;
	float speed;


	public EnemyMovement(Transform self, Transform player, float speed = 0.05f)
	{
		this.self = self;
		this.player = player;
		this.speed = speed;
	}


	public MovementType TryMove()
	{
		if (Vector3.Distance(self.position, player.position) > .5f)
		{
			self.position = Vector3.MoveTowards(self.position, player.position, speed);
			return MovementType.FollowPlayer;
		}
		else
		{
			return MovementType.Arrive;
		}
	}




}