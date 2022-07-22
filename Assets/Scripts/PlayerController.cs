using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour//, PlayerControls.IPlayerActions
{
    private Rigidbody2D _rigidbody;
    private Collider2D _collider;
    private PlayerControls _controls;

    private float _xMovement = 0;
    private bool _jump = false;
    private bool _isJumping = false;
    private bool _isFalling = false;

    public int Velocity = 5;
    public int JumpForce = 5;

    private bool _isGrounded
    {
        get
        {
            return !(_isFalling || _isJumping);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (_controls == null)
        {
            _controls = new PlayerControls();
            //_controls.Player.SetCallbacks(this);
        }

        _controls.Player.Enable();

        this._rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    private void FixedUpdate()
    {
        if (_collider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            Debug.Log("Grounded");
            this._isJumping = false;
        }

        if (_jump && _isGrounded)
        {
            Debug.Log("Jump");
            _jump = false;
            _isJumping = true;
            _rigidbody.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
        }


        if(this._xMovement == 0)
        {
            return;
        }

        var position = new Vector2(this._rigidbody.position.x + Velocity * Time.fixedDeltaTime * Mathf.Sign(_xMovement), this._rigidbody.position.y);
        this._rigidbody.MovePosition(position);
    }

    public void OnMove(InputValue input)
    {
        _xMovement = input.Get<float>();
    }

    public void OnJump(InputValue input)
    {
        _jump = input.Get<float>() > 0 && _isGrounded;
    }
}
