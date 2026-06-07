using System;

using UnityEngine;
using UnityEngine.UI;
public class GameScore : MonoBehaviour
{
    public float ScoreNUM = 0;
    public string GameName = null; //타이틀 이름
  //  public int StarPick; //별 갯수
    [Header("타이틀")]
    public Text titleImg;
    public Text ScoreText;  //점수 

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
 
    }
    public static implicit operator GameScore(int v)
    {
        throw new NotImplementedException();
    }
}
