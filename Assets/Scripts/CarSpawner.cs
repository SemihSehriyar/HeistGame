using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> carPrefabs;
    public float maxTime;
    public float minTime;
    public float timer;
    public float spawnTime;

    private void Start()
    {
        spawnTime = Random.Range(minTime, maxTime);
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= minTime)
        {
            timer = 0;
            var car = carPrefabs[Random.Range(0, carPrefabs.Count)];

            var spawnedCar = Instantiate(car, transform.position, transform.rotation, transform);

            spawnedCar.AddComponent<CarController>();

            Destroy(spawnedCar, 6f);

            spawnTime = Random.Range(minTime, maxTime);
        }
    }
}
