using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasMoveHorizontal : MonoBehaviour
{
    public void MoveHorizontal(Vector2 movement)
    {
       transform.Translate(movement);
    }
}
