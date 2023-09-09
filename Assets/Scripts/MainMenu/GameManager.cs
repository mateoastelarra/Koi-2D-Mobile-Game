 using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void Start()
    {
        //PlayerPrefs.SetInt("Progress", 0);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
        SFXManager.GetInstance().PlayGongSound(gameObject);
    }

    public void GoToMainMenu()
    {
        SFXManager.GetInstance().PlayUISound(gameObject);
        SceneManager.LoadScene(0);
    }

    public void GoToLegendScene()
    {
        SFXManager.GetInstance().PlayUISound(gameObject);
        SceneManager.LoadScene(3);
    }

    public void Quit()
    {
        SFXManager.GetInstance().PlayDrumSound(gameObject);
        Debug.Log("Quit");
        Application.Quit();
    }
}
