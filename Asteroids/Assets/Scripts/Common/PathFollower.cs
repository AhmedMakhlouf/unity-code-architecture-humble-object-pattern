using UnityEngine;

public class PathFollower : IPathFollower
{
    private Vector3[] points;
	private int index;

	public PathFollower(Vector3[] points)
	{
		this.points = points;
	}

	public Vector3 GetDistination(Vector3 position)
	{
		if (Vector3.Distance(position, points[index]) > 0.01f)
			return points[index];

		index = (index + 1) % points.Length;

		return points[index];
	}

	public bool ReachedFinalDistination(Vector3 position)
	{
		if (Vector3.Distance(position, points[points.Length - 1]) <= 0.01f)
			return true;

		return false;
	}
}
