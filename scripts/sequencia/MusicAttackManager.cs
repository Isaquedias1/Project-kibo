using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class MusicAttackManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject sequencePanel;      // Painel com layout horizontal
    public GameObject IndicarCanva;      // Painel com layout horizontal
    public GameObject arrowPrefab;        // Prefab contendo uma Image para a seta
    public Sprite upSprite;
    public Sprite downSprite;
    public Sprite leftSprite;
    public Sprite rightSprite;

    [Header("Configuração")]
    public int sequenceSize = 5;
    public float startWindow = 10f; // 10s para apertar M

    private List<KeyCode> currentSequence = new List<KeyCode>();
    private int index = 0;
    private float startTimer;
    private bool canStart = false;
    private bool playingSequence = false;
    private float inputDelayTimer;
    public Movimento movimentoScript;

    [Header("Tempo da sequência")]
    public float sequenceTimeLimit = 5f; // por exemplo, 5 segundos
    private float sequenceTimer = 0f;
    public Vidas Boss;

    public bool MinigameFinished = false;

    void Start()
    {
        sequencePanel.SetActive(false);
        IndicarCanva.SetActive(false);
    }

    // Chamado pelo boss ao terminar um combo
    public void EnableMusicChance()
    {
        MinigameFinished = false;
        canStart = true;
        startTimer = startWindow;
        Debug.Log("Você tem 10 segundos para apertar M!");
        IndicarCanva.SetActive(true);

    }

    void Update()
    {
        if (canStart)
        {
            startTimer -= Time.deltaTime;

            if (startTimer > 0)
            {
                if (Input.GetKeyDown(KeyCode.M))
                {
                    StartMusicSequence();
                    
                }
            }
            else 
            {
                Debug.Log("Perdeu a chance!");
                FinishMinigame(false);
            }
        }

        if (playingSequence)
        {
            sequenceTimer -= Time.deltaTime;

            if (sequenceTimer <= 0)
            {
                Debug.Log("Você demorou demais!");
                FinishMinigame(false);
                return;
            }
        }

        if (playingSequence)
        {
            CheckPlayerInput();
        }
    }

    private void StartMusicSequence()
    {
        canStart = false;
        playingSequence = true;

        GenerateRandomSequence();
        ShowSequenceOnUI();

        sequencePanel.SetActive(true);
        Debug.Log("Sequência iniciada!");
        IndicarCanva.SetActive(false);

        sequenceTimer = sequenceTimeLimit; 
        inputDelayTimer = 0.15f; 
        movimentoScript.enabled = false;
    }

    private void GenerateRandomSequence()
    {
        currentSequence.Clear();

        KeyCode[] arrows = new KeyCode[]
        {
            KeyCode.UpArrow, KeyCode.DownArrow,
            KeyCode.LeftArrow, KeyCode.RightArrow
        };

        for (int i = 0; i < sequenceSize; i++)
        {
            currentSequence.Add(arrows[Random.Range(0, arrows.Length)]);
        }

        
        currentSequence.Reverse();
        index = 0;
    }

    private void ShowSequenceOnUI()
    {
        foreach (Transform t in sequencePanel.transform)
            Destroy(t.gameObject);

        foreach (var key in currentSequence)
        {
            GameObject obj = Instantiate(arrowPrefab, sequencePanel.transform);
            Image img = obj.GetComponent<Image>();

            if (key == KeyCode.UpArrow) img.sprite = upSprite;
            if (key == KeyCode.DownArrow) img.sprite = downSprite;
            if (key == KeyCode.LeftArrow) img.sprite = leftSprite;
            if (key == KeyCode.RightArrow) img.sprite = rightSprite;
        }
    }

    private void CheckPlayerInput()
    {
        if (inputDelayTimer > 0f)
        {
            inputDelayTimer -= Time.deltaTime;
            return;
        }

        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(currentSequence[index]))
            {
                index++;

                
                Destroy(sequencePanel.transform.GetChild(0).gameObject);

                if (index >= currentSequence.Count)
                {
                    Debug.Log("Sequência correta! Boss toma dano!");
                   
                    FinishMinigame(true);
                }
            }
            else
            {
                Debug.Log("Errou a sequência!");
                FinishMinigame(false);
            }
        }
    }

    private void FinishMinigame(bool success)
    {
        playingSequence = false;
        canStart = false;
        MinigameFinished = true;

        sequencePanel.SetActive(false);
        movimentoScript.enabled = true;

        if (success)
        {
            Debug.Log("Golpe musical executado com sucesso!");
            if (Boss != null) Boss.TomarDano(10);
        }
        else
        {
            Debug.Log("Você falhou o ataque musical.");
        }
    }

}
