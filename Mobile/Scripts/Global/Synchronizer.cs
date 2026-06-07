using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class MyEvent : UnityEvent<Vector2> { }

public class Synchronizer : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	private bool _bIsMoved;
	private bool _bIsTouchPressed;
	[SerializeField] float touchSensitivity = 0.3f;
	[SerializeField] float movingThreshold = 10;
	Vector2 touchPositionInFirst;

	private float _velocity;
	private float _fNowRotationZ;

	public ScrollRect ParentSR;
	public bool isDrag;
	public Button[] GamePanel;

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    public void OnBeginDrag(PointerEventData e)
	{
		ParentSR.OnBeginDrag(e);
		_bIsMoved = false;
		touchPositionInFirst = e.position;
		for (int i = 0; i < GamePanel.Length; i++)
		{
			GamePanel[i].interactable = false;		
		}
	}
	public void OnDrag(PointerEventData e)
	{
		Debug.Log(e);
		//Debug.Log(e.delta.x);
		Debug.Log(Mathf.Abs(e.delta.x));

		/*	ParentSR.OnDrag(e);
			if (Mathf.Abs(e.delta.x) > 0.001f || Mathf.Abs(e.delta.y) > 0.001f)
			{
				isDrag = true;
				for (int i = 0; i < GamePanel.Length; i++)
				{
					GamePanel[i].interactable = false;
				}
			}*/
		float movingValue = e.delta.x;
		if (!_bIsMoved && _velocity == 0)
		{
			if (Mathf.Abs(touchPositionInFirst.x - e.position.x) > movingThreshold)
			{
				isDrag = true;
				for (int i = 0; i < GamePanel.Length; i++)
				{
					GamePanel[i].interactable = false;
				}
			}
			else
			{
				return;
			}
		}
	}
	public void OnEndDrag(PointerEventData e)
	{
		/*	ParentSR.OnEndDrag(e);
			isDrag = false;
			for (int i = 0; i < GamePanel.Length; i++)
			{
				GamePanel[i].interactable = true;
			}*/
		float movingValue = e.delta.x;
		if (!_bIsMoved && _velocity == 0)
		{
			if (Mathf.Abs(touchPositionInFirst.x - e.position.x) > movingThreshold)
			{
				isDrag = true;
				for (int i = 0; i < GamePanel.Length; i++)
				{
					GamePanel[i].interactable = true;
				}
			}
			else
			{
				return;
			}
		}
	}
}


