using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWall : MonoBehaviour
{
    [SerializeField] private GameObject[] bricks;
    private List<Rigidbody> rigidbodiesBricks = new List<Rigidbody>();

    private Collider wallCollider;

    private List<Vector3> brickPositions = new List<Vector3>();
    private List<Quaternion> brickRotations = new List<Quaternion>();
    [SerializeField] private GameObject explosion;

    private void OnEnable()
    {
        wallCollider.enabled = true;
        for (int i = 0; i < bricks.Length; i++)
        {
            bricks[i].transform.localPosition = brickPositions[i];
            bricks[i].transform.localRotation = brickRotations[i];
            rigidbodiesBricks[i].isKinematic = true;
        }

        //if (RandomPosFire.randomPosFire != null && 
        //    (transform.gameObject.name.Equals("DummyWallThin") || (transform.gameObject.name.Equals("DummyWall"))))
        //{
        //    RandomPosFire.randomPosFire.RandomPos();
        //}
        
    }

    // Start is called before the first frame update
    void Awake()
    {
        wallCollider = GetComponent<Collider>();

        foreach (GameObject brick in bricks)
        {
            rigidbodiesBricks.Add(brick.GetComponent<Rigidbody>());
            brickPositions.Add(brick.transform.localPosition);
            brickRotations.Add(brick.transform.localRotation);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("spell"))
        {
            GameObject explode = Instantiate(explosion, collision.contacts[0].point, Quaternion.identity);
            collision.gameObject.SetActive(false);
            Destroy(explode, 2.5f); // Detele SystemParticle explosion
            wallCollider.enabled = false;

            foreach (var rbBrick in rigidbodiesBricks)
            {
                rbBrick.isKinematic = false;
            }
        }
    }
}
