using UnityEngine;
public class CursorScript : MonoBehaviour
{   
    private Transform cursorLigthPosition;
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
        cursorLigthPosition = GetComponent<Transform>();
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
            if (hit.collider.CompareTag("Ground"))
            {
                float x = hit.point.x;
                float z = hit.point.z;
                cursorLigthPosition.position = new Vector3(x, 12f, z);
            }                        
        }
    }
    public Vector3 GetCursorPosition()
    {
        Ray camRay = Camera.main.ScreenPointToRay(_input.Player.MousePosition.ReadValue<Vector2>());
        RaycastHit hit;
        if (Physics.Raycast(camRay, out hit))
        {
            float x = hit.point.x;
            float z = hit.point.z;
            
            return new Vector3(x, 0, z);
        }
        else
        {
            return new Vector3(0, 0, 0);
        }
    }
}