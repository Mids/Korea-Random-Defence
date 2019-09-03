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

		// Start is called before the first frame update
		void Start()
		{
			_button = GetComponent<Button>();
			_characterFactory = GameObject.FindObjectOfType<CharacterFactory>();
		}

		public void SpawnCharacter()
		{
			_characterFactory.SpawnRandomCharacter();
		}
	}
}