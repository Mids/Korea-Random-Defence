using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KRD
{
	public class StoryPortal : MonoBehaviour
	{
		public Transform Exit;

		protected void OnTriggerEnter(Collider other)
		{
			if (other.gameObject.layer == LayerMask.NameToLayer("Character"))
			{
				var character = other.GetComponent<Character>();
				character.Warp(Exit.position);
				character.Stop();
			}
		}
	}
}