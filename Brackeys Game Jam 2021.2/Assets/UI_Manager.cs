using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{
    private void Start() {
        Cursor.visible = true;
    }

    public string PlayScene;

    public GameObject CreditsPannel;

    public void Play()
    {
        SceneManager.LoadScene(PlayScene);
    }
    public void CreditsOpen()
    {
        CreditsPannel.SetActive(true);
    }
    public void CreditsClose()
    {
        CreditsPannel.SetActive(false);
    }
    public void Quit()
    {
        Application.Quit(0);
    }
}
