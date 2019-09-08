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

		public int ChanceLeft = 5;
		private const string RandomText = "Random";

		// Start is called before the first frame update
		void Start()
		{
			_button = GetComponent<Button>();
			_characterFactory = GameObject.FindObjectOfType<CharacterFactory>();
			_text = _button.GetComponentInChildren<Text>();
			ChangeButtonText();
		}

		public void SpawnCharacter()
		{
			if (ChanceLeft > 0)
			{
				ChanceLeft--;
				_characterFactory.SpawnRandomCharacter();
				ChangeButtonText();
            }
		}

		private void ChangeButtonText()
		{
			_text.text = RandomText + "\n(" + ChanceLeft + ")";
        }
	}
}