using UnityEngine;
using TMPro;

public class EndScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI finalScoreText;
    ScoreKeeper scoreKeeper;

    void Start()
    {
        scoreKeeper = FindFirstObjectByType<ScoreKeeper>();
    }

    public void ShowFinalScore()
    {
        finalScoreText.text = "Congratulations! \nYou're score is: " + scoreKeeper.CalculateScore() + "%";
    }

}
