using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace KRD
{
	public class CharacterFactory : MonoBehaviour
	{
		public Character[] SpawnableCharacters;
		private RTSManager _rtsManager;
		public List<Character> CurrentCharacters = new List<Character>();
		public List<Character> SelectedCharacters = new List<Character>();
		public bool IsTargeting;

		public Texture2D Crosshair;
		public Texture2D DefaultCursor;

		// Start is called before the first frame update
		void Start()
		{
			_rtsManager = GameObject.FindGameObjectWithTag("RTSManager").GetComponent<RTSManager>();
			Cursor.SetCursor(DefaultCursor, Vector2.zero, CursorMode.Auto);
			//when game was started, 5 common unit gerneration
			for (int i = 0; i < 5; i++)
			{
				SpawnRandomCharacter();
			}
		}

		// Update is called once per frame
		void Update()
		{
			// If 'a' is not pressed
			if (!IsTargeting)
			{
				// If we release the left mouse button
				if (Input.GetMouseButtonUp(0))
				{
					if (_rtsManager.IsSelecting)
					{
						CheckSelection();
					}
				}
                _rtsManager.CheckSelecting();
			}

            // If there is no selected characters, skip below actions
            if (SelectedCharacters.Count == 0)
				return;

			// If we press the right mouse button, let the characters go
			if (Input.GetMouseButtonDown(1))
			{
				if (IsTargeting)
				{
					SetTargeting(false);
				}
				else
				{
					MoveFormation();
				}
			}

			// If we press a, change cursor to target image
			if (Input.GetButtonDown("Attack"))
			{
				// Change cursor
				SetTargeting(true);
			}

			// If we press the left mouse button
			if (Input.GetMouseButtonDown(0))
			{
				if (IsTargeting)
				{
					SetTargeting(false);
					// TODO: Look for target
					AttackFormation();
				}
			}
		}

		public void SetTargeting(bool b)
		{
			IsTargeting = b;
			Cursor.SetCursor(b ? Crosshair : DefaultCursor, Vector2.zero, CursorMode.Auto);
		}

		/// <summary>
		/// Selected characters move as a formation
		/// </summary>
		void MoveFormation()
		{
			foreach (var character in SelectedCharacters)
			{
				var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

				if (Physics.Raycast(ray, out var hit, 1000L, LayerMask.GetMask("Ground")))
				{
					// TODO: Keep the formation
					character.Move(hit.point);
					character.IsAttacking = false;
				}
			}
		}

		/// <summary>
		/// Selected characters attack as a formation
		/// </summary>
		void AttackFormation()
		{
			foreach (var character in SelectedCharacters)
			{
				var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

				if (Physics.Raycast(ray, out var hit, 1000L, LayerMask.GetMask("Ground")))
				{
					// TODO: Keep the formation
					character.IsAttacking = true;
					if (!character.CheckEnemyInRange())
					{
						character.Move(hit.point);
					}
				}
			}
		}

		/// <summary>
		/// Check if selected
		/// </summary>
		void CheckSelection()
		{
			foreach (var character in CurrentCharacters)
			{
				if (_rtsManager.IsWithinSelectionBounds(character.gameObject))
				{
					if (character.IsSelected) continue;

					SelectedCharacters.Add(character);
					character.Select();
				}
				else
				{
					if (!character.IsSelected) continue;

					SelectedCharacters.Remove(character);
					character.Deselect();
				}
			}
		}

		void DeselectAll()
		{
			foreach (var character in CurrentCharacters)
			{
				character.Deselect();
			}
		}

		public void SpawnRandomCharacter()
		{
			var randomNumber = Random.Range(0, SpawnableCharacters.Length);
			CurrentCharacters.Add(Instantiate(SpawnableCharacters[randomNumber], transform));
		}
	}
}