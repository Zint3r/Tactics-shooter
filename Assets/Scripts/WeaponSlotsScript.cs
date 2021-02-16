using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WeaponSlotsScript : MonoBehaviour
{
    [SerializeField] private GameObject[] weapons;
    [SerializeField] private GameObject[] weaponsModels;
    [SerializeField] private int[] ammo;
    [SerializeField] private float[] rechargeTime;
    [SerializeField] private GameObject[] bullet;
    [SerializeField] private Text[] ammoCountTexts;
    [SerializeField] private Transform shootPosition;
    private float[] currentRechargeTime = new float[4];
    private int currentWeapon = 0;
    public int CurrentWeapon { get => currentWeapon; set => currentWeapon = value; }
    public int[] Ammo { get => ammo; set => ammo = value; }

    void Start()
    {
        GetTextComponent();
        SetRechage();
        WeaponSelected(0);        
    }
    private void FixedUpdate()
    {
        RechargeWeapon(currentWeapon);
    }
    public void WeaponSelected(int slot)
    {
        currentWeapon = slot;
        for (int i = 0; i < weapons.Length; i++)
        {
            if (i == slot)
            {
                weapons[i].SetActive(true);
                weaponsModels[i].SetActive(true);
            }
            else
            {
                weapons[i].SetActive(false);
                weaponsModels[i].SetActive(false);
            }
        }
    }
    public void WeaponShoot(Vector3 target)
    {
        if (ammo[currentWeapon] > 0 && currentRechargeTime[currentWeapon] >= rechargeTime[currentWeapon])
        {
            GameObject currentBullet = Instantiate(bullet[currentWeapon]) as GameObject;
            Ray weaponRay = new Ray(shootPosition.position, shootPosition.forward);
            RaycastHit hit;
            if (Physics.Raycast(weaponRay, out hit))
            {
                float distToTarget = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(target.x, 0, target.z));
                float distToHit = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(hit.point.x, 0, hit.point.z));
                if (distToTarget >= distToHit)
                {
                    currentBullet.transform.position = hit.point;
                    if (hit.collider.gameObject.GetComponent<DestructibleObjectScript>() != null)
                    {
                        hit.collider.gameObject.GetComponent<DestructibleObjectScript>().ObjectActivation();
                    }
                }
                else
                {
                    currentBullet.transform.position = target;
                }                
            }
            else
            {
                currentBullet.transform.position = target;
            }
            ammo[currentWeapon]--;
            currentRechargeTime[currentWeapon] = 0;            
            ammoCountTexts[currentWeapon].text = ammo[currentWeapon].ToString();            
            Destroy(currentBullet, 3f);
            GetTextComponent();
            if (ammo[currentWeapon] == 0)
            {
                ammoCountTexts[currentWeapon].color = Color.red;
            }
        }
    }
    private void ExplosionShooting()
    {

    }
    public void GetTextComponent()
    {
        for (int i = 0; i < weapons.Length; i++)
        {           
            ammoCountTexts[i].text = ammo[i].ToString();            
        }
    }
    private void SetRechage()
    {
        for (int i = 0; i < weapons.Length; i++)
        {            
            currentRechargeTime[i] = rechargeTime[i];
        }
    }
    private void RechargeWeapon(int num)
    {
        if (currentRechargeTime[num] < rechargeTime[num])
        {
            currentRechargeTime[num] += 0.02f;
        }            
    }
}