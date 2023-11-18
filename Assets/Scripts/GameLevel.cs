using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System;

public class GameLevel : MonoBehaviour
{
    // Стена
    public GameObject wallPrefab;
    // Еда pacman
    public GameObject pacManFoodPrefab;
    public GameObject pacManPrefab;

    private List<Level> levels;
    private int currentLevelNumber = 0;
    private List<GameObject> levelObjects;

    // Start is called before the first frame update
    void Start()
    {
        levels = new List<Level> {
            new Level("level1", pacManPrefab, pacManFoodPrefab, wallPrefab, instantiateObject),
            new Level("level2", pacManPrefab, pacManFoodPrefab, wallPrefab, instantiateObject)
        };
        levelObjects = levels[currentLevelNumber].createLevel();
    }

    // Создать новый объект в заданной позиции
    Func<GameObject, Vector3, GameObject> instantiateObject = (objectForCreation, position) => {
        return Instantiate(objectForCreation, position, Quaternion.identity) as GameObject;
    };

    public Level getCurrentLevel() {
        return levels[currentLevelNumber];
    }

    private void destoryCurrentElements() {
        foreach (GameObject gameObject in levelObjects) {
            if (gameObject.tag != "Player") {
                Destroy(gameObject);
            }
        }
    }

    public void startNextLevel() {
        destoryCurrentElements();
        if (currentLevelNumber < levels.Count - 1) {
            currentLevelNumber++;
            levelObjects = levels[currentLevelNumber].createLevel();
        }
    }
}
