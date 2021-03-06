﻿using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace KRD
{
	public class Enemy : MonoBehaviour
	{
		public int Level;
		public int CurrentHP, MaxHP;
		public Direction CurDirection;
		public float MoveSpeed = 0.1f;
		private float _moveSpeed;
		public bool IsActive = false;

		public Slider HPSlider;
		private float _stunTime;

		/// <summary>
		/// Initialize Enemy
		/// </summary>
		/// <param name="HP">Max HP</param>
		public void Init(int level, int HP)
		{
			Inactivate();
			Level = level;
			CurrentHP = HP;
			MaxHP = HP;
			HPSlider.maxValue = MaxHP;
			HPSlider.value = CurrentHP;
			SetDirection(Direction.South);
			_moveSpeed = MoveSpeed * EnemyFactory.DifficultyMultiplier;
		}

		/// <summary>
		/// Activate enemy
		/// </summary>
		public void Activate()
		{
			IsActive = true;
			gameObject.SetActive(IsActive);
		}

		/// <summary>
		/// Inactivate enemy
		/// </summary>
		public void Inactivate()
		{
			IsActive = false;
			gameObject.SetActive(IsActive);
		}

		// Update is called once per frame
		void Update()
		{
			if (!IsActive) return;

			// Move
			if (_stunTime <= 0)
			{
				transform.localPosition += DirectionTool.GetDirectionVector(CurDirection) * _moveSpeed * (Time.deltaTime * 30);
			}
			else
			{
				_stunTime -= Time.deltaTime;
			}

			// Turn counterclockwise
			if (CheckTurnPosition())
				CurDirection = DirectionTool.CounterClockwise(CurDirection);


			if (CurrentHP < 0)
			{
				Destroy(gameObject);
			}
		}

		/// <summary>
		/// Set Direction
		/// </summary>
		/// <param name="dir">Direction</param>
		public void SetDirection(Direction dir)
		{
			CurDirection = dir;
		}

		/// <summary>
		/// Copy
		/// </summary>
		/// <param name="target">target</param>
		public void CopyFrom(Enemy target)
		{
			CurrentHP = target.CurrentHP;
			MaxHP = target.MaxHP;
			Level = target.Level;
		}

		/// <summary>
		/// Check if it is time to turn
		/// </summary>
		/// <returns>true of false</returns>
		public bool CheckTurnPosition()
		{
			var pos = transform.localPosition;
			if (CurDirection == Direction.South && pos.x > pos.z ||
			    CurDirection == Direction.East && -pos.x < pos.z ||
			    CurDirection == Direction.North && pos.x < pos.z ||
			    CurDirection == Direction.West && -pos.x > pos.z)
				return true;

			return false;
		}

		/// <summary>
		/// Take damage and die if hp is below 0
		/// </summary>
		/// <param name="damage"></param>
		/// <returns>True if died</returns>
		public bool TakeDamage(int damage,float DamagePerTotalHealth)
		{
			CurrentHP -= (damage+ (int)(MaxHP*DamagePerTotalHealth));
			if (CurrentHP <= 0)
			{
				//Die
				Inactivate();
				return true;
			}

			HPSlider.value = CurrentHP;

			return false;
		}

		public void ReceiveStun(float[] stun)
		{
			float stunProb = Random.Range(0.0f, stun[1]);

			if (stunProb < 100)
			{
				_stunTime = stun[0];
			}
		}

		public void ReceiveAura(float[] aura)
		{
			MoveSpeed = 0.1f - aura[0];
			TakeDamage((int) aura[1],0.0f);
		}

		public void ReleaseAura()
		{
			MoveSpeed = 0.1f;
		}
	}
}