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

    [Header("Music Manager")]
    [SerializeField] private MusicManager musicManager;

    private bool gameStarted = false;

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

        mainMenuController.ShowMenu();
        gameHud.SetActive(false);
        musicManager.PlayMenuMusic();

        InputReader.Instance.EnableUIInputs(true);
        InputReader.Instance.EnablePlayerInputs(false);
    }

    public void SetupEventListeners()
    {
        InputReader.Instance.OnAnyInteract += StartGame;
    }

    public void RemoveEventListeners()
    {
        InputReader.Instance.OnAnyInteract -= StartGame;
    }

    private void StartGame()
    {
        if (!gameStarted)
        {
            gameStarted = true;

            mainMenuController.HideMenu();
            gameHud.SetActive(true);

            StartGameSystems();

            InputReader.Instance.EnableUIInputs(false);
            InputReader.Instance.EnablePlayerInputs(true);
        }
    }

    private void StartGameSystems()
    {
        musicManager.PlayGameplayMusic();
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
}
