using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KRD
{
	public class Aura : MonoBehaviour
	{
		public float[] _debuff;

		// Start is called before the first frame update
		void Start()
		{
			_debuff = new float[2];
			//0 = speed reduction, 1 = ambition
			_debuff[0] = 0.05f;
			_debuff[1] = 10.0f;
		}

		// Update is called once per frame
		void Update()
		{
		}

		void OnTriggerStay(Collider collider)
		{
			if (collider.gameObject.tag == "Enemy")
			{
				//TODO : add deltaTime (because of Frame issue)
				collider.SendMessage("ReceiveAura", _debuff);
			}
		}

		void OnTriggerExit(Collider collider)
		{
			if (collider.gameObject.tag == "Enemy")
			{
				collider.SendMessage("ReleaseAura");
			}
		}
	}
}