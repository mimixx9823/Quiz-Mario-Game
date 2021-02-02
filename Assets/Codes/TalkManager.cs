using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    Dictionary<int, string[]> AnswerData;

    int keyID;
    int talkCounts;
    string[] talkArr;
    string[] answerArr;
    const int answerCounts = 3;


    private void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        AnswerData = new Dictionary<int, string[]>();
        ReadTalkData();
    }



    void ReadTalkData()
    {
        //초기화
        talkData.Clear();
        AnswerData.Clear();

        //대화 파일 읽기
        TextAsset textFile = Resources.Load("talk") as TextAsset;
        StringReader stringReader = new StringReader(textFile.text);

        string line = stringReader.ReadLine();

        //데이터 저장
        while (line != null)
        {

            //key랑 문자열 받기
            keyID = int.Parse(line.Split(',')[0]);
            talkCounts = int.Parse(line.Split(',')[1]);
            talkArr = new string[talkCounts];
            answerArr = new string[answerCounts];

            for (int i = 0; i < talkCounts; i++) //talk데이터 가져오기
            {
                line = stringReader.ReadLine();
                talkArr[i] = line;
            }

            for (int i = 0; i < answerCounts; i++) //quiz데이터 가져오기
            {
                line = stringReader.ReadLine();
                answerArr[i] = line;
            }

            talkData.Add(keyID, talkArr);
            AnswerData.Add(keyID, answerArr);

            line = stringReader.ReadLine();
        }

        
        //텍스트파일 닫기
        stringReader.Close();
    }

    public string GetTalk(int id, int talkIndex)
    {
        if (talkIndex == talkData[id].Length)
            return null;
        else
            return talkData[id][talkIndex];
    }

    public string GetAnswer(int id, int AnswerIndex)
    {
        return AnswerData[id][AnswerIndex];
    }

}
