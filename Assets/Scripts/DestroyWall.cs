using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWall : MonoBehaviour
{
    [SerializeField] private GameObject[] bricks;
    private List<Rigidbody> rigidbodiesBricks = new List<Rigidbody>();

    private Collider wallCollider;
    // Start is called before the first frame update
    void Start()
    {
        wallCollider = GetComponent<Collider>();

        foreach (GameObject brick in bricks)
        {
            rigidbodiesBricks.Add(brick.GetComponent<Rigidbody>());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("spell"))
        {
            wallCollider.enabled = false;

            foreach (var rbBrick in rigidbodiesBricks)
            {
                rbBrick.isKinematic = false;
            }
        }
    }
}
