using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace KRD
{
	public class HPBar : MonoBehaviour
	{
		// Update is called once per frame
		void Update()
		{
			transform.eulerAngles = Camera.main.transform.eulerAngles;
		}
	}
}