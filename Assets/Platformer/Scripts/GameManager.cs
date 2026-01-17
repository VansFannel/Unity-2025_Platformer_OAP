using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private MoveCamera moveCamera;
    [SerializeField] private float defaultCameraMovementSpeed = 3.0f;

    [Header("UI Elements")]
    [SerializeField] private MainMenuController mainMenuController;
    [SerializeField] private GameObject gameHud;
    [SerializeField] private Image fadingBackgroundImage;
    [SerializeField] private GameObject gameControlsTextInfo;

    [Header("Game Elements")]
    //[SerializeField] private EnemySpawner enemySpawner;
    //[SerializeField] private PlatformRepositioner platformRepositioner;

    [Header("Music Manager")]
    [SerializeField] private MusicManager musicManager;

    private bool gameStarted = false;
    //private Vector3 cameraInitialPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitializeGame();
        SetupEventListeners(); 
    }

    private void OnDestroy()
    {
        RemoveEventListeners();
    }

    private void InitializeGame()
    {
        //cameraInitialPosition = moveCamera.transform.position;

        mainMenuController.ShowMenu();
        gameHud.SetActive(false);
        //moveCamera.SetAutoMove(false);
        musicManager.PlayMenuMusic();

        InputReader.Instance.EnableUIInputs(true);
        InputReader.Instance.EnablePlayerInputs(false);
    }

    public void SetupEventListeners()
    {
        InputReader.Instance.OnAnyInteract += StartGame;

        //playerController.PlayerStartDyingEvent += PreFinishGame;
        //playerController.PlayerDeadEvent += FinishGame;
        //playerController.PlayerBriefSlowDown += SetCameraMovementSpeed;
        //playerController.PlayerKeepRunning += SetCameraDefaultMovementSpeed;
    }

    public void RemoveEventListeners()
    {
        InputReader.Instance.OnAnyInteract -= StartGame;

        //playerController.PlayerStartDyingEvent -= PreFinishGame;
        //playerController.PlayerDeadEvent -= FinishGame;
        //playerController.PlayerBriefSlowDown -= SetCameraMovementSpeed;
        //playerController.PlayerKeepRunning -= SetCameraDefaultMovementSpeed;
    }

    private void StartGame()
    {
        if (!gameStarted)
        {
            gameStarted = true;

            mainMenuController.HideMenu();
            gameHud.SetActive(true);
            //ShowGameControls();

            StartGameSystems();

            InputReader.Instance.EnableUIInputs(false);
            InputReader.Instance.EnablePlayerInputs(true);
        }
    }

    private void StartGameSystems()
    {
        //moveCamera.SetAutoMove(true);
        musicManager.PlayGameplayMusic();
        //SetCameraDefaultMovementSpeed();
        //playerController.PlayRun();
        //enemySpawnner.StartSpawning();
    }

    private void PreFinishGame()
    {
        //moveCamera.SetAutoMove(false);
        //enemySpawner.StopSpawning();
    }

    private void FinishGame()
    {
        StartCoroutine(FinishGameRoutine());
    }

    private void ShowGameControls()
    {
        ActivateGameControlsInfoText();
        Invoke(nameof(DeactivateGameControlsInfoText), 2.0f);
    }

    private void ActivateGameControlsInfoText()
    {
        gameControlsTextInfo.SetActive(true);
    }

    private void DeactivateGameControlsInfoText ()
    {
        gameControlsTextInfo.SetActive(false);
    }

    private void SetCameraMovementSpeed(float newSpeed)
    {
        moveCamera.SetSpeed(newSpeed);
    }

    private void SetCameraDefaultMovementSpeed()
    {
        SetCameraMovementSpeed(defaultCameraMovementSpeed);
    }

    private IEnumerator FinishGameRoutine()
    {
        gameStarted = false;

        yield return HandleNewHighScore();

        yield return FadeInBgImageRoutine();

        ResetGameState();

        yield return FadeOutBgImageRoutine();
    }

    private IEnumerator HandleNewHighScore()
    {
        yield return null;
    }

    private void ResetGameState()
    {
        mainMenuController.ShowMenu();
        gameHud.SetActive(false);

        moveCamera.SetAutoMove(false);
       // moveCamera.transform.position = cameraInitialPosition;

        musicManager.PlayMenuMusic();

        InputReader.Instance.EnableUIInputs(true);
        InputReader.Instance.EnablePlayerInputs(false);
    }

    private IEnumerator FadeInBgImageRoutine()
    {
        yield return null;
    }

    private IEnumerator FadeOutBgImageRoutine()
    {
        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
