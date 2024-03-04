using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] private GameObject slashAnimPrefab;
    [SerializeField] private Transform slashAnimationPoint;
    [SerializeField] private Transform weaponCollider;

    private PlayerControls playControls;
    private Animator animator;
    private PlayerController playerController;
    private ActiveWeapon activeWeapon;

    //slash anim instantinate when attack
    private GameObject slashAnim;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        playControls = new PlayerControls();
        activeWeapon = GetComponentInParent<ActiveWeapon>();
        playerController = GetComponentInParent<PlayerController>();
    }
    private void OnEnable()
    {
        playControls.Enable();
    }

    private void Start()
    {
        playControls.Combat.Attack.started += _ => Attack();
    }

    // Update is called once per frame
    void Update()
    {
        MouseFollowWithOffSet();

    }

    private void Attack()
    {
        animator.SetTrigger("Attack");
        slashAnim = Instantiate(slashAnimPrefab, slashAnimationPoint.position, Quaternion.identity);
        weaponCollider.gameObject.SetActive(true);

    }
    public void DoneAttackingAnimEvent()
    {
        weaponCollider.gameObject.SetActive(false);
    }
    //handle when animation of swing up
    public void SwingUpFlipAnim()
    {
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);

        if (playerController.FacingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public void SwingDownFlipAnim()
    {
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

        if (playerController.FacingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    private void MouseFollowWithOffSet()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playScreenPoint = Camera.main.WorldToScreenPoint(playerController.transform.position);
        float angle = Mathf.Atan2(mousePos.x, mousePos.y) * Mathf.Rad2Deg;
        if (mousePos.x < playScreenPoint.x)
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0, -180, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0, 0, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
