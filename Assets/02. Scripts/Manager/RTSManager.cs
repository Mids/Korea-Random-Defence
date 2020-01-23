using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

namespace KRD
{
	public class RTSManager : MonoBehaviour, IRTSManager
	{
		public bool IsSelecting = false;
		public bool GetSelecting() => IsSelecting;

		Vector3 mousePosition1;
		public int ScrollWidth => 15;
		public int ScrollSpeed => 20;

		void Update()
		{
			ScreenScrolling();
		}

		/// <summary>
		/// Screen scrolling
		/// TODO: set limitation of x and z axis
		/// </summary>
		private void ScreenScrolling()
		{
			float xpos = Input.mousePosition.x;
			float ypos = Input.mousePosition.y;
			Vector3 movement = Vector3.zero;

			//horizontal camera movement
			if (xpos >= 0 && xpos < ScrollWidth)
			{
				movement.x -= ScrollSpeed;
			}
			else if (xpos <= Screen.width && xpos > Screen.width - ScrollWidth)
			{
				movement.x += ScrollSpeed;
			}

			//vertical camera movement
			if (ypos >= 0 && ypos < ScrollWidth)
			{
				movement.z -= ScrollSpeed;
			}
			else if (ypos <= Screen.height && ypos > Screen.height - ScrollWidth)
			{
				movement.z += ScrollSpeed;
			}

			//make sure movement is in the direction the camera is pointing
			//but ignore the vertical tilt of the camera to get sensible scrolling
			movement = Camera.main.transform.TransformDirection(movement);

			//calculate desired camera position based on received input
			Vector3 origin = Camera.main.transform.position;
			Vector3 destination = origin;
			destination.x += movement.x;
			destination.z += movement.z;

			//if a change in position is detected perform the necessary update
			if (destination != origin)
			{
				Camera.main.transform.position = Vector3.MoveTowards(origin, destination, Time.deltaTime * ScrollSpeed);
			}
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