using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerControlScript : MonoBehaviour
{
    [SerializeField] private Transform _bodyObj;    
    private Controllers _input;
    private Rigidbody _rb;        
    private float _speedRotation = 20;
    private Mouse mouse;
    private WeaponSlotsScript weapons;
    private CursorScript cursor;
    private bool shooting;
    private void Awake()
    {
        cursor = FindObjectOfType<CursorScript>();
        mouse = Mouse.current;
        _input = new Controllers();
        _input.Player.LeftClick.performed += context => PlayerShoot();
        _input.Player.Weapon1.performed += context => WeaponSwitch(0);
        _input.Player.Weapon2.performed += context => WeaponSwitch(1);
        _input.Player.Weapon3.performed += context => WeaponSwitch(2);
        _input.Player.Weapon4.performed += context => WeaponSwitch(3);
    }
    private void OnEnable()
    {
        _input.Enable();
    }
    private void OnDisable()
    {
        _input.Disable();
    }
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        weapons = GetComponent<WeaponSlotsScript>();
    }
    private void Update()
    {
        PlayerMove();
        RotationToMouse();
        if (mouse.leftButton.wasPressedThisFrame && shooting == false)
        {
            shooting = true;            
        }
        else if (mouse.leftButton.wasReleasedThisFrame)
        {
            shooting = false;
        }
        if (shooting == true)
        {
            weapons.WeaponShoot(cursor.GetCursorPosition());
        }
    }
    private void PlayerShoot()
    {        
                
    }
    private void PlayerMove()
    {
        Vector2 direction = _input.Player.Move.ReadValue<Vector2>();
        Vector3 movement = new Vector3(-direction.x, 0.0f, -direction.y);
        _rb.AddForce(movement * 100);       
    }
    private void WeaponSwitch(int num)
    {              
        weapons.WeaponSelected(num);
    }
    private void RotationToMouse()
    {
        Ray camRay = Camera.main.ScreenPointToRay(_input.Player.MousePosition.ReadValue<Vector2>());
        RaycastHit hit;
        if (Physics.Raycast(camRay, out hit))
        {
            Vector3 toMouse = hit.point - _bodyObj.position;
            toMouse.y = 0f;
            Quaternion rotation = Quaternion.LookRotation(toMouse);
            _bodyObj.rotation = Quaternion.Lerp(_bodyObj.rotation, rotation, _speedRotation * Time.deltaTime);
        }
    }
}