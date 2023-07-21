using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorToScreen : MonoBehaviour
{
    [SerializeField] string position;
    [SerializeField] float xOffset;
    [SerializeField] float yOffset;

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
        if (position.Equals("Bottom Center")) {
            // Calculate the position of the bottom edge of the screen in world coordinates
            float cameraBottom = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0f, mainCamera.nearClipPlane)).y;
            // Set the sprite's position to the bottom of the screen
            Vector3 spritePosition = player.transform.position;
            spritePosition.y = cameraBottom + yOffset;
            transform.position = spritePosition;
        } else if (position.Equals("Bottom Left")) {
            // Calculate the position of the bottom edge of the screen in world coordinates
            float cameraBottom = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0f, mainCamera.nearClipPlane)).y;
            float cameraLeft = mainCamera.ViewportToWorldPoint(new Vector3(0f, 0.5f, mainCamera.nearClipPlane)).x;
            // Set the sprite's position to the bottom of the screen
            Vector3 spritePosition = player.transform.position;
            spritePosition.x = cameraLeft + xOffset;
            spritePosition.y = cameraBottom + yOffset;
            transform.position = spritePosition;
        }
    }
}
