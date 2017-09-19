using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadAreaController : MonoBehaviour {

    private ArrayList characters = new ArrayList();

    [SerializeField]
    private float dot = 1.0f;

	void Start ()
    {

    }
	
    void Update () {
        if (characters.Count > 0)
        {
            for(int i = 0; i < characters.Count; ++i)
            {
                GameObject character = characters[i] as GameObject;
                SheepController sheep = character.GetComponent<SheepController>();
                Debug.Log(characters.Count);
                if (sheep)
                {
                    sheep.DecreaseHP(dot);
                }
            }
        }
	}

    void OnTriggerEnter(Collider other)
    {
        characters.Add(other);
        Debug.Log("IN");
    }


    void OnTriggerExit(Collider other)
    {
        characters.Remove(other);
        Debug.Log("OUT");
    }
}
