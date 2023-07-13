using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateWorld : MonoBehaviour
{
    public static GameObject dummy;
    static public GameObject lastPlatform;
    private void Awake()
    {
        dummy = new GameObject("dummy");
    }

    public static void RunDummy()
    {
        GameObject p = Pool.singleton.GetRandomItem();

        if (p == null)
        {
            return;
        }

        if (lastPlatform != null)
        {
            if (lastPlatform.tag == "platformTSection")
            {
                dummy.transform.position = lastPlatform.transform.position +
                PlayerController.player.transform.forward * 20;
            }
            else
            {
                dummy.transform.position = lastPlatform.transform.position +
                PlayerController.player.transform.forward * 10;
            }
            

            if (lastPlatform.tag == "stairsUp")
            {
                dummy.transform.Translate(0, 5, 0);
            }
        }

        lastPlatform = p;
        p.SetActive(true);
        p.transform.position = dummy.transform.position;
        p.transform.rotation = dummy.transform.rotation;

        if (p.tag == "stairsDown")
        {
            dummy.transform.Translate(0, -5, 0);
            p.transform.Rotate(0, 180, 0);
            p.transform.position = dummy.transform.position;
        }
    }
}
