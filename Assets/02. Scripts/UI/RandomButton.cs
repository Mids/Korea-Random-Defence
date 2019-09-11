using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KRD
{
	public class RandomButton : MonoBehaviour
	{
		private Button _button;
		private Text _text;
		private CharacterFactory _characterFactory;

		private int _chanceLeft = 5;
		private bool _isInitialized = false;

		public int ChanceLeft
		{
			get => _chanceLeft;
			set
			{
				_chanceLeft = value;
				ChangeButtonText();
			}
		}

		private const string RandomText = "Random";

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

			_button = GetComponent<Button>();
			_text = _button.GetComponentInChildren<Text>();
			ChangeButtonText();
			_isInitialized = true;
		}

		private void Update()
		{
			if (!_isInitialized)
			{
				Init();
				return;
			}
		}

		public void SpawnCharacter()
		{
			if (ChanceLeft > 0)
			{
				ChanceLeft--;
				_characterFactory.SpawnRandomCommonCharacter();
				ChangeButtonText();
			}
		}

		private void ChangeButtonText()
		{
			_text.text = RandomText + "\n(" + ChanceLeft + ")";
		}
	}
}