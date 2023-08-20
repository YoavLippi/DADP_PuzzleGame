using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearMovement : MonoBehaviour
{
    [SerializeField] private bool movable, rotatable;
    [SerializeField] private GearController attachedController;
    [SerializeField] private LayerMask boardLayer;
    [SerializeField] private LayerMask gearLayer;
    [SerializeField] private GameObject arrowPointer;

    private float timer;

    private Rigidbody2D rb;

    private Vector2 offset;

    private Vector2 initialPos;

    private BoardManager bm;

    private TileHoldChecker lastTile;

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
        bm = GameObject.FindWithTag("BoardController").GetComponent<BoardManager>();
        RaycastHit2D hit = Physics2D.Raycast(rb.position, Vector2.zero,
            Mathf.Infinity, boardLayer);
        if (hit)
        {
            lastTile = hit.transform.GetComponent<TileHoldChecker>();
            lastTile.occupied = true;
            lastTile.GetComponent<Collider2D>().enabled = false;
            rb.position = hit.transform.position;
        }
        
        foreach (var child in GetComponentsInChildren<SpriteRenderer>())
        {
            if (child.transform.name == "Circle")
            {
                child.color = rotatable ? Color.white : Color.black;
            }
        }
    }

    private void OnMouseEnter()
    {
        if (rotatable)
        {
            arrowPointer.SetActive(true);
            if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x > rb.position.x)
            {
                arrowPointer.transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
            }
            else
            {
                arrowPointer.transform.rotation = Quaternion.Euler(new Vector3(0,0,180));
            }
        }
    }

    private void OnMouseOver()
    {
        if (rotatable)
        {
            if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x > rb.position.x)
            {
                arrowPointer.transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
            }
            else
            {
                arrowPointer.transform.rotation = Quaternion.Euler(new Vector3(0,180,0));
            }
        }
    }

    private void OnMouseExit()
    {
        if (rotatable)
        {
            arrowPointer.SetActive(false);
        }
    }

    private void OnMouseDown()
    {
        //Debug.Log($"{transform} was clicked");
        initialPos = rb.position;
        if (movable)
        {
            if (lastTile)
            {
                lastTile.occupied = false;
                lastTile.transform.GetComponent<Collider2D>().enabled = true;
            }
            RaycastHit2D hit = Physics2D.Raycast(rb.position, Vector2.zero,
                Mathf.Infinity, boardLayer);
            if (hit)
            {
                lastTile = hit.transform.GetComponent<TileHoldChecker>();
                lastTile.occupied = false;
                lastTile.transform.GetComponent<Collider2D>().enabled = true;
            }
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
            //check if the gear is out of the bounds of the map
            if (rb.position.y is >= 5f or <= -5f || rb.position.x is >= 8.8f or <= -8.8f)
            {
                rb.position = initialPos;
            }
            
            RaycastHit2D hit = Physics2D.Raycast(rb.position, Vector2.zero,
                Mathf.Infinity, boardLayer);
            if (hit)
            {
                if (!hit.transform.GetComponent<TileHoldChecker>().occupied)
                {
                    rb.position = hit.collider.transform.position;
                    hit.transform.GetComponent<TileHoldChecker>().occupied = true;
                    lastTile = hit.transform.GetComponent<TileHoldChecker>();
                    lastTile.transform.GetComponent<Collider2D>().enabled = false;
                }
            }
            else
            {
                lastTile = null;
            }
            
            //changing layers so the raycast doesn't hit this object
            int originalLayer = this.gameObject.layer;
            this.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
            RaycastHit2D hitGear = Physics2D.Raycast(rb.position, Vector2.zero,
                Mathf.Infinity, gearLayer);
            if (hitGear)
            {
                if (hitGear.transform != this.transform)
                {
                    rb.position = initialPos;
                    RaycastHit2D returnHit = Physics2D.Raycast(initialPos, Vector2.zero,
                        Mathf.Infinity, boardLayer);
                    if (returnHit)
                    {
                        lastTile = returnHit.transform.GetComponent<TileHoldChecker>();
                        returnHit.transform.GetComponent<TileHoldChecker>().occupied = true;
                        lastTile.transform.GetComponent<Collider2D>().enabled = false;
                    }
                }
            }

            this.gameObject.layer = originalLayer;
        }

        if (rotatable)
        {
            if (Time.time - timer <= 0.3f && Vector2.Distance(rb.position, initialPos) < 0.01f)
            {
                attachedController.RotateGear(Camera.main.ScreenToWorldPoint(Input.mousePosition).x < rb.position.x); 
                bm.IncrementCounter();
            }
        }

        if (movable || rotatable)
        {
            if (attachedController.ThisBoardManager.CheckAllOccupied())
            {
                attachedController.ThisBoardManager.EndLevel();
            }
        }
    }
}
