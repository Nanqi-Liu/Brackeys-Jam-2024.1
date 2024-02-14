using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public static bool IsPaused = false;
    public GameObject pauseMenuUI;

    [SerializeField]
    InputActionReference pauseButton;
    // Update is called once per frame
    void Update()
    {
        if (pauseButton.action.triggered)
        {
            Debug.Log("Pressed");
            if (IsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    private void FixedUpdate()
    {
        
    }

    public void Resume()
    {
        Debug.Log("Pause");
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        IsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        IsPaused = true;
    }
}
