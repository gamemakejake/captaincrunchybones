using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonScript : MonoBehaviour
{
    public Camera cannonCam;
    public Vector3 viewOffset;
    public GameObject projectile;
    private GameObject projectileShot;
    public Transform shootPos;
    public Transform[] startingPositions;
    public bool canFire;
    public int spotNum;
    public float rotY, rotYInitial;
    public float shotSpeed;
    // Start is called before the first frame update
    void Start()
    {
        spotNum = Random.Range(0, startingPositions.Length - 1);

        if (startingPositions != null)
        {
            this.transform.position = startingPositions[spotNum].position;
        }
        rotYInitial = this.transform.rotation.y;
        rotY = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(canFire == true)
        {
            

            rotY += Input.GetAxis("Mouse X");
            this.transform.rotation = Quaternion.Euler(45,Mathf.Clamp(rotY, -45, 45),this.transform.rotation.z);

            cannonCam.transform.position = this.transform.position + viewOffset;
            cannonCam.transform.LookAt(this.transform.position + transform.up * 2);
            if (Input.GetButtonDown("Fire1"))
            {
                projectileShot = Instantiate(projectile, shootPos.position, Quaternion.identity, null);
                projectileShot.GetComponent<Rigidbody>().velocity = this.transform.up * shotSpeed;
                projectileShot.GetComponent<Rigidbody>().AddTorque(new Vector3(Random.Range(-25f, 25f), Random.Range(-25f, 25f), Random.Range(-25f, 25f)));
                canFire = false;
            }
        }

        if(canFire == false)
        {
            if(projectileShot != null)
            {
                cannonCam.transform.position = projectileShot.transform.position + viewOffset;
                cannonCam.transform.LookAt(projectileShot.transform.position);

                if (Physics.CheckBox(projectileShot.transform.position,Vector3.one * 0.25f,Quaternion.identity,1,QueryTriggerInteraction.Ignore))
                {
                    projectileShot.GetComponent<Rigidbody>().isKinematic = true;
                    projectileShot.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    projectileShot.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                    projectileShot.GetComponent<Collider>().isTrigger = true;
                    GameObject gameMan = GameObject.FindObjectOfType<GameManager>().gameObject;
                    StartCoroutine(gameMan.GetComponent<GameManager>().SwitchToPlayerState());
                }

                if(projectileShot.transform.position.y <= -50f)
                {
                    Destroy(projectileShot);
                    canFire = true;
                }
            }
        }
    }

    
}
