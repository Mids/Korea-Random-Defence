using UnityEngine;
using UnityEngine.UI;

namespace KRD
{
	public class EnemyFactory : MonoBehaviour
	{
		//about game Interface
		public Text EnemyCountText;
		public Text RoundTimeText;
		public Enemy[] RoundEnemy = new Enemy[_maxGameRound];
		private GameObject[] _enemyObject;
		private int _enemyCount;
		private float _roundTime;
		private int _gameRound;
		private static int _maxGameRound = 40;
		private int _limitEnemy = 80;
		private RandomButton _randomButton;

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
		public static float EnemyHPMultiple = 1.3f;
		public static float EnemyHPInitialRound = 240f;
		private int[] _enemyHP = new int[_maxGameRound];

		// Start is called before the first frame update
		void Start()
		{
			_enemyCount = 0;
			_roundTime = 80;
			_gameRound = 0;
			_randomButton = GameObject.FindObjectOfType<RandomButton>();

			//calcuate each round enemy HP
			for (int i = 0; i <= _maxGameRound - 1; i++)
			{
				float calcuatedHP = EnemyHPInitialRound * Mathf.Pow(EnemyHPMultiple, i);
				_enemyHP[i] = (int) calcuatedHP;

				if (i > 0 && i % 9 == 0)
				{
					_enemyHP[i] = (int) calcuatedHP * 100;
				}
			}

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
			var enemy = Instantiate(CurrentEnemy, Vector3.zero, Quaternion.identity, transform);
			Enemies[0] = enemy;
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
			_randomButton.ChanceLeft += 2;

			if (_gameRound % 9 == 0)
			{
				CurrentEnemy = RoundEnemy[_gameRound];
				CurrentEnemy.Init(_gameRound, _enemyHP[_gameRound]);
				CurrentEnemyNum = 0;
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