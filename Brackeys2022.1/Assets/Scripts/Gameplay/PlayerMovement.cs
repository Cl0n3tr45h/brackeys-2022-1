using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float m_JumpForce = 400f;							// Amount of force added when the player jumps.
    [SerializeField] private int m_RunSpeed = 5;
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
	[SerializeField] private bool m_AirControl = false;							// Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;							// A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;							// A position marking where to check if the player is grounded.
	[SerializeField] private float m_DashDistance = 5f;
	
	private bool m_Grounded;            // Whether or not the player is grounded.
	private Rigidbody2D m_Rigidbody2D;
	public bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 m_Velocity = Vector3.zero;
	private Vector2 dashForce = new Vector2();
	/*[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }
	*/

	private float horizontalInput;
	private bool m_Jumping;
	private float jumpTimeCounter;
	private bool m_Dashing;
	
	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();

	//	if (OnLandEvent == null)
	//		OnLandEvent = new UnityEvent();

	}

	private void FixedUpdate()
	{
		bool wasGrounded = m_Grounded;
		bool wasDashing = m_Dashing;
		m_Grounded = false;
		m_Dashing = false;
		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		RaycastHit hit;
		if (Physics.Raycast(m_GroundCheck.position, Vector3.down, out hit, .5f, m_WhatIsGround,
			QueryTriggerInteraction.Ignore))
		{
			m_Grounded = true;
		}
		else
		{
			m_Grounded = false;
		}

		horizontalInput = Input.GetAxisRaw("Horizontal") * m_RunSpeed;
		if (Input.GetKey(KeyCode.W) && m_Grounded)
		{
			m_Jumping = true;
		}

		if (Input.GetKey(KeyCode.Space) && !wasDashing)
		{
			m_Dashing = true;
		}
		if (Input.GetKeyUp(KeyCode.Space))
		{
			m_Dashing = false;
		}
		Move(horizontalInput * Time.fixedDeltaTime, m_Jumping, m_Dashing);
		m_Jumping = false;
	}


	public void Move(float move, bool jump, bool dash)
	{
		if (dash)
		{
			var direction = -(this.transform.position - Mouse.GetMousePos(0)).normalized;
			direction *= m_DashDistance;
			/*dashForce = new Vector2(Vector2.one.x * direction.x, Vector2.one.y * direction.y);
			m_Rigidbody2D.AddRelativeForce(dashForce, ForceMode2D.Impulse);
			*/
			this.transform.position += direction;
		}
		else
		{
			if(dashForce != Vector2.zero)
				m_Rigidbody2D.velocity = -dashForce;
		}
		if (m_Grounded && jump)
		{
			// Add a vertical force to the player.
			m_Grounded = false;
			m_Rigidbody2D.AddForce(new Vector2(m_Rigidbody2D.velocity.x, m_JumpForce));
		}
		//only control the player if grounded or airControl is turned on
		if (m_Grounded)
		{
			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
			// And then smoothing it out and applying it to the character
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

		}
		else if (m_AirControl)
		{
			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
			// And then smoothing it out and applying it to the character
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
		}
		// If the input is moving the player right and the player is facing left...
		if (transform.position.x - Mouse.GetMousePos(0).x  < 0 && !m_FacingRight)
		{
			// ... flip the player.
			Flip();
		}
		// Otherwise if the input is moving the player left and the player is facing right...
		else if (transform.position.x - Mouse.GetMousePos(0).x > 0  && m_FacingRight)
		{
			// ... flip the player.
			Flip();
		}
		// If the player should jump...
		
	}

	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}