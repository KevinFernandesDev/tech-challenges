using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DebugCameraFocus))]
public class CameraFocus : AbstractCameraFocus
{
    /// Every function here could be put in Update for realtime changes.
    /// For performance reasons, the script only calculates
    /// and moves once in-game.
    public void Start()
    { 
        // Create a new Bounds object combining all bounds from selected scene objects
        Bounds bounds = Bounds.CreateGroupedBoundsFromRendererList(FindObjectsOfType<MeshRenderer>());

        CalculateOrthographicSizeToFrameTarget(bounds);
        CalculateCameraPositionToFrameTarget(bounds);
    }

    public void Update()
    {
        MoveCamera();
    }
}