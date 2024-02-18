using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class GameoverController : MonoBehaviour
{
    public static GameoverController instance;

    [SerializeField]
    private float gameoverDisplayTime = 2f;
    [SerializeField]
    private ScriptableRendererFeature _fullScreenEffect;
    [SerializeField]
    private Material _material;
    private int centerStrength = Shader.PropertyToID("_CenterStrength");

    public bool isGameover = false;
    public bool isFinishing = false;
    public bool isStarting = false;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        _fullScreenEffect.SetActive(false);
    }

    public void StartGame()
    {
        if (!isStarting)
            StartCoroutine(StartNewGame());
    }

    public void StartGameover()
    {
        if(!isFinishing && !isGameover)
            StartCoroutine(GameOver());
    }

    public void StartFinishGame()
    {
        if(!isFinishing)
        {
            StartCoroutine(FinishGame());
        }
    }

    private IEnumerator GameOver()
    {
        isGameover = true;
        //Debug.Log("Gameover");
        _fullScreenEffect.SetActive(true);
        _material.SetFloat(centerStrength, 5f);
        // disable control?

        // game over effect
        //Debug.Log("Effect ON");
        StartCoroutine(FadeIn());
        yield return new WaitForSeconds(gameoverDisplayTime + 0.5f);
        //Debug.Log("Effect OFF");
        // Call change/reload scene here
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);

        // undo effect
        StartCoroutine(FadeOut());
        yield return new WaitForSeconds(gameoverDisplayTime + 0.1f);

        _fullScreenEffect.SetActive(false);
        isGameover = false;
    }

    private IEnumerator FinishGame()
    {
        isFinishing = true;
        _fullScreenEffect.SetActive(true);
        _material.SetFloat(centerStrength, 5f);
        StartCoroutine(FadeIn());
        yield return new WaitForSeconds(gameoverDisplayTime + 1f);

        SceneManager.LoadScene("FinishGame");

        yield return new WaitForSeconds(5f);
        _fullScreenEffect.SetActive(false);

        SceneManager.LoadScene("MainMenu");
        isFinishing = false;
    }

    private IEnumerator StartNewGame()
    {
        isStarting = true;
        //Debug.Log("Gameover");
        _fullScreenEffect.SetActive(true);
        _material.SetFloat(centerStrength, 5f);
        // disable control?

        SceneManager.LoadScene("MainLevel");

        // undo effect
        StartCoroutine(FadeOut());
        yield return new WaitForSeconds(gameoverDisplayTime + 0.1f);

        _fullScreenEffect.SetActive(false);
        isStarting = false;
    }

    private IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        while (elapsedTime < gameoverDisplayTime)
        {
            elapsedTime += Time.fixedDeltaTime;

            float lerpedCenterStrength = Mathf.Lerp(5f, -5f, (elapsedTime / gameoverDisplayTime));
            _material.SetFloat(centerStrength, lerpedCenterStrength);

            yield return null;
        }
    }

    private IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        while (elapsedTime < gameoverDisplayTime)
        {
            elapsedTime += Time.deltaTime;

            float lerpedCenterStrength = Mathf.Lerp(-5f, 5f, (elapsedTime / gameoverDisplayTime));
            _material.SetFloat(centerStrength, lerpedCenterStrength);

            yield return null;
        }
    }
}
