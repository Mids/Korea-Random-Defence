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

		protected GameObject[] Luffy;
		protected GameObject[] Zoro;
		protected GameObject[] Nami;
		protected GameObject[] Usopp;
		protected GameObject[] Sanji;
		protected GameObject[] Chopper;
		protected GameObject[] MarinSword;
		protected GameObject[] MarinGun;
		protected GameObject[] Buggy;

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
			Luffy = GameObject.FindGameObjectsWithTag("Luffy");
			Zoro = GameObject.FindGameObjectsWithTag("Zoro");
			Nami = GameObject.FindGameObjectsWithTag("Nami");
			Usopp = GameObject.FindGameObjectsWithTag("Usopp");
			Sanji = GameObject.FindGameObjectsWithTag("Sanji");
			Chopper = GameObject.FindGameObjectsWithTag("Chopper");
			MarinSword = GameObject.FindGameObjectsWithTag("MarinSword");
			MarinGun = GameObject.FindGameObjectsWithTag("MarinGun");
			Buggy = GameObject.FindGameObjectsWithTag("Buggy");

			CommonText.text = "   Luffy   : " + Luffy.Length.ToString() + "  \n" +
			                  "   Zoro    : " + Zoro.Length.ToString() + "\n" +
			                  "   Nami    : " + Nami.Length.ToString() + "\n" +
			                  "   Usopp   : " + Usopp.Length.ToString() + "\n" +
			                  "   Sanji   : " + Sanji.Length.ToString() + "\n" +
			                  "  Chopper  : " + Chopper.Length.ToString() + "\n" +
			                  "MarinSword : " + MarinSword.Length.ToString() + "\n" +
			                  "MarinGun   : " + MarinGun.Length.ToString() + "\n" +
			                  "   Buggy   : " + Buggy.Length.ToString() + "\n";

			_color.a -= Time.deltaTime / 3;
			CommonText.color = _color;
			//RoundTimeText.text = "Round  " + (_gameRound + 1).ToString() + "    " + Mathf.Ceil(_roundTime).ToString();
		}
	}
}