using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private MeshRenderer[] mrs;
    [SerializeField] private GameObject floatPointsUI;
    private GameObject canvas;

    private void Start()
    {
        mrs = GetComponentsInChildren<MeshRenderer>();
        canvas = GameObject.Find("Canvas");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject floatingPoints = Instantiate(floatPointsUI, canvas.transform);
            Vector3 positionFloatCoin = Camera.main.WorldToScreenPoint(this.transform.position);
            floatingPoints.transform.position = positionFloatCoin;

            PlayerController.sfx[7].Play();
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
