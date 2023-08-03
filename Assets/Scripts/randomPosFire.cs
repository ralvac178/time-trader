using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomPosFire : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float maxLengh = 0;
        float randomOffset = 0f;
        if (transform.parent.gameObject.name.Equals("platformTSectionFire(Clone)") 
            || transform.parent.gameObject.name.Equals("platformTSectionWall(Clone)"))
        {
            maxLengh = 8f;
            randomOffset = Random.Range(-maxLengh, maxLengh);
            if (randomOffset < 2 && randomOffset > -2)
            {
                randomOffset += 3;
            }
            transform.localPosition += new Vector3(randomOffset, 0, 0);
        }
        else
        {
            maxLengh = 3f;
            randomOffset = Random.Range(-maxLengh, maxLengh);
            transform.localPosition += new Vector3(0,
                                    0, randomOffset);
        }

        
    }


}
