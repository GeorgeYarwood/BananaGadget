using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : MonoBehaviour
{

    float adjust = 20f;
    private void OnCollisionStay(Collision collision)
    {
        CharacterController player;
        Rigidbody rb;
        player = collision.gameObject.GetComponentInParent<CharacterController>();
        rb = player.GetComponent<Rigidbody>();
        rb.AddForce(0, 0, -15);
    }

    private void OnCollisionEnter(Collision collision)
    {
        CharacterController player;
        player = collision.gameObject.GetComponentInParent<CharacterController>();

        player.currentMoveSpeed += adjust;

    }

    private void OnCollisionExit(Collision collision)
    {
        CharacterController player;
        Rigidbody rb;
        player = collision.gameObject.GetComponentInParent<CharacterController>();

        player.currentMoveSpeed -= adjust;
    }

}
