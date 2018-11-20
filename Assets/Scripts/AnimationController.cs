using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class AnimationController : MonoBehaviour {

    public Animator animator;
    private Rigidbody rb;
    private Animation anime;

    // Use this for initialization
    void Start () {
        //animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        anime = GetComponent<Animation>();
    }

    public AnimationClip GetAnimationClip(string name)
    {
        if (!animator) return null; // no animator

        foreach (AnimationClip clip in animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == name)
            {
                return clip;
            }
        }
        return null; // no clip by that name
    }

    // Update is called once per frame
    void Update () {
        float x = CrossPlatformInputManager.GetAxis("Horizontal");
        float y = CrossPlatformInputManager.GetAxis("Vertical");

        Vector3 movement = new Vector3(x, 0, y);
        rb.velocity = movement * 4f;


        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Kick triggered");
            anime.Play("RoundKick");
        }
        if (x != 0 && y != 0)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, Mathf.Atan2(x, y) * Mathf.Rad2Deg, transform.eulerAngles.z);
            if (Input.GetKeyDown(KeyCode.W))
            {
                Debug.Log("Run");
                anime.Play("Run");
            }
            else
            {
                Debug.Log("Walk");
                anime.Play("Walk");
            }
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            Debug.Log("Idle State");
            anime.Play("Idle");
        }
        else if(Input.GetKeyDown(KeyCode.S))
        {
            anime.Play("Smile");
        }
        else if(Input.GetKeyDown(KeyCode.F))
        {
            anime.Play("Frown");
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            anime.Play("Cry");
        }
        else if (Input.GetKeyDown(KeyCode.V))
        {
            anime.Play("Surprise");
        }

    }
}
