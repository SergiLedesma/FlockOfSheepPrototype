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

    [SerializeField]
    private float hp;
    [SerializeField]
    private float maxHp = 100;

    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController");
        if (gameController)
        {
            mouse = gameController.GetComponent<MouseController>();
        }
        rig = GetComponent<Rigidbody2D>();
        defaultDestination = new Vector2(Random.Range(-8.5f, 8.5f), Random.Range(-4.5f, 4.5f));
        hp = maxHp;
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
            if (!followingOrders)
            {
                defaultDestinationReached = false;
            }
        }
        else if (!defaultDestinationReached)
        {
            defaultDestinationReached = !GoToDestination(defaultDestination);
            if (defaultDestinationReached)
            {
                defaultDestination = new Vector2(Random.Range(-8.5f, 8.5f), Random.Range(-4.5f, 4.5f));
                defaultDestinationReached = false;
            }
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

    public void DecreaseHP(float value)
    {
        hp -= value;
    }
}
