using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // > external scripts
    DialogueManager dManager;
    // <

    // > data
    Data playerData;
    Skill[] playerSkills;
    public Skill Lunge;
    public Skill Quake;
    // <

    // > key input/press
    [HideInInspector] public bool rightPressed = false;
    [HideInInspector] public bool leftPressed = false;
    [HideInInspector] public bool jumpPressed = false;
    [HideInInspector] public bool mouseClickLeft = false;
    [HideInInspector] public bool lungeSelected = false;
    [HideInInspector] public bool quakeSelected = false;
    [HideInInspector] public bool lungeActive = false;
    [HideInInspector] public bool quakeActive = false;
    // <

    [HideInInspector] public bool hasContact = false;

    // > animation parameters
    [HideInInspector] public bool isGrounded = true;
    [HideInInspector] public bool isRunning = false;
    [HideInInspector] public bool isJumping = false;
    [HideInInspector] public bool isFalling = false;
    [HideInInspector] public bool isLanding = false;
    [HideInInspector] public bool isCrouching = false;
    [HideInInspector] public bool isAttacking = false;
    [HideInInspector] public bool isHit = false;
    // <

    // > player velocity
    public float runSpeed = 12.5f;
    public float jumpSpeed = 115.0f;

    [HideInInspector] public float maxVelocity = 7.5f;

    // > knockback distance from enemy attack
    [HideInInspector] public float knockbackDist = 0;
    // <
    // > force of knockback / how hard it hits
    [HideInInspector] public float knockbackForce = 6.5f;
    // <

    [HideInInspector] public PlayerJump jumpPhase;
    [HideInInspector] public PlayerRun runPhase;
    // <

    // > player components
    GameObject player;
    SpriteRenderer p_Sprite;
    Animator p_Animator;
    // >> colliders
    GameObject hitCollider;
    GameObject topCollider;
    Vector3 hitColliderPos;
    Vector3 topColliderPos;
    // <<
    // <


    // Start is called before the first frame update
    void Start()
    {
        player = this.gameObject;
        p_Sprite = player.GetComponent<SpriteRenderer>();
        p_Animator = player.GetComponent<Animator>();
        dManager = GameObject.Find("SceneManager").GetComponent<DialogueManager>();

        hitCollider = GameObject.Find("hitCollider");
        topCollider = GameObject.Find("topCollider");

        playerData = DataManager.playerData;
        playerSkills = playerData.skills;

        foreach (Skill skill in playerData.skills)
        {
            if (skill.name == "Lunge")
            {
                Lunge = skill;
            }
            else if (skill.name == "Quake")
            {
                Quake = skill;
            }

        }

    }

    // Update is called once per frame
    void Update()
    {
        if (dManager != null && dManager.sceneIsPlaying) return;

        hitColliderPos = hitCollider.transform.localPosition;
        topColliderPos = topCollider.transform.localPosition;

        if (Input.GetKey(KeyCode.D))
        {
            if (isAttacking)
            {
                isRunning = false;
                return;
            }

            rightPressed = !CameraFollow.screenIsScrolling_X;
            isRunning = !CameraFollow.screenIsScrolling_X;
        }
        else
        {
            rightPressed = false;
        }
        
        if (Input.GetKey(KeyCode.A))
        {
            if (isAttacking)
            {
                isRunning = false;
                return;
            }

            leftPressed = !CameraFollow.screenIsScrolling_X;
            isRunning = !CameraFollow.screenIsScrolling_X;
        }
        else
        {
            leftPressed = false;
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpPressed = !CameraFollow.screenIsScrolling_Y;
            isJumping = !CameraFollow.screenIsScrolling_Y;
            jumpPhase = jumpPhase == PlayerJump.Grounded ? PlayerJump.Ascend : jumpPhase;

        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (Lunge.isAccessible)
            {
                Lunge.isSelected = !Lunge.isSelected;
            }

            lungeSelected = Lunge.isSelected;

            if (lungeSelected)
            {
                Quake.isSelected = false;
                quakeSelected = false;
                // skill 3
                // skill 3
                // skill 4
                // skill 4
            }

            HUDManager.UpdateHUD(HUD.SkillsUI, 1);
            playerSkills = new Skill[] { Lunge, Quake };
            // playerData = new Data(playerData.stats, playerSkills, playerData.cPoint, playerData.cutscene);
            playerData.skills = playerSkills;

            // Debug.Log(playerData.skills[0].isSelected + "-- from controller");
            Debug.Log(lungeSelected ? $"{Lunge.name} is selected" : $"{Lunge.name} is not selected");

            DataManager.Save(CharType.Player, playerData);

        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            
            if (Quake.isAccessible)
            {
                Quake.isSelected = !Quake.isSelected;
            }

            quakeSelected = Quake.isSelected;

            if (quakeSelected)
            {
                Lunge.isSelected = false;
                lungeSelected = false;
                // skill 3
                // skill 3
                // skill 4
                // skill 4
            }

            HUDManager.UpdateHUD(HUD.SkillsUI, 2);
            playerSkills = new Skill[] { Lunge, Quake };
            playerData = new Data(playerData.stats, playerSkills, playerData.cPoint, playerData.cutscene);

            Debug.Log(quakeSelected ? $"{Quake.name} is selected" : $"{Quake.name} is not selected");

            DataManager.Save(CharType.Player, playerData);

        }

        if (Input.GetKeyDown(KeyCode.T))
        {

            lungeActive = lungeSelected;
            quakeActive = quakeSelected;

        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            mouseClickLeft = true;
        }

        if (isGrounded)
        {
            p_Animator.SetBool("isGrounded", isGrounded);
        }


        if (Input.GetKeyDown(KeyCode.N))
        {
            //PlayerStatus.ApplyDamage(20);
            //PlayerStatus.DeductMP(10);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.otherCollider.name == "bottomCollider")
        {
            if (collision.collider.tag == "Ground" || collision.collider.tag == "Platform")
            {
                // Debug.Log("Entered");
                isGrounded = true;
                jumpPhase = jumpPhase != PlayerJump.Grounded ? PlayerJump.Landing : jumpPhase;

            }
        }
        else
        {
            //
        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.otherCollider.name == "bottomCollider")
        {
            if (collision.collider.tag == "Ground" || collision.collider.tag == "Platform")
            {
                // Debug.Log("Exited");
                isGrounded = false;
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Attack")
        {
            Debug.Log("Hit");
            hasContact = true;

            switch(collider.transform.parent.name)
            {
                case "Skeleton":

                    PlayerStatus.ApplyDamage(Combat.CalculateDamage(playerData.stats.DEF, DataManager.skeletonData.stats.ATK));

                    break;

                default:
                    break;
            }

            knockbackDist = collider.transform.parent.position.x > this.transform.position.x ? -2: 2;

        }
    }

    public void Move(Rigidbody2D playerBody, Direction direction)
    {

        if (isAttacking)
        {
            isRunning = false;
            return;
        }

        switch (direction)
        {
            case Direction.Right:

                if (playerBody.velocity.x < 0.5f)
                {
                    p_Sprite.flipX = false;

                    if (hitColliderPos.x < 0)
                    {
                        hitColliderPos = new Vector3(hitColliderPos.x * -1, hitColliderPos.y, hitColliderPos.z);
                        hitCollider.transform.localPosition = hitColliderPos;
                    }
                }
                if (playerBody.velocity.x < maxVelocity)
                {
                    playerBody.velocity += new Vector2(runSpeed * Time.deltaTime, 0);
                }

                runPhase = PlayerRun.Right;

                break;
            case Direction.Left:
                if (playerBody.velocity.x > -0.5f)
                {
                    p_Sprite.flipX = true;

                    if (hitColliderPos.x > 0)
                    {
                        hitColliderPos = new Vector3(hitColliderPos.x * -1, hitColliderPos.y, hitColliderPos.z);
                        hitCollider.transform.localPosition = hitColliderPos;
                    }
                }
                if (playerBody.velocity.x > -7.5f)
                {
                    playerBody.velocity -= new Vector2(runSpeed * Time.deltaTime, 0);
                }

                runPhase = PlayerRun.Left;

                break;
            case Direction.Up:
                if (playerBody.velocity.y < maxVelocity)
                {
                    playerBody.velocity += new Vector2(0, jumpSpeed * Time.deltaTime);
                    jumpPhase = PlayerJump.Ascend;
                }
                else
                {
                    jumpPhase = PlayerJump.MaxHeight;
                    isJumping = false;
                    isFalling = true;
                }
                break;
            case Direction.Down:
                // crouch
                break;
            default:
                Debug.LogWarning("Uh-oh.");
                break;
        }
    }

    public void Stop (Rigidbody2D playerBody, Direction direction)
    {
        if (direction == Direction.Left || direction == Direction.Right || direction == Direction.Idle)
        {
            playerBody.velocity = new Vector2(0, playerBody.velocity.y);
            isRunning = false;
            runPhase = PlayerRun.Idle;
        }
        else if (direction == Direction.Up)
        {
            
            if (jumpPhase == PlayerJump.Ascend)
            {
                isJumping = false;
            }
            else if (jumpPhase == PlayerJump.Landing)
            {
                isFalling = false;
            }

            if (jumpPhase != PlayerJump.MaxHeight)
            {
                playerBody.velocity = new Vector2(playerBody.velocity.x, 0);
                isLanding = jumpPhase == PlayerJump.Landing;
            }

            jumpPhase = !isGrounded ? PlayerJump.Suspended : PlayerJump.Landing;

        }
    }
    public IEnumerator Animate (Movement movementType)
    {
        switch (movementType) // **SET ALL BOOLS TO REF VAR?** \\
        {

            case Movement.Run:
                p_Animator.SetBool("isRunning", isRunning);
                yield return new WaitWhile(() => isRunning);
                p_Animator.SetBool("isRunning", isRunning);

                break;

            case Movement.Jump:

                p_Animator.SetBool("isGrounded", isGrounded);
                p_Animator.SetBool("isJumping", isJumping);
                yield return new WaitWhile(() => isJumping);
                p_Animator.SetBool("isGrounded", isGrounded);
                p_Animator.SetBool("isJumping", isJumping);

                break;

            case Movement.Falling:

                p_Animator.SetBool("isGrounded", isGrounded);
                p_Animator.SetBool("isFalling", isFalling);
                yield return new WaitWhile(() => isFalling);
                p_Animator.SetBool("isGrounded", isGrounded);
                p_Animator.SetBool("isFalling", isFalling);

                break;

            case Movement.Landing:

                p_Animator.SetBool("isLanding", isLanding);
                yield return new WaitForSeconds(0.225f);
                isLanding = false;
                p_Animator.SetBool("isLanding", isLanding);

                jumpPressed = false;
                jumpPhase = PlayerJump.Grounded;

                break;

            case Movement.Attack:

                mouseClickLeft = false;
                isAttacking = true;
                p_Animator.SetBool("isAttacking", isAttacking);
                yield return new WaitForSeconds(0.6f);
                isAttacking = false;
                p_Animator.SetBool("isAttacking", isAttacking);

                break;

            case Movement.Hit:

                hasContact = false;
                isHit = true;
                p_Animator.SetBool("isHit", isHit);
                yield return new WaitForSeconds(0.25f);
                isHit = false;
                p_Animator.SetBool("isHit", isHit);

                break;

            case Movement.Death:

                //

                break;

            default:
                break;
        }

        yield return null;
    }

    public void UseSkill(string activeSkill)
    {
        switch (activeSkill)
        {
            case "Lunge":

                // if running stop
                // animate attack
                // move player forward (lunge)

                break;

            case "Quake":

                // if moving stop
                // animate crouch/quake motion
                // shake screen

                break;

            default:
                break;

        }

    }
}
