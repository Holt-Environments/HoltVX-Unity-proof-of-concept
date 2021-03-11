using UnityEngine;
using UnityEditor;

public static class SetFBXCollider
{
    [MenuItem("GameObject/Set FBX Colliders", true, 10)]
    static bool SetFBXCollidersValidate()
    {
        if(Selection.transforms.Length > 1){
            return false;
        }

        string[] guid = AssetDatabase.FindAssets(Selection.activeTransform.name);

        if(guid.Length == 0){
            return false;
        }  

        return true;
    }

    [MenuItem("GameObject/Set FBX Colliders")]
    static void SetFBXColliders() {

        Transform selected = Selection.activeTransform;
        string[] guid = AssetDatabase.FindAssets(selected.name);
        Object[] objects = AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GUIDToAssetPath(guid[0]));

        recursiveColliderAdd(selected, objects);
    }

    static void recursiveColliderAdd(Transform element, Object[] objects)
    {
        foreach (Transform child in element)
        {
            recursiveColliderAdd(child, objects);
        }

        if(element.childCount == 0)
        {
            for (int i = 0; i < objects.Length; i++)
            {
                if (objects[i] is Mesh){
                    if(objects[i].name == element.name)
                    {
                        MeshCollider meshCollider = element.gameObject.AddComponent<MeshCollider>();
                        meshCollider.convex = true;
                        meshCollider.sharedMesh = (Mesh) objects[i];
                    }
                }
            }
        }
    }
}

