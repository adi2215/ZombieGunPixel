using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ManagerMenu : MonoBehaviour
{
    public Animator trans;

    public Data data;

    public Button info, rule;

    public void BackToMenu() => StartCoroutine(LoadLevelNext(0));
    public void GoToGame() => StartCoroutine(LoadLevelNext(1));
    public void ExitToGame() => Application.Quit();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            BackToMenu();
            data.DialogManager = false;
            data.countFuel = 0;
            data.countCoins = 0;
        }

        else if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            data.countFuel = 0;
            data.countCoins = 0;
            data.DialogManager = false;
            LoadLevel(SceneManager.GetActiveScene().buildIndex);
        }

        else if (Input.GetKeyDown(KeyCode.K))
        {
            if (info != null)
                info.onClick.Invoke();
        }

        else if (Input.GetKeyDown(KeyCode.L))
        {
            if (rule != null)
                rule.onClick.Invoke();
        }
    }


    public void LoadLevel(int index)
    {
        StartCoroutine(LoadLevelNext(index));
    }

    IEnumerator LoadLevelNext(int levelIndex)
    {
        trans.SetTrigger("Start");

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(levelIndex);
    }
}
