using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEditor.ShaderKeywordFilter;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] QuestionsSO question;

    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons;
    bool hasAnsweredEarly;

    [Header("Button Colors")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;

    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;
    void Start()
    {
        timer = FindFirstObjectByType<Timer>();
        GetNextQuestion();

    }

    void Update()
    {
        timerImage.fillAmount = timer.fillFraction;
        if (timer.loadNextQuestion)
        {
            hasAnsweredEarly = false;
            GetNextQuestion();
            timer.loadNextQuestion = false;
        }
        else if(!hasAnsweredEarly && !timer.isAnsweringQuestion)
        {
            DisplayAnswer(10);
            SetButtonState(false);
        }
    }
    void DisplayQuestions()
    {
        questionText.text = question.Question;

        int index = 0;
        foreach (var button in answerButtons)
        {
            button.GetComponentInChildren<TextMeshProUGUI>().text = question.Answers[index];
            index++;
        }
    }

    void SetButtonState(bool state) 
    {
        for (int i = 0; i < answerButtons.Length; i++) 
        {
            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }

    void DisplayAnswer(int index)
    {

        if (index == question.CorrectAnswerIndex)
        {
            Image buttonImage = answerButtons[index].GetComponent<Image>();
            questionText.text = "Correct";
            buttonImage.sprite = correctAnswerSprite;
        }
        else
        {
            questionText.text = "Sorry, the correct answer was:\n" + question.Answers[question.CorrectAnswerIndex];
            Image buttonImage = answerButtons[question.CorrectAnswerIndex].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
        }
    }

    void GetNextQuestion()
    {
        SetButtonState(true);
        SetDefaultButtonSprites();
        DisplayQuestions();
    }



    public void OnAnswerSelected(int index)
    {
        hasAnsweredEarly = true;
        DisplayAnswer(index);
        SetButtonState(false);
        timer.CancelTimer();
    }
    private void SetDefaultButtonSprites()
    {

        for (int i = 0; i < answerButtons.Length; i++)
        {
            Image buttonImage = answerButtons[i].GetComponent<Image>();
            buttonImage.sprite = defaultAnswerSprite;
        }
    }
}
