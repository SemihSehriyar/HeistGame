using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableController : MonoBehaviour
{
    [SerializeField] private GameObject _goldObj;

    public bool IsGoldCollectable => _goldObj.activeSelf;

    private void OnCollisionEnter(Collision collision)
    {
        if (IsGoldCollectable == false)
            return;

        var player = collision.gameObject.GetComponent<PlayerController>();

        if (collision.gameObject.tag != "Player")
            return;

        if (player.CollectgGold())
        {
            _goldObj.SetActive(false);
            Invoke(nameof(ReloadGold), Random.Range(5f, 15f));
        }
    }

    private void ReloadGold()
    {
        _goldObj.SetActive(true);
    }
}
