using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public static class Utility
{
    public static readonly Vector3 InvalidEndpoint = new(float.MaxValue, float.MaxValue, float.MaxValue);
    
#if UNITY_EDITOR
    public static void RefreshSceneView()
    {
        SceneView.RepaintAll();
        EditorApplication.QueuePlayerLoopUpdate();
    }
#endif

    public static void ProvideTransformArrayFromType<T>(string arrayName) where T : Component
    {
        T[] _array = Object.FindObjectsOfType<T>();
        Transform[] transforms = new Transform[_array.Length];
        for (int i = 0; i < _array.Length; i++) {
            transforms[i] = _array[i].transform;
        }
        ServiceLocator.Provide(arrayName, transforms);
    }
}
