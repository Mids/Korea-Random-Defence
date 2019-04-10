using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

		// Start is called before the first frame update
		protected virtual void Start()
		{
			_selectionCircle = GetComponentInChildren<Projector>();
			Deselect();
			Range = GetComponentInChildren<AttackRange>();
			IsAttacking = true;
			IsMoving = false;
		}

		// Update is called once per frame
		protected virtual void Update()
		{
			AttackCooldown += Time.deltaTime;
			if (IsAttacking && Range.GetEnemiesInRange().Count > 0 && AttackCooldown > AttackSpeed)
			{
				AttackCooldown -= AttackSpeed;
				Attack();
			}
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
		}
	}
}