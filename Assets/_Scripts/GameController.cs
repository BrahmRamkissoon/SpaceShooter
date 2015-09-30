using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{

    public GameObject hazard;

    // define since spawnPosition has a fixed x value, we need it random
    public Vector3 spawnValues;

    // count number of times cycling through loop of hazard waves
    public int hazardCount;

    // wait time value between hazard spawns
    // see coroutine spawnWaves
    public float spawnWait;

    // ready time for player before game starts
    public float startWait;

    // time between waves
    public float waveWait;

    // hold score text
    public GUIText scoreText;
    private int _score;

    public GUIText restartText;
    public GUIText gameOverText;
    public GUIText waveNumberText;

    private bool _gameOver;
    private bool _restart;
    private int _wave;


    void Update()
    {
        if (_restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Application.LoadLevel(Application.loadedLevel);

            }
        }
    }
    void Start()
    {
        

        _gameOver = false;
        _restart = false;
        restartText.text = "";
        gameOverText.text = "";
        waveNumberText.text = "Wave 1 (10) asteroids!";
        _wave = 1;
        _score = 0;
        UpdateScore();
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);

        // infinite loop
        while (true)
        {
           
            for (int i = 0; i < hazardCount; i++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y,
                    spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
            // add an extra asteroid up from initial value
            hazardCount += 5;
            _wave++;
            waveNumberText.text = "Wave " + _wave + " (" + hazardCount + ")" + " asteroids!";

            if (_gameOver)
            {
                restartText.text = "Press 'R' for Restart";
                _restart = true;
                break;
            }
        }
    }

    public void AddScore(int newScoreValue)
    {
        _score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + _score;
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over";
        _gameOver = true;
    }
}
