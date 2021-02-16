using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ItemScript : MonoBehaviour
{
    [SerializeField] private Transform namePosition;
    [SerializeField] private GameObject itemPanel;
    public GameObject ItemPanel { get => itemPanel; }
    public int Count { get => count; set => count = value; }
    public int ItemNumber { get => itemNumber; set => itemNumber = value; }
    private int count;
    private int itemNumber;
    private ParticleSystem pickUpAnimation;
    private bool pickUp;
    void Start()
    {
        pickUpAnimation = GetComponent<ParticleSystem>();
    }
    void Update()
    {
        ChangeEnemyPanelPosition();
    }
    private void ChangeEnemyPanelPosition()
    {
        if (pickUp == false)
        {
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 cameraRelative = Camera.main.transform.InverseTransformPoint(transform.position);
            if (cameraRelative.z > 0)
            {
                itemPanel.SetActive(true);
                itemPanel.GetComponent<RectTransform>().position = new Vector3(screenPosition.x, screenPosition.y + 40, screenPosition.z);
            }
            else
            {
                itemPanel.SetActive(false);
            }
        }        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<WeaponSlotsScript>() != null && pickUp == false)
        {
            pickUp = true;
            WeaponSlotsScript currentItem = other.GetComponent<WeaponSlotsScript>();
            currentItem.Ammo[itemNumber] += count;
            currentItem.GetTextComponent();
            pickUpAnimation.Play();
            itemPanel.SetActive(false);
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            Destroy(transform.parent.gameObject, 1f);
        }
    }
}