using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CanvasSize : MonoBehaviour
{
    public RectTransform mScreen = null;
    public float wid;
    public float hig;
    // Start is called before the first frame update
    /*   private void Awake()
       {
           wid = Screen.width;
           hig = Screen.height;
           mScreen.sizeDelta = new Vector2(wid, hig);
       }
       void OnEnable()
       {
           wid = Screen.width;
           hig = Screen.height;
           mScreen.sizeDelta = new Vector2(wid, hig);
       }*/
    /* void Start()
     {
         float fScaleWidth = ((float)Screen.width / (float)Screen.height) / ((float)16 / (float)9);
         Vector3 vecButtonPos = GetComponent<RectTransform>().localPosition;
         vecButtonPos.x = vecButtonPos.x * fScaleWidth;
         GetComponent<RectTransform>().localPosition = new Vector3(vecButtonPos.x, vecButtonPos.y, vecButtonPos.z);
     }
     */

      /*RectTransform Panel;
        Rect LastSafeArea = new Rect(0, 0, 0, 0);

        public Text message;

        void Awake()
        {
            Panel = this.gameObject.GetComponent<RectTransform>();
            Refresh();
        }

        void Update()
        {
            Refresh();
        }
        void Refresh()
        {
            Rect safeArea = GetSafeArea();

            if (safeArea != LastSafeArea)
                ApplySafeArea(safeArea);
        }
        Rect GetSafeArea()
        {
            return Screen.safeArea;
        }
        void ApplySafeArea(Rect r)
        {
            LastSafeArea = r;

           Vector2 anchorMin = r.position;
            Vector2 anchorMax = r.position + r.size;
            anchorMin.x /= Screen.width;
            anchorMin.y /= Screen.height;
            anchorMax.x /= Screen.width;
            anchorMax.y /= Screen.height;
            Panel.anchorMin = anchorMin;
            Panel.anchorMax = anchorMax;
        }*/
    
}
