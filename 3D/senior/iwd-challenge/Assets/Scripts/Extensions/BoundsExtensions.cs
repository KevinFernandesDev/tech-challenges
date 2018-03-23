using UnityEngine;

public static class BoundsExtensions
{
    /// <summary>
    /// Adds the Bounds object (b) to the current Bounds in a cumulative
    /// function so the specified Bounds grows in size.
    /// </summary>
    /// <param name="boundsToAdd"></param>
    /// <returns></returns>
    public static Bounds GrowBounds(this Bounds a, Bounds boundsToAdd)
    {
        Vector3 max = Vector3.Max(a.max, boundsToAdd.max);
        Vector3 min = Vector3.Min(a.min, boundsToAdd.min);

        a = new Bounds((max + min) * 0.5f, max - min);
        return a;
    }

    /// <summary>
    /// Retrieve all 8 vertices position from a Bounds object (which is a cube)
    /// and return them in as a list.
    /// </summary>
    /// <returns></returns>
    public static Vector3[] GetVerticesPositions(this Bounds bounds)
    {
        return new Vector3[8]
        {
            bounds.min,
            bounds.max,
            new Vector3(bounds.min.x, bounds.min.y, bounds.max.z),
            new Vector3(bounds.min.x, bounds.max.y, bounds.min.z),
            new Vector3(bounds.max.x, bounds.min.y, bounds.min.z),
            new Vector3(bounds.min.x, bounds.max.y, bounds.max.z),
            new Vector3(bounds.max.x, bounds.min.y, bounds.max.z),
            new Vector3(bounds.max.x, bounds.max.y, bounds.max.z)
        };
    }

    /// <summary>
    /// Create a new Bounds object by getting all specific renderers from scene
    /// and adding their Bounds to the final Bounds object in a cumulative function.
    /// </summary>
    /// <param name="renderers"></param>
    /// <returns></returns>
    public static Bounds CreateGroupedBoundsFromRendererList(this Bounds bounds, Renderer[] renderers)
    {
        bounds = renderers[0].bounds;

        foreach (Renderer renderer in renderers)
        {
            bounds = bounds.GrowBounds(renderer.bounds);
        }
        return bounds;
    }
}