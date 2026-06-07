using UnityEngine;
using UnityEditor;

public class EditorMaterialChanger : MonoBehaviour
{
  public Material targetMaterial;

  internal void ChangeMaterials()
  {
    var renderers = GetComponentsInChildren<MeshRenderer>();
    foreach (var ren in renderers)
    {
      var mats = new Material[ren.sharedMaterials.Length];

      for (int i = 0; i < mats.Length; i++)
      {
        mats[i] = targetMaterial;
      }

      ren.sharedMaterials = mats;
    }
  }
}

#if UNITY_EDITOR
[CustomEditor(typeof(EditorMaterialChanger))]
public class EditorMaterialChangerHelper : Editor
{
  public override void OnInspectorGUI()
  {
    DrawDefaultInspector();

    EditorMaterialChanger _target = (EditorMaterialChanger)target;

    if (GUILayout.Button("Change all children materials"))
    {
      _target.ChangeMaterials();
    }
  }
}
#endif