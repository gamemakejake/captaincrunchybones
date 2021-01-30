using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Camera playerCamera;
    public Rigidbody rb;
    public bool enableHeadBob;
    public float rotSpeed;
    public float moveSpeed;
    public float cameraTargetPos;
    public float cameraBobSpeed;
    public enum STATES { Moving, PreJump, Jump}
    public STATES currentState;
    // Start is called before the first frame update
    void Start()
    {
        //Cursor actually performs like in an FPS
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        switch(currentState)
        {
            //Moving State, for moving camera and player around.
            case (STATES.Moving):
            {
                //Apply Rotation of Camera and Movement of Player.
                this.transform.localEulerAngles += new Vector3(0, Input.GetAxis("Mouse X") * rotSpeed, 0);
                rb.velocity = (this.transform.right * Input.GetAxis("Horizontal") + this.transform.forward * Input.GetAxis("Vertical")).normalized * moveSpeed;
                    //Bob head if setting is enabled.
                    #region HeadBobbing
                    if (enableHeadBob == true)
                    {
                        if (rb.velocity.magnitude > 0f)
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
        }
    }
}
