using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KRD
{
	public class CommonButton : MonoBehaviour
	{
		private Button _button;
		private CharacterFactory _characterFactory;
		public Text CommonText;

		private Color _color = Color.white;

		//gangrae
		protected GameObject[] _luffy;
		protected GameObject[] _zoro;
		protected GameObject[] _nami;
		protected GameObject[] _usopp;
		protected GameObject[] _sanji;
		protected GameObject[] _chopper;
		protected GameObject[] _marinSword;
		protected GameObject[] _marinGun;
		protected GameObject[] _buggy;

		// Start is called before the first frame update
		void Start()
		{
			_button = GetComponent<Button>();
		}

		public void CommonActive()
		{
			_color = Color.white;
		}

		// Update is called once per frame
		void Update()
		{
			// counting common character number
			_luffy = GameObject.FindGameObjectsWithTag("Luffy");
			_zoro = GameObject.FindGameObjectsWithTag("Zoro");
			_nami = GameObject.FindGameObjectsWithTag("Nami");
			_usopp = GameObject.FindGameObjectsWithTag("Usopp");
			_sanji = GameObject.FindGameObjectsWithTag("Sanji");
			_chopper = GameObject.FindGameObjectsWithTag("Chopper");
			_marinSword = GameObject.FindGameObjectsWithTag("MarinSword");
			_marinGun = GameObject.FindGameObjectsWithTag("MarinGun");
			_buggy = GameObject.FindGameObjectsWithTag("Buggy");

			CommonText.text = "   Luffy   : " + _luffy.Length.ToString() + "  \n" +
			                  "   Zoro    : " + _zoro.Length.ToString() + "\n" +
			                  "   Nami    : " + _nami.Length.ToString() + "\n" +
			                  "   Usopp   : " + _usopp.Length.ToString() + "\n" +
			                  "   Sanji   : " + _sanji.Length.ToString() + "\n" +
			                  "  Chopper  : " + _chopper.Length.ToString() + "\n" +
			                  "MarinSword : " + _marinSword.Length.ToString() + "\n" +
			                  "MarinGun   : " + _marinGun.Length.ToString() + "\n" +
			                  "   Buggy   : " + _buggy.Length.ToString() + "\n";

			_color.a -= Time.deltaTime / 3;
			CommonText.color = _color;
			//RoundTimeText.text = "Round  " + (_gameRound + 1).ToString() + "    " + Mathf.Ceil(_roundTime).ToString();
		}
	}
}