using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    [SerializeField] Camera FPCamera;
    [SerializeField] float range = 100f;
    [SerializeField] float damage = 30f;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject hitEffect;
    [SerializeField] Ammo ammoSlot;
    [SerializeField] AmmoType ammoType;
    [SerializeField] float timeBetweenShots = .5f;
    [SerializeField] TextMeshProUGUI ammoText;
    [SerializeField] AudioClip reloadSFX;
    [SerializeField] AudioClip shootSFX;

    AudioSource myAudioSource;

    bool canShoot = true;

    private void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
    }
    private void OnEnble()
    {
        canShoot = true;
    }

    void Update()
    {
        DisplayAmmo();
        if (Input.GetButtonDown("Fire1") && canShoot == true)
        {
            StartCoroutine(Shoot());
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if(ammoSlot.GetCurrentAmmo(ammoType) == ammoSlot.GetAmmoInGun(ammoType))
            {
                //do nothing
            }
            else
            {

                StartCoroutine(Reload());
            }
           
        }

        }

    private void DisplayAmmo()
    {
        int currentAmmoL = ammoSlot.GetCurrentAmmo(ammoType);
        int currentAmmoR = ammoSlot.GetAmmoAmount(ammoType);
        ammoText.text = "Ammo " + currentAmmoL.ToString() + '/' + currentAmmoR.ToString();
    }

    IEnumerator Reload()
    {
        canShoot = false;
        myAudioSource.PlayOneShot(reloadSFX);
        ammoSlot.ReloadMechanics(ammoType);
        FindObjectOfType<WeaponSwitcher>().enabled = false;
        yield return new WaitForSeconds(3.0f);
        canShoot = true;
        FindObjectOfType<WeaponSwitcher>().enabled = true;
    }

    IEnumerator Shoot()
    {
        canShoot = false;
        if (ammoSlot.GetCurrentAmmo(ammoType) > 0 )
        {            
            PlayMuzzleFlash();
            ProccessRaycast();
            ammoSlot.ReduceCurrentAmmo(ammoType);
            ammoSlot.ReduceAmmoAmount(ammoType);
        }
        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true;

    }

    private void PlayMuzzleFlash()
    {
        muzzleFlash.Play();
        myAudioSource.PlayOneShot(shootSFX);
    }

    private void ProccessRaycast()
    {
        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out RaycastHit hit, range))
        {
            CreateHitImpact(hit);
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target == null) return;
            target.TakeDamage(damage);
        }
        else
        {
            return;
        }
    }

    private void CreateHitImpact(RaycastHit hit)
    {
        GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impact, .1f);
    }
}
