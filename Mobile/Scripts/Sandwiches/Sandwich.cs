using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
//완성샘플의 종류 하나
public class Sandwich : MonoBehaviour
{
  public string recipe;


  void Awake()
  {
    recipe = name; //레시피에 이름을 담음

    Reset(); //맨처음 화면의 레시피 안보이게 함
  }

  void OnDisable()
  {
    Reset();
  }
    //오브젝트의 자식의 위치 값을 받아옴
  public Transform GetIngredient(int index)
  {
    return transform.GetChild(index);
  }
    //오브젝트를 비활성화 시킨다
  public void Reset()
  {
    foreach (Transform t in transform)
    {
      t.gameObject.SetActive(false);
    }
  }

  public void RemoveTop()
  {
    for (int i = transform.childCount - 1; i >= 0; i--)
    {
      var child = transform.GetChild(i).gameObject;

      if (child.activeSelf == true)
      {
        child.SetActive(false);
        break;
      }
    }
  }
    //음식을 제대로 놓았을 떄 해당하는 위치의 오브젝트를 활성화 시킨다.
  public void BuildNext()
  {
    foreach (Transform t in transform)
    {
      if (t.gameObject.activeSelf == false)
      {
        t.gameObject.SetActive(true);
        break;
      }
    }
  }
    //완료했을 떄, 이 샌드위치의 오브젝트를 활성화 시킴
  public void Complete()
  {
    foreach (Transform t in transform)
    {
      t.gameObject.SetActive(true);
    }
  }
}

#if UNITY_EDITOR
[CustomEditor(typeof(Sandwich))]
class SandwichEditor : Editor
{
  public override void OnInspectorGUI()
  {
    DrawDefaultInspector();

    Sandwich sandwich = (Sandwich)target;

    if (GUILayout.Button("Remove All"))
      sandwich.Reset();

    if (GUILayout.Button("Remove Top Ingredient"))
      sandwich.RemoveTop();

    if (GUILayout.Button("Build Next Ingredient"))
      sandwich.BuildNext();

    if (GUILayout.Button("Build All"))
      sandwich.Complete();
  }
}
#endif
