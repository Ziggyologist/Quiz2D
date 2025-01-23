using UnityEngine;

[CreateAssetMenu(menuName ="Quiz Question", fileName = "New Question")] //Asset Menu makes the object appear as an option in the Unity UI
public class QuestionsSO : ScriptableObject
{
    [TextArea(2,6)] [SerializeField] string question;
    [SerializeField] string[] answers = new string[4];
    [SerializeField]  int correctAnswerIndex;
    public string Question { get { return question; }}
    public string[] Answers { get { return answers; }}
    public int CorrectAnswerIndex {  get { return correctAnswerIndex; }}
    public string GetAnswer(int index)
    {
        return answers[index];
    }
}
