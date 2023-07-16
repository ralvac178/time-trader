using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    private void FixedUpdate()
    {
        if (PlayerController.isDead)
        {
            return;
        }

        transform.position += PlayerController.player.transform.forward * -0.1f;
        if (PlayerController.platform != null)
        {
            if (PlayerController.platform.tag == "stairsUp")
            {
                transform.Translate(0, -0.0577f, 0);
            }
            else if (PlayerController.platform.tag == "stairsDown")
            {
                transform.Translate(0, 0.0577f, 0);
            }
        }
    }
}
