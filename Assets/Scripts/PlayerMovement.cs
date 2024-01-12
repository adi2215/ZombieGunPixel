using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using System;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public event EventHandler<OnShootEventsArgs> OnShoot;
    public class OnShootEventsArgs : EventArgs {
        public Vector3 gunEndPointPosition;
        public Vector3 shootPosition;
    }

    public event EventHandler<OnShootEventsArg> OnShootGranate;
    public class OnShootEventsArg : EventArgs {
        public Vector3 PlayerEndPointPosition;
        public Vector3 shootPosition;
    }

    public Data data;

    //MovementSystem
    private float horizontalValue;
    private float verticalValue;
    public float moveSpeed = 1f;
    [HideInInspector] public float movementSpeed;
    private int facingDirect = 1;

    //DashSystem
    private float dashTimeLeft;
    private float lastDash = -100f;
    public float dashTiem;
    public float dashSpeed;
    public float dashCoolDown;
    private Vector2 dashDirection;
    public AnimationCurve curve;

    //ShootingSystem
    [HideInInspector] public Vector2 shooTing;
    public Vector2 movementInput;
    public float timebetTime = 0.25f, timebetTimeGranate = 1.2f;
    private float timebet, timebetGranate;
    public bool ActualReload = false;
    [SerializeField] private int weaponPatrons = 9;
    private Vector2 LocalshooTing;
    [SerializeField] private Transform[] aimTransform;
    [SerializeField] private Transform[] aimGunEndPosintTransform;
    [SerializeField] private GameObject[] gunLight;
    [SerializeField] private Transform aimGranatePos;
    public int currentGun;
    public int patrons;


    //LogicalCheck
    private bool facingRight = false;
    public bool isDashing;
    [HideInInspector] public bool canMove;

    //Objects
    public GameObject Aim;
    public PlayerAfterImage ghostEffect;
    public GameObject Dusteffect;
    private Rigidbody2D rb;
    public InventoryManager inventoryManager;
    public Weapons weapon;
    public Text countGranates;
    [SerializeField] private AudioSource shootSound;
    [SerializeField] private AudioSource Reload;
    [SerializeField] private AudioSource Granade;
    public Text counCoints;

    //Animators
    private Animator animator;
    public Animator cam;
    public Animator[] aimAnimator;
    public Animator RealodAnim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        canMove = true;
        timebet = timebetTime;
        timebetGranate = timebetTimeGranate;
        currentGun = 0;
        patrons = 9;
        weapon.SortGun(currentGun);
    }

    public void SpeedInc() 
    {
        if (data.countCoins < 20)
            return;

        moveSpeed++;
        data.countCoins -= 20;
        counCoints.text = data.countCoins.ToString();
    }

    void Update()
    {
        if (data.YoucantShoot != true)
        {
            CheckInput();
            CheckDash();
            Shooting();
        }

        if (data.itemChachedl)
        {
            ItemChanged();
            data.itemChachedl = false;
        }
    }

    void FixedUpdate()
    {
        if (!canMove)
            return;

        ProcessInputs();

        AimWork();

        TurnPlayer();

        AnimationPlayer();
    }


    //Animation---------------------------------------------------------------------
    public void Die() => animator.SetTrigger("Die");

    private void AnimationPlayer()
    {
        if (movementInput != Vector2.zero)
        {
            animator.SetBool("Moving", true);
        }
        else
        {
            animator.SetBool("Moving", false);
        }
    }
    //---------------------------------------------------------------------


    //Dashing---------------------------------------------------------------------
    private void CheckInput()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (Time.time >= (lastDash + dashCoolDown))
                AttemptToDash();
        }
    }

    private void AttemptToDash()
    {
        Physics2D.IgnoreLayerCollision(6, 7, true);
        ghostEffect.makeGhost = true;
        cam.SetBool("DashSystem", true);
        Instantiate(Dusteffect, transform.position, Quaternion.identity);
        isDashing = true;
        dashTimeLeft = dashTiem;
        lastDash = Time.time;
    }

    private void CheckDash()
    {
        if (isDashing)
        {
            if (dashTimeLeft > 0)
            {
                if (movementInput == Vector2.zero)
                    dashDirection = LocalshooTing;
                else 
                    dashDirection = movementInput;
                canMove = false;
                rb.MovePosition(rb.position + dashDirection * dashSpeed * curve.Evaluate(Mathf.Cos((dashTiem - dashTimeLeft) * Mathf.PI/2)));
                dashTimeLeft -= Time.deltaTime;
            }

            if (dashTimeLeft <= 0)
            {
                rb.velocity = Vector2.zero;
                canMove = true;
                isDashing = false;
                ghostEffect.makeGhost = false;
                Physics2D.IgnoreLayerCollision(6, 7, false);
                Invoke(nameof(CamShake), 0.1f);
            }
        }
    }
    private void CamShake() => cam.SetBool("DashSystem", false);
    //---------------------------------------------------------------------



    //Movement---------------------------------------------------------------------
    private void TurnPlayer()
    {
        if (shooTing.x < transform.position.x && !facingRight)
        {
            Flip();
            facingDirect = -1;
        }
        else if (shooTing.x > transform.position.x && facingRight)
        {
            Flip();
            facingDirect = 1;
        }
    }
 
    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    void ProcessInputs()
    {
        horizontalValue = Input.GetAxisRaw("Horizontal");
        verticalValue = Input.GetAxisRaw("Vertical");
        movementInput = new Vector2(horizontalValue, verticalValue);
        movementSpeed = Mathf.Clamp(movementInput.magnitude, 0.0f, 1.0f);

        movementInput.Normalize();
        if (movementInput != Vector2.zero)
        {
            rb.MovePosition(rb.position + movementInput * moveSpeed * movementSpeed * Time.fixedDeltaTime);
            //data.moveIdle = true;
            return;
        }
        //data.moveIdle = false;
    }
    //---------------------------------------------------------------------



    //Aim and Shooting---------------------------------------------------------------------
    private void Shooting()
    {
        timebet -= Time.deltaTime;
        timebetGranate -= Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && timebet < 0 && weaponPatrons > 0)
        {
            timebet = timebetTime;
            Vector3 mousePosition = shooTing;

            aimAnimator[currentGun].SetTrigger("ShootPistol");
            cam.SetBool("ShootCam", true);
            weaponPatrons--;
            shootSound.Play();
            gunLight[currentGun].SetActive(true);
            
            OnShoot?.Invoke(this, new OnShootEventsArgs {
                gunEndPointPosition = aimGunEndPosintTransform[currentGun].position, 
                shootPosition = mousePosition,
            });
        }

        if (Input.GetMouseButtonDown(2) && data.GranateCount > 0 && timebetGranate < 0)
        {
            timebetGranate = timebetTimeGranate;
            Vector3 mousePosition = shooTing;

            data.GranateCount--;
            countGranates.text = data.GranateCount.ToString();
            GranadeExplosion();

            OnShootGranate?.Invoke(this, new OnShootEventsArg {
                PlayerEndPointPosition = aimGranatePos.position,
                shootPosition = mousePosition,
            });
        }

        if (weaponPatrons <= 0 && Input.GetMouseButtonDown(0) && !ActualReload)
        {
            RealodAnim.SetTrigger("Patrons");
            ActualReload = true;
            Reload.Play();
        }

        if (timebet < 0)
        {
            cam.SetBool("ShootCam", false);
            gunLight[currentGun].SetActive(false);
        }
    }

    private void GranadeExplosion() => Granade.Play();

    public void NewPatrons() 
    {
        weaponPatrons = patrons;
        ActualReload = false;
    }

    void AimWork() 
    { 
        shooTing = Aim.transform.position;

        LocalshooTing = Aim.transform.localPosition;
        LocalshooTing.Normalize();
        LocalshooTing = new Vector2(facingDirect * LocalshooTing.x, LocalshooTing.y);
    }

    public void ItemChanged()
    {
        Gun receivedItem = inventoryManager.GetSelectedItem();
        if (receivedItem != null)
        {
            Debug.Log(receivedItem);
            switch(receivedItem.name)
            {
                case "Pistol":
                    currentGun = 0;
                    timebet = 0.25f;
                    timebetTime = 0.25f;
                    weaponPatrons = 9;
                    patrons = 9;
                    data.damage = 1;
                    data.destroyBullet = 0.4f;
                    break;
                case "AK":
                    currentGun = 1;
                    timebet = 0.07f;
                    timebetTime = 0.07f;
                    weaponPatrons = 30;
                    patrons = 30;
                    data.damage = 2;
                    data.destroyBullet = 0.7f;
                    break;
                case "MP":
                    currentGun = 2;
                    break;
            }

            weapon.SortGun(currentGun);
        }
        else
        {
            Debug.Log("Nothing");
        }
    }

    //---------------------------------------------------------------------

}
