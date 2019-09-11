using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace KRD
{
	public class Bullet : MonoBehaviour
	{
		private Transform _targetTransform;
		private Enemy _target;

		public int Damage;
		public float Speed = 70f;
		public bool StunAbility;
		public GameObject StunObject;

		public void Seek(Transform target)
		{
			_targetTransform = target;
			_target = _targetTransform.GetComponent<Enemy>();
		}

		// Update is called once per frame
		void Update()
		{
			if (_targetTransform == null)
			{
				Destroy(gameObject);
				return;
			}

			transform.LookAt(_targetTransform);

			Vector3 dir = _targetTransform.position - transform.position;
			float distanceThisFrame = Speed * Time.deltaTime;

			if (dir.magnitude <= distanceThisFrame)
			{
				HitTarget();
				//StunObject = range stun object
				if (StunAbility)
				{
					//TODO: stun time and percent though of each character
                    Instantiate(StunObject, transform.position, transform.rotation);
				}

				Destroy(gameObject);
				return;
			}

			transform.Translate(dir.normalized * distanceThisFrame, Space.World);
		}

		void HitTarget()
		{
			_target.TakeDamage(Damage);
		}
	}
}