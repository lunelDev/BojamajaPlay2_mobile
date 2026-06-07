using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankRegistration : MonoBehaviour
{
    [SerializeField] private Button rankingReg_Bnt;
    [SerializeField] private GameObject InputYourNickName;
    [SerializeField] private GameObject DuplicatedNamePanel;
    [SerializeField] private GameObject NicknameNotavailable;
    [SerializeField] private GameObject RegCompleted;
    [SerializeField] private GameObject MyRankingPanel;
    [SerializeField] private Text nickname;
    [SerializeField] private InputField nickname_inputField;
    [SerializeField] private GameObject rankPanel;

    private bool isBadWords;

    private void Start()
    {
        isBadWords = false;
        DuplicatedNamePanel.SetActive(false);
        RegCompleted.SetActive(false);
        MyRankingPanel.SetActive(false);
    }
    // ServerManager input
    private void OnEnable()
    {
        // 서버 등록 이벤트 스트립트 처리
        rankingReg_Bnt.onClick.AddListener(() => { SearchingNickName(); });
    }

    // ServerManager output
    private void OnDisable()
    {
        // 서버 등록 이벤트 스트립트 처리
        rankingReg_Bnt.onClick.RemoveListener(() => { SearchingNickName(); });
    }

    public void SearchingNickName()
    {
        if (nickname.text == "")
        {
            // 닉네임을 입력해주세요. 팝업띄우기
            StartCoroutine(_EmptyString());
            return;
        }

        Debug.Log("지나가나?");
        StopAllCoroutines();
        StartCoroutine(_BadWordsFiltering());
    }

    IEnumerator _EmptyString()
    {
        WaitForSeconds ws = new WaitForSeconds(0.8f);
        InputYourNickName.SetActive(true);
        yield return ws;
        InputYourNickName.SetActive(false);
    }

    IEnumerator _BadWordsFiltering()
    {
        yield return new WaitUntil(() => Filtering());

        if (isBadWords)
        {
            // 욕이 있다면 팝업
            StartCoroutine(_PopUpBadWordsFiltering());
        }
        else
        {
            // 욕이 없으므로 등록전 네트워크 상태 체크
            StartCoroutine(_GetNetworkState());
        }
    }
    public bool Filtering()
    {
        string[] filterWord;
        string ftemp, wtemp;
        string content;

        wtemp = nickname_inputField.text.ToString().Replace(" ", ""); //공백없애기          

        content = "ㅅ1ㅂ,좆,좇,개새끼,소새끼,병신,지랄,씨팔,십팔,찌랄,쌍년,쌍놈,빙신,좆까,좆같은게,잡놈,벼엉신,바보새끼,씹새끼,씨발,시벌,씨벌,떠그랄,좆밥,짜져,추천아이디,추/천/인,쉐이,싸가지,미친,미친넘,찌랄,씨밸넘,시발,좃까,씹팔놈,씹새,씨방새,씨부랄,썅년,쌍놈,개같은년,개같은놈,육시랄,니미뽕,좃같은,씨방,썅놈,개판,새끼,바보,님아,님들아,죽습니다,추천인,추천id,니기미,등신,개쓰레기,10새,10새기,10새리,10세리,10쉐이,10쉑,10스,10쌔,10쌔기,10쎄,10알,10창,10탱,18것,18넘,18년,18노,18놈,18뇬,18럼,18롬,18새,18새끼,18색,18세끼,18세리,18섹,18쉑,18스,18아,ㄱㅐ,ㄲㅏ,ㄲㅑ,ㄲㅣ,ㅅㅂㄹㅁ,ㅅㅐ,ㅆㅂㄹㅁ,ㅆㅍ,ㅆㅣ,ㅆ앙,ㅍㅏ,凸, 갈보,갈보년,강아지,같은년,같은뇬,개같은,개구라,개년,개놈,개뇬,개대중,개독,개돼중,개랄,개보지,개뻥,개뿔,개새,개새기,개새끼,개새키,개색기,개색끼,개색키,개색히,개섀끼,개세,개세끼,개세이,개소리,개쑈,개쇳기,개수작,개쉐,개쉐리,개쉐이,개쉑,개쉽,개스끼,개시키,개십새기,개십새끼,개쐑,개씹,개아들,개자슥,개자지,개접,개좆,개좌식,개허접,걔새,걔수작,걔시끼,걔시키,걔썌,걸레,게색기,게색끼,광뇬,구녕,구라,구멍,그년,그새끼,냄비,놈현,뇬,눈깔,뉘미럴,니귀미,니기미,니미,니미랄,니미럴,니미씹,니아배,니아베,니아비,니어매,니어메,니어미,닝기리,닝기미,대가리,뎡신,도라이,돈놈,돌아이,돌은놈,되질래,뒈져,뒈져라,뒈진,뒈진다,뒈질,뒤질래,등신,디져라,디진다,디질래,딩시,따식,때놈,또라이,똘아이,똘아이,뙈놈,뙤놈,뙨넘,뙨놈,뚜쟁,띠바,띠발,띠불,띠팔,메친넘,메친놈,미췬,미췬,미친,미친넘,미친년,미친놈,미친새끼,미친스까이,미틴,미틴넘,미틴년,미틴놈,바랄년,병자,뱅마,뱅신,벼엉신,병쉰,병신,부랄,부럴,불알,불할,붕가,붙어먹,뷰웅,븅,븅신,빌어먹,빙시,빙신,빠가,빠구리,빠굴,빠큐,뻐큐,뻑큐,뽁큐,상넘이,상놈을,상놈의,상놈이,새갸,새꺄,새끼,새새끼,새키,색끼,생쑈,세갸,세꺄,세끼,섹스,쇼하네,쉐,쉐기,쉐끼,쉐리,쉐에기,쉐키,쉑,쉣,쉨,쉬발,쉬밸,쉬벌,쉬뻘,쉬펄,쉽알,스패킹,스팽,시궁창,시끼,시댕,시뎅,시랄,시발,시벌,시부랄,시부럴,시부리,시불,시브랄,시팍,시팔,시펄,신발끈,심발끈,심탱,십8,십라,십새,십새끼,십세,십쉐,십쉐이,십스키,십쌔,십창,십탱,싶알,싸가지,싹아지,쌉년,쌍넘,쌍년,쌍놈,쌍뇬,쌔끼,쌕,쌩쑈,쌴년,썅,썅년,썅놈,썡쇼,써벌,썩을년,썩을놈,쎄꺄,쎄엑,쒸벌,쒸뻘,쒸팔,쒸펄,쓰바,쓰박,쓰발,쓰벌,쓰팔,씁새,씁얼,씌파,씨8,씨끼,씨댕,씨뎅,씨바,씨바랄,씨박,씨발,씨방,씨방새,씨방세,씨밸,씨뱅,씨벌,씨벨,씨봉,씨봉알,씨부랄,씨부럴,씨부렁,씨부리,씨불,씨붕,씨브랄,씨빠,씨빨,씨뽀랄,씨앙,씨파,씨팍,씨팔,씨펄,씸년,씸뇬,씸새끼,씹같,씹년,씹뇬,씹보지,씹새,씹새기,씹새끼,씹새리,씹세,씹쉐,씹스키,씹쌔,씹이,씹자지,씹질,씹창,씹탱,씹퇭,씹팔,씹할,씹헐,아가리,아갈,아갈이,아갈통,아구창,아구통,아굴,얌마,양넘,양년,양놈,엄창,엠병,여물통,염병,엿같,옘병,옘빙,오입,왜년,왜놈,욤병,육갑,은년,을년,이년,이새끼,이새키,이스끼,이스키,임마,자슥,잡것,잡넘,잡년,잡놈,저년,저새끼,접년,젖밥,조까,조까치,조낸,조또,조랭,조빠,조쟁이,조지냐,조진다,조찐,조질래,존나,존나게,존니,존만, 존만한,좀물,좁년,좆,좁밥,좃까,좃또,좃만,좃밥,좃이,좃찐,좆같,좆까,좆나,좆또,좆만,좆밥,좆이,좆찐,좇같,좇이,좌식,주글,주글래,주데이,주뎅,주뎅이,주둥아리,주둥이,주접,주접떨,죽고잡,죽을래,죽통,쥐랄,쥐롤,쥬디,지랄,지럴,지롤,지미랄,짜식,짜아식,쪼다,쫍빱,찌랄,창녀,캐년,캐놈,캐스끼,캐스키,캐시키,탱구,팔럼,퍽큐,호로,호로놈,호로새끼,호로색,호로쉑,호로스까이,호로스키,후라들,후래자식,후레,후뢰,씨ㅋ발,ㅆ1발,ㅆ11발,ㅆ111발,씌발,띠발,띄발,뛰발,띠ㅋ발,뉘뮈,박지수바보,잼민이,거지,씨봉봉,거지깽깽이";

        filterWord = content.Split(','); //필터링 단어(띄어쓰기 구분하지 않는 욕설)

        for (int i = 0; i < filterWord.Length; i++)
        {
            ftemp = filterWord[i];

            if (wtemp.IndexOf(ftemp) != -1)// 입력받은 문자열을 찾으면 List 에서 그때의 Index 값을 출력 
            {
                nickname_inputField.text = "";
                isBadWords = true;
            }
        }

        return true;
    }

    IEnumerator _PopUpBadWordsFiltering()
    {
        isBadWords = false;

        WaitForSeconds ws = new WaitForSeconds(0.8f);
        NicknameNotavailable.SetActive(true);
        yield return ws;
        NicknameNotavailable.SetActive(false);
    }

    IEnumerator _GetNetworkState()
    {
        ServerManager.Instance.GetNetworkState();

        yield return new WaitUntil(() => ServerManager.Instance.isConnCompleted);
        ServerManager.Instance.isConnCompleted = false;
        if (ServerManager.Instance.isConnected)
        {
            StartCoroutine(_SearchingNickName());
        }
    }

    IEnumerator _SearchingNickName()
    {
        // 서버에 서치 닉네임
        ServerManager.Instance.RankingSearchByNickName(nickname.text);

        yield return new WaitUntil(() => ServerManager.Instance.isSearchComplete);

        // 있으면 중복으로 존재하는 닉네임 처리 
        if (ServerManager.Instance.isExistName)
        {
            //Debug.LogError("닉네임 있으면 : " + ServerManager.Instance.isExistName);
            // 닉네임이 있으면
            DuplicatedNamePanel.SetActive(true);
            GameManager.Instance.OnClick_BtnSound3();
            StartCoroutine(_DisappearPopUp());
        }
        else
        {
            // 닉네임이 없으면 등록하기
            ServerManager.Instance.RankingReg(nickname.text);
            // 랭킹 등록되었습니다. 팝업 교체 > 바로 랭킹존 패널 띄우기
            //RegCompleted.SetActive(true);

            //Debug.Log("랭킹존??");

            //LoadingSceneManager.LoadScene("Ranking");
            SetRankingPanel();
        }

        ServerManager.Instance.isSearchComplete = false;
    }

    // 몇초뒤에 사라짐
    IEnumerator _DisappearPopUp()
    {
        WaitForSeconds ws = new WaitForSeconds(2f);

        yield return ws;

        DuplicatedNamePanel.SetActive(false);
    }

    public void SetRankingPanel()
    {
        StartCoroutine(_Delay(1f));
    }

    IEnumerator _Delay(float time)
    {
        WaitForSeconds ws = new WaitForSeconds(time);
        yield return ws;
        End_UIManager.Instance.Ranking_registration = false;
        rankPanel.SetActive(false);
        MyRankingPanel.SetActive(true);
    }

    // 닉네임 랜덤으로 정하기
    private System.Random randomSeed = new System.Random((int)DateTime.Now.Ticks & 0x0000FFFF); //랜덤 시드값
    public void RandomNickName(int _nLength = 8)
    {
        const string strPool = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";  //문자 생성 풀
        char[] chRandom = new char[_nLength];

        for (int i = 0; i < _nLength; i++)
        {
            chRandom[i] = strPool[randomSeed.Next(strPool.Length)];
        }
        string str = "BMP_" + new String(chRandom);   // char to string

        nickname_inputField.text = str;
    }

}