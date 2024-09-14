using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems; 

public class Dialog : MonoBehaviour
{
    public GameObject fox;
    public GameObject gruffalo;

    private Animator animator;

    public AudioSource audioSource;
    public AudioClip[] audioClips;

    
    public Canvas canvas;
    public Canvas instruction;
    public Canvas next;

    private Image[] avatars;
    
    private bool stopMoving = true;
    private int index = 0;
    
    

    void Start()
    {
        //fox.GetComponent<Collider>().enabled  = true;
        //canvas.GetComponentInChildren<Text>().enabled = true;
        //GameObject fox = GameObject.FindWithTag("Fox");
        animator = fox.GetComponent<Animator>();
        avatars = canvas.GetComponentsInChildren<Image>();

        instruction.enabled= false;
        next.enabled= false;
        //animator = gameObject.GetComponent<Animator>();
        //if (fox != null) Debug.Log("fox miss");
        if (animator == null) Debug.Log("animator miss");
        gruffalo.gameObject.SetActive(false);
        
    }
    
    void Update()
    {     
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject.CompareTag("Fox"))
                    {
                        Debug.Log("hit fox");
                        AvatarDisplay();
                        if (index >= audioClips.Length)
                        {
                            //Index = 0;
                            return;
                        }
                        if (audioClips.Length == 0)
                        {
                            Debug.Log("AudioSource or clips array not set or empty"); return;
                        }
                        audioSource.clip = audioClips[index];
                        audioSource.Play();
                        if (index == 5)
                        {
                            animator.SetBool("back", true);
                            animator.SetBool("backpose", true);
                            stopMoving = false;
                            //CheckAnimationStatus();
                        }
                        index++;
                        Debug.Log("Index:" + index);
                    }
                }
            }
        }
        //CheckAnimationStatus();
    }
    
    /*public void AudioPlay()
    {
        AvatarDisplay();
        audioSource.clip = audioClips[index];
        audioSource.Play();
        if (index == 5)
        {
            animator.SetBool("back", true);
            animator.SetBool("backpose", true);
            stopMoving = false;
            CheckAnimationStatus();
        }
        index++;
        Debug.Log("Index:" + index);
    }*/

    public void AvatarDisplay()
    {
        if(index>= audioClips.Length)
        {
            avatars[0].gameObject.SetActive(false);
            avatars[1].gameObject.SetActive(false);
            fox.GetComponentInParent<Collider>().enabled=false;
        }
        
        if(index%2==0&&index< audioClips.Length)
        {
            avatars[0].gameObject.SetActive(true);
            avatars[1].gameObject.SetActive(false);
        }
        if(index%2==1 && index < audioClips.Length)
        {
            avatars[0].gameObject.SetActive(false);
            avatars[1].gameObject.SetActive(true);
        }
        if(index==2)
        {
            gruffalo.gameObject.SetActive(true);
        }
        if(index==3)
        {
            gruffalo.gameObject.SetActive(false);
        }
        if(index==6)
        {
            instruction.enabled = false;
            next.enabled=true;
        }
    }

    public void CheckAnimationStatus()
    {
        if (!stopMoving)
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(2); // "move" layer
            Debug.Log("stateInfo.normalizedTime:" + stateInfo.normalizedTime);
            Debug.Log("stateInfo:" + stateInfo);
            if (stateInfo.normalizedTime < 1.0f) // check if "moving" animation finished
            {
                animator.SetBool("back", true);
                animator.SetBool("backpose", true);
                
            }
            else
            {
                //stopMoving = true;
                animator.SetBool("backpose", false);
                //fox.SetActive(true);
            }
        }
    }

}
