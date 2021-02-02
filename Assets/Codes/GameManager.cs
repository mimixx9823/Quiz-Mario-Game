using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int fruitPoint;
    public int lifePoint = 5;
    public int stageIndex;
    public int quizPoint;
    

    public GameObject menuSet;
    public GameObject gameoverSet;
    public GameObject gameclearSet;
    public GameObject[] Stages;
    public Image[] UIlife;
    public Button pauseBtn;
    public PlayerMove player;
    

    public StageData[] stgData;
    public int StageCount;

    public void Start()
    {
        for (int i = 0; i < StageCount; i++) {
            stgData[i] = Stages[i].GetComponent<StageData>();
            
        }
        
    }

    public void NextStage(){

        if(stageIndex <Stages.Length-1){
            Stages[stageIndex].SetActive(false);
            stageIndex++;
            Stages[stageIndex].SetActive(true);
            quizPoint = stgData[stageIndex].Quiz;
            PlayerReposition();
        }
        else{
            Time.timeScale = 0;

            Debug.Log("game clear");

        }
        //calculate point
        //total+=stage p
        //stagep = 0;
    }

    public void GameClear()
    {
        gameclearSet.SetActive(true);
        SoundManager.instance.PlayClearBGM();
        SoundManager.instance.PlaySE("nextstage");
    }

    public void LifeDown()
    {
        if(lifePoint > 0)
        {
            lifePoint--;
            UIlife[lifePoint].color = new Color(1, 1, 1, 0.4f);
        }
        if(lifePoint <=0)
        {
            OpenGameOver();
            player.OnDie();
            Debug.Log("죽었습니다.");
        }
    }

    public void PlayClickSound()
    {
        SoundManager.instance.PlaySE("click");
    }

    public void OpenGameOver()
    {
        gameoverSet.SetActive(true);
    }

    public void ReStart()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenMenu()
    {
        menuSet.SetActive(true);
    }

    public void GoMain()
    {
        SceneManager.LoadScene(0);
    }

    void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.tag == "Player"){

            //player reposition
            if (lifePoint > 1)
            {
                PlayerReposition();

                LifeDown();
            }
        }
    }

    void PlayerReposition(){
        player.transform.position = new Vector3(-8, -4, 0);
        player.VelocityZero();
    }
}

