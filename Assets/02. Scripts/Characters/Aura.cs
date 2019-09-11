using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KRD
{
	public class Aura : MonoBehaviour
	{
		public float[] _debuff;

		// _auraDuration = real aura active duration, _calcuateDuration = temp valiation to caculate aura time
		// _ auraActive = true : aura on false : aura off
		private float _auraDuration;
		private float _caculateDuration;
		private bool _auraActive;

		// Start is called before the first frame update
		void Start()
		{
			_debuff = new float[2];
			//0 = speed reduction, 1 = ambition, 2 = aura duration
			_debuff[0] = 0.05f;
			_debuff[1] = 5.0f;
			_auraDuration = 0.05f;
			_auraActive = true;
		}

		// Update is called once per frame
		void Update()
		{
			_caculateDuration -= Time.deltaTime;
			if (_caculateDuration < 0)
			{
				_auraActive = true;
				_caculateDuration = _auraDuration;
			}
			else
			{
				_auraActive = false;
			}
		}

		void OnTriggerStay(Collider collider)
		{
			if (collider.gameObject.tag == "Enemy")
			{
				if (_auraActive)
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