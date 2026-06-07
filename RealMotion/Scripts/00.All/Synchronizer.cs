using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class MyEvent : UnityEvent<Vector2> { }

public class Synchronizer : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	public ScrollRect ParentSR;
	public bool isDrag;
	public Button[] GamePanel; 

	public void OnBeginDrag(PointerEventData e)
	{
		ParentSR.OnBeginDrag(e);
		for (int i = 0; i < GamePanel.Length; i++)
		{
			GamePanel[i].interactable = false;
		}
	}
	public void OnDrag(PointerEventData e)
	{
		ParentSR.OnDrag(e);
		if (Mathf.Abs(e.delta.x) > 0.2f || Mathf.Abs(e.delta.y) > 0.2f)
		{
			isDrag = true;
			for (int i = 0; i < GamePanel.Length; i++)
			{
				GamePanel[i].interactable = false;
			}
		}
	}
	public void OnEndDrag(PointerEventData e)
	{
		ParentSR.OnEndDrag(e);
		isDrag = false;
		for (int i = 0; i < GamePanel.Length; i++)
		{
			GamePanel[i].interactable = true;
		}
	}
 /*   public void Update()
    {
        if (isDrag.Equals(true)){
			for(int i=0; i<GamePanel.Length; i++)
            {
				GamePanel[i].interactable = false;
            }
        }
        else
        {
			for (int i = 0; i < GamePanel.Length; i++)
			{
				GamePanel[i].interactable = true;
			}
		}
    }*/
}


