using UnityEngine;
using UnityEngine.InputSystem;

public class MovHuguinho : MonoBehaviour
{
    private Rigidbody rb;
    private float vel = 5f;
    private Vector3 inputDirecao;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current == null) return;
        float x = 0f;
        float z = 0f;
        
        if (Keyboard.current.wKey.isPressed) z = 1f;
        if (Keyboard.current.sKey.isPressed) z = -1f;
        if (Keyboard.current.aKey.isPressed) x = -1f;
        if (Keyboard.current.dKey.isPressed) x = 1f;
        
        inputDirecao = new Vector3(x,0f, z).normalized;
    }
    void FixedUpdate()
    {
        MoverHuguinho();
    }
    private void MoverHuguinho()
    {
        Vector3 movimento = new Vector3(inputDirecao.x, 0f, inputDirecao.z);
        rb.linearVelocity = new Vector3(movimento.x * vel, rb.linearVelocity.y, movimento.z * vel);
    }
}
