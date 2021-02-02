using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public Button Start_Btn;
    public Button HowToPlay_Btn;
    public Button Exit_Btn;
    public EventSystem eventSystem;
    public GameObject UIPanel;

    public void InitBtnState()
    {
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void PlayClickSound()
    {
        SoundManager.instance.PlaySE("click");
    }

    public void GameStart()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenGameControls()
    {
        UIPanel.SetActive(true);
    }

    public void CloseGameControls()
    {
        UIPanel.SetActive(false);
    }

    public void ExitGame()
    {
        Debug.Log("게임종료");
        Application.Quit();
    }

}
