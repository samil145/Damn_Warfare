using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.UI;

public class Revolver : MonoBehaviour
{
    AudioSource shot, reload, addition;

    GameObject bullet;
    Rigidbody r;

    public static float directionX,directionZ,bulletCount,additionalCount;

    public Vector3 movement = Vector3.zero;

    bool fire;

    // Start is called before the first frame update
    public void Start()
    {
        bulletCount = 10;
        shot = GameObject.Find("Audio Source 1").GetComponent<AudioSource>();
        reload = GameObject.Find("Audio Source 2").GetComponent<AudioSource>();
        addition = GameObject.Find("Audio Source 4").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && bulletCount > 0)
        {
            bulletCount--;
            bullet = Instantiate(GameObject.Find("Bullet"),
                GameObject.Find("Capsule").transform.position, Quaternion.Euler(
                    new Vector3(GameObject.Find("Capsule").transform.rotation.x,
                (GameObject.Find("Capsule").transform.rotation.ToEulerAngles().y * Mathf.Rad2Deg + 90),
                GameObject.Find("Capsule").transform.rotation.z)));

            r = bullet.GetComponent<Rigidbody>();
            //directionY = GameObject.Find("Capsule").transform.rotation.y;
            //directionW = GameObject.Find("Capsule").transform.rotation.w;
            fire = true;
            shot.PlayOneShot(shot.clip);

        }
        else if (Input.GetKeyDown(KeyCode.Mouse0) && bulletCount == 0)
            reload.PlayOneShot(reload.clip);

        if (Input.GetKeyDown(KeyCode.R) && additionalCount > 0)
        {
            addition.PlayOneShot(addition.clip);
            if (bulletCount + additionalCount > 10)
            {
                additionalCount -= 10 - bulletCount;
                bulletCount = 10;
            }
            else
            {
                bulletCount += additionalCount;
                additionalCount = 0;
            }
        }
        
        
        directionX = Mathf.Sin(GameObject.Find("Capsule").transform.rotation.ToEulerAngles().y) * 2500;
        directionZ = Mathf.Cos(GameObject.Find("Capsule").transform.rotation.ToEulerAngles().y) * 2500;
        movement = new Vector3(directionX,0,directionZ);
    }

    public void FixedUpdate()
    {
        //movement = 5 * movementDirection * Time.fixedDeltaTime;
        if (fire)
        {
            r.AddForce(movement);
            fire = false;
        }
    }


}
