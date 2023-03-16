using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner_Script : MonoBehaviour
{
    // Start is called before the first frame update
    public float timerDust = 5;
    public float timerMouse;
    public GameObject mousePrefab;
    public GameObject dustPrefab;
    public RandomLocationGenerator randomLocationGenerator;
    private GameObject[] exitPoints;
    void Start()
    {
        timerMouse = 10;//Random.Range(20.0f, 30.0f);
        exitPoints = GameObject.FindGameObjectsWithTag("EXIT");
    }

    // Update is called once per frame
    void Update()
    {
        
        timerDust -= Time.deltaTime;
        timerMouse -= Time.deltaTime;

        if (timerDust <= 0) 
        {
            GameObject dustSpawn = dustPrefab;
            dustSpawn.transform.position = RandomLocationGenerator.RandomWalkableLocation();
            dustSpawn.GetComponent<SpriteRenderer>().color = Random.ColorHSV();
            Instantiate(dustSpawn);
            timerDust = 1;
        }
        if (timerMouse <= 0)
        {
            Instantiate(mousePrefab, RandomExitPoint().transform);
            timerDust = 10;//Random.Range(20.0f, 30.0f);
        }

    }
    public GameObject RandomExitPoint()
    {
        return exitPoints[Random.Range(0, exitPoints.Length)];
    }
}
