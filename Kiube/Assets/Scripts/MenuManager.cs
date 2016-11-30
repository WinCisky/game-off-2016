using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using GooglePlayGames;

public class MenuManager : MonoBehaviour {

    public Text l_score, h_score;

    void Awake()
    {
        h_score.text = "Highest score: " + PlayerPrefs.GetInt("player_highest_score").ToString();
        l_score.text = "Latest score: "+PlayerPrefs.GetInt("player_latest_score").ToString();
        /*
        // authenticate user:
        Social.localUser.Authenticate((bool success) => {
            // handle success or failure
        });
        */
    }
	public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    public void OpenClassific()
    {
        //PlayGamesPlatform.Instance.ShowLeaderboardUI("CgkIzLWV9OYPEAIQAA");
    }
}
