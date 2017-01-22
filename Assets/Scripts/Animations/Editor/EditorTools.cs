using UnityEngine;
using UnityEditor;

public class EditorTools {
    [MenuItem("Poseidon Tools/Set Random Rotation/All axis")]
    public static void RandomRotationAll() {
        if (Selection.transforms.Length > 0) {
            Undo.RecordObjects(Selection.transforms, "random rotation");
        }

        foreach (var transform in Selection.transforms)
        {
            transform.localRotation = Random.rotationUniform;
        }
    }

    [MenuItem("Poseidon Tools/Set Random Rotation/X axis")]
    public static void RandomRotationX() {
        if (Selection.transforms.Length > 0) {
            Undo.RecordObjects(Selection.transforms, "random rotation around x");
        }

        foreach (var transform in Selection.transforms)
        {
            transform.localEulerAngles = new Vector3(Random.Range(0f, 360f), 0, 0);
        }
    }

    [MenuItem("Poseidon Tools/Set Random Rotation/Y axis")]
    public static void RandomRotationY() {
        if (Selection.transforms.Length > 0) {
            Undo.RecordObjects(Selection.transforms, "random rotation around y");
        }

        foreach (var transform in Selection.transforms)
        {
            transform.localEulerAngles = new Vector3(0, Random.Range(0f, 360f), 0);
        }
    }

    [MenuItem("Poseidon Tools/Set Random Rotation/Z axis")]
    public static void RandomRotationZ() {
        if (Selection.transforms.Length > 0) {
            Undo.RecordObjects(Selection.transforms, "random rotation around Z");
        }

        foreach (var transform in Selection.transforms)
        {
            transform.localEulerAngles = new Vector3(0, 0, Random.Range(0f, 360f));
        }
    }
}