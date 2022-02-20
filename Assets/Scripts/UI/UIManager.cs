using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum SceneName
{
    Scene_Game,
    Scene_MainMenu
}

public enum TowerType
{
    Cannon,Blaster,Balista
}
public class UIManager : SingletonMonoBehaviour<UIManager>
{


    [Header("Tower Prefab")]
    [SerializeField] private GameObject cannonTower;
    [SerializeField] private GameObject blasterTower;
    [SerializeField] private GameObject balistaTower;

    [Header("UI Buttons")]
    [SerializeField] private Button cannonButton;
    [SerializeField] private Button balistaButton;
    [SerializeField] private Button blasterButton;
    [Header("UI Text")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI baseHealthText;
    [SerializeField] private TextMeshProUGUI maxAllowedTowerText;

    [Header("Menus")]
    [SerializeField] private GameObject PauseUI;
    [SerializeField] private GameObject GameOverUI;

    [Header("Fader UI")]
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private CanvasGroup faderCanvasGroup;
    [SerializeField] private Image faderImage = null;
    private bool isFading = false;


    private SceneName SceneName;

    private void OnEnable()
    {
        cannonButton.onClick.AddListener(SetTowerToCannon);
        balistaButton.onClick.AddListener(SetTowerToBalista);
        blasterButton.onClick.AddListener(SetTowerToBlaster);

    }

    private void OnDisable()
    {
        cannonButton.onClick.RemoveListener(SetTowerToCannon);
        balistaButton.onClick.RemoveListener(SetTowerToBalista);
        blasterButton.onClick.RemoveListener(SetTowerToBlaster);
    }

    private void SetTowerToCannon()
    {
        TowerFactory.Instance.TowerPrefab = cannonTower;
    }

    private void SetTowerToBalista()
    {
        TowerFactory.Instance.TowerPrefab = balistaTower;
    }

    private void SetTowerToBlaster()
    {
        TowerFactory.Instance.TowerPrefab = blasterTower;
    }

    public void UpdateScoreUI(int score)
    {
        scoreText.SetText($"Score: {score.ToString()}");
    }

    public void UpdateBaseHealthUI(int currentHealth)
    {
        baseHealthText.SetText($"Base Health: {currentHealth.ToString()}");
    }

    public void UpdateMaxAllowedTowerUI(int count)
    {
        maxAllowedTowerText.SetText($"Max Tower: {count.ToString()}");
    }

    public void ShowPauseUI()
    {
        Time.timeScale = 0;
        PauseUI.SetActive(true);
    }

    public void HidePauseUI()
    {
        Time.timeScale = 0;
        PauseUI.SetActive(false);
    }

    public void ShowGameOverUI()
    {
        Time.timeScale = 0;
        GameOverUI.SetActive(true);
    }

    public void RestartGame()
    {
        print("restart");
        FadeAndLoadScene(SceneName.Scene_Game.ToString());
    }

    public void LoadMainMenu()
    {
        print("Main menu");
        FadeAndLoadScene(SceneName.Scene_MainMenu.ToString());
    }

    private void FadeAndLoadScene(string sceneName)
    {
        if (!isFading)
        {
            StartCoroutine(FadeAndLoadCoroutine(sceneName));
        }
    }

    private IEnumerator FadeAndLoadCoroutine(string sceneName)
    {
        print("FadeAndLoadCoroutine");
        yield return StartCoroutine(Fade(1f));

        SceneManager.LoadSceneAsync(sceneName);
    }

    private IEnumerator Fade(float finalAlpha)
    {

        // Set the fading flag to true so the FadeAndSwitchScenes coroutine won't be called again.
        isFading = true;

        // Make the CanvasGroup blocks raycast into the scene so no more input can be accepted.
        faderCanvasGroup.blocksRaycasts = true;

        // Calculate how fast the CanvasGroup should fade based on it's current alpha,
        // it's final alpha and how long it has to change between the two
        float fadeSpeed = Mathf.Abs(faderCanvasGroup.alpha - finalAlpha) / fadeDuration;

        // While the CanvasGroup hasn't reached the final alpha yet...
        while (!Mathf.Approximately(faderCanvasGroup.alpha,finalAlpha))
        {
            // ... move the alpha twards it's target alpha
            faderCanvasGroup.alpha = Mathf.MoveTowards(faderCanvasGroup.alpha,
                finalAlpha, fadeSpeed * Time.unscaledDeltaTime);

            // Wait for a frame then continue
            yield return null;
        }

        // Set the flag to false since the fade has finished.
        isFading = false;

        // Stop the CanvasGroup from blocking raycast to input is no longer ignored.
        faderCanvasGroup.blocksRaycasts = false;
    }

    private IEnumerator Start()
    {
        Time.timeScale = 0;

        // Set the initial alpha to start off with a black screen
        faderImage.color=new Color(0f,0f,0f,1f);
        faderCanvasGroup.alpha = 1f;

        // Once the scene is finished loading, start fading in.
        StartCoroutine(Fade(0f));

        yield return new WaitForSecondsRealtime(1f);
        Time.timeScale = 1;
    }
}
