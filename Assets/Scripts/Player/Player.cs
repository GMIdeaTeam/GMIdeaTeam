using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [field: SerializeField]
    float MoveSpeed { get; set; } = 5f; // ModeController의 speed를 참조할 수 있도록 추후 변경

    Vector2 moveVector;

    Animator playerAnimator;

    public ModeController modeController;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
    }

    private void PlayerMove()
    {
        moveVector.x = Input.GetAxisRaw("Horizontal");
        moveVector.y = Input.GetAxisRaw("Vertical");

        if (moveVector.x != 0)
        {
            playerAnimator.SetBool("isMoveX", true);
        }
        else
        {
            playerAnimator.SetBool("isMoveX", false);
        }

        if (moveVector.y != 0)
        {
            playerAnimator.SetBool("isMoveY", true);
        }
        else
        {
            playerAnimator.SetBool("isMoveY", false);
        }

        playerAnimator.SetFloat("inputX", moveVector.x);
        playerAnimator.SetFloat("inputY", moveVector.y);

        transform.Translate(moveVector * Time.deltaTime * MoveSpeed);
    }
}
