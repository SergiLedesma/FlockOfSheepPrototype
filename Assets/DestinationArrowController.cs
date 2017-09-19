using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationArrowController : MonoBehaviour {

    [SerializeField]
    private float lifeTime = 0.5f;
    
    public void AutoDestroy()
    {
        Destroy(gameObject, lifeTime);
    }
}
