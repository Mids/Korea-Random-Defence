using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KRD
{
	public class CombinationButton : MonoBehaviour
	{
		public CombinationList Combination;
		public Character OwnerCharacter;

		private Button _button;
		private Text _text;

		// Start is called before the first frame update
		void Start()
		{
			_button = GetComponent<Button>();
			_text = GetComponentInChildren<Text>();
			_text.text = "";

			for (int j = 0; j < Combination.Characters.Count; j++)
			{
				if (j > 0) _text.text += " + ";

				_text.text += Combination.Characters[j].tag; // TODO: Change to use name not tag
			}

			_text.text += " = " + Combination.ResultCharacter.tag;
			_button.onClick.AddListener(OnClickListener);
		}

		private void OnClickListener()
		{
			FindObjectOfType<CombinationButtonManager>().RemoveAllCombinationButtons();
			FindObjectOfType<CharacterFactory>().RemoveCharacter(OwnerCharacter);
		}
	}
}