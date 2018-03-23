using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public abstract class AbstractCameraFocus : MonoBehaviour
{
    public float MovementSmoothing = 25f;
    internal Bounds Bounds { get; set; }
    internal Vector3 TargetPosition { get; set; }

    private Camera _camera { get { return GetComponent<Camera>(); } }
    private Vector3 _currentVelocity = Vector3.zero;

    /// <summary>
    /// Move the camera over time to the target position defined by an object
    /// </summary>
    internal virtual void MoveCamera()
    {
        transform.localPosition = Vector3.SmoothDamp(transform.position, TargetPosition, ref _currentVelocity, MovementSmoothing * Time.deltaTime);
    }

    /// <summary>
    /// Calculate ortographic size of camera from bounds values
    /// </summary>
    internal virtual void CalculateOrthographicSizeToFrameTarget(Bounds bounds)
    {
        _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, Vector3.Distance(bounds.min, bounds.max) / 2f, MovementSmoothing * Time.deltaTime);
    }

    /// <summary>
    /// Calculate the best position for the camera to have the bounds
    /// object in its view fustrum
    /// </summary>
    /// <param name="bounds"></param>
    internal virtual void CalculateCameraPositionToFrameTarget(Bounds bounds)
    {
        // Calculate distance needed to frame the Bounds object in the camera view
        var distanceFactor = (Vector3.Distance(bounds.min, bounds.max) * 0.5f) / Mathf.Abs(Mathf.Sin(_camera.fieldOfView * Mathf.Deg2Rad / 2));

        // Create the target position where the camera should move to have all objects in view
        Vector3 newTargetPos = bounds.center;
        TargetPosition = (newTargetPos - (transform.forward * distanceFactor));
    }

    
}
