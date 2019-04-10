using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KRD
{
	public class Character : MonoBehaviour
	{
		private Projector _selectionCircle;
		public bool IsSelected;

		// Start is called before the first frame update
		void Start()
		{
			_selectionCircle = GetComponentInChildren<Projector>();
			Deselect();
		}

		// Update is called once per frame
		void Update()
		{
		}

		public void Select()
		{
			IsSelected = true;
			_selectionCircle.enabled = true;
		}

		public void Deselect()
		{
			IsSelected = false;
			_selectionCircle.enabled = false;
		}
	}
}