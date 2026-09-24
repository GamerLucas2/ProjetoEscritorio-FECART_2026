using System;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
// using UnityEngine.UIElements;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager Instance { get; private set; }

    [SerializeField] private EventSystem eventSystem;

    [Header("-HUD-")]
    [SerializeField] private GameObject gameHUD;
    [SerializeField] private GameObject taskList;
    [SerializeField] private GameObject preStartText;
    
    [FormerlySerializedAs("hotbarSlots")] [SerializeField] private GameObject[] hotbarSlotFiled;
    [FormerlySerializedAs("hotbarSlotImage")] [SerializeField] private Image[] hotbarSlotSelect;
    [SerializeField] private TextMeshProUGUI[] hotbarItemName;
    [SerializeField] private TextMeshProUGUI[] taskNameText = new TextMeshProUGUI[length];
    
    /*[Header("-Dialogue Box-")]
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private GameObject dialoguePanel;*/
    
    [Header("-EndScreen-")]
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject endScreen;
    [SerializeField] private TextMeshProUGUI finalTimeText;
    [SerializeField] private TextMeshProUGUI bestTimeText;
    [SerializeField] private Selectable[] buttonToSelect;

    [Header("-Other-")] 
    [SerializeField] TextMeshProUGUI fadeIntext;
    [SerializeField] private float fadeDuration;
    [SerializeField] private string fadeTextName;
    
    

    private static int length;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (Instance != null)
            Destroy(this);
        else
            Instance = this;
        // taskNameText = GameObject.FindGameObjectsWithTag("taskName");
    }

    private void Start()
    {
        gameHUD.SetActive(true);
        endScreen.SetActive(false);
        taskList.SetActive(false);
        preStartText.SetActive(true);
        
        hotbarSlotFiled[0].gameObject.SetActive(false);
        hotbarSlotFiled[1].gameObject.SetActive(false);
        hotbarSlotSelect[0].gameObject.SetActive(true);
        hotbarSlotSelect[1].gameObject.SetActive(false);
        
        fadeIntext.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.tasksActive)
        {
            preStartText.SetActive(false);
            taskList.SetActive(true);
        }
    }

    public void EndLevelScreen()
    {
        Time.timeScale = 0f;
        endScreen.SetActive(true);
        gameHUD.SetActive(false);
        finalTimeText.text = "Clear Time: " + FormatEndTimer(ScoreManager.Instance.time, finalTimeText);
        bestTimeText.text = "Best Time: " + FormatEndTimer(ScoreManager.Instance.bestTime, bestTimeText); 
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        eventSystem.SetSelectedGameObject(buttonToSelect[0].gameObject);
    }

    private string FormatEndTimer(float displayTime, TextMeshProUGUI timerText)
    {
        float minutes = Mathf.FloorToInt(displayTime / 60);
        float seconds = Mathf.FloorToInt(displayTime % 60);
        
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void GameOverScreen()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        gameHUD.SetActive(false);
        gameOverScreen.SetActive(true);
        eventSystem.SetSelectedGameObject(buttonToSelect[1].gameObject);
    }

    public void CheckTaskInList(int taskNumber)
    {
        taskNameText[taskNumber].text = "- Completo";
    }

    public void ChangeItemIndicatorState(int i)
    {
        if (hotbarSlotSelect[i].gameObject.activeSelf)
            hotbarSlotSelect[i].gameObject.SetActive(false);
        else if (!hotbarSlotSelect[i].gameObject.activeSelf)
            hotbarSlotSelect[i].gameObject.SetActive(true);
    }

    public void ToggleItemIndicator(int i, string name)
    {
        if(hotbarSlotFiled[i].gameObject.activeSelf)
            hotbarSlotFiled[i].SetActive(false);
        else if (!hotbarSlotFiled[i].gameObject.activeSelf)
        {
            hotbarSlotFiled[i].SetActive(true);
            hotbarItemName[i].text = name;
        }
    }

    public void UpdateTaskCounter(TextMeshProUGUI taskCounter, string tasksCompleted, string tasksToComplete)
    {
        taskCounter.text = string.Format("Tasks: {0}/{1}", tasksCompleted, tasksToComplete);
    }

    public void ShowTextOnShiftStart()
    {
        if (fadeIntext != null)
        {
            DialogueController.OnDialogueEnded -= ShowTextOnShiftStart;
            fadeIntext.text = "Tasks Start";
            fadeIntext.gameObject.SetActive(true);
            fadeIntext.color = Color.clear;
            StartCoroutine(FadeTextIn(Color.white, fadeDuration));
        }
    }

    public void ShowTextOnShiftEnd()
    {
        TaskSystem.AllTasksComplete -= ShowTextOnShiftEnd;

        if (fadeIntext != null)
        {
            fadeIntext.text = "Fale com " + fadeTextName;
            fadeIntext.gameObject.SetActive(true);
            fadeIntext.color = Color.clear;
            StartCoroutine(FadeTextIn(Color.white, fadeDuration));
        }
    }

    IEnumerator FadeTextIn(Color endValue, float duration)
    {
        float time = 0;
        Color startValue = fadeIntext.color;

        while (time < duration)
        {
            fadeIntext.color = Color.Lerp(startValue, endValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        fadeIntext.color = endValue;
        StartCoroutine(WaitForFadeOut(new WaitForSeconds(0.6f)));
        StopCoroutine(FadeTextIn(endValue, duration));
    }

    IEnumerator FadeTextOut(Color endValue, float duration)
    {
        float time = 0;
        Color startValue = fadeIntext.color;

        while (time < duration)
        {
            fadeIntext.color = Color.Lerp(startValue, endValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        fadeIntext.color = endValue;
        fadeIntext.gameObject.SetActive(false);
        StopCoroutine(FadeTextIn(endValue, duration));
    }

    IEnumerator WaitForFadeOut(WaitForSeconds wait)
    {
        for (int i = 0; i < 2; i++)
            yield return wait;
        
        StartCoroutine(FadeTextOut(Color.clear, fadeDuration));
        StopCoroutine(WaitForFadeOut(new WaitForSeconds(0)));
    }

    private void OnEnable()
    {
        if (GameEndScript.Instance ==null)
            TaskSystem.AllTasksComplete += ShowTextOnShiftEnd;
    }
    private void OnDisable()
    {
        if (GameEndScript.Instance ==null)
            DialogueController.OnDialogueEnded -= EndLevelScreen;
        
        DialogueController.OnDialogueEnded -= ShowTextOnShiftEnd;
        DialogueController.OnDialogueEnded -= ShowTextOnShiftStart;
    }
    
}
