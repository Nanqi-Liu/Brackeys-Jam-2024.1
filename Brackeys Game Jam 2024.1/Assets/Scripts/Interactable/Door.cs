using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    private Animator animator;
    public int isLocked = 0;
    public int hasKey = 0;

    public bool isOpen = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnSelected()
    {
        //Debug.Log(gameObject.name + " is selected");
    }

    public override void OffSelected()
    {
        //Debug.Log(gameObject.name + " is deselected");
    }

    public override void Interact()
    {
        // No animation is playing
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !animator.IsInTransition(0))
        {
            if (isLocked <= 0)
            {
                // Unlocked
                if (isOpen)
                {
                    Close();
                }
                else
                {
                    Open();
                }
                isOpen = !isOpen;
            }
            else
            {
                if (hasKey > 0)
                {
                    // Using key to unlock
                    Unlock();
                }
                else
                { 
                    // Locked
                    Locked();
                }
            }
        }
    }

    private void Open()
    {
        animator.Play("DoorOpen", 0, 0f);
        AudioManager.instance.PlaySound("DoorOpen");
    }

    private void Close()
    {
        animator.Play("DoorClose", 0, 0f);
        AudioManager.instance.PlaySound("DoorClose");
    }

    private void Locked()
    {
        // Play some lock sound
        AudioManager.instance.PlaySound("DoorLock");
    }

    private void Unlock()
    {
        hasKey -= 1;
        isLocked -= 1;
        // Play some unlock sound
        AudioManager.instance.PlaySound("DoorUnlock");
    }
}
