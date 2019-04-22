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

		public int AttackDamage;
		public readonly float AttackSpeed = 0.8f;
		private float AttackCooldown = 0.8f;
		private NavMeshAgent _nav;
		private Enemy CurrentTarget = null;

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
					//Time to Attack
                    //TODO: AttackCooldown %= AttackSpeed;
                    AttackCooldown = 0;

					Debug.Log("Choose whom to attack");
                    if (CurrentTarget == null || !Range.Contains(CurrentTarget))
                    {
	                    // Get Closest Enemy in range and set to new target
	                    int count = Range.GetEnemiesInRange().Count;
	                    int closestEnemyIndex = -1;
	                    float distanceToClosest = 0f;

	                    for (int i = 0; i < count; i++)
	                    {
		                    Enemy enemy = Range.GetEnemyIn(i);
		                    Vector3 enemyPos = Range.GetEnemyIn(i).transform.position;
		                    Vector3 myPos = transform.position;

		                    if (closestEnemyIndex == -1)
		                    {
			                    closestEnemyIndex = i;
			                    distanceToClosest = Vector3.Distance(myPos, enemyPos);
		                    }
		                    else
		                    {
			                    // Change Closest Enemy to current enemy
			                    float curDistance = Vector3.Distance(myPos, enemyPos);
			                    if (curDistance < distanceToClosest)
			                    {
				                    closestEnemyIndex = i;
				                    distanceToClosest = curDistance;
			                    }
		                    }
                        }
                        if (closestEnemyIndex == -1)
                        {
                            Debug.Log("WTF?");
                        }
                        else
                        {
                            CurrentTarget = Range.GetEnemyIn(closestEnemyIndex);
                        }
                    }


					Attack(CurrentTarget);
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

		public void Attack(Enemy target)
		{
			Debug.Log("Attack!");
			if (_nav.isStopped == false)
			{
				_nav.isStopped = true;
			}

			//TODO: Attack motion

			target.TakeDamage(AttackDamage);
		}
	}
}