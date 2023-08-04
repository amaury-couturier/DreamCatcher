using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShootPistol : MonoBehaviour
{
    public GameObject bullet;

    public float shootForce;

    public float timeBetweenShooting, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool buttonHold;

    private int bulletsLeft, bulletsShot;

    private bool shooting, readyToShoot;

    public Camera cam;
    public Transform pistolTip;

    public GameObject muzzleFlash;
    public TextMeshProUGUI ammunitionDisplay;

    public bool allowInvoke = true;

    private void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }

    private void Update()
    {
        MyInput();

        if(ammunitionDisplay != null)
            ammunitionDisplay.SetText(bulletsLeft + " / " + magazineSize);
    }

    private void MyInput()
    {
        if(buttonHold)
            shooting = Input.GetKey(KeyCode.Mouse1);
        else   
            shooting = Input.GetKeyDown(KeyCode.Mouse1);

        if(readyToShoot && shooting && bulletsLeft > 0)
        {
            bulletsShot = 0;

            Shoot();
        }
    }

    private void Shoot()
    {
        readyToShoot = false;
        bulletsLeft--;
        bulletsShot++;

        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        Vector3 targetPoint;
        if(Physics.Raycast(ray, out hit))
            targetPoint = hit.point;
        else   
            targetPoint = ray.GetPoint(100);

        Vector3 directionWithoutSpread = targetPoint - pistolTip.position;

        GameObject currentBullet = Instantiate(bullet, pistolTip.position, Quaternion.identity);
        currentBullet.transform.forward = directionWithoutSpread.normalized;

        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithoutSpread.normalized * shootForce, ForceMode.Impulse);

        if(muzzleFlash != null)
            Instantiate(muzzleFlash, pistolTip.position, Quaternion.identity);

        if(allowInvoke)
        {
            Invoke("ResetShot", timeBetweenShooting);
            allowInvoke = false;
        }
    }

    private void ResetShot() 
    {
        readyToShoot = true;
        allowInvoke = true;
    }
}