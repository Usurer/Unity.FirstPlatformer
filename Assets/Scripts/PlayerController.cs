using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour//, PlayerControls.IPlayerActions
{
    private Rigidbody2D _rigidbody;
    private PlayerControls _controls;

    private float _xMovement = 0;
    private bool _isJumping = false;
    private bool _isFalling = false;

    public int Velocity = 5;

    private bool _isGrounded
    {
        get
        {
            return !(_isFalling || _isJumping);
        }
    }

    public void OnEnable()
    {
        //if (_controls == null)
        //{
        //    _controls = new PlayerControls();
        //    _controls.Player.SetCallbacks(this);
        //}
        //_controls.Player.Enable();
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
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        if(this._xMovement == 0)
        {
            return;
        }

        var position = new Vector2(this._rigidbody.position.x + Velocity * Time.fixedDeltaTime * Mathf.Sign(_xMovement), this._rigidbody.position.y);
        this._rigidbody.MovePosition(position);

        this._xMovement = 0;
    }

    public void OnMove(InputValue input)
    {
        //var movement = context.ReadValue<float>();
        Debug.Log(input.Get<float>());
    }
}
