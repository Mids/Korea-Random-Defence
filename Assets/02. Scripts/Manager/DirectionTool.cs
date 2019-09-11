using Vector3 = UnityEngine.Vector3;

namespace KRD
{
	public enum Direction
	{
		North,
		South,
		West,
		East
	};

	public static class DirectionTool
	{
		/// <summary>
		/// Reverse Direction
		/// </summary>
		/// <param name="dir"> Input direction </param>
		/// <returns>Reversed Direction</returns>
		public static Direction CounterClockwise(Direction dir)
		{
			if (dir == Direction.East)
				dir = Direction.North;
			else if (dir == Direction.West)
				dir = Direction.South;
			else if (dir == Direction.North)
				dir = Direction.West;
			else if (dir == Direction.South)
				dir = Direction.East;

			return dir;
		}

		/// <summary>
		/// Get Direction Vector
		/// </summary>
		/// <param name="dir">Input Direction</param>
		/// <returns>Return Vector</returns>
		public static Vector3 GetDirectionVector(Direction dir)
		{
			Vector3 dv = Vector3.zero;
			if (dir == Direction.East)
				dv = Vector3.right;
			else if (dir == Direction.West)
				dv = Vector3.left;
			else if (dir == Direction.North)
				dv = Vector3.forward;
			else if (dir == Direction.South)
				dv = Vector3.back;

			return dv;
		}
	}
}