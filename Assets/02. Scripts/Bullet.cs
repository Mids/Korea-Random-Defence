using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace KRD
{
	public class Bullet : MonoBehaviour
	{
		private Transform _target;

		public float speed = 70f;

		public void Seek(Transform target)
		{
			_target = target;
		}

		// Update is called once per frame
		void Update()
		{
			if (_target == null)
			{
				Destroy(gameObject);
				return;
			}

			Vector3 dir = _target.position - transform.position;
			float distanceThisFrame = speed * Time.deltaTime;

			if (dir.magnitude <= distanceThisFrame)
			{
				HitTarget();
				return;
			}

			transform.Translate(dir.normalized * distanceThisFrame, Space.World );
		}

		void HitTarget()
		{

		}
	}
}