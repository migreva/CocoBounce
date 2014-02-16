using System.Collections.Generic;
using Microsoft.Xna.Framework;
using UnityEngine;

/// <summary> Version with RageFarseer enabled </summary>
public class RageFarseerAdapter : MonoBehaviour {

    public static bool FarseerEnabled = true;

    public static void DestroyFarseerPhysics(RageSpline spline) {
#if !UNITY_FLASH
        var fsConcave = spline.GetComponent<FSConcaveShapeComponent>();
        if (fsConcave != null) DestroyImmediate(fsConcave);
        var fsBody = spline.GetComponent<FSBodyComponent>();
        if (fsBody != null) DestroyImmediate(fsBody);
        spline.DestroyFarseerNow = true;
#endif
    }

    public static void UpdateFarseerPhysics(RageSpline spline) {
#if !UNITY_FLASH
        var fsConcave = spline.GetComponent<FSConcaveShapeComponent>();
        if (fsConcave == null) fsConcave = spline.gameObject.AddComponent<FSConcaveShapeComponent>();
        var fsBody = spline.GetComponent<FSBodyComponent>();
        if (fsBody == null) spline.gameObject.AddComponent<FSBodyComponent>();
        fsConcave.PointInput = FSShapePointInput.Vector2List;
        var points = new List<Vector2>();
        if (spline.FarseerPhysicsPointsOnly)
            for (int i = 0; i < spline.GetPointCount(); i++)
                points.Add(spline.GetPosition(i));
        else {
            int splitCount = spline.LockPhysicsToAppearence ? spline.GetVertexCount() : spline.GetPhysicsColliderCount();
            for (int i = 0; i < splitCount; i++) {
                float splinePos = (float)i / (float)splitCount;
                Vector3 normal = spline.GetNormal(splinePos);
                points.Add(spline.GetPosition(splinePos) + normal * spline.GetPhysicsNormalOffset());
            }
        }
        fsConcave.PointsCoordinates = points.ToArray();
        fsConcave.ConvertToConvex();
#endif
    }

    public static bool UpdateFarseerPositionCheck(GameObject reference, GameObject target) {
        bool hasFarseer;
#if !UNITY_FLASH
        var fsBody = target.GetComponent<FSBodyComponent>();
        hasFarseer = fsBody != null && fsBody.body != null;
        if (hasFarseer)
            fsBody.body.Position = new FVector2(reference.transform.position.x, reference.transform.position.y);
#endif
        return hasFarseer;
    }

    public static bool UpdateFarseerRotationCheck(GameObject reference, GameObject target) {
    bool hasFarseer;
#if !UNITY_FLASH
        var fsBody = target.GetComponent<FSBodyComponent>();
        hasFarseer = fsBody != null && fsBody.body != null;
        if (hasFarseer)
            fsBody.body.Rotation = reference.transform.rotation.eulerAngles.z * Mathf.Deg2Rad;
#endif
        return hasFarseer;
    }

}
