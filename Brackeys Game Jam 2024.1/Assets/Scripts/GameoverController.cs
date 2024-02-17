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

    public void StartGameover()
    {
        StartCoroutine(GameOver());
    }

    public IEnumerator GameOver()
    {
        //Debug.Log("Gameover");
        _fullScreenEffect.SetActive(true);
        _material.SetFloat(centerStrength, 5f);
        // disable control?

        // game over effect
        //Debug.Log("Effect ON");
        float elapsedTime = 0f;
        while (elapsedTime < gameoverDisplayTime)
        {
            elapsedTime += Time.fixedDeltaTime;

            float lerpedCenterStrength = Mathf.Lerp(5f, -5f, (elapsedTime / gameoverDisplayTime));
            _material.SetFloat(centerStrength, lerpedCenterStrength);

            yield return null;
        }
        //Debug.Log("Effect OFF");
        // Call change/reload scene here
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);

        // Wait for 1 sec
        //Debug.Log("Start waiting");
        yield return new WaitForSeconds(0.5f);
        //Debug.Log("End waiting");

        // undo effect
        elapsedTime = 0f;
        while (elapsedTime < gameoverDisplayTime)
        {
            elapsedTime += Time.deltaTime;

            float lerpedCenterStrength = Mathf.Lerp(-5f, 5f, (elapsedTime / gameoverDisplayTime));
            _material.SetFloat(centerStrength, lerpedCenterStrength);

            yield return null;
        }

        _fullScreenEffect.SetActive(false);
    }
}
