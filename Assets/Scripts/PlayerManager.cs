using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{

    public void SimonInput(InputAction.CallbackContext context)
    {
        if(context.phase != InputActionPhase.Performed) return;
        if (Simon.instance.isReseting) return;
        Vector2 input = context.ReadValue<Vector2>();
        if (Vector2.Dot(input, Vector2.up) > 0.9)
        {
            Debug.Log("SimonInput : up" );
            Simon.instance.AddMovement(Simon.Movements.Up);
            AudioManager.instance.PlaySound(AudioManager.Sounds.W);
        }
        else if (Vector2.Dot(input, Vector2.down) > 0.9)
        {
            Debug.Log("SimonInput : down" );
            Simon.instance.AddMovement(Simon.Movements.Down);
            AudioManager.instance.PlaySound(AudioManager.Sounds.S);
        }
        else if(Vector2.Dot(input, Vector2.left) > 0.9)
        {
            Debug.Log("SimonInput : left" );
            Simon.instance.AddMovement(Simon.Movements.Left);
            AudioManager.instance.PlaySound(AudioManager.Sounds.A);
        }
        else if (Vector2.Dot(input, Vector2.right) > 0.9)
        {
            Debug.Log("SimonInput : right" );
            Simon.instance.AddMovement(Simon.Movements.Right);
            AudioManager.instance.PlaySound(AudioManager.Sounds.D);
        }
        else if (input.sqrMagnitude < 0.1) Debug.Log("SimonInput : neutral"); // shouldn't be possible

        Simon.SimonStatus simonStatus = Simon.instance.CompareMovements();
        switch (simonStatus)
        {
            case Simon.SimonStatus.Wrong :
                // reset
                StartCoroutine(Simon.instance.ResetSimon());
                break;
            case Simon.SimonStatus.Pending :
                // Do nothing
                break;
            case Simon.SimonStatus.Correct :
                // do something
                break;
        }
    }

    
}
