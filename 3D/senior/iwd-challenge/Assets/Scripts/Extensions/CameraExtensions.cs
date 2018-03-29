using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CameraExtensions {

    /// <summary>
    /// Calculates the vertical field of view in radians
    /// </summary>
    /// <returns>float</returns>
	public static float GetVerticalFieldOfViewInRadians(this Camera camera)
    {
        return camera.fieldOfView * Mathf.Deg2Rad;
    }

    /// <summary>
    /// Calculates the horizontal field of view in radians from the vertical one
    /// </summary>
    /// <returns>float</returns>
    public static float GetHorizontalFieldOfViewInRadians(this Camera camera)
    {
        return 2f * Mathf.Atan(Mathf.Tan(camera.GetVerticalFieldOfViewInRadians() / 2f) * camera.aspect);
    }

    /// <summary>
    /// Get the smallest value between the vertical and horizontal fov
    /// </summary>
    /// <returns>float</returns>
    public static float GetFieldOfViewSmallestSideValue(this Camera camera)
    {
        return Mathf.Min(camera.GetVerticalFieldOfViewInRadians(), camera.GetHorizontalFieldOfViewInRadians());
    }

    /// <summary>
    /// Get the greatest value between the vertical and horizontal fov
    /// </summary>
    /// <returns>float</returns>
    public static float GetFieldOfViewGreatestSideValue(this Camera camera)
    {
        return Mathf.Max(camera.GetVerticalFieldOfViewInRadians(), camera.GetHorizontalFieldOfViewInRadians());
    }
}
