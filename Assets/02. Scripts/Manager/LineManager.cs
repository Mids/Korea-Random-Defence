using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KRD
{
	public class LineManager : MonoBehaviour
	{
		public int PlayerLineNumber = 0;

		public GameObject[] Lines = new GameObject[4];

		private bool _isInitialized = false;

		// Start is called before the first frame update
		private void Init()
		{
			var PlayerLine = Lines[PlayerLineNumber];
			PlayerLine.GetComponentInChildren<EnemyFactory>().enabled = true;
			PlayerLine.GetComponentInChildren<CharacterFactory>().enabled = true;
			_isInitialized = true;
		}

		// Update is called once per frame
		void Update()
		{
			if (!_isInitialized)
			{
				Init();
				return;
			}
		}
	}
}
