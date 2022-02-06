using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Snake : MonoBehaviour
{
    [SerializeField] GameObject apple;
    [SerializeField] float speed = 0.5f;
    private int applePicup = 0;
    private int appleCount = 0;
    private float rotation;
    private float scaleX;
    private float scaley;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveDir();
        SpoonApple();
        WinGame();
    }
    private void MoveDir() // dir
    {

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            rotation = 0f;
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            rotation = 180f;
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            rotation = 90f;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            rotation = -90f;
        }
        transform.position = transform.position + transform.up * speed * Time.deltaTime;
        transform.eulerAngles = Vector3.forward * rotation;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Apple")
        {
            Destroy(collision.gameObject);
            applePicup++;
            appleCount--;
            speed++;
            SnakeBiger();

        }
        if(collision.gameObject.tag == "Wall")
        {
            SceneManager.LoadScene(2);
        }
    }
    private void SpoonApple()
    {
        Vector2 position = new Vector2(Random.Range(-8.5f,8.5f), Random.Range(-4.2f,4.2f));

        if (appleCount == 0)
        {
            Instantiate(apple, position, Quaternion.identity);
            appleCount++;
        }
        else if(appleCount > 0)
        {
            return;
        }
    }
    private void SnakeBiger()
    {
        scaleX = transform.localScale.x + 0.2f;
        scaley = transform.localScale.y + 0.2f;
        transform.localScale = new Vector3(scaleX, scaley);
    }
    private void WinGame()
    {
        if(applePicup == 10)
        {
            NextLevel();
        }
    }
    void NextLevel()
    {
        int CarrentScene = SceneManager.GetActiveScene().buildIndex;
        int NextSceneIndex = CarrentScene + 1;
        if (NextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            NextSceneIndex = 0;
        }
        SceneManager.LoadScene(NextSceneIndex);
    }
    public void ReloadLevelOne() // Button
    {
        
        SceneManager.LoadScene(0);
    }
    public void ReloadSecond()  // Button
    {

        SceneManager.LoadScene(1);
    }
}
