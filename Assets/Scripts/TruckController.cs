using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckController : MonoBehaviour
{
    private int _currentGold;
    public List<GameObject> golds;
    public GameObject goldsParent;

    private void Start()
    {
        golds = new List<GameObject>();

        foreach (Transform gold in goldsParent.transform)
        {
            golds.Add(gold.gameObject);
            gold.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Player")
            return;

        var player = collision.gameObject.GetComponent<PlayerController>();

        var gold = player.LoadGoldsToTruck();

        _currentGold += gold;

        for (int i = 0; i < _currentGold; i++)
            golds[i].SetActive(true);
    }
}
