using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GM : MonoBehaviour
{
    public GameObject food;

    public Text Play ;


    public Transform borderTop;
    public Transform borderBottom;
    public Transform borderLeft;
    public Transform borderRight;
    public GameObject menuScreen;

    private bool isGameStarted = false;

    public float timeDuration = 5.0f;
    private float timeInterval = 0f;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
    }

    public void gamePlay()
    {
        if (!isGameStarted)
        {
            isGameStarted = true;
            Time.timeScale = 1;
            menuScreen.SetActive(false);
        }
        else
        {
            SceneManager.LoadScene("game");
        }

    }


    public void gameQuit()
    {
        Application.Quit();
    }

    public void gameOver()
    {
        Play.text = "Menu";
        Time.timeScale = 0f;
        menuScreen.SetActive(true);
        
    }

    // Update is called once per frame
    void Update()
    {

        if (timeInterval>0)
        {
            timeInterval -= Time.deltaTime;
        }
        else
        {
            Spawn();
            timeInterval = timeDuration;
        }
    }

    void Spawn()
    {
        int x = (int)(Random.Range(borderLeft.position.x+5,borderRight.position.x-5));
        int y = (int)(Random.Range(borderBottom.position.y+5, borderTop.position.y-5));
        Instantiate(food,new Vector2(x,y) , Quaternion.identity);
    }

}
