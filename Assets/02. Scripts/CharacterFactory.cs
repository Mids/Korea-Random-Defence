using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KRD
{
	public class CharacterFactory : MonoBehaviour
	{
		public Character[] SpawnableCharacters;
		private RTSManager _rtsManager;
		public List<Character> CurrentCharacters = new List<Character>();

		// Start is called before the first frame update
		void Start()
		{
			_rtsManager = GameObject.FindGameObjectWithTag("RTSManager").GetComponent<RTSManager>();
			CurrentCharacters.Add(Instantiate(SpawnableCharacters[0], transform));
		}

		// Update is called once per frame
		void Update()
		{
			if (_rtsManager.IsSelecting)
			{
				CheckSelection();
			}
		}

		void CheckSelection()
		{
			foreach (var character in CurrentCharacters)
			{
				if (_rtsManager.IsWithinSelectionBounds(character.gameObject))
				{
					character.Select();
				}
				else
				{
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