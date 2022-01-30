using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum Option { SceneChange, ExitGame, TogglePause}

public class MenuButton : MonoBehaviour
{
    [SerializeField] string scene = "";
    [SerializeField] Option option = Option.SceneChange;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        switch (option)
        {
            case Option.SceneChange:
                SceneManager.LoadScene(scene);
                Time.timeScale = 1;
                break;
            case Option.ExitGame:
                Application.Quit();
                break;
            case Option.TogglePause:
                Pause.TogglePause();
                break;
        }
    }
}
