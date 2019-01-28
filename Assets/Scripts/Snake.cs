using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;



public class Snake : MonoBehaviour
{
    private float speed = 0.20f;
    Vector2 vector = Vector2.up;
    Vector2 moveVector;
    public Text scoreText;
   
    bool ate = false;

    public GameObject tailPrefab;

    private float timeToCall = 0.3f;

    

    private int scoreCount;
    private List<Transform> tail = new List<Transform>();
    // Start is called before the first frame update
    void Start()
    {
       
        scoreCount = 0;
        InvokeRepeating("Movement", timeToCall, speed);
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            vector = Vector2.right;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            vector = Vector2.left;
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            vector = Vector2.up;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            vector = Vector2.down;
        }
        moveVector = vector /3f;
    }

    void Movement()
    {
        Vector2 oldHeadPos = transform.position; 

        transform.Translate(moveVector);

        if (ate)
        {
            GameObject g = (GameObject)Instantiate(tailPrefab, oldHeadPos, Quaternion.identity);
            scoreCount++;
            scoreText.text = scoreCount + "";
            tail.Insert(0, g.transform);
            ate = false;
            timeToCall -= 0.002f;
            FindObjectOfType<GM>().timeDuration -= 0.0005f;
        }

        else if(tail.Count>0)
        {
            tail.Last().position = oldHeadPos;
            tail.Insert(0, tail.Last());
            tail.RemoveAt(tail.Count - 1);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name.StartsWith("Food"))
        {
            ate = true;
            Destroy(collision.gameObject);

        }
        else
        {
            Debug.Log(collision.name);
            if (collision.gameObject.transform!=tail[0] || collision.name.StartsWith("Border"))
            {

                FindObjectOfType<GM>().gameOver();
            }

        }
    }


}
