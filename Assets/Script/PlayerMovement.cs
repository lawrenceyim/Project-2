using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float playerSpeed;
    [SerializeField] private float interactDistance = 1;
    [SerializeField] private GameObject lightBulletPrefab;
    [SerializeField] private GameObject mediumBulletPrefab;

    [SerializeField] private Vector3 bulletOrigin;
    private List<Guns> guns;
    [SerializeField] private int equippedWeapon;

    void Start()
    {
        equippedWeapon = 0;
        guns = new List<Guns>();
        // Initialize with rifle and pistol for now
        // string name, float cooldown, int magazineSize, float reloadTime, float weaponSpread, GameObject bulletPrefab
        guns.Add(new Guns("Glock", .05f, 17, 1.5f, .1f, lightBulletPrefab));
        guns.Add(new Guns("AK-47", .1f, 30, 3f, .2f, mediumBulletPrefab));
    }


    /* 
    The Input.GetKeyDown("e") is included in Update instead of FixedUpdate because detection is 
    unreliable in the latter since FixedUpdate is called at an interval instead of every frame
    */
    private void Update() {
        Aim();
        Fire();
        Reload();
        SwitchWeapons();
    }

    /* 
    Input detection should be in Update to be more consistent if the button should only be pressed once.
    It will be fine for move since the buttons are held down
    */
    void FixedUpdate()
    {
        Move();
        WeaponReloading();
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
        if (Input.GetMouseButton(0) && guns[equippedWeapon].CanFire()) {
            guns[equippedWeapon].Fire();
            Quaternion rotation = transform.rotation;
            rotation.z += Random.Range(-guns[equippedWeapon].weaponSpread, guns[equippedWeapon].weaponSpread);
            Instantiate(guns[equippedWeapon].bulletPrefab, bulletOrigin, rotation);
        }
    }
    void Reload() {
        if (Input.GetKeyDown("r")) {
            guns[equippedWeapon].Reload();
        }
    }

    void SwitchWeapons() {
        if (Input.GetKeyDown("q")) {
            if (guns[equippedWeapon].reloading) {
                guns[equippedWeapon].InterruptReload();
            }
            equippedWeapon--;
            equippedWeapon %= guns.Count;
        } else if (Input.GetKeyDown("e")) {
            if (guns[equippedWeapon].reloading) {
                guns[equippedWeapon].InterruptReload();
            }
            equippedWeapon++;
            equippedWeapon %= guns.Count;
        }
    }


    /*
    This function is included in the PlayerMovement.cs instead of the Guns.cs because the Guns class is
    not attached to a game object and therefore the Update function does not run for the instantiated Guns
    */
    void WeaponReloading() {
        if (guns[equippedWeapon].reloading) {
            if (Time.time >= guns[equippedWeapon].reloadFinishIn) {
                guns[equippedWeapon].bulletsLeft = guns[equippedWeapon].magazineSize;
                guns[equippedWeapon].reloading = false;
            }
        } 
    }
}
