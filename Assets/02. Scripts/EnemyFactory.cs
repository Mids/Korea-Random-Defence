using UnityEngine;

namespace KRD
{
	public class EnemyFactory : MonoBehaviour
	{
		// Spawning
		public bool IsSpawning = true;
		public const int UnitCount = 80;
		public float SpawningTime = 1.0f;
		public float SpawningCooldown;
		public int CurrentUnitCount = 0;
		public readonly Vector3 SpawningPosition = new Vector3(-40, 0, 40);

		// Enemy
		public Enemy CurrentEnemy;
		public Enemy[] Enemies;
		public int CurrentEnemyNum = 0;


		// Start is called before the first frame update
		void Start()
		{
			// TODO: Set First Enemy
//			CurrentEnemy = Instantiate();
			CurrentEnemy.Init(1, 10);
			InstantiateEnemies();
		}

		// Update is called once per frame
		void Update()
		{
			if (!IsSpawning) return;

			SpawningCooldown += Time.deltaTime;
			if (SpawningCooldown > SpawningTime)
			{
				Debug.Log("Spawning");
				SpawningCooldown -= SpawningTime;
				Spawn();

				if (CurrentUnitCount >= UnitCount)
				{
					// TODO: Game Over
				}
			}
		}

		private void InstantiateEnemies()
		{
			for (int i = 0; i < UnitCount; i++)
			{
				var enemy = Instantiate(CurrentEnemy, Vector3.zero, Quaternion.identity, transform);
				Enemies[i] = enemy;
			}
		}

		public void Spawn()
		{
			Enemy curEnemy = Enemies[CurrentEnemyNum];
			curEnemy.CopyFrom(CurrentEnemy);
			curEnemy.transform.localPosition = SpawningPosition;
			curEnemy.Activate();

			// Reset Count
			CurrentEnemyNum++;
			if (CurrentEnemyNum >= UnitCount)
			{
				CurrentEnemyNum = 0;
			}
		}
	}
}