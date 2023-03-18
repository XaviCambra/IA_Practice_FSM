using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner_Script : MonoBehaviour
{
    // Start is called before the first frame update
    private float timerDust;
    private float timerMouse;
    public GameObject mousePrefab;
    public GameObject dustPrefab;
    public RandomLocationGenerator randomLocationGenerator;
    private GameObject[] exitPoints;
    void Start()
    {
        timerDust = 5;
        timerMouse = Random.Range(20.0f, 30.0f);
        exitPoints = GameObject.FindGameObjectsWithTag("EXIT");
    }

    // Update is called once per frame
    void Update()
    {
        
        timerDust -= Time.deltaTime;
        if (timerDust <= 0) 
        {
            GameObject dustSpawn = dustPrefab;
            dustSpawn.transform.position = RandomLocationGenerator.RandomWalkableLocationOnScreen();
            dustSpawn.GetComponent<SpriteRenderer>().color = Random.ColorHSV();
            Instantiate(dustSpawn);
            timerDust = 5;
        }

        timerMouse -= Time.deltaTime;
        if (timerMouse <= 0)
        {
            GameObject l_MouseSpawn = mousePrefab;
            l_MouseSpawn.transform.position = RandomExitPoint().transform.position;
            Instantiate(l_MouseSpawn);
            timerMouse = Random.Range(20.0f, 30.0f);
        }

    }
    public GameObject RandomExitPoint()
    {
        int l_Index = Random.Range(0, exitPoints.Length - 1);
        return exitPoints[l_Index];
    }
}
