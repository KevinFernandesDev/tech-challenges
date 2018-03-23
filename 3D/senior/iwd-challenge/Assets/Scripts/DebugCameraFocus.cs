using System;
using UnityEngine;

public class DebugCameraFocus : AbstractCameraFocus
{
    [SerializeField] private bool IsEditorDebugGizmosEnabled = true;
    [SerializeField] private bool IsEditorDebugCameraActive = true;

    void OnDrawGizmos()
    {
        // Only run when game is not in play mode to not impact game performance
        if (Application.isPlaying) return;

        // Create a new Bounds object combining all bounds from selected scene objects
        Bounds bounds = Bounds.CreateGroupedBoundsFromRendererList(FindObjectsOfType<MeshRenderer>());

        if (IsEditorDebugGizmosEnabled)
        {
            // Create vertices position list from the generated Bounds
            Vector3[] boundsVerticesPositions = bounds.GetVerticesPositions();

            // Draw all gizmos in editor viewport
            DrawDebugGizmosToViewport(bounds, boundsVerticesPositions);
        };

        if (!IsEditorDebugCameraActive) return;

        CalculateOrthographicSizeToFrameTarget(bounds);
        CalculateCameraPositionToFrameTarget(bounds);
        MoveCamera();
    }

    private void DrawDebugGizmosToViewport(Bounds bounds, Vector3[] boundsVerticesPositions)
    {
        // Draw cube gizmo in viewport representing the calculated Bounds
        Gizmos.DrawCube(bounds.center, bounds.size);

        // Draw lines pointing from the camera to each of the Bounds vertices
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, boundsVerticesPositions[0]);
        Gizmos.DrawLine(transform.position, boundsVerticesPositions[1]);
        Gizmos.DrawLine(transform.position, boundsVerticesPositions[2]);
        Gizmos.DrawLine(transform.position, boundsVerticesPositions[3]);
        Gizmos.DrawLine(transform.position, boundsVerticesPositions[4]);
        Gizmos.DrawLine(transform.position, boundsVerticesPositions[5]);
        Gizmos.DrawLine(transform.position, boundsVerticesPositions[6]);
        Gizmos.DrawLine(transform.position, boundsVerticesPositions[7]);

        // Draw red line from the camera to the center of the generated Bounds
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, bounds.center);
    }
}
