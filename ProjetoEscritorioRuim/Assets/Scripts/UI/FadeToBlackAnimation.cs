
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeToBlackAnimation : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image blackBox;
    [SerializeField] Color targetColor = Color.black;
    [SerializeField] private GameObject endSceneTextThing;
    
    [Header("Booleans")]
    [SerializeField] private bool isTitleScreen;
    [SerializeField] private bool isIntermission;
    [SerializeField] private bool isEndScene;
    
    private bool movingToNextLevel;
    private LevelManager levelManager;

    private void Awake()
    {
        levelManager = gameObject.GetComponent<LevelManager>();
    }

    private void OnEnable()
    {
        if (isIntermission || isEndScene)
            DialogueController.OnDialogueEnded += LevelTransition;
    }

    private void OnDisable()
    {
        DialogueController.OnDialogueEnded -= LevelTransition;
    }

    private void Start()
    {
        if (endSceneTextThing != null)
            endSceneTextThing.SetActive(false);
        
        if(!isTitleScreen)
        {
            blackBox.gameObject.SetActive(true);
            movingToNextLevel = false;
            blackBox.color = Color.black;
            StartCoroutine(FadeFromBlack(Color.clear, 1f));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (blackBox.color == targetColor && movingToNextLevel)
        {
            if (isIntermission)
                levelManager.MoveToNextLevel(PlayerPrefs.GetInt("LevelID") + 1);
            else if (isEndScene)
            {
                Time.timeScale = 0;
                DialogueController.OnDialogueEnded += GameEndScript.Instance.EndGame;
                endSceneTextThing.SetActive(true);
                GameEndScript.Instance.ChangeDialogueBox();
            }
        }
        if(GameManager.Instance.inConversation)
            endSceneTextThing.SetActive(false);
    }
    
    public void LevelTransition()
    {
        blackBox.gameObject.SetActive(true);
        print("moving to next level");
        movingToNextLevel = true;
        StartCoroutine(FadeToBlack(Color.black, 1f));
    }
    
    
    IEnumerator FadeToBlack(Color endValue, float duration)
    {
        float time = 0;
        Color startValue = blackBox.color;

        while (time < duration)
        {
            blackBox.color = Color.Lerp(startValue, endValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        blackBox.color = endValue;
        StopCoroutine(FadeFromBlack(endValue, duration));
    }

    IEnumerator FadeFromBlack(Color endValue, float duration)
    {
        float time = 0;
        Color startValue = blackBox.color;

        while (time < duration)
        {
            blackBox.color = Color.Lerp(startValue, endValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        blackBox.color = endValue;
        blackBox.gameObject.SetActive(false);
        StopCoroutine(FadeFromBlack(endValue, duration));
    }
}
