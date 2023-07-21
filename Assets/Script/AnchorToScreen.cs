using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorToScreen : MonoBehaviour
{
    private Camera mainCamera;
    private GameObject player;

    void Start()
    {
        mainCamera = Camera.main;
        player = GameObject.FindGameObjectWithTag("Player");        
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the position of the bottom edge of the screen in world coordinates
        float cameraBottom = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0f, mainCamera.nearClipPlane)).y;
        
        // Set the sprite's position to the bottom of the screen
        Vector3 spritePosition = player.transform.position;
        spritePosition.y = cameraBottom + 1;
        transform.position = spritePosition;
    }
}
