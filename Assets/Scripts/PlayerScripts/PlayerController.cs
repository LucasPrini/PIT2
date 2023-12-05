using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int playerMovementSpeed;
    private Rigidbody2D _playerRigidbody;

    public Animator playerAnimator;

    [SerializeField] private Vector3 _movementDirection;
    // Start is called before the first frame update
    void Start()
    {
        _playerRigidbody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //_movementDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        _movementDirection.x = Input.GetAxisRaw("Horizontal");
        _movementDirection.y = Input.GetAxisRaw("Vertical");
        

        playerAnimator.SetFloat("Horizontal", _movementDirection.x);
        playerAnimator.SetFloat("Vertical", _movementDirection.y);
        playerAnimator.SetFloat("Speed", _movementDirection.magnitude);

    }

    void FixedUpdate(){
        //_playerRigidbody.velocity = _movementDirection * playerMovementSpeed;
        //_playerRigidbody.MovePosition((_playerRigidbody.position + _movementDirection.normalized) * playerMovementSpeed * Time.fixedDeltaTime);
        this.transform.position = this.transform.position + (_movementDirection).normalized * playerMovementSpeed * Time.deltaTime;
    }
}
