using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
using System.Collections;
using Unity.PlasticSCM.Editor.WebApi;

public class EnemyQ : MonoBehaviour
{
    //need to serialize for nexted classes
    [System.Serializable]
    public class QuestionData
    {
        public string question;
        public string[] choices;
        public string correct;
    }

    public Canvas quizCanvas;

    public PlayerMovement p;

    public int typeQ;
    public TextMeshProUGUI questionText;
    public Button[] answerButtons;
    private int correctIndex = -1;
    private string apiUrl1 = "http://127.0.0.1:5000/generateConstant";
    private string apiUrl2 = "http://127.0.0.1:5000/generateTore";
    private string apiUrl3 = "http://127.0.0.1:5000/generatePower";

    void Start()
    {
        foreach (var btn in answerButtons)
        {
            btn.onClick.AddListener(() => OnAnswerSelected(btn));
        }

        quizCanvas.gameObject.SetActive(true);
        ShowQuiz();
    }

    public void ShowQuiz()
    {
        StartCoroutine(GenerateQuestion());
    }

    //Ienumerators ~ coroutines are defined a methods with a return type of IEnumerator - why coroutines? perform ops w/o blocking the main thread - though they still execute on main
    IEnumerator GenerateQuestion()
    {
        //do my funny requests depending on the enemy type
        UnityWebRequest request;
        if(typeQ == 1){
            request = UnityWebRequest.Get(apiUrl1);
            yield return request.SendWebRequest();
        }
        else if(typeQ == 2)
        {
            request = UnityWebRequest.Get(apiUrl2);
            yield return request.SendWebRequest();
        }
        else{
            request = UnityWebRequest.Get(apiUrl3);
            yield return request.SendWebRequest();
        }

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error fetching question: " + request.error);
            yield break;
        }

        // Parse JSON
        QuestionData questionData = JsonUtility.FromJson<QuestionData>(request.downloadHandler.text);
        questionText.text = questionData.question;

        // Determine correct answer 
        correctIndex = questionData.correct.ToUpper()[0] - 'A';

        // Set answer button text and set them as interactable
        for (int i = 0; i < answerButtons.Length; i++)
        {
            var txt = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            txt.text = questionData.choices[i];
            answerButtons[i].interactable = true;
        }
    }

    void OnAnswerSelected(Button btn)
    {
        int chosenIndex = System.Array.IndexOf(answerButtons, btn);
        if (chosenIndex == correctIndex)
        {
            //vanquish our foe
            StartCoroutine(Die());
        }
        else
        {
            Debug.Log("Incorrect answer.");
            //penalty
            float curr = p.getcurrhealth();
            p.setcurrhealth(curr -= 5);
            StartCoroutine(FlashRed(p.GetComponent<SpriteRenderer>(), 0.2f));
        }
    }

    IEnumerator Die()
    {
        p.setcurrhealth(100); //give them their health back 
        foreach (var btn in answerButtons)
            btn.interactable = false;
        
        Destroy(gameObject); //destroy the enemy
        yield break;
    }

    public IEnumerator FlashRed(SpriteRenderer sr, float duration)
    {
        sr.color = new Color(1f, 0.4f, 0.4f); // red
        yield return new WaitForSeconds(duration);
        sr.color = Color.white; // reset 
    }
}

