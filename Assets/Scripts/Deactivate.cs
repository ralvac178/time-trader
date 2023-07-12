using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deactivate : MonoBehaviour
{
    private bool dSchedule = false;
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Player") && !dSchedule)
        {
            Invoke("SetInactive", 3f);
            dSchedule = true;
        }   
    }

    public void SetInactive()
    {
        gameObject.SetActive(false);
        dSchedule = false;
    }
}
