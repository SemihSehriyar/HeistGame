using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TruckController : MonoBehaviour
{
    private int _currentGold;
    public TextMeshProUGUI scoreText;
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
       TakeGold(collision);
    }

    public void TakeGold(Collision collision)
    {
        if (collision.gameObject.tag != "Player")
            return;

        var player = collision.gameObject.GetComponent<PlayerController>();

        var gold = player.DropGold();

        _currentGold += gold;
        scoreText.SetText("Collected Gold: " + _currentGold);


        for (int i = 0; i < _currentGold; i++)
            golds[i].SetActive(true);
    }
}
