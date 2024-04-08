using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// Written by Sebastian Lague for his Field of View visualisation series
/// https://www.youtube.com/watch?v=rQG9aUWarwE
/// 
/// This code was published under the MIT License
/// https://github.com/SebLague/Field-of-View/
/// </summary>

[CustomEditor (typeof (ViewCone))]
public class ViewConeEditor : Editor {

    void OnSceneGUI() {
        ViewCone viewCone = (ViewCone)target;
        Handles.color = Color.white;
        Handles.DrawWireArc (viewCone.transform.position, Vector3.up, Vector3.forward, 360, viewCone.viewRadius);
        Vector3 viewAngleA = viewCone.DirFromAngle (-viewCone.viewAngle / 2, false);
        Vector3 viewAngleB = viewCone.DirFromAngle (viewCone.viewAngle / 2, false);

        Handles.DrawLine (viewCone.transform.position, viewCone.transform.position + viewAngleA * viewCone.viewRadius);
        Handles.DrawLine (viewCone.transform.position, viewCone.transform.position + viewAngleB * viewCone.viewRadius);

        Handles.color = Color.red;
        foreach (Transform visibleTarget in viewCone.visibleTargets) {
            Handles.DrawLine (viewCone.transform.position, visibleTarget.position);
        }
    }

}
