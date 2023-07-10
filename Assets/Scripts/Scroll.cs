using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    private void FixedUpdate()
    {
        transform.position += PlayerController.player.transform.forward * -0.1f;
    }
}
