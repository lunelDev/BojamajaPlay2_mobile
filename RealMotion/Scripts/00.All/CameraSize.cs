using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSize : MonoBehaviour
{
    /*   private void Awake()
       {
           SetupAndroidTheme(ToARGB(Color.black), ToARGB(Color.black));
       }
       public static void SetupAndroidTheme(int primaryARGB, int darkARGB, string label = null)
       {
   #if UNITY_ANDROID && !UNITY_EDITOR
       label = label ?? Application.productName;
       Screen.fullScreen = false;
       AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
       activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
       {
           AndroidJavaClass layoutParamsClass = new AndroidJavaClass("android.view.WindowManager$LayoutParams");
           int flagFullscreen = layoutParamsClass.GetStatic<int>("FLAG_FULLSCREEN");
           int flagNotFullscreen = layoutParamsClass.GetStatic<int>("FLAG_FORCE_NOT_FULLSCREEN");
           int flagDrawsSystemBarBackgrounds = layoutParamsClass.GetStatic<int>("FLAG_DRAWS_SYSTEM_BAR_BACKGROUNDS");
           AndroidJavaObject windowObject = activity.Call<AndroidJavaObject>("getWindow");
           windowObject.Call("clearFlags", flagFullscreen);
           windowObject.Call("addFlags", flagNotFullscreen);
           windowObject.Call("addFlags", flagDrawsSystemBarBackgrounds);
           int sdkInt = new AndroidJavaClass("android.os.Build$VERSION").GetStatic<int>("SDK_INT");
           int lollipop = 21;
           if (sdkInt > lollipop)
           {
               windowObject.Call("setStatusBarColor", darkARGB);
               string myName = activity.Call<string>("getPackageName");
               AndroidJavaObject packageManager = activity.Call<AndroidJavaObject>("getPackageManager");
               AndroidJavaObject drawable = packageManager.Call<AndroidJavaObject>("getApplicationIcon", myName);
               AndroidJavaObject taskDescription = new AndroidJavaObject("android.app.ActivityManager$TaskDescription", label, drawable.Call<AndroidJavaObject>("getBitmap"), primaryARGB);
               activity.Call("setTaskDescription", taskDescription);
           }
       }));
   #endif
       }

       public static int ToARGB(Color color)
       {
           Color32 c = (Color32)color;
           byte[] b = new byte[] { c.b, c.g, c.r, c.a };
           return System.BitConverter.ToInt32(b, 0);
       }
    */
    // 고정해상도를 적용하고 남는 부분(Letterbox)에 사용될 게임오브젝트(Prefab)
    /*  public GameObject m_objBackScissor;

      void Awake()
      {
          // 시작시 한번 실행(게임 실행중에 해상도가 변경되면 다시 호출)
          UpdateResolution();
      }


      void UpdateResolution()
      {
          // 프로젝트 내에 있는 모든 카메라 얻어오기
          Camera[] objCameras = Camera.allCameras;

          // 비율 구하기(16:9 기준)
          //width 9, height 16
          float fResolutionX = Screen.width / 16f;
          float fResolutionY = Screen.height / 9f;

          // X가 Y보가 큰 경우는 화면이 가로로 놓인 경우
          if (fResolutionX > fResolutionY)
          {
              // 종횡비(Aspect Ratio) 구하기
              // 16:9의 경우 1.77:1
              float fValue = (fResolutionX - fResolutionY) * 0.5f;
              fValue = fValue / fResolutionX;

              // 위에서 구한 종횡비를 기준으로 카메라의 뷰포트를 재설정
              // 정규화된 좌표라는걸 잊으면 안됨!
              foreach (Camera obj in objCameras)
              {
                  obj.rect = new Rect(((Screen.width * fValue) / Screen.width) + (obj.rect.x * (1.0f - (2.0f * fValue))),
                                      obj.rect.y,
                                      obj.rect.width * (1.0f - (2.0f * fValue)),
                                      obj.rect.height);
              }


              // 왼쪽에 들어갈 레터박스를 생성하고 위치지정
              GameObject objLeftScissor = (GameObject)Instantiate(m_objBackScissor);
              objLeftScissor.GetComponent<Camera>().rect = new Rect(0, 0, (Screen.width * fValue) / Screen.width, 1.0f);

              // 오른쪽 레터박스
              GameObject objRightScissor = (GameObject)Instantiate(m_objBackScissor);
              objRightScissor.GetComponent<Camera>().rect = new Rect((Screen.width - (Screen.width * fValue)) / Screen.width,
                                                                     0,
                                                                     (Screen.width * fValue) / Screen.width,
                                                                     1.0f);


              // 생성된 두 레터박스를 자식으로 추가
              objLeftScissor.transform.parent = gameObject.transform;
              objRightScissor.transform.parent = gameObject.transform;
          }
          // 화면이 세로로 놓은 경우도 동일한 과정을 거침
          else if (fResolutionX < fResolutionY)
          {
              float fValue = (fResolutionY - fResolutionX) * 0.5f;
              fValue = fValue / fResolutionY;

              foreach (Camera obj in objCameras)
              {
                  obj.rect = new Rect(obj.rect.x,
                                      ((Screen.height * fValue) / Screen.height) + (obj.rect.y * (1.0f - (2.0f * fValue))),
                                      obj.rect.width,
                                      obj.rect.height * (1.0f - (2.0f * fValue)));

                  //obj.rect = new Rect( obj.rect.x , obj.rect.y + obj.rect.y * fValue, obj.rect.width, obj.rect.height - obj.rect.height * fValue );
              }


              GameObject objTopScissor = (GameObject)Instantiate(m_objBackScissor);
              objTopScissor.GetComponent<Camera>().rect = new Rect(0, 0, 1.0f, (Screen.height * fValue) / Screen.height);

              GameObject objBottomScissor = (GameObject)Instantiate(m_objBackScissor);
              objBottomScissor.GetComponent<Camera>().rect = new Rect(0, (Screen.height - (Screen.height * fValue)) / Screen.height
                                                      , 1.0f, (Screen.height * fValue) / Screen.height);


              objTopScissor.transform.parent = gameObject.transform;
              objBottomScissor.transform.parent = gameObject.transform;
          }
          else
          {
              // Do Not Setting Camera
          }
      }
      */
    /* private void Start()
     {

         Camera camera = GetComponent<Camera>();
         Rect rect = camera.rect;
         float scaleheight = ((float)Screen.width / Screen.height) / ((float)16 / 9); // (가로 / 세로)
         float scalewidth = 1f / scaleheight;
         if (scaleheight < 1)
         {
             rect.height = scaleheight;
             rect.y = (1f - scaleheight) / 2f;
         }
         else
         {
             rect.width = scalewidth;
             rect.x = (1f - scalewidth) / 2f;
         }
         camera.rect = rect;
     }
     void OnPreCull() => GL.Clear(true, true, Color.black);

     */
    /*  private void Awake()
      {
          setCamera();
      }
      private void setCamera()
      {
          float targetWidthAspect = 16.0f;
          float targetHeightAspect = 9.0f;

          Camera mainCamera = Camera.main;
          mainCamera.aspect = targetWidthAspect / targetHeightAspect;

          float widthRatio = (float)Screen.width / targetWidthAspect;
          float heightRatio = (float)Screen.height / targetHeightAspect;

          float heightAdd = ((widthRatio/(heightRatio /100))-100)/ 200;
          float widthAdd = ((heightRatio / (widthRatio / 100))-100)/ 200;

          if (heightRatio > widthRatio)
              widthAdd = 0.0f;
          else
              heightAdd = 0.0f;
          mainCamera.rect = new Rect(
          mainCamera.rect.x + Mathf.Abs(widthAdd),
          mainCamera.rect.y + Mathf.Abs(heightAdd),
          mainCamera.rect.width + (widthAdd * 2),
          mainCamera.rect.height + (heightAdd * 2));
      }*/
    /* private void Awake()
      {
          Camera cam = GetComponent<Camera>(); // 카메라 컴포넌트의  Viewport Rect 
          Rect rt = cam.rect; // 현재 세로 모드 9:16, 반대로 하고 싶으면 16:9를 입력. 
          float scale_height = ((float)Screen.width / Screen.height) / ((float)16 / 9); // (가로 / 세로) 
          float scale_width = 1f / scale_height; if (scale_height < 1) 
          { rt.height = scale_height; rt.y = (1f - scale_height) / 2f; } 
          else { rt.width = scale_width; rt.x = (1f - scale_width) / 2f; } 
          cam.rect = rt; 
      }*/
    /*float originWidth =1920;
    float originHeight =1080;
    private void Awake()
    {
        float screenW = (float)Screen.width;
        float screenH = (float)Screen.height;
       
        float one = originWidth / originHeight / screenW * screenH;
        float two = screenW / screenH / originWidth * originHeight;

        float wFactor = screenW / 1920;
        float hFactor = screenH / 1080;

        float currentRatio = screenW / screenH;
       float oriRatio = originWidth / originHeight;

        if (currentRatio < oriRatio)
        {
            //GameObject.Find("UI").GetComponent<CanvasScaler>().matchWidthOrHeight = 0;
            GameObject.Find("Main Camera").GetComponent<Camera>().orthographicSize *= (screenH / originHeight);
        }

        Screen.SetResolution(Screen.width, Screen.height, true);
    }
    */
/*
    private void Awake()
    {
        SetupAndroidTheme(0, 0);
    }
    public static void SetupAndroidTheme(int primaryARGB, int darkARGB, string label = null)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
    label = label ?? Application.productName;
    Screen.fullScreen = false;
    AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
    activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
    {
        AndroidJavaClass layoutParamsClass = new AndroidJavaClass("android.view.WindowManager$LayoutParams");
        int flagFullscreen = layoutParamsClass.GetStatic<int>("FLAG_FULLSCREEN");
        int flagNotFullscreen = layoutParamsClass.GetStatic<int>("FLAG_FORCE_NOT_FULLSCREEN");
        int flagDrawsSystemBarBackgrounds = layoutParamsClass.GetStatic<int>("FLAG_DRAWS_SYSTEM_BAR_BACKGROUNDS");
        AndroidJavaObject windowObject = activity.Call<AndroidJavaObject>("getWindow");
        windowObject.Call("clearFlags", flagFullscreen);
        windowObject.Call("addFlags", flagNotFullscreen);
        windowObject.Call("addFlags", flagDrawsSystemBarBackgrounds);
        int sdkInt = new AndroidJavaClass("android.os.Build$VERSION").GetStatic<int>("SDK_INT");
        int lollipop = 21;
        if (sdkInt > lollipop)
        {
            windowObject.Call("setStatusBarColor", darkARGB);
            string myName = activity.Call<string>("getPackageName");
            AndroidJavaObject packageManager = activity.Call<AndroidJavaObject>("getPackageManager");
            AndroidJavaObject drawable = packageManager.Call<AndroidJavaObject>("getApplicationIcon", myName);
            AndroidJavaObject taskDescription = new AndroidJavaObject("android.app.ActivityManager$TaskDescription", label, drawable.Call<AndroidJavaObject>("getBitmap"), primaryARGB);
            activity.Call("setTaskDescription", taskDescription);
        }
    }));
#endif
    }

    public static int ToARGB(Color color)
    {
        Color32 c = (Color32)color;
        byte[] b = new byte[] { c.b, c.g, c.r, c.a };
        return System.BitConverter.ToInt32(b, 0);
    }*/
}
