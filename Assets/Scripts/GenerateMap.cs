using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMap : MonoBehaviour
{
    [SerializeField] private GameObject[] worldPart;
    private GameObject dummy;
    // Start is called before the first frame update
    void Start()
    {
        dummy = new GameObject("dummy");
        //Vector3 pos = Vector3.zero;
        for (int i = 0; i < 20; i++)
        {
            int partNumber = Random.Range(0, worldPart.Length);
            GameObject lastPart = Instantiate(worldPart[partNumber].gameObject, dummy.transform.position, dummy.transform.rotation) as GameObject;           

            if (lastPart.tag.Equals("stairsUp"))
            {
                dummy.transform.Translate(Vector3.up * 5); //pos.y += 5
            }
            else if (lastPart.tag.Equals("stairsDown"))
            {
                lastPart.transform.Translate(Vector3.down * 5);
                lastPart.transform.Rotate(Vector3.up * 180);
                dummy.transform.Translate(Vector3.down * 5); //pos.y -= 5
            }
            else if (lastPart.tag.Equals("platformTSection"))
            {
                int randomDirection = Random.Range(0, 2);
                if (randomDirection == 0)
                {
                    dummy.transform.Rotate(Vector3.up * 90);  
                }
                else
                {
                    dummy.transform.Rotate(Vector3.up * -90);
                }

                dummy.transform.Translate(Vector3.back * 10); //pos.z -= 10
            }

            dummy.transform.Translate(Vector3.back * 10); //pos.z -= 10
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
