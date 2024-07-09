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


	public EnemyMovement(Transform self, Transform player, float speed = 1)
	{
		this.self = self;
		this.player = player;
		this.speed = speed;
	}


	public MovementType TryMove()
	{
		if (Vector3.Distance(self.position, player.position) > .5f)
		{
			self.position = Vector3.MoveTowards(self.position, player.position, speed * Time.deltaTime);
			return MovementType.FollowPlayer;
		}
		else
		{
			return MovementType.Arrive;
		}
	}




}