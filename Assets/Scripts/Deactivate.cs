using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deactivate : MonoBehaviour
{
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            Invoke("SetInactive", 3f);
        }   
    }

    public void SetInactive()
    {
        gameObject.SetActive(false);
    }
}
