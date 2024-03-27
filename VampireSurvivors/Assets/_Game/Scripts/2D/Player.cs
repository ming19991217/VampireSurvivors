using UnityEngine;

public class Player : MonoBehaviour
{

	public HP HP { get; private set; }

	PlayerController_2D playerController;
	Weapon weapon;
	[SerializeField]
	Bullet bulletPrefab;


	private void Awake()
	{
		Init();
	}

	void Init()
	{
		playerController = new();
		playerController.Init(this.transform);
		HP = new HP(100);
		weapon = new Weapon(bulletPrefab);
	}

	private void FixedUpdate()
	{
		playerController.UpdateMove();
		weapon.Attack();
	}

}