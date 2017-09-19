using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepController : MonoBehaviour
{

    [SerializeField]
    private float speed = 1.0f;

    private Rigidbody2D rig;
    private GameObject gameController;
    private MouseController mouse;

    private float destinationOffset = 0.5f;
    private Vector2 destination;
    [SerializeField]
    private bool followingOrders = false;

    private bool defaultDestinationReached = false;
    private Vector2 defaultDestination;

    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController");
        if (gameController)
        {
            mouse = gameController.GetComponent<MouseController>();
        }
        rig = GetComponent<Rigidbody2D>();
        defaultDestination = new Vector2(0, 0);
    }

    void Update()
    {
    }

    void FixedUpdate()
    {
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        if (followingOrders)
        {
            followingOrders = GoToDestination(destination);
        }
        else if (!defaultDestinationReached)
        {
            defaultDestinationReached = !GoToDestination(defaultDestination);
        }
    }
    public void SetDestination(Vector2 dest)
    {
        destination = dest;
        followingOrders = true;
        defaultDestinationReached = true;
    }

    public Vector2 GetDestination()
    {
        return destination;
    }

    private bool GoToDestination(Vector2 dest)
    {
        bool ret = true;
        Vector2 direction = (dest - rig.position).normalized;
        Vector2 velocity = direction * speed;
        rig.MovePosition(rig.position + velocity * Time.deltaTime);
        if (Vector3.Distance(transform.position, dest) <= destinationOffset)
        {
            ret = false;
        }
        return ret;
    }
}
