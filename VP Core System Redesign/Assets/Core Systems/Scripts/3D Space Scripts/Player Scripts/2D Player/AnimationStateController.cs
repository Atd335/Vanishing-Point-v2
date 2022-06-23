using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationStateController : MonoBehaviour
{
    Image img;
    Animator anim;
    CharacterController2D cc2d;
    ModeSwitcher switcher;

    bool walking;
    bool inAir;
    bool paused;

    bool jumpTrigger;

    public void _Start()
    {
        anim = GameObject.FindGameObjectWithTag("AnimatedPlayer").GetComponent<Animator>();
        img = GameObject.FindGameObjectWithTag("AnimatedPlayer").GetComponent<Image>();
        cc2d = UpdateDriver.ud.GetComponent<CharacterController2D>();
        switcher = UpdateDriver.ud.GetComponent<ModeSwitcher>();
    }

    public void _Update()
    {
        walking = Mathf.Abs(cc2d.moveDirection.x) > 0;
        inAir = !(cc2d.isGrounded||cc2d.isGrounded2);
        paused = switcher.firstPerson || UpdateDriver.ud.GetComponent<DeveloperTools>().devModeEnabled;

        anim.SetBool("Walking",walking);
        anim.SetBool("In Air",inAir);
        anim.SetBool("Paused",paused);
        img.transform.localScale = Vector3.Lerp(img.transform.localScale, Vector3.one, Time.deltaTime * 12);

        //----------------------------
        if (paused) { return; }

        if (Input.GetButtonDown("Jump_2D")&&!inAir) { img.transform.localScale = new Vector3(.6f,1.2f,1); }
        if (!inAir && !jumpTrigger)
        {
            img.transform.localScale = new Vector3(1.3f, .6f, 1);
            jumpTrigger = true;
        }
        else if (inAir)
        {
            jumpTrigger = false;
        }

        if (Input.GetAxis("Horizontal_2D") > 0) { img.transform.rotation = Quaternion.Euler(0,0,0); }
        if (Input.GetAxis("Horizontal_2D") < 0) { img.transform.rotation = Quaternion.Euler(0, 180, 0); }
    }
}
