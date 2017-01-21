using UnityEngine;
using UnityEditor;

public class EditorTools {
    [MenuItem("Poseidon Tools/Set Random Rotation")]
    public static void RandomRotation() {
        if (Selection.transforms.Length > 0) {
            Undo.RecordObjects(Selection.transforms, "Setting random rotation");
        }

        foreach (var transform in Selection.transforms)
        {
            transform.localRotation = Random.rotationUniform;
        }
    }
}