using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Countdown : MonoBehaviour
{
    public Sprite[] sprites; //카운트 다운 숫자 이미지
    public string[] strings;
    public UnityEvent onComplete = null;

    Image _image;
    Text _textField;


    void Awake()
    {
        _image = GetComponent<Image>();
        _textField = GetComponent<Text>();
    }
    //활성화 시, 실행
    void OnEnable()
    {
        if (_image)
            _image.sprite = sprites[0];
        if (_textField)
            _textField.text = strings[0];
        StartCoroutine(_Countdown());
    }

    IEnumerator _Countdown()
    {      
        if (_image) //이미지
            foreach (var num in sprites)
            {
                _image.sprite = num;
                yield return new WaitForSeconds(1f);//1초후 다음 이미지
            }
        if (_textField) //text
            foreach (var num in strings)
            {
                _textField.text = num;
                yield return new WaitForSeconds(1f); //1초후 다음 Text
            }
        if (GameManager.RankMode.Equals(true))
        {
            onComplete.Invoke();
        }
         
    }
}
