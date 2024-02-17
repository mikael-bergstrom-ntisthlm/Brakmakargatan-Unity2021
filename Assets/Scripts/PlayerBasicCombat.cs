using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerBasicCombat : MonoBehaviour {

    [SerializeField]
    Animator animator;

    [SerializeField]
    float delayBetweenMoves = 0.5f;

    private PlayerController playerController;

    private string punchAxis;
    private string kickAxis;

    private bool hasReleasedPunchButton = true;
    private bool hasReleasedKickButton = true;

    private float punchTimer = 0;
    private float kickTimer = 0;


    void Start()
    {
        playerController = GetComponent<PlayerController>();

        punchAxis = playerController.PunchAxis;
        kickAxis = playerController.KickAxis;
    }

	void Update () {

        float punchAxisValue = Input.GetAxisRaw(punchAxis);
        float kickAxisValue = Input.GetAxisRaw(kickAxis);

        CheckPunch(punchAxisValue);
        CheckKick(kickAxisValue);
	}

    void CheckPunch(float punchAxisValue)
    {
        if (punchTimer > 0)
        {
            punchTimer -= Time.deltaTime;
        }

        if (punchAxisValue > 0 && hasReleasedPunchButton && punchTimer <= 0)
        {
            animator.SetTrigger("punch");
            hasReleasedPunchButton = false;
            punchTimer = delayBetweenMoves;
        }
        else if (punchAxisValue <= 0)
        {
            hasReleasedPunchButton = true;
        }
    }

    void CheckKick(float kickAxisValue)
    {
        if (kickTimer > 0)
        {
            kickTimer -= Time.deltaTime;
        }

        if (kickAxisValue > 0 && hasReleasedKickButton && kickTimer <= 0)
        {
            animator.SetTrigger("kick");
            hasReleasedKickButton = false;
            kickTimer = delayBetweenMoves;
            print(playerController.controlPrefix + " kick!");
        }
        else if (kickAxisValue <= 0)
        {
            hasReleasedKickButton = true;
        }

    }

}
