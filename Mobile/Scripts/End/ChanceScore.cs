using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChanceScore : MonoBehaviour
{
    //public Image Check;
    //public Sprite UnCheckState;
    //public Sprite CheckState;

    public float ScoreNUM = 0;
    public string GameName = null; //타이틀 이름
    public string GameSceneName = null; //타이틀 씬이름  
    [Header("타이틀")]
    public Text titleImg;
    public Text ScoreText;  //점수 
    public Toggle tgGameSelect;
    
    private void Awake()
    {
        tgGameSelect.onValueChanged.AddListener(delegate {
            ToggleValueChanged();
        });
    }

    // 현재 선택된 토글에서 
    void ToggleValueChanged()
    {
        // 토글이 체크된 것만 들어감
        if (tgGameSelect.isOn)
        {
            ChancePopupManager.CheckGameName = GameSceneName; //체크된 게임의 이름을 넣어준다.
            ChancePopupManager.CheckGameScore = ScoreNUM;
        }
    }
    private void Start()
    {
        SetTitle(); //순서에 따른 타이틀 변경
    }
    private void Update()
    {
        SetTitle(); //순서에 따른 타이틀 변경
    }
    //판넬위 타이틀 바꾸기 
    public void SetTitle()
    {
        ScoreText.text = ScoreNUM.ToString() + "p";
        titleImg.text = GameName.ToString();
        //ScoreText.text = "10p";
        //titleImg.text = "볼링치자!";
    }
    //public void OnCheckGame()
    //{
        //if (ChancePopupManager.checkState.Equals(0))
        //{
        //    //Debug.Log(ChancePopupManager.checkState);
        //    //게임 체크가 안되있을 경우, 체크 해줌
        //    if (Check.sprite.name == "ChanceUnCheck")
        //    {
        //        ChancePopupManager.checkState = 1;
        //        ChancePopupManager.CheckGameName = GameSceneName; //체크된 게임의 이름을 넣어준다.
        //        ChancePopupManager.CheckGameScore = 0;
        //        Check.sprite = CheckState;
        //    }
        //}
        //else if (ChancePopupManager.checkState.Equals(1))
        //{
        //    //게임 체크가 되있을 경우, 체크 해제 해줌
        //    if (Check.sprite.name == "ChanceCheck")
        //    {
        //        ChancePopupManager.checkState = 0;
        //        Check.sprite = UnCheckState;
        //        ChancePopupManager.CheckGameScore = ScoreNUM;
        //        ChancePopupManager.CheckGameName = null; //선택된 이름을 없앰
        //    }
        //}
    //}
}
