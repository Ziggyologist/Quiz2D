using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEditor.ShaderKeywordFilter;
using NUnit.Framework;
using System.Collections.Generic;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] List<QuestionsSO> questions = new List<QuestionsSO>();
    QuestionsSO currentQuestion;

    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons;
    bool hasAnsweredEarly = true;

    [Header("Button Colors")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;

    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;

    [Header("Scoring")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    [Header("ProgressBar")]
    [SerializeField] Slider progressBar;

    public bool isComplete = false;


    void Awake()
    {
        timer = FindFirstObjectByType<Timer>();
        scoreKeeper = FindFirstObjectByType<ScoreKeeper>();
        progressBar.maxValue = questions.Count;
        progressBar.value = 0;

    }

    void Update()
    {
        timerImage.fillAmount = timer.fillFraction;
        if (timer.loadNextQuestion)
        {
            if (progressBar.value == progressBar.maxValue)
            {
                isComplete = true;
                return;
            }

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
        questionText.text = currentQuestion.Question;

        int index = 0;
        foreach (var button in answerButtons)
        {
            button.GetComponentInChildren<TextMeshProUGUI>().text = currentQuestion.Answers[index];
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

        if (index == currentQuestion.CorrectAnswerIndex)
        {
            Image buttonImage = answerButtons[index].GetComponent<Image>();
            questionText.text = "Correct";
            buttonImage.sprite = correctAnswerSprite;
            scoreKeeper.IncrementCorrectAnswers();
        }
        else
        {
            questionText.text = "Sorry, the correct answer was:\n" + currentQuestion.Answers[currentQuestion.CorrectAnswerIndex];
            Image buttonImage = answerButtons[currentQuestion.CorrectAnswerIndex].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
        }
    }

    void GetNextQuestion()
    {

        if (questions.Count > 0)
        {
            SetButtonState(true);
            SetDefaultButtonSprites();
            GetRandomQuestion();
            DisplayQuestions();
            scoreKeeper.IncrementQuestionsSeen();
            progressBar.value++;
        }
    }

    private void GetRandomQuestion()
    {
        int index = UnityEngine.Random.Range(0, questions.Count);
        currentQuestion = questions[index];
        if (questions.Contains(currentQuestion))
        {
            questions.Remove(currentQuestion);
        }
    }

    public void OnAnswerSelected(int index)
    {
        hasAnsweredEarly = true;
        DisplayAnswer(index);
        SetButtonState(false);
        timer.CancelTimer();
        scoreText.text = "Score: " + scoreKeeper.CalculateScore() + "%";

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
