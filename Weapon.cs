using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float range;
    public float fireRate;

    public float reloadingTime;

    public float maxAmmo;
    public float currentAmmo;

    public Camera cam;
    public TextMeshProUGUI textMeshProUGUI;

    public bool isReloading;

    private float nextFire;

    void Start()
    {
        currentAmmo = maxAmmo;
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && isReloading == false && Time.time >= nextFire)
        {
            nextFire = Time.time + 1f / fireRate;
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R) || currentAmmo == 0 && currentAmmo < maxAmmo)
        {
            StartCoroutine(Reloading());
        }

        textMeshProUGUI.text = maxAmmo + " / " + currentAmmo;
    }

    void Shoot()
    {
        --currentAmmo;
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, range))
        {
            Debug.Log("hit in " + hit.collider);
        }
    }

    IEnumerator Reloading()
    {
        isReloading = true;

        yield return new WaitForSeconds(reloadingTime);

        currentAmmo = maxAmmo;
        isReloading = false;
    }
}
