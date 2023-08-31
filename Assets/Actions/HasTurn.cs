using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasTurn : MonoBehaviour
{
    [SerializeField] private GameObject magic;
    private Rigidbody magicRb;
    // Start is called before the first frame update
    void Start()
    {
        magicRb = magic.GetComponent<Rigidbody>();
    }

    public void TurnRight()
    {
        if (PlayerController.canTurn)
        {
            transform.Rotate(Vector3.up * 90);
            GenerateWorld.dummy.transform.forward = -this.transform.forward;
            GenerateWorld.RunDummy();

            //If the rotations is less than 10 degress, so constrait Z pos
            if (transform.rotation.eulerAngles.y < 190 && transform.rotation.eulerAngles.y > 170
                 || transform.rotation.eulerAngles.y < 10 && transform.rotation.eulerAngles.y > -10)
            {
                magicRb.constraints = RigidbodyConstraints.FreezePositionX;
            }
            else
            {
                magicRb.constraints = RigidbodyConstraints.FreezePositionZ;
            }

            if (GenerateWorld.lastPlatform.tag != "platformTSection")
            {
                GenerateWorld.RunDummy();
            }

            transform.position = new Vector3(PlayerController.startPosition.x, transform.position.y,
                                            PlayerController.startPosition.z);
        }
    }

    public void TurnLeft()
    {
        if (PlayerController.canTurn)
        {
            transform.Rotate(Vector3.up * -90);
            GenerateWorld.dummy.transform.forward = -this.transform.forward;
            GenerateWorld.RunDummy();

            //If the rotations is less than 10 degress, so constrait Z pos
            if (transform.rotation.eulerAngles.y < 190 && transform.rotation.eulerAngles.y > 170
                || transform.rotation.eulerAngles.y < 10 && transform.rotation.eulerAngles.y > -10)
            {
                magicRb.constraints = RigidbodyConstraints.FreezePositionX;
            }
            else
            {
                magicRb.constraints = RigidbodyConstraints.FreezePositionZ;
            }

            if (GenerateWorld.lastPlatform.tag != "platformTSection")
            {
                GenerateWorld.RunDummy();
            }

            transform.position = new Vector3(PlayerController.startPosition.x, transform.position.y,
                                            PlayerController.startPosition.z);
        }
    }
}
