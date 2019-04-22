using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace KRD
{
	public class AttackRange : MonoBehaviour
	{
		private Character _parent;
		private List<Enemy> _enemiesInRange;

		// Start is called before the first frame update
		void Start()
		{
			_parent = GetComponentInParent<Character>();
			if (!_parent)
			{
				Debug.LogError("AttackRange should have Character parent.");
			}

			_enemiesInRange = new List<Enemy>();
		}

		private void Update()
		{
			for (int i = _enemiesInRange.Count - 1; i >= 0; i--)
			{
				if (!_enemiesInRange[i].IsActive)
				{
					_enemiesInRange.RemoveAt(i);
				}
			}
		}

        void OnTriggerEnter(Collider collider)
		{
			if (collider.gameObject.tag == "Enemy")
			{
				_enemiesInRange.Add(collider.gameObject.GetComponent<Enemy>());
			}
		}

		void OnTriggerExit(Collider collider)
		{
			if (collider.gameObject.tag == "Enemy")
			{
				_enemiesInRange.Remove(collider.gameObject.GetComponent<Enemy>());
			}
		}

		public List<Enemy> GetEnemiesInRange()
		{
			return _enemiesInRange;
		}

		public Enemy GetEnemyIn(int i)
		{
			return _enemiesInRange[i];
		}

		public bool Contains(Enemy enemy)
		{
			return _enemiesInRange.Contains(enemy);
		}
	}
}