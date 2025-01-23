using UnityEngine;
using TMPro;

public class Quiz : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] QuestionsSO question;
    [SerializeField] GameObject[] answerButtons;
    void Start()
    {
        questionText.text = question.Question;

        int index = 0;
        foreach (var button in answerButtons) 
        {
            button.GetComponentInChildren<TextMeshProUGUI>().text = question.Answers[index];
            index++;
        }
    }
}
