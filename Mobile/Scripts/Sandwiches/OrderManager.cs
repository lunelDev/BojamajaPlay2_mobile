using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
//주문 받는 목록들 관리
public class OrderManager : MonoBehaviour
{
    public CustomerQueueManager customerQueue; //손님들 
    public Customer customer;
    public Sandwich[] sandwiches; //샌드위치 종류
    public GameObject speechBubbel;   //말풍선
    public GameObject confettiPrefab;
    public GameObject failParticle;
    
    public Punisher penaltyOverlay;
    public static bool FailTime = false;
    public static bool CantHold = false;
    public static bool angry = false;


    int _recipeStep;
    Collider _collider;
    Sandwich _currentSandwich;
    GameObject _currentSpeechBubbleSandwich;
    Sandwiches.PlayerController _playerController;
    Dictionary<string, GameObject> _speechBubbleSandwiches = new Dictionary<string, GameObject>();

    public static OrderManager instance { get; private set;  } 
    private void Awake()
    {
        if (instance != null) Destroy(this);
        else instance = this; 
    }
    void Start()
    {
        customer = FindObjectOfType<Customer>();
        _playerController = FindObjectOfType<Sandwiches.PlayerController>();
        _collider = GetComponent<Collider>();
        //speechBubbel안의 자식들의 이름과 오브젝트를 딕셔너리안에 넣어줌 
        foreach (Transform child in speechBubbel.transform.GetChild(0))
        {
            _speechBubbleSandwiches.Add(child.name, child.gameObject);
        }
        //샌드위치의 종류와 딕셔너리의 갯수가 같지 않을 떄 확인
        if (sandwiches.Length < _speechBubbleSandwiches.Count || sandwiches.Length > _speechBubbleSandwiches.Count)
            Debug.LogError("Mismatching number of sandwich speech bubbles (" + _speechBubbleSandwiches.Count + ") and actual sandwiches (" + sandwiches.Length + ").");

    }

    void OnTriggerEnter(Collider collider)
    {
        FoodItem food; //음식 재료 

        if (food = collider.GetComponent<FoodItem>())
        {
            EvaluateIngredient(food);
        }
    }

    public void EvaluateIngredient(FoodItem foodItem)
    {//올바른 재료를 놓았을 떄
        if (foodItem.Equals(_currentSandwich.recipe[_recipeStep]))
        {
            //랜덤 사운드 실행
            switch (Random.Range(0, 2))
            {
                case 0:
                    SoundManager.Instance.PlaySFX("18 pile material", 1.5f); break;
                case 1:
                    SoundManager.Instance.PlaySFX("19 pile material", 1.5f); break;
            }
            foodItem.FloatIntoSandwich(_currentSandwich.GetIngredient(_recipeStep));
            _recipeStep++;
            DataManager.Instance.scoreManager.Add(100); //100점 획득

            if (_recipeStep > _currentSandwich.recipe.Length - 1) // If we finish the current sandwich
            {
                SoundManager.Instance.PlaySFX("05 Sandwich complete");
                StartCoroutine(OnComplete());
                DataManager.Instance.scoreManager.Add(300); //1000점 획득
            }
        }
        //재료를 잘못 놓았을 떄
        else
        {
            SoundManager.Instance.PlaySFX("06 failed sandwich");
            SoundManager.Instance.PlaySFX("ScoreDown");
            penaltyOverlay.ExecutePenalty();
            DataManager.Instance.scoreManager.Subtract(100); //50점 상실
            StartCoroutine(_Failed());
        }
    }
    //재료를 완성 했을 떄
    IEnumerator OnComplete()
    {
        Instantiate(confettiPrefab, _currentSandwich.transform.position, Quaternion.identity); //파티클 생성
        _collider.enabled = false; //재료 못올리게 함
        CantHold = true;
        FailTime = true;
        yield return new WaitForSeconds(0.5f);
        _currentSandwich.gameObject.SetActive(false);
        _currentSpeechBubbleSandwich.SetActive(false);
        customerQueue.OnOrderComplete();
        speechBubbel.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        StartNewSandwich(); //새로운 샌드위치 주문
    }
    IEnumerator _Failed()
    {
        Instantiate(failParticle, _currentSandwich.transform.position, Quaternion.identity); //파티클 생성
        _collider.enabled = false; //재료 못올리게 함
        FailTime = true;
        CantHold = true;
        angry = true;
        yield return new WaitForSeconds(0.5f);
        _currentSandwich.gameObject.SetActive(false);
        _currentSpeechBubbleSandwich.SetActive(false);
        customerQueue.OnOrderComplete();
        speechBubbel.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        StartNewSandwich(); //새로운 샌드위치 주문
    }
    public void StartNewSandwich()
    {
        speechBubbel.SetActive(true);
        _recipeStep = 0; //순서 초기화
        FailTime = false;
        CantHold = false;
        angry = false;
        _currentSandwich = sandwiches[Random.Range(0, sandwiches.Length)]; //랜덤으로 샌드위치의 종류를 넣어준다.
        _currentSandwich.gameObject.SetActive(true); //쟁반위의 샌드위치를 보이지 않게 활성화 시킴
        _currentSpeechBubbleSandwich = _speechBubbleSandwiches[_currentSandwich.recipe];
        _currentSpeechBubbleSandwich.SetActive(true); //말풍선 안의 샌드위치 레시피 활성화
        _collider.enabled = true;
        SoundManager.Instance.PlaySFX("10 Order Sound");
    }
    
}
