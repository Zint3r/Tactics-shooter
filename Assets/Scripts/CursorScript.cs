using UnityEngine;
public class CursorScript : MonoBehaviour
{
    private Transform _cursorPosition;
    private Controllers _input;
    private void Awake()
    {
        _input = new Controllers();
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
        _cursorPosition = GetComponent<Transform>();
    }
    private void Update()
    {
        ChangeCursorPosition();
    }
    private void ChangeCursorPosition()
    {        
        Ray camRay = Camera.main.ScreenPointToRay(_input.Player.MousePosition.ReadValue<Vector2>());
        RaycastHit hit;
        if (Physics.Raycast(camRay, out hit))
        {
            float x = hit.point.x;
            float z = hit.point.z;
            _cursorPosition.position = new Vector3(x, 12f, z);
        }
    }
}