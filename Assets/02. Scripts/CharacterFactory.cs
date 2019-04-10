using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace KRD
{
	public class CharacterFactory : MonoBehaviour
	{
		public Character[] SpawnableCharacters;
		private RTSManager _rtsManager;
		public List<Character> CurrentCharacters = new List<Character>();
		public List<Character> SelectedCharacters = new List<Character>();

		// Start is called before the first frame update
		void Start()
		{
			_rtsManager = GameObject.FindGameObjectWithTag("RTSManager").GetComponent<RTSManager>();
			CurrentCharacters.Add(Instantiate(SpawnableCharacters[0], transform));
		}

		// Update is called once per frame
		void Update()
		{
			// Select units if player is selecting
			if (_rtsManager.IsSelecting)
			{
				CheckSelection();
				return;
			}

			// If we press the right mouse button, let the characters go
			if (Input.GetMouseButton(1))
			{
				MoveFormation();
			}
		}

		/// <summary>
		/// Move selected characters as a formation
		/// </summary>
		void MoveFormation()
		{
			foreach (var character in SelectedCharacters)
			{
				var nav = character.GetComponent<NavMeshAgent>();
				var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

				if (Physics.Raycast(ray, out var hit))
				{
					// TODO: Keep the formation
					nav.destination = hit.point;
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
	}
}