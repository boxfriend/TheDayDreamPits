using UnityEngine;
using UnityEngine.SceneManagement;
using Boxfriend.Player;

public class GameEnder : MonoBehaviour
{
    private void Awake ()
    {
        PlayerWinCondition.OnPlayerWin += () => SceneManager.LoadScene("TheEnd", LoadSceneMode.Single);
        PlayerStats.OnPlayerDeath += () => SceneManager.LoadScene("Death", LoadSceneMode.Additive);
    }
}
