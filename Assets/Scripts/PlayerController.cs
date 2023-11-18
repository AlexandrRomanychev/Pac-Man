using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{

    public GameObject PacMan;
    public TMP_Text Score;
    private int Speed = 2;
    private int Scores = 0;
    public GameLevel gameLevel;

    void Start() {
        gameLevel = PacMan.GetComponent<GameLevel>();
    }

    // Update is called once per frame
    void FixedUpdate() {
        checkKeyDown();
    }

    private void OnCollisionEnter(Collision collision)
    {
        print("collision");
        print(collision.gameObject.tag);
        if (collision.gameObject.tag == "PacManFood") {
            Scores++;
            Destroy(collision.gameObject);
            Score.text = "Scores:" + Scores;
            gameLevel.getCurrentLevel().decrementCountFood();
            if (gameLevel.getCurrentLevel().isCompleted()) {
                gameLevel.startNextLevel();
            }
        }
        print(collision.gameObject);
    }

    private void checkKeyDown() {
        if (Input.GetKey(KeyCode.UpArrow)) {
            PacMan.transform.Translate(0, 0, Speed * Time.deltaTime);
        } else if (Input.GetKey(KeyCode.DownArrow)) {
            PacMan.transform.Translate(0, 0, -Speed * Time.deltaTime);
        } else if (Input.GetKey(KeyCode.LeftArrow)) {
            PacMan.transform.Translate(-Speed * Time.deltaTime, 0, 0);
        } else if (Input.GetKey(KeyCode.RightArrow)) {
            PacMan.transform.Translate(Speed * Time.deltaTime, 0, 0);
        }
    }
}
