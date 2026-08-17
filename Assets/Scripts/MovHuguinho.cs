using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
public class MovHuguinho : MonoBehaviour
{
    private float gridDistance = 1f;
    private Rigidbody rb;
    private bool canMove = true;
    private bool fit = false;
    // Update is called once per frame
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
    }
    void Update()
    {
        if (Keyboard.current == null) return;
        if (canMove == false) return;
        if (rb.linearVelocity.sqrMagnitude > 0.01f) return;
        
        Vector3 dir = Vector3.zero;
        
        if (Keyboard.current.wKey.wasPressedThisFrame)
        {
            dir.z = 1f;
        }
        else if (Keyboard.current.sKey.wasPressedThisFrame)
        {
            dir.z = -1f;
        }
        else if (Keyboard.current.aKey.wasPressedThisFrame)
        {
            dir.x = -1f;
        }
        else if (Keyboard.current.dKey.wasPressedThisFrame)
        {
            dir.x = 1f;
        }
        
        if (dir == Vector3.zero) return;
        
        
        if(!Physics.Raycast(rb.position, dir, gridDistance))
        {
            if (Physics.Raycast(rb.position, Vector3.down, out RaycastHit hit, 1f) && hit.collider.CompareTag("Gelo"))
            {
                rb.AddForce(dir * 15f, ForceMode.Impulse);
            }
            else
            {
                rb.MovePosition(rb.position + dir * gridDistance);
                rb.linearVelocity = Vector3.zero;
            }
        }
    
    }
    void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Canto") && rb.linearVelocity.sqrMagnitude < 0.01f)
        {
            if (fit) return;
            rb.transform.position = other.transform.position;
            rb.linearVelocity = Vector3.zero;
            fit = true;
            if (canMove)
            {
                StartCoroutine(Cd());
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Canto"))
        {
            fit = false;
        }
    }
    IEnumerator Cd()
    {
        canMove = false;
        yield return new WaitForSeconds(0.5f);
        canMove = true;
    }
    
}
