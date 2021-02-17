using UnityEngine.Assertions;
using UnityEngine;
using System;

// Main class for the behaviour of the ball. Acts as a controller for user input.
// Most of this code comes from the week 4 lab example ProjectileController.
public class BallBehaviour : MonoBehaviour
{
    // Create required variables for controlling the ball
    private ProjectileComponent m_projectile = null;
    private InfoUI m_interface = null;
    private int m_iGoals = 0;
    private int m_iMisses = 0;
    public bool m_bGoalScored = false;

    private void Start()
    {
        // Ensure the interface and projectile component are attached to the ball
        m_interface = GetComponent<InfoUI>();
        Assert.IsNotNull(m_interface, "ERROR: InfoUI is not attached!");

        m_projectile = GetComponent<ProjectileComponent>();
        Assert.IsNotNull(m_projectile, "ERROR: ProjectileComponent is not attached!");

        // Update the HUD with starting information
        m_interface.OnRequestUpdateUI(m_iGoals, m_iMisses, m_projectile.m_fLaunchPower, m_projectile.m_iVerticalAngle, m_projectile.m_iHorizontalAngle, m_bGoalScored);
    }

    // Update first takes in any user input, then updates the UI.
    private void Update()
    {
        HandleUserInput();
        m_interface.OnRequestUpdateUI(m_iGoals, m_iMisses, m_projectile.m_fLaunchPower, m_projectile.m_iVerticalAngle, m_projectile.m_iHorizontalAngle, m_bGoalScored);
    }

    // This function handles any input from the user
    // Quick Controls:
    // Left/Right = Adjust horizontal kicking angle
    // Up/Down = Adjust vertical kicking angle
    // Space (Press & Hold) = Adjust Power
    // Space (Release) = Launch Ball
    // R key = Reset ball position
    private void HandleUserInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            m_projectile.AdjustPower();
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            m_projectile.AttemptImpulse();
        }

        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            m_projectile.AdjustVerticalAngle(true);
        }

        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            m_projectile.AdjustVerticalAngle(false);
        }

        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            m_projectile.AdjustHorizontalAngle(true);
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            m_projectile.AdjustHorizontalAngle(false);
        }

        if (Input.GetKeyUp(KeyCode.R))
        {
            m_projectile.Reset();
            // If a goal was scored, get a point. Otherwise, get a miss. Then reset the goal bool.
            if (m_bGoalScored)
            {
                m_iGoals++;
            }
            else
            {
                m_iMisses++;
            }
            m_bGoalScored = false;
        }
    }
}
