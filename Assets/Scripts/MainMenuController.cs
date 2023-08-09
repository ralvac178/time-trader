using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject HelpPanel;
    [SerializeField] private GameObject[] panels;

    // Start is called before the first frame update
    private void Start()
    {
        foreach (var item in panels)
        {
            item.SetActive(false);
        }
    }

    public void LoadNewScene()
    {
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }

    public void GameQuit()
    {
        Application.Quit();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameQuit();
        }
    }

    public void CloseWindow(GameObject panel)
    {
        panel.SetActive(false);
    }

    public void OpennWindow(GameObject panel)
    {
        panel.SetActive(true);
    }
}
