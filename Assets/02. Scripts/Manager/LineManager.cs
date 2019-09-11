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
			var playerLine = Lines[PlayerLineNumber];
			playerLine.GetComponentInChildren<EnemyFactory>().enabled = true;
			playerLine.GetComponentInChildren<CharacterFactory>().enabled = true;
			Camera.main.transform.transform.position = new Vector3(playerLine.transform.position.x, Camera.main.transform.transform.position.y, playerLine.transform.position.z - 20);
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