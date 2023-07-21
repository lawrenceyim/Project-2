using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private float topLimit;
    [SerializeField] private float bottomLimit;
    [SerializeField] private float leftLimit;
    [SerializeField] private float rightLimit;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");        
    }

    void Update()
    {
        Vector3 position = player.transform.position;
        position.z = -10;
        if (position.x < leftLimit) {
            position.x = leftLimit;
        }
        if (position.x > rightLimit) {
            position.x = rightLimit;
        }
        if (position.y > topLimit) {
            position.y = topLimit;
        } 
        if (position.y < bottomLimit) {
            position.y = bottomLimit;
        }
        transform.position = position;        
    }
}
