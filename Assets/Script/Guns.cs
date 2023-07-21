using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guns : MonoBehaviour
{
    public string name;
    public  float cooldown;
    public  int magazineSize;
    public  int bulletsLeft;
    public  float reloadTime;
    public  float nextShotIn;
    public  float reloadFinishIn;
    public bool reloading;
    public GameObject bulletPrefab;
    public float weaponSpread;

    public Guns(string name, float cooldown, int magazineSize, float reloadTime, float weaponSpread, GameObject bulletPrefab) {
        this.name = name;
        this.cooldown = cooldown;
        this.magazineSize = magazineSize;
        this.reloadTime = reloadTime;
        this.bulletsLeft = magazineSize;
        this.nextShotIn = Time.time;
        this.bulletPrefab = bulletPrefab;
        this.weaponSpread = weaponSpread;
    }

    public bool CanFire() {
        if (Time.time >= nextShotIn && bulletsLeft > 0) {
            return true;
        }
        return false;
    }

    public void Fire() {
        nextShotIn = Time.time + cooldown;
        bulletsLeft--;
    }

    public void Reload() {
        Debug.Log("Reloading");
        reloading = true;
        reloadFinishIn = Time.time + reloadTime;
        bulletsLeft = 0;
    }

    public void InterruptReload() {
        reloading = false;
    }
}
