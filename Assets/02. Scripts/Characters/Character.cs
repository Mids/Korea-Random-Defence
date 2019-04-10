using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace KRD
{
	public class Character : MonoBehaviour
	{
		private Projector _selectionCircle;
		public bool IsSelected;
		public AttackRange Range;
		public bool IsAttacking;
		public bool IsMoving;

		public readonly float AttackSpeed = 0.8f;
		private float AttackCooldown = 0.8f;
		private NavMeshAgent _nav;

		// Start is called before the first frame update
		protected virtual void Start()
		{
			_selectionCircle = GetComponentInChildren<Projector>();
			Deselect();
			Range = GetComponentInChildren<AttackRange>();
			IsAttacking = true;
			IsMoving = false;
			_nav = GetComponent<NavMeshAgent>();
		}

		// Update is called once per frame
		protected virtual void Update()
		{
			AttackCooldown += Time.deltaTime;
			if (IsMoving)
			{
				if (_nav.remainingDistance <= _nav.stoppingDistance)
				{
					// When arrived to destination.
					IsMoving = false;
				}
			}
			else if (IsAttacking && Range.GetEnemiesInRange().Count > 0)
			{
				if (AttackCooldown > AttackSpeed)
				{
					//TODO: AttackCooldown %= AttackSpeed;
					AttackCooldown = 0;
					Attack();
				}

				return;
			}

			if (_nav.isStopped)
			{
				// There is nothing to attack, so keep going
				_nav.isStopped = false;
			}
		}

		public void Move(Vector3 destination)
		{
			_nav.destination = destination;
			IsMoving = true;
		}

		public void Select()
		{
			IsSelected = true;
			_selectionCircle.enabled = true;
		}

		public void Deselect()
		{
			IsSelected = false;
			_selectionCircle.enabled = false;
		}

		public void OnAttackRange()
		{
		}

		public void Attack()
		{
			Debug.Log("Attack!");
			if (_nav.isStopped == false)
			{
				_nav.isStopped = true;
			}
		}
	}
}