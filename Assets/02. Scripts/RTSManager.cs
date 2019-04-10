﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KRD
{
	public class RTSManager : MonoBehaviour
	{
		public bool IsSelecting = false;
		Vector3 mousePosition1;

		void Update()
		{
		}

		public void CheckSelecting()
		{
			// If we press the left mouse button, save mouse location and begin selection
			if (Input.GetMouseButtonDown(0))
			{
				IsSelecting = true;
				mousePosition1 = Input.mousePosition;
			}

			// If we let go of the left mouse button, end selection
			if (Input.GetMouseButtonUp(0))
				IsSelecting = false;
		}

		void OnGUI()
		{
			if (IsSelecting)
			{
				// Create a rect from both mouse positions
				var rect = RTSTool.GetScreenRect(mousePosition1, Input.mousePosition);
				RTSTool.DrawScreenRect(rect, new Color(0.8f, 0.8f, 0.95f, 0.25f));
				RTSTool.DrawScreenRectBorder(rect, 2, new Color(0.8f, 0.8f, 0.95f));
			}
		}

		/// <summary>
		/// Check if it is in boundary
		/// </summary>
		/// <param name="gameObject"></param>
		/// <returns></returns>
		public bool IsWithinSelectionBounds(GameObject gameObject)
		{
			if (!IsSelecting)
				return false;

			var camera = Camera.main;
			var viewportBounds =
				RTSTool.GetViewportBounds(camera, mousePosition1, Input.mousePosition);

			return viewportBounds.Contains(
				camera.WorldToViewportPoint(gameObject.transform.position));
		}
	}
}