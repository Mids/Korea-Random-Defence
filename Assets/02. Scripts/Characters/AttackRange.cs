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
	}
}