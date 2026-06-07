using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CopyNickName : MonoBehaviour
{
    [SerializeField] private InputField nickNameField;

    private InputField myNickNameField;
    // Start is called before the first frame update
    void Awake()
    {
        myNickNameField = GetComponent<InputField>();
    }

    private void OnEnable()
    {
        // 오브젝트 활성화
        myNickNameField.text = nickNameField.text;
    }

    private void OnDisable()
    {
        // 오브젝트 비활성화
        myNickNameField.text = "";
    }
}
