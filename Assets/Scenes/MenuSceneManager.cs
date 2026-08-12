using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuSceneManager : MonoBehaviour
{
    [SerializeField] Button btnPlay;
    [SerializeField] Button btnWipe;
    [SerializeField] Button btnQuit;


    public void Play()
    {
        SceneManager.LoadScene("Gameplay");
    }


    public void Wipe()
    {
        PlayerPrefs.DeleteAll();
    }

    public void QuitGame()
    {
        Application.Quit(); 
    }

}
