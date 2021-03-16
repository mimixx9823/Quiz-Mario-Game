# QuizMario Game
Unity를 활용하여 만든 2D 횡스크롤 게임

게임 <슈퍼마리오>에서 영감을 받아 제작한 게임이다.

## 주요기능
* 메인 화면 버튼 애니메이션(확대) 효과

![시작 화면 버튼 효과](https://user-images.githubusercontent.com/62532316/110442342-15c1c100-80fe-11eb-9f9f-364217ad5bb3.gif)

* 키보드 입력을 통한 캐릭터 이동 및 점프, 더블점프
* 맵에 있는 지형들은 타일맵으로 구성
* 게임 UI : GameManager.cs속 변수들 가공하여 출력
  - 🍒 : 과일 포인트(fruitPoint)
  - 🏆 : 현재 스테이지에 남아있는 퀴즈 포인트(quizPoint)
  ```cs
  //UIManager.cs
  fruitText.text = string.Format("{0:n0}", manager.fruitPoint);
  QuizText.text = string.Format("{0:n0}",manager.quizPoint);
  ```
  - 💚 : 생명력(lifePoint)
  ```cs
  //GameManager.cs/LifeDown()
  lifePoint--;
  UIlife[lifePoint].color = new Color(1, 1, 1, 0.4f);
  ```
* 과일 포인트 획득 시 UI에 출력

![이동 및 점프, 과일포인트](https://user-images.githubusercontent.com/62532316/110417332-bc936680-80d8-11eb-92b1-0d3cdcc878e9.gif)

* npc 대화
  - Dictionary 구조에 npc번호(Key값)와 문자열(Value값)을 저장
  ```cs
  //TalkManager.cs
  private void Awake()
      {
          talkData = new Dictionary<int, string[]>();   //npc 대화 데이터
          AnswerData = new Dictionary<int, string[]>(); //객관식 선택지 데이터
          ReadTalkData(); //text파일에서 읽어오기
      }
  ```
  - talk.txt파일
    + npcID, 대화 문자열 개수
    + 대화중 문자열:0 
    + 대화중 문자열:0 
    + 마지막 대화(퀴즈 선택지 주어짐) 문자열:1  
    + 퀴즈 선택지1:0 👈🏻 0은 오답
    + 퀴즈 선택지2:1 👈🏻 1은 정답
    + 퀴즈 선택지3:0 👈🏻 총 세가지의 선택지로 고정
  ```
  1000,3
  안녕마리오!:0	
  여기에 과일들이 많이 떨어져 있어!:0	
  이 맵에 있는 사과 갯수는 몇개일까?:1	
  4개:1	
  7개:0	
  9개:0
  ```  

* 퀴즈 풀이
  - 틀렸을 때 경고음/경고 메세지 출력, 생명력 깎임
  - 맞았을 때 사운드 출력/대화 종료, 남아있는 퀴즈포인트 줄어듦

![Npc대화 퀴즈 풀이](https://user-images.githubusercontent.com/62532316/110441598-4d7c3900-80fd-11eb-8baf-c52b32b2c6fc.gif)

* 몬스터 피격 시 일정시간 무적판정, 생명력 깎임
* 풀지 못한 퀴즈가 있을 때 경고 메세지 출력
* 퀴즈를 다 풀고 도착지점에 접근 시 다음 스테이지로 넘어감 

