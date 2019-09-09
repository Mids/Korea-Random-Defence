using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KRD
{
	public class CombinationButtonManager : MonoBehaviour
	{
		public CombinationButton ButtonPrefab;
		private bool _isCombinationKeyPressed = false;
		private CharacterFactory _characterFactory;
		private List<CombinationButton> _buttons;

		// Start is called before the first frame update
		void Start()
		{
			_characterFactory = FindObjectOfType<CharacterFactory>();
			_buttons = new List<CombinationButton>();
		}

		// Update is called once per frame
		void Update()
		{
			var character = _characterFactory.MainCharacter;
			if (character == null) return;

			if (Input.GetButtonDown("Combination"))
			{
				_isCombinationKeyPressed = true;

				// Generate Buttons
				for (int i = 0; i < character.CombinationLists.Count; i++)
				{
					var combination = character.CombinationLists[i];
					var button = Instantiate(ButtonPrefab);
					button.transform.parent = transform;
					button.transform.localPosition = Vector3.up * 100 * (i + 1);
					button.GetComponentInChildren<Text>().text = "";


					for (int j = 0; j < combination.Characters.Count; j++)
					{
						if (j > 0) button.GetComponentInChildren<Text>().text += " + ";

						button.GetComponentInChildren<Text>().text += combination.Characters[j].tag; // TODO: Change to use name not tag
					}

					button.GetComponentInChildren<Text>().text += " = " + combination.ResultCharacter.tag;
				}
			}
			else if (Input.GetButtonUp("Combination"))
			{
				_isCombinationKeyPressed = false;
			}

			if (_isCombinationKeyPressed)
			{
				var camera = Camera.main;
				var characterTransform = _characterFactory.MainCharacter.transform;
				transform.position = camera.WorldToScreenPoint(characterTransform.position);
			}
		}
	}
}