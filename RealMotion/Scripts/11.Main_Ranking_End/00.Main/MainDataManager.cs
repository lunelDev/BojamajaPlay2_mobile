using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainDataManager : MonoBehaviour
{
    //게임 씬 전환 Data
    // 배열 사용
    //게임의 진행 순서를 랜덤으로 뽑음
    public static MainDataManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else
            instance = this;
    }
    public void RandStartDataSave()
    {
        //게임의 갯수 만큼 배열을 생성
        bool isSame; //배열안에 똑같은 숫자가 있는지 확인
        for (int i = 0; i < GameManager.Season2_GameKindCount; ++i)
        {
            while (true)
            {
                //Season2_NextCallNum [0] 부터 배열의 길이 만큼 확인하고 중복 체크
                GameManager.Season2_NextCallNum[i] = Random.Range(0, GameManager.Season2_GameKindCount);
                isSame = false;
                for (int j = 0; j < i; j++)
                {
                    if (GameManager.Season2_NextCallNum[j].Equals(GameManager.Season2_NextCallNum[i]))
                    {
                        isSame = true;
                        break;
                    }           
                }
                if (!isSame) break;
            }
            Debug.Log(GameManager.Season2_NextCallNum[i]);
        }
    }
}

