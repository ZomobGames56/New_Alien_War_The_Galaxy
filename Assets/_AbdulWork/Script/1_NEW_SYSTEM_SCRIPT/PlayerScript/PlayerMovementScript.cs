using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Accessibility;
using UnityEngine.UI;
public class PlayerMovementScirpt : MonoBehaviour
{
    public event EventHandler gameOverEvent;
    public event EventHandler playerDyingAnimation;
    public event Action<PlayerState> playerStateChanged;
    public enum PlayerState
    {
        Moving, 
        Grabbed, 
        Freed
    }
    private Rigidbody rb;
    private GameObject alienEnemy;
    private float camtilt = 200;
    [SerializeField] private PlayerState state;
    [SerializeField] private DynamicJoystick d_joystick;
    [SerializeField] private float forwardSpeed, sideSpeed;
    [SerializeField] private LevelTaskManager levelTaskManager;
    [SerializeField] private PlayerWeaponScript playerWeaponScript;
    [SerializeField] private GameObject FireButton , grabbedTriggerObject , movementTriggerObject, meleeButton;
    // Start is called before the first frame update
    void Awake()
    {
        Application.targetFrameRate = 144;
        state = PlayerState.Moving;
        rb = GetComponent<Rigidbody>();
        meleeButton.SetActive(false);
        FireButton.SetActive(true);
        grabbedTriggerObject.SetActive(true);
    }
    private void OnEnable()
    {
        playerWeaponScript.knifeAttack += PlayerWeaponScript_knifeAttack;
        playerWeaponScript.FreedPlayer += PlayerWeaponScript_FreedPlayer;
    }
    private void OnDisable()
    {
        playerWeaponScript.knifeAttack -= PlayerWeaponScript_knifeAttack;
        playerWeaponScript.FreedPlayer -= PlayerWeaponScript_FreedPlayer;
    }
    private void PlayerWeaponScript_FreedPlayer(object sender, EventArgs e)
    {
        GrabbedToFree();
    }
    private void PlayerWeaponScript_knifeAttack(object sender, EventArgs e)
    {
        HitAlien();
    }
    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case PlayerState.Moving:
                {
                    Movement();
                    break;
                }
            case PlayerState.Grabbed:
                {
                    //Grabbed();
                    break;
                }
            case PlayerState.Freed:
                {
                    //Freed();
                    break;
                }
        }
    }
    private void Movement()
    {
        float Horizontal;
        if(d_joystick.Horizontal == 0)
        {
            Horizontal = Input.GetAxis("Horizontal");
        }
        else
            Horizontal = d_joystick.Horizontal;
        rb.MovePosition(transform.position + transform.forward * forwardSpeed * Time.deltaTime + transform.right * sideSpeed * Time.deltaTime * Horizontal);
        Camera.main.gameObject.transform.rotation = Quaternion.Euler(0, 0, Mathf.Lerp(Camera.main.gameObject.transform.rotation.z
    , Mathf.Clamp(Horizontal, -0.8f, 0.8f) * camtilt
    , Time.fixedDeltaTime * 1f));
    }
    public void HitAlien()
    {
        alienEnemy.GetComponent<AlienClass>().KnifeHit();
    }
    public void FreeToGrabbed(GameObject enemy)
    {
        alienEnemy  = enemy;
        this.transform.LookAt(alienEnemy.transform.position);
        alienEnemy.transform.LookAt(transform.position);
        alienEnemy.transform.position = transform.position + transform.forward * 1.2f + transform.right * (-.2f);
        state = PlayerState.Grabbed;
        playerStateChanged?.Invoke(state);
        FireButton.SetActive(false);
        meleeButton.SetActive(true);
        grabbedTriggerObject.SetActive(false);
        movementTriggerObject.SetActive(false);
        playerWeaponScript.SetKnife();
        meleeButton.GetComponent<Button>().onClick.AddListener(KnifeAttack);
        alienEnemy.GetComponent<AlienClass>().PlayerKilled += PlayerMovementScirpt_PlayerKilled;
        alienEnemy.GetComponent<AlienClass>().PlayerDying += PlayerMovementScirpt_PlayerDying;
    }
    private void KnifeAttack()
    {
        meleeButton.GetComponent<Button>().onClick.RemoveListener(KnifeAttack);
        meleeButton.SetActive(false);
        playerWeaponScript.AttackKnife();
    }
    private void PlayerMovementScirpt_PlayerKilled(object sender, EventArgs e)
    {
        gameOverEvent?.Invoke(this, EventArgs.Empty);
    }
    private void PlayerMovementScirpt_PlayerDying(object sender, EventArgs e)
    {
        PlayerDying();
    }
    private void PlayerDying()
    {
        playerDyingAnimation?.Invoke(this, EventArgs.Empty);
    }
    public void GrabbedToFree()
    {
        this.transform.rotation = Quaternion.identity;
        playerWeaponScript.SetMainWeaponFromKnife();
        state = PlayerState.Moving;
        playerStateChanged?.Invoke(state);
        FireButton.SetActive(true);
        meleeButton.SetActive(false);
        StartCoroutine(ExecuteAfterTime(.1f));
        alienEnemy.GetComponent<AlienClass>().PlayerKilled -= PlayerMovementScirpt_PlayerKilled;
        alienEnemy.GetComponent<AlienClass>().PlayerDying -= PlayerMovementScirpt_PlayerDying;
    }
    IEnumerator ExecuteAfterTime(float time)
    {
        // Wait for the given time
        yield return new WaitForSeconds(time);
        
        grabbedTriggerObject.SetActive(true);
        movementTriggerObject.SetActive(true);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Object")
        {
            levelTaskManager.PickObject();
        }
    }
    public PlayerState GetCurrentState()
    {
        return state;
    }
}