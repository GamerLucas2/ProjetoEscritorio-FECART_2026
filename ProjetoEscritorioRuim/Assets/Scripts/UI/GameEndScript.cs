using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameEndScript : MonoBehaviour
{
    public static GameEndScript Instance  { get; private set; }
    
    [Header("End Screen")]
    [SerializeField] EventSystem eventSystem;
    [SerializeField] private Selectable button;
    [SerializeField] private GameObject endScreen;
    private GameObject canvas;
    
    [Header("Dialogue Box")]
    [SerializeField] TextMeshProUGUI dialogue;
    [SerializeField] private GameObject box;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (Instance != null)
            Destroy(this);
        else
            Instance = this;
        
        if (endScreen == null)
            print("No end screen found");
    }

    private void Start()
    {
        endScreen.SetActive(false);
    }

    // Update is called once per frame
    public void EndGame()
    {
        if (endScreen != null)
        {
            endScreen.SetActive(true);
            GameManager.Instance.levelCleared =  true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        
            eventSystem.SetSelectedGameObject(button.gameObject);
        }
    }

    public void ChangeDialogueBox()
    {
        box.SetActive(false);
        dialogue.transform.localPosition = new Vector3(0, 0, 0);
        dialogue.color = Color.white;
    }

    void OnDisable()
    {
        DialogueController.OnDialogueEnded -= EndGame;
    }
}
