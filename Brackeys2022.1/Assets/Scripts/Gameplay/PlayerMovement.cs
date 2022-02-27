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
	[SerializeField] private float m_dashtimer;
	
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
	private bool m_AllowedToDash;
	private float m_timer;

	private static bool m_Active;
	
	private Animator a_animator;
	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
		m_timer = m_dashtimer;
		//	if (OnLandEvent == null)
		//		OnLandEvent = new UnityEvent();
		a_animator = GetComponentInChildren<Animator>();

	}

	private void Update()
	{
		if (GameLoop.gameState != GameState.FIGHT)
		{
			m_Rigidbody2D.gravityScale = 0;
			m_Grounded = false;
			m_Dashing = false;
			horizontalInput = 0;
			return;
		}

		if (m_Rigidbody2D.gravityScale == 0)
		{
			m_Rigidbody2D.gravityScale = 2;
		}
		if (Input.GetKeyDown(KeyCode.W) && m_Grounded)
		{
			m_Jumping = true;
		}

		if (Input.GetKeyDown(KeyCode.Space) || (Input.GetKeyDown(KeyCode.W) && !m_Grounded))
		{
			//m_Rigidbody2D.gravityScale = 0.1f;
			m_Rigidbody2D.drag = 25f;
		}

		if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.W))
		{
			//m_Rigidbody2D.gravityScale = 2f;
			m_Rigidbody2D.drag = 0f;
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.color= Color.red;
		Gizmos.DrawLine(m_GroundCheck.position, new Vector3(m_GroundCheck.position.x, m_GroundCheck.position.y - .5f, m_GroundCheck.position.z));
		
		Gizmos.DrawRay(transform.position, -(transform.position-Mouse.GetMousePos(0)).normalized * m_DashDistance);
	}

	private void FixedUpdate()
	{
		if (GameLoop.gameState != GameState.FIGHT) return;
		bool wasGrounded = m_Grounded;
		bool wasDashing = m_Dashing;
		m_Grounded = false;
		m_Dashing = false;
		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		RaycastHit hit;
		if (Physics2D.Raycast((Vector2) m_GroundCheck.position, Vector2.down, .5f, m_WhatIsGround, -Mathf.Infinity,
			Mathf.Infinity))
		{
			m_Grounded = true;
		}
		else
		{
			m_Grounded = false;
		}

		horizontalInput = Input.GetAxisRaw("Horizontal") * m_RunSpeed;

		//if (Input.GetKey(KeyCode.Space) && m_AllowedToDash)
		if (Input.GetMouseButton(1) && m_AllowedToDash)
		{
			m_Dashing = true;
			m_AllowedToDash = false;
		}

		if (Input.GetMouseButtonUp(1))
		{
			m_Dashing = false;
		}

		if (!m_AllowedToDash)
		{
			m_timer -= Time.deltaTime;
			if (m_timer <= 0)
			{
				m_AllowedToDash = true;
				m_timer = m_dashtimer;
			}
		}


		Move(horizontalInput * Time.fixedDeltaTime, m_Jumping, m_Dashing);
		m_Jumping = false;
	}


	public void Move(float move, bool jump, bool dash)
	{
		if (dash)
		{
			var direction = -(this.transform.position - Mouse.GetMousePos(0)).normalized;
			RaycastHit2D hit = Physics2D.Raycast(this.transform.position, direction, m_DashDistance, m_WhatIsGround,
				Mathf.NegativeInfinity, Mathf.Infinity);
			Debug.Log(hit.distance);
			if (hit)
			{
				if(hit.collider.isTrigger)
					direction *= m_DashDistance;
				else
					direction *= hit.distance;
			}
			else
			{
				direction *= m_DashDistance;
			}
				/*dashForce = new Vector2(Vector2.one.x * direction.x, Vector2.one.y * direction.y);
			m_Rigidbody2D.AddRelativeForce(dashForce, ForceMode2D.Impulse);
			*/
				//m_Rigidbody2D.AddForce(new Vector2(m_Rigidbody2D.velocity.x, m_JumpForce/5f));
			this.transform.position = new Vector3(Mathf.Lerp(this.transform.position.x, this.transform.position.x + direction.x, 0.9f), Mathf.Lerp(this.transform.position.y, this.transform.position.y + direction.y, 0.9f), 0);
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
			a_animator.Play("jump");
		}
		//only control the player if grounded or airControl is turned on
		if (m_Grounded)
		{
			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
			// And then smoothing it out and applying it to the character
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
			a_animator.SetFloat("speed",m_Rigidbody2D.velocity.x / m_RunSpeed);

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
