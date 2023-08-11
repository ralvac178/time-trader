using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private MeshRenderer[] mrs;

    private void Start()
    {
        mrs = GetComponentsInChildren<MeshRenderer>();    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameData.singleton.UpdateTextScore(10);

            if (mrs != null)
            {
                foreach (var item in mrs)
                {
                    item.enabled = false;
                }
            }
        }
    }

    private void OnEnable()
    {
        if (mrs != null)
        {
            foreach (var item in mrs)
            {
                item.enabled = true;
            }
        }
    }
}
