using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputProvider
{
    public delegate void JumpProvider();
    public static JumpProvider jumpProvider;

    public delegate void ShootProvider();
    public static ShootProvider shootProvider;

    public delegate void MoveHorizontalProvider(Vector3 moveHorizontal);
    public static MoveHorizontalProvider moveHorizontalProvider;

    public delegate void TurnProvider();
    public static TurnProvider turnProvider;

    //Methods to Invoke delegates that aren't null
    public static void OnHasJump()
    {
        jumpProvider?.Invoke();
    }

    public static void OnHasShoot()
    {
        shootProvider?.Invoke();
    }

    public static void OnMoveHorizontally(Vector3 movement)
    {
        moveHorizontalProvider?.Invoke(movement);
    }

    public static void OnTurn()
    {
        turnProvider?.Invoke();
    }
}
