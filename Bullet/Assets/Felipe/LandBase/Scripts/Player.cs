using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour
{

    public bool stepingOnShop;
    public GameObject ShopCanvas;
    public GameObject ShopPowerUp1;
    public GameObject ShopPowerUp2;
    public GameObject ShopPowerUp3;
    public GameObject ShopPowerUp4;
    public GameObject ShopPowerUp5;

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

    Controller2D controller;

    void Start()
    {
        controller = GetComponent<Controller2D>();

        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);
        print("Gravity: " + gravity + "  Jump Velocity: " + maxJumpVelocity);

        ShopCanvas.SetActive(false);
    }

    void Update()
    {
        stepingOnShop = ShopCanvas;
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


        if (controller.collisions.Onshop)
        {
            if (Input.GetKeyUp(KeyCode.Return))
            {
                if (!ShopCanvas.activeInHierarchy)
                {
                    ShopCanvas.SetActive(true);
                    Cursor.visible = true;
                    ShopPowerUp1.GetComponent<PowerUp>().Start();
                    ShopPowerUp2.GetComponent<PowerUp>().Start();
                    ShopPowerUp3.GetComponent<PowerUp>().Start();
                    ShopPowerUp4.GetComponent<PowerUp>().Start();
                    ShopPowerUp5.GetComponent<PowerUp>().Start();
                }
                else
                {
                    //Ask for script to hide it self
                    ShopCanvas.GetComponent<Canvas>().CloseCanvas = true;
             
                    //ShopCanvas.SetActive(false);
                    // Cursor.visible = false;

                }
            }
        }
        else if (ShopCanvas.activeInHierarchy) {
            //Ask for script to hide it self
            ShopCanvas.GetComponent<Canvas>().CloseCanvas = true;
            //ShopCanvas.SetActive(false);
            //Cursor.visible = false;
        }

    }
}