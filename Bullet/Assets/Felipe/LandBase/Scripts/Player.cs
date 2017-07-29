using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour
{
    public bool ReadyToLeave;
    public bool lostPlayer;
    public bool lostPlayer2;
    public bool stepingOnShop;
    public GameObject ShopCanvas;
    public GameObject PlayerObj;


    public float maxJumpHeight = 4;
    public float minJumpHeight = 1;
    public float timeToJumpApex = .4f;
    float accelerationTimeAirborne = .2f;
    float accelerationTimeGrounded = .1f;
    float moveSpeed = 6;

    public Vector2 wallJumpClimb;
    public Vector2 wallJumpOff;
    public Vector2 wallLeap;

    public float wallSlideSpeedMax = 3;
    public float wallStickTime = .25f;
    float timeToWallUnstick;

    float gravity;
    float maxJumpVelocity;
    float minJumpVelocity;
    Vector3 velocity;
    float velocityXSmoothing;

    bool Helptimer;
    public bool notMoving ;
    public bool notJumping;
    int numberOfTimesMovePress;
    int numberOfTimesJumpPress;
    int numberOfYielTimes;
    public int timeyield;
    public int SetHelpTime;


    Controller2D controller;

    void Start()
    {
        lostPlayer2 = false;
        lostPlayer = false;

        controller = GetComponent<Controller2D>();

        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);

        ShopCanvas.SetActive(false);
    }

    void Update()
    {

        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        int wallDirX = (controller.collisions.left) ? -1 : 1;

        float targetVelocityX = input.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);

        bool wallSliding = false;
            if ((controller.collisions.left || controller.collisions.right) && !controller.collisions.below && velocity.y < 0)
        {
            wallSliding = true;

            if (velocity.y < -wallSlideSpeedMax)
            {
                velocity.y = -wallSlideSpeedMax;
            }

            if (timeToWallUnstick > 0)
            {
                velocityXSmoothing = 0;
                velocity.x = 0;

                if (input.x != wallDirX && input.x != 0)
                {
                    timeToWallUnstick -= Time.deltaTime;
                }
                else
                {
                    timeToWallUnstick = wallStickTime;
                }
            }
            else
            {
                timeToWallUnstick = wallStickTime;
            }

        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (wallSliding)
            {
                if (wallDirX == input.x)
                {
                    velocity.x = -wallDirX * wallJumpClimb.x;
                    velocity.y = wallJumpClimb.y;
                }
                else if (input.x == 0)
                {
                    velocity.x = -wallDirX * wallJumpOff.x;
                    velocity.y = wallJumpOff.y;
                }
                else
                {
                    velocity.x = -wallDirX * wallLeap.x;
                    velocity.y = wallLeap.y;
                }
            }
            if (controller.collisions.below)
            {
                velocity.y = maxJumpVelocity;
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (velocity.y > minJumpVelocity)
            {
                velocity.y = minJumpVelocity;
            }
        }


        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime, input);

        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
        }

            if (controller.collisions.ReadyToLeave) {
                if (Input.GetKeyUp(KeyCode.Return))
                {
                    SceneManager.LoadScene("_main");
                }

            }
        if (controller.collisions.Onshop)
        {
            stepingOnShop = true;

            if (Input.GetKeyUp(KeyCode.Return))
            {
                if (!ShopCanvas.activeInHierarchy)
                {
                    ShopCanvas.SetActive(true);
                    Cursor.visible = true;
                    ShopCanvas.GetComponent<Canvas>().Start();
                }
                else
                {
                    //Ask for script to hide it self
                    ShopCanvas.GetComponent<Canvas>().CloseCanvasFunction();

                    //ShopCanvas.SetActive(false);
                    // Cursor.visible = false;

                }
            }
        } 
        else if (ShopCanvas.activeInHierarchy) {
            //Ask for script to hide it self
            ShopCanvas.GetComponent<Canvas>().CloseCanvasFunction();
            //ShopCanvas.SetActive(false);
            //Cursor.visible = false;
        }
        else
            stepingOnShop = false;

        if (controller.collisions.NeedHelp)
        {
            lostPlayer = true;
        }
        else
            lostPlayer = false;
        if (controller.collisions.ReadyToLeave)
        {
            ReadyToLeave = true;
        }
        else
    
            ReadyToLeave = false;
    }

}