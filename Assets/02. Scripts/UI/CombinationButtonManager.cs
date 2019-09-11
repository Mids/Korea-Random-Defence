using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
		private Character _targetCharacter = null;
		private bool _isInitialized = false;

		// Start is called before the first frame update
		private void Init()
		{
			var characterFactories = FindObjectsOfType<CharacterFactory>();
			for (int i = 0; i < characterFactories.Length; i++)
			{
				if (characterFactories[i].enabled)
				{
					_characterFactory = characterFactories[i];
					break;
				}
			}

			if (_characterFactory == null)
			{
				return;
			}

			_buttons = new List<CombinationButton>();
			_isInitialized = true;
		}

		// Update is called once per frame
		void Update()
		{
			if (!_isInitialized)
			{
				// not started yet
				Init();
				return;
			}

			if (_characterFactory.MainCharacter == null) return;

			if (Input.GetButtonDown("Combination"))
			{
				_targetCharacter = _characterFactory.MainCharacter;
				_isCombinationKeyPressed = true;

				// Generate Buttons
				for (int i = 0; i < _targetCharacter.CombinationLists.Count; i++)
				{
					var combination = _targetCharacter.CombinationLists[i];
					CombinationButton button = Instantiate(ButtonPrefab);
					button.transform.SetParent(transform);
					button.transform.localPosition = Vector3.up * 90 * (i + 1);
					button.Combination = combination;
					button.OwnerCharacter = _targetCharacter;
				}
			}
			else if (Input.GetButtonUp("Combination"))
			{
				_targetCharacter = null;
				_isCombinationKeyPressed = false;
				RemoveAllCombinationButtons();
			}

			if (_isCombinationKeyPressed)
			{
				var camera = Camera.main;
				var characterTransform = _characterFactory.MainCharacter.transform;
				transform.position = camera.WorldToScreenPoint(characterTransform.position);
			}
		}

		public void RemoveAllCombinationButtons()
		{
			for (int i = GetComponentsInChildren<CombinationButton>().Length; i > 0; i--)
			{
				Destroy(GetComponentsInChildren<CombinationButton>()[i - 1].gameObject);
			}
		}
	}
}