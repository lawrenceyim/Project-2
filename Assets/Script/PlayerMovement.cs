using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float playerSpeed;
    [SerializeField] private float interactDistance = 1;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Vector3 bulletOrigin;
    [SerializeField] private float nextShotIn;
    [SerializeField] private float weaponCooldown;
    [SerializeField] private float weaponSpread;

    void Start()
    {
        nextShotIn = Time.time;
    }


    /* 
    The Input.GetKeyDown("e") is included in Update instead of FixedUpdate because detection is 
    unreliable in the latter since FixedUpdate is called at an interval instead of every frame
    */
    private void Update() {
        Aim();
        Fire();
        if (Input.GetKeyDown("e")) {
            Debug.Log("Interact");
            Interact();
        }
    }

    /* 
    Input detection should be in Update to be more consistent if the button should only be pressed once.
    It will be fine for move since the buttons are held down
    */
    void FixedUpdate()
    {
        Move();
    }

    private void Move() {
        int xMovement = 0;
        int yMovement = 0;
        float movementVector = playerSpeed * Time.deltaTime;
        if (Input.GetKey("w")) {
            yMovement += 1;
        }
        if (Input.GetKey("s")) {
            yMovement -= 1;
        }
        if (Input.GetKey("a")) {
            xMovement -= 1;
        }
        if (Input.GetKey("d")) {
            xMovement += 1;
        }
        if (xMovement != 0 && yMovement != 0) {
            movementVector = Mathf.Sqrt(playerSpeed * playerSpeed / 2)  * Time.deltaTime;
        }
        transform.position = new Vector3(transform.position.x + movementVector * xMovement, transform.position.y + movementVector * yMovement, transform.position.z);
    }

    private void Interact() {
        // if (PlayerData.holdingFishingRod) {
        //     RaycastHit2D hit = Physics2D.Raycast(transform.position, GetVector2FromDirection(), 1.5f, LayerMask.GetMask("Water"));
        //     if (hit == null || hit.collider == null) return;
        //     if (hit.collider.CompareTag("Water")) {
        //         Debug.Log("Fishing");
        //     }
        // }
    }

    void Aim() {
        Vector3 mousePositionScreen = Input.mousePosition;
        Vector3 mousePositionWorld = Camera.main.ScreenToWorldPoint(mousePositionScreen);
        mousePositionWorld.z = 0f;
        Vector3 direction = mousePositionWorld - transform.position;
        transform.up = direction.normalized;

        bulletOrigin = transform.position + direction.normalized * 1.3f;
    }

    void Fire() {
        if (Input.GetMouseButton(0) && Time.time >= nextShotIn) {
            Quaternion rotation = transform.rotation;
            rotation.z += Random.Range(-weaponSpread, weaponSpread);
            Instantiate(bulletPrefab, bulletOrigin, rotation);
            nextShotIn = Time.time + weaponCooldown;
        }
    }
}
