using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
  public static SceneLoader Instance;

  List<string> _scenes;


  void Awake()
  {
    if (Instance == null)
    {
      Instance = this;
      DontDestroyOnLoad(this);
    }
    else if (this != Instance)
    {
      Destroy(this.gameObject);
    }
  }

  void Start()
  {
    _scenes = new List<string>();

    Reset();

    foreach (var s in _scenes) Debug.Log(s);
  }

  void Reset()
  {
    _scenes.Clear();
    for (int i = 1; i < SceneManager.sceneCountInBuildSettings; i++)
      _scenes.Add(GetSceneNameByBuildIndex(i));
  }

  private string GetSceneNameByBuildIndex(int num)
  {
    string pathToScene = SceneUtility.GetScenePathByBuildIndex(num);
    string sceneName = System.IO.Path.GetFileNameWithoutExtension(pathToScene);

    return sceneName;
  }

  // index -1 means load next scene in build settings
  public void LoadScene(int index = -1)
  {
    if (_scenes.Count > 0)
    {
      if (index == -1)
      {
        index = Random.Range(0, _scenes.Count);
        SceneManager.LoadScene(_scenes[index]);

        _scenes.RemoveAt(index);
      }
      else if (index == 0)
      {
        Reset();
        SceneManager.LoadScene(0);
      }
      else
      {
        _scenes.RemoveAt(index - 1);
        SceneManager.LoadScene(index);
      }
    }
    else
    {
      Reset();
      SceneManager.LoadScene(0);
    }

    foreach (var s in _scenes) Debug.Log(s);
  }
}
