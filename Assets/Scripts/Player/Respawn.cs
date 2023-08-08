using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public float threshold;
    [SerializeField] private GameObject player;
    [SerializeField] private List<GameObject> checkPoints;
    [SerializeField] private Vector3 vectorPoint;

    void Update()
    {
        if(transform.position.y < threshold)
            transform.position = vectorPoint;
    }

    void OnTriggerEnter(Collider other)
    {
        vectorPoint = player.transform.position;
        Destroy(other.gameObject);
    }
}
