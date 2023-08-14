using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearMovement : MonoBehaviour
{
    [SerializeField] private bool movable, rotatable;
    [SerializeField] private GearController attachedController;
    [SerializeField] private LayerMask boardLayer;

    private Rigidbody2D rb;

    private Vector2 offset;

    private Vector2 initialPos;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnMouseDown()
    {
        if (movable)
        {
            RaycastHit2D hit = Physics2D.Raycast((Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero,
                Mathf.Infinity, boardLayer);
            if (hit)
            {
                hit.transform.GetComponent<TileHoldChecker>().occupied = false;
            }
            initialPos = rb.position;
            offset = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - rb.position;
        }

        if (rotatable)
        {
            StartCoroutine(clickChecker());
        }
    }

    private IEnumerator clickChecker()
    {
        yield return new WaitForSeconds(0.12f);
        if (!Input.GetMouseButton(0))
        {
            attachedController.RotateGear(Camera.main.ScreenToWorldPoint(Input.mousePosition).x < rb.position.x);
        }
    }

    private void OnMouseDrag()
    {
        if (movable)
        {
            rb.position = (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition) - offset;
        }
    }

    private void OnMouseUp()
    {
        if (movable)
        {
            RaycastHit2D hit = Physics2D.Raycast((Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero,
                Mathf.Infinity, boardLayer);
            if (hit)
            {
                if (!hit.transform.GetComponent<TileHoldChecker>().occupied)
                {
                    rb.position = hit.collider.transform.position;
                    hit.transform.GetComponent<TileHoldChecker>().occupied = true;
                }
                else
                {
                    rb.position = initialPos;
                    RaycastHit2D returnHit = Physics2D.Raycast(initialPos, Vector2.zero,
                        Mathf.Infinity, boardLayer);
                    if (returnHit)
                    {
                        returnHit.transform.GetComponent<TileHoldChecker>().occupied = true;
                    }
                }
            }
        }
    }
}
