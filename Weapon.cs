using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float range;
    public float fireRate;

    public float reloadTime;

    public float maxAmmo;
    private float currentAmmo;

    public Camera cam;
    public TextMeshProUGUI textMeshProUGUI;

    public bool isReloading;

    private float nextFire;

    private void Start()
    {
        currentAmmo = maxAmmo;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && Time.time >= nextFire && isReloading == false)
        {
            nextFire = Time.time + 1f / fireRate;
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R) || currentAmmo == 0f)
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

        yield return new WaitForSeconds(reloadTime);

        isReloading = false;
        currentAmmo = maxAmmo;
    }
}
