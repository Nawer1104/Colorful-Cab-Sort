using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    private int char_number = 0;

    public float speed = 10f;

    private bool canMove = false;

    public Transform posToMove;

    public GameObject vfxOnSuccess;

    public void PlusChar()
    {
        char_number += 1;

        if (char_number == 3)
        {
            canMove = true;

            transform.localEulerAngles = new Vector3(0f, 0f, -90f);
        }
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, posToMove.position, speed * Time.deltaTime);
        }
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "destroy")
        {
            if (char_number < 3) return;

            GameManager.Instance.levels[GameManager.Instance.GetCurrentIndex()].cars.Remove(this);
            GameObject explosion = Instantiate(vfxOnSuccess, transform.position, transform.rotation);
            Destroy(explosion, 2f);
            Destroy(gameObject);
        }
    }
}
