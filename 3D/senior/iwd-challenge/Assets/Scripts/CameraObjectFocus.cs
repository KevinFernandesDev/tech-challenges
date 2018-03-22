using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraObjectFocus : MonoBehaviour
{

    public bool ShowBoundingBoxGizmo = false;
    public bool IsDebugCameraPositionActive = true;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnDrawGizmos()
    {
        if (!ShowBoundingBoxGizmo) return;
        MeshRenderer[] rends = FindObjectsOfType<MeshRenderer>();
        Bounds bounds = rends[0].GetComponent<Renderer>().bounds;

        foreach (MeshRenderer rend in rends)
        {
            bounds = bounds.GrowBounds(rend.GetComponent<Renderer>().bounds);
        }
        List<Vector3> boundPointsList = new List<Vector3>();
        Vector3 center = bounds.center;
        Gizmos.DrawCube(bounds.center, bounds.size);

        Vector3 boundPoint1 = bounds.min;
        Vector3 boundPoint2 = bounds.max;
        Vector3 boundPoint3 = new Vector3(boundPoint1.x, boundPoint1.y, boundPoint2.z);
        Vector3 boundPoint4 = new Vector3(boundPoint1.x, boundPoint2.y, boundPoint1.z);
        Vector3 boundPoint5 = new Vector3(boundPoint2.x, boundPoint1.y, boundPoint1.z);
        Vector3 boundPoint6 = new Vector3(boundPoint1.x, boundPoint2.y, boundPoint2.z);
        Vector3 boundPoint7 = new Vector3(boundPoint2.x, boundPoint1.y, boundPoint2.z);
        Vector3 boundPoint8 = new Vector3(boundPoint2.x, boundPoint2.y, boundPoint1.z);

        boundPointsList.Add(boundPoint1);
        boundPointsList.Add(boundPoint2);
        boundPointsList.Add(boundPoint3);
        boundPointsList.Add(boundPoint4);
        boundPointsList.Add(boundPoint5);
        boundPointsList.Add(boundPoint6);
        boundPointsList.Add(boundPoint7);
        boundPointsList.Add(boundPoint8);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, boundPoint1);
        Gizmos.DrawLine(transform.position, boundPoint2);
        Gizmos.DrawLine(transform.position, boundPoint3);
        Gizmos.DrawLine(transform.position, boundPoint4);
        Gizmos.DrawLine(transform.position, boundPoint5);
        Gizmos.DrawLine(transform.position, boundPoint6);
        Gizmos.DrawLine(transform.position, boundPoint7);
        Gizmos.DrawLine(transform.position, boundPoint8);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, bounds.center);
        Gizmos.DrawCube(bounds.min, Vector3.one);
        Gizmos.DrawCube(bounds.max, Vector3.one);

        if (!IsDebugCameraPositionActive) return;

        if (Camera.main.orthographic)
        {
            Camera.main.orthographicSize = Vector3.Distance(bounds.min, bounds.max);
        }

        var distanceFactor = (Vector3.Distance(bounds.min, bounds.max) * 0.5f) / Mathf.Abs(Mathf.Sin(Camera.main.fieldOfView * Mathf.Deg2Rad / 2));

        Vector3 newTargetPos = bounds.center;
        newTargetPos = newTargetPos - (transform.forward * distanceFactor);

        transform.localPosition = Vector3.MoveTowards(transform.position, newTargetPos, 50f * Time.deltaTime);
    }

}
