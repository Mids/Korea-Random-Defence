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

			Vector3 dir = _targetTransform.position - transform.position;
			float distanceThisFrame = Speed * Time.deltaTime;

			if (dir.magnitude <= distanceThisFrame)
			{
				HitTarget();
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