using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent (typeof (Rigidbody))]
public class PlayerBehaviour : MonoBehaviour
{
    [System.Serializable]
    public class MoveSettings
    {
        public float runVelocity = 4;
        public float rotateVelocity = 100;
        public float jumpVelocity = 3;
        public float distanceToGround = 1.3f;
        public LayerMask ground;
    }

    [System.Serializable]
    public class InputSettings
    {
        public string FORWARD_AXIS = "Vertical";
        public string SIDEWAYS_AXIS = "Horizontal";
        public string TURN_AXIS = "Mouse X";
        public string JUMP_AXIS = "Jump";
    }

    [System.Serializable]
    public class Materials
    {
        public Material RED;
        public Material GREY;
        public Material BLUE;
    }

    public MoveSettings moveSettings;
    public InputSettings inputSettings;
    public Materials materials;

    public static int characterID;
    public SkinnedMeshRenderer characterRenderer;
    public GameObject characterModel;

    public static PlayerBehaviour PB;

    public GameObject shop;
    public static bool attributesSet = false;

    //Stats
    public static int agility;
    public static int strength;
    public int baseDoubleJumpMax = 0;
    public float baseRunVelocity = 4;
    public float baseMaxHealth = 20;
    public float baseProtection = 1;
    public float baseRegen = 0.25f;


    public float maxHealth;
    public float regen;
    public float protection;
    public static float health = 20;
    public int doubleJump;
    public int doubleJumpMax;
    public static int money;

    public Text moneyText;
    public Text healthText;
    public GameObject healthBar;
    public Animator modelAnimator;

    private Rigidbody playerRigidbody;
    private Vector3 velocity;
    private Quaternion targetRotation;
    private float forwardInput, sidewaysInput, turnInput, jumpInput;
    

    // Sets all the start values
    void Awake()
    {
        Cursor.visible = false;

        PB = this;

        velocity = Vector3.zero;
        forwardInput = sidewaysInput = turnInput = jumpInput = 0;
        targetRotation = transform.rotation;

        playerRigidbody = gameObject.GetComponent<Rigidbody>();

        refreshStats();

        if (characterID == 1 && !attributesSet)
        {
            characterRenderer.material = materials.GREY;
            //%50 slow
            moveSettings.runVelocity = baseRunVelocity = 2.5f;
            maxHealth = baseMaxHealth = 40;
            health = maxHealth;
            regen = baseRegen = 0.5f;
            protection = baseProtection = 0.66f;
        }

        else if (characterID == 2 && !attributesSet)
        {
            characterRenderer.material = materials.RED;
            maxHealth = baseMaxHealth = 40;
            health = maxHealth;
            regen = baseRegen = 1.25f;
            protection = baseProtection = 1f;
        }

        else if (characterID == 3 && !attributesSet)
        {
            characterRenderer.material = materials.BLUE;
            moveSettings.runVelocity += 3;
            doubleJumpMax = baseDoubleJumpMax = 1;
            maxHealth = baseMaxHealth = 20;
            health = maxHealth;
            regen = baseRegen = 0.5f;
            protection = baseProtection = 1f;
        }

        refreshStats();

        health = maxHealth;

        UpdateStats();
    }

    // Called every frame
    void Update()
    {
        //Debug.DrawLine(transform.position, transform.position + Vector3.down * moveSettings.distanceToGround);

        GetInput();

        Turn();

        if (new Vector3(-forwardInput, 0.0f, sidewaysInput) != Vector3.zero)
            characterModel.transform.rotation = Quaternion.LookRotation(transform.TransformDirection(new Vector3(sidewaysInput, 0.0f, forwardInput)));

        //Shop Menu
        if (Input.GetKeyDown("b")){
            IncreaseStats s = shop.GetComponentInChildren<IncreaseStats>();
            s.updateStatText();
            shop.SetActive(!shop.activeSelf);
            Cursor.visible = shop.activeSelf;
        }
            

        //Regeneration
        if (health < maxHealth)
        {
            health += regen * Time.deltaTime;
            UpdateStats();
        }

        //Model and Animation
        bool walking = forwardInput > 0.1 || forwardInput < -0.1 || sidewaysInput > 0.1 || sidewaysInput < -0.1;

        modelAnimator.SetBool("IsWalking", walking);

        modelAnimator.SetBool("Grounded", Grounded());

        if (Grounded())
            doubleJump = doubleJumpMax;

        if(transform.position.y < -4.0f){
            Damage(5);
            transform.position = GameObject.Find("SpawnPoint").transform.position;
        }

        UpdateStats();

    }

    // Called in fixed timesteps (can be changed in the project settings)
    void LateUpdate()
    {

        Run();
        Jump();

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Collectable")
        {
            Destroy(other.gameObject);
            money++;
            UpdateStats();

        }
    }

    // Saves user input for later use
    void GetInput()
    {
        if (inputSettings.FORWARD_AXIS.Length != 0)
        {
            forwardInput = Input.GetAxis(inputSettings.FORWARD_AXIS);
        }
        if (inputSettings.SIDEWAYS_AXIS.Length != 0)
        {
            sidewaysInput = Input.GetAxis(inputSettings.SIDEWAYS_AXIS);
        }
        if (inputSettings.TURN_AXIS.Length != 0)
        {
            turnInput = Input.GetAxis(inputSettings.TURN_AXIS);
        }
        if (inputSettings.JUMP_AXIS.Length != 0)
        {
            jumpInput = Input.GetAxisRaw(inputSettings.JUMP_AXIS);
        }
    }

    void Run()
    {
        velocity.z = forwardInput * moveSettings.runVelocity;
        velocity.x = sidewaysInput * moveSettings.runVelocity;
        velocity.y = playerRigidbody.velocity.y;

        playerRigidbody.velocity = transform.TransformDirection(velocity);
    }

    void Turn()
    {
        if (Mathf.Abs(turnInput) > 0)
        {
            targetRotation *= Quaternion.AngleAxis(
                moveSettings.rotateVelocity * turnInput * Time.deltaTime, 
                Vector3.up);
        }
        transform.rotation = targetRotation;
    }

    void Jump()
    {

        if (Input.GetKeyDown("space") && (Grounded() || doubleJump > 0))
        {
            if(!Grounded())
            doubleJump--;

            playerRigidbody.velocity = new Vector3(
                playerRigidbody.velocity.x, 
                moveSettings.jumpVelocity, 
                playerRigidbody.velocity.z);
        }
    }

    bool Grounded()
    {

        return Physics.Raycast(
            transform.position,
            Vector3.down,
            moveSettings.distanceToGround,
            moveSettings.ground);
    }

    public void UpdateStats(){

        float percent = (health / maxHealth);

        moneyText.text = money.ToString();


        if (health > 0)
        {
            healthText.text = Mathf.RoundToInt(health).ToString() + "/" + maxHealth;
            healthBar.transform.localScale = new Vector3((health / maxHealth), 1, 1);
        }

        else
        {
            healthText.text = "Dead";
            healthBar.transform.localScale = new Vector3(1, 1, 1);
            healthBar.GetComponent<Image>().color = Color.red;
            Cursor.visible = true;
            Application.LoadLevel("YouLose");
        }


        
        
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy" && characterID == 1)
            health -= (1 * Time.deltaTime) / 3;

        else if (collision.gameObject.tag == "Enemy")
        {
            health -= 1 * Time.deltaTime;
        }


    }

    public static void Damage(float damage){

        health -= damage * PB.protection;

    }

    public void refreshStats(){
        moveSettings.runVelocity = baseRunVelocity + (agility * 0.2f);
        doubleJumpMax = baseDoubleJumpMax + (agility / 3);
        maxHealth = baseMaxHealth + strength * 5;
        regen = baseRegen + strength * 0.25f;
        protection = baseProtection - strength * 0.01f;
    }
    
}

