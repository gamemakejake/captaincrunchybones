using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Camera playerCamera;
    public Rigidbody rb;
    public GameObject PS_Digging;
    public AudioSource bark;
    public AudioClip[] barkSounds;
    public bool enableHeadBob;
    public float rotSpeed;
    public float moveSpeed;
    public float jumpSpeed;
    private float cameraXRot;
    public float cameraTargetPos;
    public float cameraBobSpeed;
    public float digIntervalTimer;
    public int numBarks;
    public enum STATES { Moving, Digging}
    public STATES currentState;
    // Start is called before the first frame update
    void Start()
    {
        cameraXRot = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            //Moving State, for moving camera and player around.
            case (STATES.Moving):
                {
                    //Switch to Digging if you can dig.
                    if (Input.GetButtonDown("Fire1") && IsGrounded() == true)
                    {
                        if (Physics.CheckSphere(this.transform.position + this.transform.forward, 0.5f, 1<<8, QueryTriggerInteraction.UseGlobal))
                        {
                            digIntervalTimer = 0.5f;
                            currentState = STATES.Digging;
                            StartCoroutine(DigParticles());
                        }
                    }

                    //BARK BARK
                    if(Input.GetButtonDown("Fire2"))
                    {
                        GameObject bone = GameObject.FindGameObjectWithTag("Bone");
                        float distance = (bone.transform.position - this.transform.position).magnitude;
                        Debug.Log("Distance to Bone: " + distance);
                        if(distance <= 25f)
                        {
                            bark.clip = barkSounds[0];
                        }
                        else if (distance <= 50f)
                        {
                            bark.clip = barkSounds[1];
                        }
                        else
                        {
                            bark.clip = barkSounds[2];
                        }
                        bark.pitch = Random.Range(0.9f, 1.1f);
                        bark.Play();
                        numBarks++;
                    }

                    //Jump
                    if (Input.GetButtonDown("Jump") && IsGrounded() == true)
                    {
                        rb.velocity += new Vector3(0, jumpSpeed, 0);
                    }

                    //Apply Rotation of Camera and Movement of Player.
                    this.transform.localEulerAngles += new Vector3(0, Input.GetAxis("Mouse X") * rotSpeed, 0);
                    cameraXRot -= Input.GetAxis("Mouse Y");
                    playerCamera.gameObject.transform.rotation = Quaternion.Euler(Mathf.Clamp(cameraXRot, -30f, 30f), this.transform.localEulerAngles.y, 0);
                    float velY = rb.velocity.y;
                    rb.velocity = (this.transform.right * Input.GetAxis("Horizontal") + this.transform.forward * Input.GetAxis("Vertical")).normalized * moveSpeed;
                    rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x, -moveSpeed, moveSpeed), velY, Mathf.Clamp(rb.velocity.z, -moveSpeed, moveSpeed));
                    //Bob head if setting is enabled.
                    #region HeadBobbing
                    if (enableHeadBob == true)
                    {
                        if ((rb.velocity.x != 0f || rb.velocity.z != 0f) && IsGrounded() == true)
                        {
                            if (playerCamera.transform.position.y != cameraTargetPos)
                            {
                                playerCamera.transform.position = Vector3.Lerp(playerCamera.transform.position, new Vector3(this.transform.position.x, this.transform.position.y + cameraTargetPos, this.transform.position.z), cameraBobSpeed);
                            }

                            if (Mathf.Abs(playerCamera.transform.position.y - (this.transform.position.y + cameraTargetPos)) < 0.005f)
                            {
                                switch (cameraTargetPos)
                                {
                                    case (0.6f):
                                        {
                                            cameraTargetPos = 0.4f;
                                            break;
                                        }
                                    case (0.4f):
                                        {
                                            cameraTargetPos = 0.6f;
                                            break;
                                        }
                                }
                            }
                        }
                    }
                    #endregion
                    break;
                }

            //Digging State, for digging up treasure.
            case (STATES.Digging):
            {
                digIntervalTimer -= Time.deltaTime;
                if (digIntervalTimer <= 0f)
                {
                            StopCoroutine(DigParticles());
                            currentState = STATES.Moving;
                }
                break;
            }
        }
    }

    public bool IsGrounded()
    {
        if(Physics.CheckBox(this.transform.position,new Vector3(0.5f,0.1f,0.4f),this.transform.rotation,1,QueryTriggerInteraction.Ignore))
        {
            Debug.Log("Is Grounded");
            return true;
        }
        Debug.Log("Is NOT Grounded");
        return false;
    }

    IEnumerator DigParticles()
    {
        while (currentState == STATES.Digging)
        {
            GameObject ps_Dirt = Instantiate(PS_Digging, this.transform.position + this.transform.forward, Quaternion.identity, null);
            Destroy(ps_Dirt, 0.5f);
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position + this.transform.forward, 0.5f);
    }
}
