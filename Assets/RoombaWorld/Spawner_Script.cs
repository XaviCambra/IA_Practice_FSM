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
        timerMouse = Random.Range(20.0f, 30.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
        timerDust -= Time.deltaTime;
        timerMouse -= Time.deltaTime;

        /*if (timerDust <= 0) 
        {
            //Instantiate(dustPrefab, RandomLocationGenerator.RandomWalkableLocation());
            dustPrefab.GetComponent<SpriteRenderer>().color = Random.ColorHSV();
            timerDust = 5;
        }*/
        if (timerMouse <= 0)
        {
            Instantiate(mousePrefab, RandomExitPoint().transform);
            timerDust = Random.Range(20.0f, 30.0f);
        }

    }
    public GameObject RandomExitPoint()
    {
        return exitPoints[Random.Range(0, exitPoints.Length)];
    }
}
