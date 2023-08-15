using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearMovement : MonoBehaviour
{
    [SerializeField] private bool movable, rotatable;
    [SerializeField] private GearController attachedController;
    [SerializeField] private LayerMask boardLayer;

    private float timer;

    private Rigidbody2D rb;

    private Vector2 offset;

    private Vector2 initialPos;

    public bool Movable
    {
        get => movable;
        set => movable = value;
    }

    public bool Rotatable
    {
        get => rotatable;
        set => rotatable = value;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        RaycastHit2D hit = Physics2D.Raycast(rb.position, Vector2.zero,
            Mathf.Infinity, boardLayer);
        if (hit)
        {
            hit.transform.GetComponent<TileHoldChecker>().occupied = true;
            rb.position = hit.transform.position;
        }
    }

    private void OnMouseDown()
    {
        if (movable)
        {
            RaycastHit2D hit = Physics2D.Raycast(rb.position, Vector2.zero,
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
            timer = Time.time;
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
            RaycastHit2D hit = Physics2D.Raycast(rb.position, Vector2.zero,
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

        if (rotatable)
        {
            if (Time.time - timer <= 0.4f)
            {
                attachedController.RotateGear(Camera.main.ScreenToWorldPoint(Input.mousePosition).x < rb.position.x);   
            }
        }
    }
}
