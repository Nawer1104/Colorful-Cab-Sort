using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Char : MonoBehaviour
{
    public static Char Instance;

    public float speed = 10f;

    Vector3 startPos;

    bool startMovement = false;

    Vector3[] positions;

    int moveIndex = 0;

    private float min_X = -2.15f;

    private float max_X = 2.15f;

    private float min_Y = -4.85f;

    private float max_Y = 4.85f;

    public GameObject vfxOnSuccess;

    public string carTagged;

    private void Awake()
    {
        Instance = this;

        startPos = transform.position;
    }

    private void OnMouseDown()
    {
        DrawWithMouse.Instance.StartLine(transform.position);
    }

    private void OnMouseDrag()
    {
        DrawWithMouse.Instance.Updateline();
    }

    private void OnMouseUp()
    {
        positions = new Vector3[DrawWithMouse.Instance.line.positionCount];
        DrawWithMouse.Instance.line.GetPositions(positions);
        startMovement = true;
        moveIndex = 0;
        DrawWithMouse.Instance.ResetLine();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == carTagged)
        {
            collision.transform.GetComponent<Car>().PlusChar();
            GameObject explosion = Instantiate(vfxOnSuccess, transform.position, transform.rotation);
            Destroy(explosion, .75f);
            Destroy(gameObject);
        }
    }

    public void ReSetPos()
    {
        transform.position = startPos;
        startMovement = false;
        DrawWithMouse.Instance.ResetLine();
    }

    private void Update()
    {
        if (startMovement)
        {
            CheckPos();

            Vector2 currentPos = positions[moveIndex];
            transform.position = Vector2.MoveTowards(transform.position, currentPos, speed * Time.deltaTime);

            float distance = Vector2.Distance(currentPos, transform.position);
            if (distance <= 0.05f)
            {
                moveIndex++;
            }

            if (moveIndex > positions.Length - 1)
            {
                startMovement = false;
            }
        }
    }

    private void CheckPos()
    {
        if (transform.position.x < min_X)
        {
            Vector3 moveDirX = new Vector3(min_X, transform.position.y, 0f);
            transform.position = moveDirX;
        }
        else if (transform.position.x > max_X)
        {
            Vector3 moveDirX = new Vector3(max_X, transform.position.y, 0f);
            transform.position = moveDirX;
        }
        else if (transform.position.y < min_Y)
        {
            Vector3 moveDirX = new Vector3(transform.position.x, min_Y, 0f);
            transform.position = moveDirX;
        }
        else if (transform.position.y > max_Y)
        {
            Vector3 moveDirX = new Vector3(transform.position.x, max_Y, 0f);
            transform.position = moveDirX;
        }
        else if (transform.position.x < min_X && transform.position.y < min_Y)
        {
            Vector3 moveDirX = new Vector3(min_X, min_Y, 0f);
            transform.position = moveDirX;
        }
        else if (transform.position.x < min_X && transform.position.y > max_Y)
        {
            Vector3 moveDirX = new Vector3(min_X, max_Y, 0f);
            transform.position = moveDirX;
        }
        else if (transform.position.x > max_X && transform.position.y > max_Y)
        {
            Vector3 moveDirX = new Vector3(max_X, max_Y, 0f);
            transform.position = moveDirX;
        }
        else if (transform.position.x > max_X && transform.position.y < min_Y)
        {
            Vector3 moveDirX = new Vector3(max_X, min_Y, 0f);
            transform.position = moveDirX;
        }
    }
}