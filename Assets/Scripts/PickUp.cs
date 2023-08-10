using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameData.singleton.UpdateTextScore(10);
            Destroy(this.gameObject, 0.1f);
        }
    }
}
