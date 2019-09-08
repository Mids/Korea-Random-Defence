using UnityEngine;
using UnityEngine.UI;

namespace KRD
{
	public class EnemyFactory : MonoBehaviour
	{
		//about enemy count parameters
		public Text EnemyCountText;
		public Text RoundTimeText;
		public Enemy[] RoundEnemy;
		private GameObject[] _enemyObject;
		private int _enemyCount;
		private float _roundTime;
		private int _gameRound;
		private int _limitEnemy = 80;

		private int[] _enemyHP =
		{
			240, 340, 460, 600, 760, 1020, 1300, 1720, 2040, 116000,
			2200, 2800, 3580, 4480, 7743, 10614, 12905, 14413, 16240, 664000,
			18850, 20880, 27260, 36830, 45965, 54810, 67425, 75400, 92800, 4880000,
			436000, 536000, 640000, 722400, 870960, 993600, 1107200, 11160000, 8, 10000000
		};

		// Spawning
		public bool IsSpawning = true;
		public int UnitCount = 80;
		public float SpawningTime = 1.0f;
		public float SpawningCooldown;
		public int CurrentUnitCount = 0;
		public readonly Vector3 SpawningPosition = new Vector3(-40, 1, 40);

		// Enemy
		public Enemy CurrentEnemy;
		public Enemy[] Enemies;
		public int CurrentEnemyNum = 0;


		// Start is called before the first frame update
		void Start()
		{
			_enemyCount = 0;
			_roundTime = 80;
			_gameRound = 0;

			CurrentEnemy = RoundEnemy[0];
			// TODO: Set First Enemy
//			CurrentEnemy = Instantiate();
			CurrentEnemy.Init(_gameRound, _enemyHP[_gameRound]);
			InstantiateEnemies();
		}

		// Update is called once per frame
		void Update()
		{
			// count enemy number for game over
			_enemyObject = GameObject.FindGameObjectsWithTag("Enemy");
			_enemyCount = _enemyObject.Length;
			_roundTime -= Time.deltaTime;

			EnemyCountText.text = "Enemy Count   " + _enemyCount.ToString();
			RoundTimeText.text = "Round  " + (_gameRound + 1).ToString() + "    " + Mathf.Ceil(_roundTime).ToString();

			//round up when round time end
			if (_roundTime < 0.0)
			{
				RoundUp();
			}


			// Spawn enemies
			if (!IsSpawning) return;
			SpawningCooldown += Time.deltaTime;
			if (SpawningCooldown > SpawningTime)
			{
				//TODO:SpawningCooldown %= SpawningTime;
				SpawningCooldown = 0;
				Spawn();

				//if (CurrentUnitCount >= UnitCount)
				if (_enemyCount >= _limitEnemy)
				{
					// TODO: Game Over
					Debug.Log("Game Over");
					IsSpawning = false;
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

		private void InstantiateBoss()
		{
			for (int i = 0; i < UnitCount; i++)
			{
				var enemy = Instantiate(CurrentEnemy, Vector3.zero, Quaternion.identity, transform);
				Enemies[i] = enemy;
			}
		}

		public void Spawn()
		{
			// Find inactive enemy
			/*
			int loopCount = 0;
			do
			{
				// The enemy is active
				if (++CurrentEnemyNum >= UnitCount)
				{
					CurrentEnemyNum = 0;

					// Prevent infinite loop
					if (++loopCount > 1)
					{
						Debug.Log("ERROR : All Enemies are active");
						return;
					}
				}
			} while (Enemies[CurrentEnemyNum].IsActive);
			*/
			//Debug.Log(CurrentEnemyNum);
			if (CurrentEnemyNum < UnitCount)
			{
				Enemy curEnemy = Enemies[CurrentEnemyNum];
				curEnemy.CopyFrom(CurrentEnemy);
				curEnemy.transform.localPosition = SpawningPosition;
				curEnemy.Activate();
				CurrentEnemyNum++;
			}
			else
			{
				IsSpawning = false;
			}
		}

		private void RoundUp()
		{
			_roundTime = 80;
			_gameRound += 1;
			IsSpawning = true;
			Debug.Log(_gameRound);

			if (_gameRound % 9 == 0)
			{
				CurrentEnemy = RoundEnemy[_gameRound];
				CurrentEnemy.Init(_gameRound, _enemyHP[_gameRound]);
				CurrentEnemyNum = 0;
				UnitCount = 1;
				InstantiateBoss();
			}
			else
			{
				CurrentEnemy = RoundEnemy[_gameRound];
				CurrentEnemy.Init(_gameRound, _enemyHP[_gameRound]);
				CurrentEnemyNum = 0;
				UnitCount = 80;
				InstantiateEnemies();
			}
		}
	}
}