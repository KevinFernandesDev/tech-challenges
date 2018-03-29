using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public abstract class AbstractCameraFocus : MonoBehaviour
{
    public float MovementSmoothing = 25f;
    public float OrthographicMargin = 3f;

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
        // Compute best fit distance from bounding box
        if (_camera.aspect >= 1f)
        {
            _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, (bounds.size.magnitude - Mathf.Sqrt(bounds.extents.magnitude) + OrthographicMargin) / 2f, MovementSmoothing * Time.deltaTime);
        }
        else
        {
            _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, (bounds.size.magnitude - Mathf.Sqrt(bounds.extents.magnitude) + OrthographicMargin) / 2f / _camera.aspect, MovementSmoothing * Time.deltaTime);
        }
        _camera.ResetAspect();
    }

    /// <summary>
    /// Calculate the best position for the camera to have the bounds
    /// object in its view fustrum
    /// </summary>
    /// <param name="bounds"></param>
    internal virtual void CalculateCameraPositionToFrameTarget(Bounds bounds)
    {
        // Get the smallest value between the vertical and horizontal fov
        var radiansAngle = _camera.GetFieldOfViewSmallestSideValue();

        // Calculate distance needed to frame the Bounds object in the camera view
        var distanceFactor = Vector3.Distance(bounds.center, bounds.max) / (Mathf.Sin(radiansAngle / 2f));

        // Create the target position where the camera should move to have all objects in view
        Vector3 newTargetPos = bounds.center;
        TargetPosition = newTargetPos - (transform.forward * distanceFactor);
    }


}
