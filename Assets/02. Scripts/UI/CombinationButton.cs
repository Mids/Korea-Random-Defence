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
			var characterFactory = FindObjectOfType<CharacterFactory>();
			bool isAllPrepared = true;
			bool[] isPrepared = new bool[Combination.Characters.Count];
			isPrepared[0] = true;

			// Check characters except owner character
			for (int i = 1; i < Combination.Characters.Count; i++)
			{
				isPrepared[i] = false;
				for (int j = 0; j < characterFactory.CurrentCharacters.Count; j++)
				{
					if (characterFactory.CurrentCharacters[j].tag == Combination.Characters[i].tag)
					{
						isPrepared[1] = true;
						break;
					}
				}
			}

			for (int i = 0; i < isPrepared.Length; i++)
			{
				if (!isPrepared[i])
				{
					isAllPrepared = false;
					break;
				}
			}

			// Every characters are here
			if (isAllPrepared)
			{
				FindObjectOfType<CombinationButtonManager>().RemoveAllCombinationButtons();

				characterFactory.SpawnCharacterByTag(Combination.ResultCharacter.tag, OwnerCharacter.transform.localPosition);
				characterFactory.RemoveCharacter(OwnerCharacter);

				for (int i = 0; i < Combination.Characters.Count; i++)
				{
					characterFactory.RemoveCharacterByTag(Combination.Characters[i].tag);
				}
			}
			else
			{
				print("COMBINATION NOT READY");
			}
		}
	}
}