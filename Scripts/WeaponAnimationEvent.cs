using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimationEvent : MonoBehaviour
{
    // Start is called before the first frame update
    private Weapon weapon;
    private bool aimingCopy;
    private PlayerInterface playerInterface;

  

    private void Start()
    {
        weapon = GameObject.Find("Player").GetComponent<PlayerController>().weapon;
        playerInterface = GameObject.Find("Player").GetComponent<PlayerInterface>();
        //debug
        weapon.weaponReady = true;
    }

    public void ReloadStarted()
    {

        weapon.firing = false;
        weapon.running = false;
        aimingCopy = weapon.aiming;
        weapon.aiming = false;
        //weapon.modelAnimator.SetBool("reloading", true);
       
    }

    public void ReloadCompleted()
    {
        int need = weapon.magazineCapacity - weapon.magazineLeft;
        need = Mathf.Min(need, weapon.ammoLeft);
        weapon.ammoLeft -= need;
        weapon.magazineLeft += need;

        weapon.reloading = false;
        weapon.aiming = aimingCopy;

        playerInterface.SendNumberOfBulletsInMagazine();
        playerInterface.SendNumberOfRemainingAmmo();
        //weapon.aiming = false;
       // weapon.modelAnimator.SetBool("reloading", false);
    }

    public void RunStarted()
    {
        weapon.running = true;
        weapon.reloading = false;
        weapon.aiming = false;
        weapon.firing = false;
    }

    public void AimStarted()
    {
        weapon.cameraController.setAimingStatus(true);
        playerInterface.Aim();
    }

    public void UnAimStarted()
    {
        weapon.cameraController.setAimingStatus(false);
        playerInterface.CancelAim();
    }

    public void WeaponReady()
    {
        weapon.weaponReady = true;
    }
}
