using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    
    public GameObject menu;
    public Text menuText;
    
    private int time;
    public int maxTime              = 15;
    public int addTime              = 5;
    public Text timerText;

    public Text scoreText;
    public int coinsNeedCollect     = 10;
    private int collectedCoins      = 0;

    public GameObject coinPrefab;
    private float[,] validPositions = new float [,] {
                                {-3.71f, -0.5f}, 
                                {-3.71f, 2.91f}, 
                                {-3.71f, 5.15f},
                                {0.19f, -0.2f}, 
                                {4.25f, 0.7f}, 
                                {4.25f, 3.0f},
                                {4.25f, 6.5f},
                            };
    private int spawnedPoint;

    // Start is called before the first frame update
    void Start()
    {
        time = maxTime;
        SpawnCoin(false);
        GlobalEventManager.OnCoinCollision.AddListener(SpawnCoin);
        InvokeRepeating("Tick", 0f, 1.0f);
    }
    
    private void SpawnCoin(bool sum = true)
    {
        if(collectedCoins + 1 == coinsNeedCollect){
            StopGame("WIN");
        }
        if(sum){
            collectedCoins += 1;
            scoreText.text = collectedCoins.ToString();
            AddTime();
        }
        Vector3 spawnPointIndex = spawnPosition();
        // Spawn the coin at the position
        Instantiate(coinPrefab, spawnPointIndex, transform.rotation * Quaternion.Euler(90f, 0f, 0f), transform);
    }

    private void AddTime()
    {
        if(time + addTime > maxTime)
        {
            time = maxTime;
        }else
        {
            time += addTime;
        }
    }

    private Vector3 spawnPosition(){
        int newDir;
 
        newDir = Random.Range(0,6);
        while (newDir == spawnedPoint)
        {
            newDir = Random.Range(0,6);
        }

        Vector3 spawnPosition = new Vector3(validPositions[newDir, 0], 0.5f, validPositions[newDir, 1]);

        spawnedPoint = newDir;
        return spawnPosition;
    }


    private void Tick() 
    {
        if(time > 0)
        {
            time -= 1;
            timerText.text = time.ToString();
        }else{
            StopGame();
        }
    }
    
    public void StopGame(string msg="LOSE")
    {
        menuText.text = msg;
        Time.timeScale = 0f;
        menu.SetActive(true);
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
