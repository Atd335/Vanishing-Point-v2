using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController3D : MonoBehaviour
{

    public float mouseSensitivity = 1;
    public float playerSpd = 6;
    public float jumpHeight = 1.3f;
    public float gravity = 6;
    public float bobTimer;
    public float bobSpd = 10f;
    public float bobMag = .05f;


    Vector3 moveDirection;
    Vector3 cameraRotation;
    Vector3 headPos;

    GameObject player3D;
    CharacterController CC;
    ModeSwitcher switcher;
    public Transform head;
    public Transform headmast;

    bool inputToggle;

	public void _Start()
    {
        switcher = UpdateDriver.ud.GetComponent<ModeSwitcher>();

        player3D = GameObject.FindGameObjectWithTag("Player 3D");
        CC = player3D.GetComponent<CharacterController>();
        head = GameObject.FindWithTag("Head 3D").transform;
        headmast = GameObject.FindWithTag("Head Mast 3D").transform;

        cameraRotation = head.localRotation.eulerAngles;

        headPos = head.localPosition;
    }
    public void _Update()
    {
        if (!switcher.firstPerson)
        {
            inputToggle = true;
            return;
        }
        Vector3 v = new Vector3(moveDirection.x,0,moveDirection.z);
        if (v.magnitude > 0.1f)
        {
            bobTimer += Time.deltaTime * bobSpd;
            head.localPosition = headPos + new Vector3(0, Mathf.Sin(bobTimer) * bobMag, 0);
        }
        else
        {
            bobTimer = 0;
            head.localPosition = Vector3.Lerp(head.localPosition, headPos, Time.deltaTime * 12f);
        }

        cameraRotation += new Vector3(-Input.GetAxis("Mouse Y"),Input.GetAxis("Mouse X"),0) * mouseSensitivity * UpdateDriver.ud.GetComponent<PauseSettingsScript>().mouseSensitiviy;
        cameraRotation.x = Mathf.Clamp(cameraRotation.x,-90,90);
        head.rotation = Quaternion.Euler(cameraRotation);

        headmast.rotation = Quaternion.Euler(0,cameraRotation.y,0);

        moveDirection = new Vector3(Input.GetAxis("Horizontal_3D"),moveDirection.y, Input.GetAxis("Vertical_3D"));
        moveDirection = moveDirection.x * headmast.right + 
                        moveDirection.z * headmast.forward + 
                        moveDirection.y * headmast.up;

        if (CC.isGrounded) 
        {
            moveDirection.y = 0;
            if (Input.GetButton("Jump_3D"))
            {
                moveDirection.y = jumpHeight;
            }

        }
        else
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }


        if (inputToggle)
        {
            bool b = Input.GetButtonDown("Horizontal_3D") ||
                     Input.GetButtonDown("Vertical_3D") ||
                     Input.GetButtonDown("Jump_3D");
            moveDirection.x = 0;
            moveDirection.z = 0;
            inputToggle = !b;
        }

        CC.Move(moveDirection * playerSpd * Time.deltaTime);
    }


    public void InterpolateCharacterToPosition(Vector3 pos, float spd = 5)
    {
        StartCoroutine(InterpolateCharacterToPositionCoroutine(pos,spd));
    }

    public void InterpolateCharacterToPosition(string posStr)
    {
        Vector3 pos;

        pos.x = float.Parse(posStr.Split(',')[0]);
        pos.y = float.Parse(posStr.Split(',')[1]);
        pos.z = float.Parse(posStr.Split(',')[2]);

        float spd = float.Parse(posStr.Split(',')[3]);
        
        StartCoroutine(InterpolateCharacterToPositionCoroutine(pos, spd));
    }

    IEnumerator InterpolateCharacterToPositionCoroutine(Vector3 pos, float spd)
    {
        float timer = 0;
        Vector3 initPos = player3D.transform.position;

        while (timer < 1)
        {
            timer += Time.deltaTime * spd;
            timer = Mathf.Clamp(timer,0,1);

            player3D.transform.position = Vector3.Lerp(initPos, pos, timer);

            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    public void InterpolateCharacterToRotation(Vector3 rot, float spd = 5)
    {
        InterpolateCharacterToRotationCoroutine(rot,spd);
    }

    public void InterpolateCharacterToRotation(string posStr)
    {
        Vector3 rot;

        rot.x = float.Parse(posStr.Split(',')[0]);
        rot.y = float.Parse(posStr.Split(',')[1]);
        rot.z = float.Parse(posStr.Split(',')[2]);

        float spd = float.Parse(posStr.Split(',')[3]);

        StartCoroutine(InterpolateCharacterToRotationCoroutine(rot, spd));
    }

    IEnumerator InterpolateCharacterToRotationCoroutine(Vector3 rot, float spd)
    {
        float timer = 0;
        Vector3 initRot = head.rotation.eulerAngles;

        while (timer < 1)
        {
            timer += Time.deltaTime * spd;
            timer = Mathf.Clamp(timer, 0, 1);

            head.rotation = Quaternion.Lerp(Quaternion.Euler(initRot), Quaternion.Euler(rot), timer);

            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

}
