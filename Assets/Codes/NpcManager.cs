using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcManager : MonoBehaviour
{
    public GameObject talkPanel;
    public GameObject choicePanel;
    public GameObject npcObject;
    public GameObject warning;

    public TalkManager talkManager;
    public GameManager gameManager;

    public Text talkText;
    public Text[] answerText;

    public int talkIndex;
    public int answerIndex;
    public bool isQuiz;
    public bool isAnswer;
    string answerData;

    public void Quiz(GameObject npcObj)
    {
        npcObject = npcObj;

        

        ObjData objData = npcObject.GetComponent<ObjData>();
        for (answerIndex = 0; answerIndex < 3; answerIndex++)
        {
            answerData = talkManager.GetAnswer(objData.id, answerIndex);
            answerText[answerIndex].text = answerData.Split(':')[0];
        }

        Talk(objData, objData.id, objData.isclear);
        talkPanel.SetActive(isQuiz);
        
    }


    void Talk(ObjData objData, int id, bool isclear)
    {
        string talkData = talkManager.GetTalk(id, talkIndex);
        
        
        if(talkData == null)    //퀴즈 풀어서 대화 끝
        {
            isQuiz = false;
            isAnswer = false;
            objData.isclear = true;
            gameManager.quizPoint--;
            talkIndex = 0;
            return;
        }

        if (isclear)    //이미 깬 퀴즈
            return;
        else
        {
            SoundManager.instance.PlaySE("talk");
            talkText.text = objData.name + ": " + talkData.Split(':')[0];

            if (int.Parse(talkData.Split(':')[1]) == 0) //일반 대화창
            {
                isAnswer = false;
                choicePanel.SetActive(false);
            }
            else if(int.Parse(talkData.Split(':')[1]) == 1) //정답 선택창
            {
                isAnswer = true;
                choicePanel.SetActive(true);
            }
            
        }

        isQuiz = true;
        talkIndex++;
    }

    public void Answer(int btnNum)
    {
        ObjData objData = npcObject.GetComponent<ObjData>();
        btnNum--;
        answerIndex = btnNum;
        answerData = talkManager.GetAnswer(objData.id, answerIndex);
        if (int.Parse(answerData.Split(':')[1]) == 1)   //정답일 때
        {
            Talk(objData, objData.id, objData.isclear);
            Quiz(npcObject);
            SoundManager.instance.PlaySE("bingo");
        }
        else if(int.Parse(answerData.Split(':')[1]) == 0)   //오답일 때
        {
            Text text = warning.GetComponent<Text>();
            text.text = string.Format("틀렸어요!");

            SoundManager.instance.PlaySE("wrong");

            warning.SetActive(true);
            Invoke("ActWarning", 1f);
            gameManager.LifeDown();

        }
    }

    void ActWarning()
    {

        warning.SetActive(false);
    }

}
