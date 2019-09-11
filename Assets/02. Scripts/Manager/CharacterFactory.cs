using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace KRD
{
	public class CharacterFactory : MonoBehaviour
	{
		public Character[] SpawnableCommonCharacters;
		public Character[] SpawnableUncommonCharacters;
		public Character[] SpawnableSpecialCharacters;
		public Character[] SpawnableRareCharacters;
		public Character[] SpawnableLegendaryCharacters;
		private List<Character> _spawnableCharacters;
		private RTSManager _rtsManager;
		public List<Character> CurrentCharacters = new List<Character>();
		public List<Character> SelectedCharacters = new List<Character>();
		public bool IsTargeting;

		public Texture2D Crosshair;
		public Texture2D DefaultCursor;
		public Character MainCharacter => SelectedCharacters.Count > 0 ? SelectedCharacters[0] : null;

		// Start is called before the first frame update
		void Start()
		{
			_rtsManager = GameObject.FindGameObjectWithTag("RTSManager").GetComponent<RTSManager>();
			Cursor.SetCursor(DefaultCursor, Vector2.zero, CursorMode.Auto);
			_spawnableCharacters = new List<Character>();
			AddSpawnableCharacters(SpawnableCommonCharacters);
			AddSpawnableCharacters(SpawnableUncommonCharacters);
			AddSpawnableCharacters(SpawnableSpecialCharacters);
			AddSpawnableCharacters(SpawnableRareCharacters);
			AddSpawnableCharacters(SpawnableLegendaryCharacters);
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

			if (Input.GetButtonDown("Stop"))
			{
				StopFormation();
			}

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
		/// Selected characters stop
		/// </summary>
		void StopFormation()
		{
			foreach (var character in SelectedCharacters)
			{
				character.Stop();
				character.IsAttacking = false;
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

		public void SpawnRandomCommonCharacter()
		{
			var randomNumber = Random.Range(0, SpawnableCommonCharacters.Length);
			CurrentCharacters.Add(Instantiate(SpawnableCommonCharacters[randomNumber], transform));

			// Sort Characters
			CurrentCharacters.Sort(SortByCharacterOrder);
		}

		public void RemoveCharacter(Character targetCharacter)
		{
			if (CurrentCharacters.Contains(targetCharacter))
				CurrentCharacters.Remove(targetCharacter);
			if (SelectedCharacters.Contains(targetCharacter))
				SelectedCharacters.Remove(targetCharacter);
			Destroy(targetCharacter.gameObject);
		}

		public void RemoveCharacterByTag(string tag)
		{
			Character targetCharacter = null;
			for (int i = 0; i < CurrentCharacters.Count; i++)
			{
				if (CurrentCharacters[i].tag == tag)
				{
					targetCharacter = CurrentCharacters[i];
					break;
				}
			}

			if (targetCharacter != null)
				RemoveCharacter(targetCharacter);
		}

		public void SpawnCharacterByTag(string tag, Vector3 position)
		{
			// TODO: Spawn any characters
			for (int i = 0; i < _spawnableCharacters.Count; i++)
			{
				if (_spawnableCharacters[i].tag == tag)
				{
					var newCharacter = Instantiate(_spawnableCharacters[i], transform);
					newCharacter.transform.localPosition = position;
					CurrentCharacters.Add(newCharacter);
				}
			}

			// Sort Characters
			CurrentCharacters.Sort(SortByCharacterOrder);
		}

		private int SortByCharacterOrder(Character c1, Character c2)
		{
			int i1 = -1, i2 = -1;

			// Same Characters
			if (c1.CompareTag(c2.tag))
			{
				return 0;
			}

			// Find index of the characters TODO: spawn any characters
			for (int i = 0; i < _spawnableCharacters.Count; i++)
			{
				if (_spawnableCharacters[i].CompareTag(c1.tag))
				{
					i1 = i;
				}
				else if (_spawnableCharacters[i].CompareTag(c2.tag))
				{
					i2 = i;
				}
				else if (i1 != -1 && i2 != -1)
				{
					break;
				}
			}

			if (i1 == -1 || i2 == -1)
			{
				print("Something Wrong in comparing index");
			}

			return i1.CompareTo(i2);
		}

		private void AddSpawnableCharacters(Character[] characters)
		{
			if (characters == null) return;
			for (int i = 0; i < characters.Length; i++)
			{
				_spawnableCharacters.Add(characters[i]);
			}
		}

		private static CharacterFactory _instance;

		/// <summary>
		/// Singleton
		/// Return null if failed.
		/// </summary>
		public static CharacterFactory GetInstance()
		{
			if (_instance == null)
			{
				var characterFactories = FindObjectsOfType<CharacterFactory>();
				for (int i = 0; i < characterFactories.Length; i++)
				{
					if (characterFactories[i].enabled)
					{
						_instance = characterFactories[i];
						break;
					}
				}
			}

			// If there is no enabled one.
			return _instance;
		}
	}
}