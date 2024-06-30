using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Controller : MonoBehaviour
{
    AudioSource picking;

    public static Vector3 direction;
    CharacterController characterController;
    float directionX, directionZ;

    Vector3 directionForward = Vector3.zero, directionRight = Vector3.zero;
    Vector3 movement = Vector3.zero;
    Vector3 moveDirectionF = Vector3.zero, moveDirectionR = Vector3.zero;

    [SerializeField]
    float speed;

    [SerializeField]
    float gravity;

    [SerializeField]
    float jumpHeight;

    bool jump;
    bool forward, right , special;

    // Start is called before the first frame update
    void Start()
    {
        forward = false;
        right = false;
        special = false;
        directionX = 0;
        directionZ = 0;
        speed = 10;
        characterController = GetComponent<CharacterController>();
        picking = GameObject.Find("Audio Source 3").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.W))
        //{
        //    directionZ = Mathf.Sin(transform.rotation.ToEulerAngles().y);
        //}
        //else if (Input.GetKeyDown(KeyCode.S))
        //{
        //    directionZ = -Mathf.Sin(transform.rotation.ToEulerAngles().y);
        //} else if (Input.GetKeyDown(KeyCode.D))
        //{
        //    directionX = Mathf.Cos(transform.rotation.ToEulerAngles().y);
        //} else if (Input.GetKeyDown(KeyCode.A))
        //{
        //    directionX = -Mathf.Cos(transform.rotation.ToEulerAngles().y);
        //}

        if (Input.GetKeyDown(KeyCode.W))
        {
            directionForward = transform.forward;
            forward = true;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            directionForward = -transform.forward;
            forward = true;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            directionRight = transform.right;
            right = true;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            directionRight = -transform.right;
            right = true;

        }
        else if (Input.GetKeyUp(KeyCode.W) && !Input.GetKey(KeyCode.S)
            || Input.GetKeyUp(KeyCode.S) && !Input.GetKey(KeyCode.W))
        {
            directionForward = Vector3.zero;
            forward = false;

        }
        //else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        //{
        //    directionRight = Vector3.zero;
        //    right = false;
        //    special = true;
        //}
        else if (Input.GetKeyUp(KeyCode.D) && !Input.GetKey(KeyCode.A) ||
            Input.GetKeyUp(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            directionRight = Vector3.zero;
            right = false;
        }
        //} else if ((Input.GetKeyUp(KeyCode.D) && !Input.GetKey(KeyCode.A) ||
        //    Input.GetKeyUp(KeyCode.A) && !Input.GetKey(KeyCode.D)) && special)
        //{
            
        //    directionForward = transform.forward;
        //    forward = true;
        //}

        if (Input.GetKeyUp(KeyCode.W) && Input.GetKey(KeyCode.S))
        {
            directionForward = -transform.forward;
        } else if (Input.GetKeyUp(KeyCode.S) && Input.GetKey(KeyCode.W))
        {
            directionForward = transform.forward;
        }

        if (Input.GetKeyUp(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            directionRight = transform.right;
        } else if (Input.GetKeyUp(KeyCode.D) && Input.GetKey(KeyCode.A))
        {
            directionRight = -transform.right;
        }
        //directionX = Input.GetAxis("Horizontal");//transform.forward.normalized.z;//Input.GetAxis("Horizontal");
        //directionZ = Input.GetAxis("Vertical");//transform.forward.normalized.x;//Input.GetAxis("Vertical");
        //moveDirectionF = new Vector3(directionX, 0, directionZ);
        
        if (!jump && Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }
    }

    public void FixedUpdate()
    {
        var mousePosition = Input.mousePosition;
        var capsuleWorldPosition = Camera.main.WorldToScreenPoint(transform.position);
        direction = mousePosition - capsuleWorldPosition;

        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Lerp(transform.rotation,
            Quaternion.AngleAxis(-angle, Vector3.up), 10 * Time.fixedDeltaTime);

        if (forward && right)
        {
            movement = (directionRight + directionForward).normalized * speed * Time.fixedDeltaTime;
        } else if (forward)
        {
            movement = directionForward.normalized * speed * Time.fixedDeltaTime;
        } else if (right)
        {
            movement = directionRight.normalized * speed * Time.fixedDeltaTime;
        }
        else
        {
            movement = Vector3.zero;
        }

        if (jump)
        {
            movement.y = jumpHeight;
            jump = false;
        }

        if (jump && characterController.isGrounded)
        {
            movement.y = 0;
        } else
        {
            movement.y -= gravity;
        }

        characterController.Move(movement);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "bullet")
        {
            picking.PlayOneShot(picking.clip);
            Revolver.additionalCount++;
            collision.gameObject.SetActive(false);
        } else if (collision.gameObject.name == "bullet (1)")
        {
            picking.PlayOneShot(picking.clip);
            Revolver.additionalCount++;
            collision.gameObject.SetActive(false);
        } else if (collision.gameObject.name == "bullet (2)")
        {
            picking.PlayOneShot(picking.clip);
            Revolver.additionalCount++;
            collision.gameObject.SetActive(false);
        }
    }
}
