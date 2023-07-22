using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float playerSpeed;
    [SerializeField] private float interactDistance = 1;
    [SerializeField] private GameObject lightBulletPrefab;
    [SerializeField] private GameObject mediumBulletPrefab;
    private Rigidbody2D rb;
    [SerializeField] private Vector3 bulletOrigin;
    private GameObject reloadingIndicator;
    private List<Guns> guns;
    [SerializeField] private int equippedWeapon;
    


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        reloadingIndicator = GameObject.FindGameObjectWithTag("Reloading Indicator");
        equippedWeapon = 0;
        guns = new List<Guns>();
        // Initialize with rifle and pistol for now
        // string name, float cooldown, int magazineSize, float reloadTime, float weaponSpread, GameObject bulletPrefab
        guns.Add(new Guns("Glock", .05f, 17, 1.5f, .1f, lightBulletPrefab));
        guns.Add(new Guns("AK-47", .1f, 30, 3f, .2f, mediumBulletPrefab));
        reloadingIndicator.GetComponent<TextMeshPro>().text = 
                $"{guns[equippedWeapon].name} {guns[equippedWeapon].bulletsLeft} / {guns[equippedWeapon].magazineSize}";
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
        float xMovement = 0;
        float yMovement = 0;
        float movementVector = playerSpeed;
        if (Input.GetKey("w")) {
            yMovement += 1f;
        }
        if (Input.GetKey("s")) {
            yMovement -= 1f;
        }
        if (Input.GetKey("a")) {
            xMovement -= 1f;
        }
        if (Input.GetKey("d")) {
            xMovement += 1f;
        }
        if (xMovement != 0 && yMovement != 0) {
            movementVector = Mathf.Sqrt(playerSpeed * playerSpeed / 2);
        }
        rb.MovePosition(transform.position + new Vector3(xMovement * movementVector, yMovement * movementVector, 0f) * Time.deltaTime);
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
            reloadingIndicator.GetComponent<TextMeshPro>().text = GetNameAmmo();
        } else if (guns[equippedWeapon].bulletsLeft == 0 && !guns[equippedWeapon].reloading) {
            guns[equippedWeapon].Reload();
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
            if (equippedWeapon < 0) {
                equippedWeapon = guns.Count - 1;
            } 
            reloadingIndicator.GetComponent<TextMeshPro>().text = GetNameAmmo();
        } else if (Input.GetKeyDown("e")) {
            if (guns[equippedWeapon].reloading) {
                guns[equippedWeapon].InterruptReload();
            }
            equippedWeapon++;
            if (equippedWeapon >= guns.Count) {
                equippedWeapon = 0;
            }
            reloadingIndicator.GetComponent<TextMeshPro>().text = GetNameAmmo();
        }
    }


    /*
    This function is included in the PlayerMovement.cs instead of the Guns.cs because the Guns class is
    not attached to a game object and therefore the Update function does not run for the instantiated Guns
    */
    void WeaponReloading() {
        if (guns[equippedWeapon].reloading) {
            reloadingIndicator.GetComponent<TextMeshPro>().text = GetNameAmmo();
            if (Time.time >= guns[equippedWeapon].reloadFinishIn) {
                guns[equippedWeapon].bulletsLeft = guns[equippedWeapon].magazineSize;
                guns[equippedWeapon].reloading = false;
                reloadingIndicator.GetComponent<TextMeshPro>().text = GetNameAmmo();
            }
        }
    }

    string GetNameAmmo() {
        if (guns[equippedWeapon].reloading) {
            return $"{guns[equippedWeapon].name} reloading";
        }
        return $"{guns[equippedWeapon].name} {guns[equippedWeapon].bulletsLeft} / {guns[equippedWeapon].magazineSize}";
    }
}
