using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameManager manager;
    public Text fruitText;
    public Text QuizText;


    // Update is called once per frame
    void LateUpdate()
    {
        fruitText.text = string.Format("{0:n0}", manager.fruitPoint);
        QuizText.text = string.Format("{0:n0}",manager.quizPoint);
    }
}
