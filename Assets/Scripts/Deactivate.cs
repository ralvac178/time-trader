using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deactivate : MonoBehaviour
{
    private bool dSchedule = false;
    private void OnCollisionExit(Collision collision)
    {
        if (PlayerController.isDead)
        {
           return;
        }
        if (collision.gameObject.tag.Equals("Player") && !dSchedule)
        {
            Invoke("SetInactive", 4f);
            dSchedule = true; //prevent to inactive platforms who will be created after
        }   
    }

    public void SetInactive()
    {
        gameObject.SetActive(false);
        dSchedule = false;
    }
}
