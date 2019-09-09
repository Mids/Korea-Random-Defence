using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stun : MonoBehaviour
{
	private GameObject[] _hitEnemies;
	private float[] _stun = new float[2];

	// Start is called before the first frame update
	void Start()
	{
		_stun[0] = 1.0f;
		_stun[1] = 100.0f;
	}

	void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.tag == "Enemy")
		{
			GameObject parentObj = transform.parent.gameObject;

			collider.SendMessage("ReceiveStun", _stun);
			Destroy(parentObj);
		}
	}
}