using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FoxHole : MonoBehaviour
{
    //public GameObject fox;
    private Animator animator;

    private bool stopMoving=true;

    private AudioSource foxAudio;

    private Collider foxCollider;
    private Collider holeCollider;

    private Image[] avatars;
    private TextMeshProUGUI holeInstruction;
    public Canvas canvas;
    public Canvas instruction;
    
    //private Button next;

    // Start is called before the first frame update
    void Start()
    {
        avatars = canvas.GetComponentsInChildren<Image>();
        //holeInstruction.gameObject.SetActive(false);
        //instruction=canvas.GetComponentInChildren<TextMeshProUGUI>();
        //mousePic.enabled=false;
        holeInstruction=instruction.GetComponentInChildren<TextMeshProUGUI>();
        //next = instruction.GetComponentInChildren<Button>();
        canvas.enabled = false;
        instruction.enabled = false;
        
        //animator = fox.GetComponent<Animator>();
        animator = GetComponent<Animator>();
        foxAudio = GetComponent<AudioSource>();
        foxCollider = GetComponent<Collider>();
        holeCollider = GetComponentInParent<Collider>();
        if(holeCollider==null)Debug.Log("no hole");
        foxCollider.enabled = false;
        //Debug.Log(fox);
        
        if (avatars == null)
        {
            Debug.Log("000");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        CheckRay();
        CheckAnimationStatus();


    }
    public void instructionDisplay()
    {
        instruction.gameObject.SetActive(true);
    }

    public void CheckRay()
    {
        
        if (Input.touchCount > 0 && Input.touches[0].phase==TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
            RaycastHit hit;
            Debug.Log($"Touch position: {Input.touches[0].position}");
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("hit");
                if(hit.collider.gameObject.CompareTag("Hole"))
                {
                    Debug.Log("hit hole");
                    if (animator == null)
                    {
                        Debug.Log("Animator component is not found!");
                    }
                    else
                    {
                        canvas.enabled=true;
                        avatars[0].gameObject.SetActive(false);
                        avatars[1].gameObject.SetActive(false);
                        instruction.enabled = true;
                        //holeInstruction.gameObject.SetActive(true);
                        //next.gameObject.SetActive(false);
                        animator.SetBool("walk", true);
                        animator.SetBool("pose", true);
                        stopMoving = false;
                        
                        foxCollider.enabled = true;
                        holeCollider.enabled = false;

                        foxAudio.Play();
                        if (foxAudio.isPlaying)
                        {
                            //mousePic.enabled = true;
                            avatars[1].gameObject.SetActive(true);
                            //holeInstruction.gameObject.SetActive(false);
                            //canvas.GetComponentInChildren<Text>().gameObject.SetActive(true);
                            //mousePic.enabled = false;
                        }
                        else
                        {
                            avatars[1].gameObject.SetActive(false);
                        }
                      
                    }
                    
                }
            }
        }
    }
    void CheckAnimationStatus()
    {
        
        if (!stopMoving)
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0); //"move" layer
            Debug.Log("stateInfo" + stateInfo);
            if (stateInfo.normalizedTime >= 1.0f) //check if "moving" animation finished
            {
                foxCollider.enabled = true;
                bool stopPose = animator.GetBool("pose");
                Debug.Log("state finish" + stopPose);
                //stopMoving = true;
                animator.SetBool("pose", false);
                if (!foxAudio.isPlaying)
                {
                    //mousePic.enabled = true;
                    canvas.enabled = true;
                    avatars[1].gameObject.SetActive(false);
                    //canvas.GetComponentInChildren<Text>().gameObject.SetActive(true);
                }

            }
            else
            {
                //stopMoving = false;
                animator.SetBool("pose", true);
            }
        }
    }

}
