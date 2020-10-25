using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class GameController : MonoBehaviour
{
    [SerializeField] GameObject opponentParent = null;
    [SerializeField] GameObject player = null;
    [SerializeField] GameObject platformParent = null;
    [SerializeField] GameObject obstacleParent = null;
    [SerializeField] GameObject wall = null;
    [SerializeField] GameObject gameOverUI = null;
    [SerializeField] GameObject raceUI = null;
    [SerializeField] GameObject paintUI = null;
    public enum State {Race, Paint, Win, GameOver}
    public static State state;
    public static int placement = 1;
    private void Start() {
        state = State.Race;
        wall.SetActive(false);
        paintUI.SetActive(false);
        gameOverUI.SetActive(false);
        
        CalcPlacement();
    }

    private void CalcPlacement()
    {
        var afterPlayer = 1;
        for(int i = 0; i < opponentParent.transform.childCount; ++i)
        {
            afterPlayer += opponentParent.transform.GetChild(i).transform.position.z > player.transform.position.z ?
                            1 : 0;
        }
        placement = afterPlayer;
    }

    private void Update()
    {
        if(state == State.Race)
        {
            raceUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = 
                        placement + ". Place";
        }    
        else if(state == State.Paint)
        {
            // Disabling other objects, no need them any more.
            platformParent.SetActive(false);
            obstacleParent.SetActive(false);

            raceUI.SetActive(false);

            wall.SetActive(true);
            paintUI.SetActive(true);

            StopPlayer();
        } 
        else if(state == State.Win)
        {
            paintUI.SetActive(false);
            gameOverUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "YOU WON!";
            gameOverUI.SetActive(true);
        } 
        else if(state == State.GameOver)
        {
            paintUI.SetActive(false);
            gameOverUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "GAME OVER!";
            gameOverUI.SetActive(true);

            StopPlayer();
        }
    }

    private void StopPlayer()
    {
        player.GetComponent<Animator>().Play(CharacterController.MotionState.Idle.ToString());            
        player.GetComponent<Rigidbody>().isKinematic = true;
    }
    public void restart()
    {
        gameOverUI.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
