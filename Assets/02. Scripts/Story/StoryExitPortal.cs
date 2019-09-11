using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KRD
{
	public class StoryExitPortal : StoryPortal
	{
		protected new void OnTriggerEnter(Collider other)
		{
			if (Exit == null)
			{
				Exit = FindObjectOfType<LineManager>().Lines[LineManager.PlayerLineNumber].transform;
			}

			base.OnTriggerEnter(other);
		}
	}
}