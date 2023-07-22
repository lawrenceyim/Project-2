using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    private void Start()
    {
        // Hide the system cursor
        Cursor.visible = false;
    }

    private void Update()
    {
        // Set the cursor position to the mouse position
        Vector3 mousePositionScreen = Input.mousePosition;
        Vector3 mousePositionWorld = Camera.main.ScreenToWorldPoint(mousePositionScreen);
        mousePositionWorld.z = -5;
        transform.position = mousePositionWorld;
    }
}
