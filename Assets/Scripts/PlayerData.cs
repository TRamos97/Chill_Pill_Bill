using ECM.Components;
using ECM.Controllers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerForm {Normal, Ball };
[RequireComponent(typeof(Rigidbody))]
public class PlayerData : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpForce = 500f;
    public float maxHealth = 20;
    public float maxSpeed = 6;
    public PlayerForm state;
    public int seedPoints;
    public float forwardForce = 1000f;
    public ClimbableObject climbableObject;
    public Transform lookDir;
    public InteractableObject interactable;

    private float currentHealth;

    public Vector3 facingDirection;
    private Rigidbody rb;
    public bool canJump;
    public GameObject ballForm;
    public GameObject platformingForm;
    public InventoryObject inventory;
    public InventoryObject equipment;
    public PlayerEffects effects;
    public Animator anim;
    public bool receiveInput = true;
    public Checkpoint activeCheckpoint;
    private BaseCharacterController controller;
    private CharacterMovement platformMovment;
    private BallFormMovement ballMovement;
    private string currAnimName = "";
    private float animationLockTime;

    [SerializeField] Transform cam;

    void Start()
    {
        controller = GetComponent<BaseCharacterController>();
        ballMovement = GetComponent<BallFormMovement>();
        platformMovment = GetComponent<CharacterMovement>();

        rb = GetComponent<Rigidbody>();
        facingDirection = transform.forward;
        currentHealth = maxHealth;
        inventory.Load();
        equipment.Load();
        PlayAnimation("Idle");
    }
    public Vector3 GetLookDirection()
    {
        return lookDir.position - transform.position;
    }
    public void PlayAnimation(string animation, float lockTime = 0f, bool forceReset = false)
    {
        if (anim && ((currAnimName != animation && animationLockTime <= 0) || forceReset))
        {
            currAnimName = animation;
            anim.Play(animation, 0, 0f);
            animationLockTime = lockTime;
        }
    }

    void Update()
    {
        if (!receiveInput)
            return;
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        
        //Camera direction
        Vector3 camForward = cam.forward;
        Vector3 camRight = cam.right;

        //Create relative camera direction
        Vector3 forwardRelative = verticalInput * camForward;
        Vector3 rightRelative = horizontalInput * camRight;

        Vector3 moveDirection = forwardRelative + rightRelative;
        controller.moveDirection = moveDirection;
        ballMovement.moveDirection = moveDirection;

        animationLockTime -= Time.deltaTime;

        if (!controller.isJumping)
        {
            if (moveDirection != Vector3.zero)
            {
                PlayAnimation("Walk");
                facingDirection = new Vector3(moveDirection.x, 0, moveDirection.z);
            }
            else
            {
                PlayAnimation("Idle");
            }
        }

        if (Input.GetKeyDown("f"))
        {
            ChangeShape();
        }

        if (Input.GetKeyDown("e") && interactable)
        {
            interactable.Interact();
        }

        // if there all holes in the world this should stop falling into infinity
        if (transform.position.y < -5)
            TakeDamage(1000);
    }
    public void SetInteractable(InteractableObject obj)
    {
        interactable = obj;
        IngameUI.instance.DisplayInteractionMessage("Press <E> to " + interactable.displayMessage);
    }
    public void RemoveInteractable(InteractableObject obj)
    {
        if (interactable == obj)
        {
            interactable = null;
            IngameUI.instance.DisplayInteractionMessage("");
        }
    }
    public void Jump()
    {
        if (canJump && platformingForm.activeInHierarchy)
        {
            rb.AddForce(Vector3.up * jumpForce);
        }
    }
    public void AddForce(Vector3 power)
    {
        rb.AddForce(power);
    }
    public void AddToInventory(InventoryItemObject item, int amount)
    {
        inventory.AddItem(new Item(item), amount);
    }
    private void OnApplicationQuit()
    {
        inventory.Save();
        equipment.Save();
        inventory.container.Clear();
        equipment.container.Clear();
    }
    public void ChangeShape()
    {
        EffectsManager.instance.MakeSound3d(transform.position, effects.soundTransform);
        if (ballForm.activeInHierarchy)
        {
            ballForm.SetActive(false);
            platformingForm.SetActive(true);

            transform.rotation = Quaternion.LookRotation(facingDirection, Vector3.up);
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            rb.useGravity = false;
            state = PlayerForm.Normal;

            controller.enabled = true;
            platformMovment.enabled = true;
            ballMovement.enabled = false;
            PlayAnimation("Idle");
        }
        else
        {
            Destroy(Instantiate(effects.transformParticles, transform.position, transform.rotation), 3);
            ballForm.SetActive(true);
            platformingForm.SetActive(false);

            rb.constraints = 0;
            rb.useGravity = true;
            state = PlayerForm.Ball;

            controller.enabled = false;
            platformMovment.enabled = false;
            ballMovement.enabled = true;
            ForwardPush();
        }
    }
    public float GetHeathStatus()
    {
        return currentHealth / maxHealth;
    }
    public void RestoreHealth(float val)
    {
        currentHealth = Mathf.Min(maxHealth, currentHealth + val);
    }
    public void TakeDamage(float val)
    {
        currentHealth = Mathf.Max(0, currentHealth - val);
        if (currentHealth == 0)
        {
            receiveInput = false;
            Vector3 moveDirection = Vector3.zero;
            controller.moveDirection = moveDirection;
            ballMovement.moveDirection = moveDirection;
            if (activeCheckpoint)
            {
                // need to add faint anumation and delay before respawn;
                StartCoroutine(Respawn());
            }
        }
    }
    private IEnumerator Respawn()
    {
        float time = 0;
        while (time < 1)
        {
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = activeCheckpoint.transform.position;
        currentHealth = maxHealth;
        receiveInput = true;
    }
    public void SetCheckpoint(Checkpoint newCheckpoint)
    {
        if (activeCheckpoint)
            activeCheckpoint.Deactivate();

        activeCheckpoint = newCheckpoint;
        activeCheckpoint.Activate();
    }
    public void PlayJumpSound()
    {
        EffectsManager.instance.MakeSound3d(transform.position, effects.soundJump);
    }
    public void PlayLandingSound()
    {
        EffectsManager.instance.MakeSound3d(transform.position, effects.soundLanding);
    }

    internal void ForwardPush()
    {
        AddForce(transform.forward * forwardForce);
    }
}