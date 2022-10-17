using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    public Transform firePoint;
    public GameObject bulletPrefab;

    public float bulletForce = 20f;
    public int maxAmmo = 7;
    public float reloadTime = 3f;
    private bool isReloading = false;
    private int ammo;

    void Start()
    {
        ammo = maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        if (isReloading) {
            return;
        }
        if (Input.GetMouseButtonDown(0) && ammo != 0 && !PauseMenu.GamePaused) {
            Shoot();
            ammo--;
        }
        if (ammo == 0) {
            StartCoroutine(Reload());
            return;
        }
    }

    void Shoot() {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }

    private IEnumerator Reload() {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        ammo = maxAmmo;
        isReloading = false;
    }

    public int getAmmo() {
        return ammo;
    }
}
