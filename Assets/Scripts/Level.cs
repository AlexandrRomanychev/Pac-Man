using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class Level {
    private string fileName;
    private GameObject pacMan;
    private GameObject pacManFoodPrefab;
    private GameObject wallPrefab;
    private int countFood;
    private float leftUpCorner_X = -4.75F;
    private float leftUpCorner_Z = 4.75F;
    private string filePath = "Assets/Levels/";
    private Func<GameObject, Vector3, GameObject> instantiateObject;

    public Level(string fileName, GameObject pacMan, GameObject pacManFoodPrefab, GameObject wallPrefab, Func<GameObject, Vector3, GameObject> instantiateObject) {
        this.fileName = fileName;
        this.pacMan = pacMan;
        this.pacManFoodPrefab = pacManFoodPrefab;
        this.wallPrefab = wallPrefab;
        this.countFood = 0;
        this.instantiateObject = instantiateObject;
    }

    public List<GameObject> createLevel() {
        List<GameObject> objects = new List<GameObject>();
        StreamReader inp_stm = new StreamReader(filePath + fileName);
        int raw = 0;
        while(!inp_stm.EndOfStream)
        {
            string inp_ln = inp_stm.ReadLine();
            for (int column = 0; column < inp_ln.Length; column++) {
                if (inp_ln[column] == '#') {
                    Vector3 position = new Vector3(leftUpCorner_X + 0.5F * column, 0.5F, leftUpCorner_Z - 0.5F * raw);
                    objects.Add(instantiateObject(wallPrefab, position));
                } else if (inp_ln[column] == '.') {
                    Vector3 position = new Vector3(leftUpCorner_X + 0.5F * column, 0.5F, leftUpCorner_Z - 0.5F * raw);
                    instantiateObject(pacManFoodPrefab, position);
                    countFood++;
                } else if (inp_ln[column] == '@') {
                    pacMan.transform.position = new Vector3(leftUpCorner_X + 0.5F * column, 0.5F, leftUpCorner_Z - 0.5F * raw);
                    objects.Add(pacMan);
                }
            }
            raw++;
        }
        inp_stm.Close();
        return objects;
    }

    public void decrementCountFood() {
        this.countFood--;
    }

    public bool isCompleted() {
        return this.countFood == 0;
    }

}