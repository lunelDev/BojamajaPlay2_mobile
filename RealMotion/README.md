# 🎮 보자마자 PLAY2 리얼모션 (Bojamaja PLAY2 Real Motion)

<p align="center">
  <img src="Screenshots/img.png" width="600" alt="보자마자 PLAY2 리얼모션 타이틀"/>
</p>

Leap Motion 제스처 인식 기반의 **캐주얼 게임 모음집**.
손동작으로 직접 조작하며 몰입형 게임 경험을 제공합니다.
업데이트로 **클래식 모드 / 랭킹 모드**가 추가되어 기존보다 확장된 게임성을 지원합니다.

* 각 게임은 30초 동안 진행되며, 클래식 모드 / 랭킹 모드 두 가지 방식으로 플레이 가능합니다.
* 순차적인 미션 구조, 손 동작 튜토리얼, 랭킹 등록 시스템, 별점 시스템, 사운드 및 이펙트 처리까지 포함된 종합형 콘텐츠입니다.


## 📅 개발 정리

| 구분             | 내용                                                                                                                                                        |
| -------------- | --------------------------------------------------------------------------------------------------------------------------------------------------------- |
| **개발 기간 / 역할** | **2020.11 ~ 2021.08 (10개월)**<br>팀 프로젝트 (개발 2명)<br>UI/게임 로직/랭킹 시스템/제스처 인식 통합 개발                                                                            |
| **플랫폼**        | Windows (리얼모션), Android·iOS (모바일 버전)                                                                                                                      |
| **기술 스택**      | Unity3D (C#)<br>Leap Motion SDK<br>Firebase Realtime DB<br>DOTween, Animation, Video Player                                                               |
| **주요 기여**      | - Leap Motion 제스처 매핑 (버튼, 선택, 조작)<br>- 랭킹 모드 시스템 (10개 랜덤 게임 → 총점 → 랭킹 등록)<br>- 클래식 모드 (별점 보상 시스템)<br>- UI/UX 흐름 설계 (튜토리얼 포함)<br>- Firebase 연동 및 점수 저장, 조회 |
| **성과**         | - Leap Motion 기반 몰입형 인터랙션 구현<br>- 단일 앱에 10종 이상의 모션 게임 통합<br>- 글로벌 랭킹 지원 및 확장성 확보                                                                          |

## ⚙️ 코드 구조 및 주요 클래스

| 클래스 이름                  | 설명                                                       |
| ----------------------- | -------------------------------------------------------- |
| `GestureManager.cs`     | Leap Motion 입력값(손가락, 손바닥)을 제스처 이벤트로 매핑. UI 버튼/게임 조작과 연동. |
| `GameFlowManager.cs`    | 전체 게임 흐름 제어 (메인 → 모드 선택 → 게임 실행 → 결과 → 랭킹 등록).           |
| `RankingManager.cs`     | Firebase 연동, 점수 저장/조회, 글로벌 랭킹 UI 반영.                     |
| `ClassicModeManager.cs` | 개별 게임 점수 계산 및 별(★) 보상 시스템 관리.                            |
| `TutorialManager.cs`    | 제스처/모션 인식 튜토리얼 로직 (손가락 버튼, 모션 인식기 사용법 안내).               |
| `UIManager.cs`          | 메뉴, 게임 선택, 결과창 UI 전환 및 애니메이션 제어.                         |

## 💻 주요 기술 구현 코드 예시

### 🎯 1. 제스처 매핑 및 입력 처리

```csharp
// GestureManager.cs
void Update()
{
    foreach (var hand in leapProvider.CurrentFrame.Hands)
    {
        if (hand.IsPinching())
            OnGestureRecognized(GestureType.Select);
        else if (hand.PalmNormal.y > 0.5f)
            OnGestureRecognized(GestureType.Up);
        else if (hand.PalmNormal.y < -0.5f)
            OnGestureRecognized(GestureType.Down);
    }
}
```

👉 Leap Motion SDK에서 손가락 상태를 감지해 **UI 버튼 클릭, 메뉴 선택, 오브젝트 조작**으로 매핑.
**비접촉식 조작 구조**를 통해 키보드·마우스 없이 플레이가 가능하게 설계되었습니다.

### 🧠 2. 게임 진행 및 점수 관리 로직

```csharp
// GameFlowManager.cs
IEnumerator GameTimer()
{
    currentTime = 30f;
    while (currentTime > 0)
    {
        currentTime -= Time.deltaTime;
        uiManager.UpdateTimerUI(currentTime);
        yield return null;
    }
    EndGame();
}
```

👉 **Coroutine 기반의 타이머 시스템**으로 각 미니게임을 통합 제어.
게임 종료 시 자동으로 결과 화면을 호출하고 랭킹 시스템에 점수를 전달합니다.


### ☁️ 3. Firebase 랭킹 등록 시스템

```csharp
// RankingManager.cs
public void SaveScore(string userId, int score)
{
    var scoreData = new Dictionary<string, object>
    {
        { "user", userId },
        { "score", score },
        { "date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") }
    };
    FirebaseDatabase.DefaultInstance
        .RootReference.Child("Ranking").Push().SetValueAsync(scoreData);
}
```

👉 Firebase를 통해 **글로벌 랭킹 데이터 실시간 저장** 및 **조회 기능**을 구현.
데이터는 Push 방식으로 누락 없이 처리되며, UI 즉시 반영으로 **게임 피드백 속도 향상**을 이끌었습니다.


### 🌟 4. 별(★) 보상 시스템

```csharp
// ClassicModeManager.cs
int CalculateStars(int score)
{
    if (score >= 900) return 3;
    if (score >= 600) return 2;
    return 1;
}
```

👉 점수에 따른 별 개수를 계산해 시각적으로 피드백을 제공합니다.
결과 화면에서는 **DOTween 애니메이션**으로 별이 점점 나타나는 연출을 추가했습니다.

### 📖 5. 튜토리얼 시스템 (코루틴 기반)

```csharp
// TutorialManager.cs
IEnumerator StartTutorial()
{
    yield return StartCoroutine(ShowGesture("Pinch", "손가락을 오므려보세요"));
    yield return StartCoroutine(ShowGesture("Swipe", "손을 좌우로 움직여보세요"));
    EndTutorial();
}
```

👉 단계별 제스처 가이드를 제공하는 **코루틴 기반 튜토리얼 구조**.
사용자는 각 제스처를 수행하면 다음 단계로 넘어가며, 학습형 UI/UX 경험을 제공합니다.

## ✋ 제스처 & 튜토리얼

| **버튼 매핑(제스처 설명)**                                                                                    | **모션 인식 시작(제스처 인식 예시)**                                                                              |
| ---------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------- |
| <img src="Screenshots/293590147-7e2c5acd-0e02-4d69-b2fa-713eb976a7c7.png" width="500" height="250"/> | <img src="Screenshots/293590148-7c3e4f92-e7f0-45cb-b734-e83618256053.gif" width="500" height="250"/> |
| 손가락 제스처를 버튼 입력으로 매핑<br>(첫번째 버튼, 두번째 버튼, 뒤로가기 등)                                                      | 모션 인식기에 손을 올려 제스처 인식 시작                                                                              |

## 🎮 게임 모드 소개

| **클래식 모드**                                                                                                | **랭킹 모드**                                                                                                        |
| --------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------- |
| <img src="Screenshots/img (3).png" width="500" height="250"/><br>원하는 게임을 골라 30초 동안 플레이, 획득 점수에 따라 별(★) 부여 | <img src="Screenshots/img (4).png" width="500" height="250"/><br>10개 미니게임을 랜덤 순서로 30초씩 플레이, 총 점수를 기반으로 글로벌 랭킹 등록 |


## 📸 게임 화면 & 시연

| **타이틀 화면**                                                | **게임 선택**                                                     | **랭킹 등록**                                                     | **결과 화면**                                                     | **랭킹 존**                                                      |
| --------------------------------------------------------- | ------------------------------------------------------------- | ------------------------------------------------------------- | ------------------------------------------------------------- | ------------------------------------------------------------- |
| <img src="Screenshots/img.png" width="200" height="100"/> | <img src="Screenshots/img (2).png" width="200" height="100"/> | <img src="Screenshots/img (4).png" width="200" height="100"/> | <img src="Screenshots/img (6).png" width="200" height="100"/> | <img src="Screenshots/img (6).png" width="200" height="100"/> |

### ✅ 기술적 특징 요약

* **Unity + Leap Motion SDK 통합 구조 설계**
* **Firebase Realtime DB 기반 랭킹 시스템 구축**
* **Coroutine, Event-driven Input 구조 적용**
* **DOTween으로 UI/피드백 애니메이션 연출**
* **비접촉형 제스처 UX 설계 및 실감형 피드백 구현**
