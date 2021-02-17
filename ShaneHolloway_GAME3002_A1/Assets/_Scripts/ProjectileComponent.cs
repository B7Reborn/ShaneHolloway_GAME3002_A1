using UnityEngine.Assertions;
using UnityEngine;
using System;

// Projectile Component for the ball. Mostly taken from the projectile
// component script in lab example from week 4, as well as SimpleImpule
// from week 3.
public class ProjectileComponent : MonoBehaviour
{
    // Create required variables for applying an impulse
    private Vector3 m_vImpulseDir;
    private Rigidbody m_rb = null;

    // Create launch parameter variables
    public int m_iVerticalAngle = 0;
    public int m_iHorizontalAngle = 0;
    public float m_fLaunchPower = 0.0f;
    bool increasing = true;

    // Setup required variables for the ball's projectile component
    private Vector3 m_vInitialVelocity = Vector3.zero;


    // On startup, ensure attached object has a rigidbody component
    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        Assert.IsNotNull(m_rb, "ERROR: Rigidbody is not attached!");
        m_vImpulseDir = new Vector3(0.0f, 0.0f, 1.0f);  // Sets default kicking angle to straight ahead.
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    #region CONTROLS
    // Adjusts power of the ball based on how long the spacebar is held for. Will stay
    // within a range of 0-25 and is applied as an impulse when the spacebar is released.
    public void AdjustPower()
    {
        if (increasing)
        {
            m_fLaunchPower += 0.1f;
            if (m_fLaunchPower >= 25.0f)
            {
                m_fLaunchPower = 25.0f;
                increasing = !increasing;
            }
        }
        else if (!increasing)
        {
            m_fLaunchPower -= 0.1f;
            if (m_fLaunchPower <= 0.0f)
            {
                m_fLaunchPower = 0.0f;
                increasing = !increasing;
            }
        }
    }

    // This function applies an impulse on the ball when called based on the direction
    // and power input by the player (direction is precise, while power is based on how long
    // the spacebar is held before being released.
    public void AttemptImpulse()
    {
        m_rb.AddForce(m_vImpulseDir.normalized * m_fLaunchPower, ForceMode.Impulse);
    }

    // Adjusts the vertical angle of the ball when launched. This is based on trigonometry, and
    // results in the y-component of the impulse direction being adjusted. Constraints are:
    // adjacent side = 1, angle between adjacent and opposite sides = 90deg, with opposite
    // side being the y-component and angle being adjusted the angle between hypotenuse
    // and adjacent side.
    public void AdjustVerticalAngle(bool up)
    {
        // One call of this function increases or decreases the kicking angle
        // by 1 degree. Limited to being between 0-90 degrees.

        // Logic if angle is being adjusted upwards
        if (up)
        {
            // Check upper limit of angle, then do calculations, otherwise do nothing
            if (m_iVerticalAngle < 90)
            {
                m_iVerticalAngle+=3;
                double topRad = (Math.PI / 180) * m_iVerticalAngle;
                double bottomRad = (Math.PI / 180) * 90;
                m_vImpulseDir.y = (float)(Math.Sin(topRad) / Math.Sin(bottomRad));
            }
        }
        // Logic if angle is being adjusted downwards
        else if (!up)
        {
            // Check lower limit of angle, then do calculations, otherwise do nothing
            if (m_iVerticalAngle > 0)
            {
                m_iVerticalAngle-=3;
                double topRad = (Math.PI / 180) * m_iVerticalAngle;
                double bottomRad = (Math.PI / 180) * 90;
                m_vImpulseDir.y = (float)(Math.Sin(topRad) / Math.Sin(bottomRad));
            }
        }
    }

    // Adjust the horizontal angle of the ball when launched. Like the vertical angle, this is
    // based on trig, with both the x- and z-component of the launch angle vector being adjusted.
    // Constraints are as follows: hypotenuse = 1, angle between adjacent and opposite sides = 90 deg,
    // with this angle being the horizontal launch angle. The z-component is the adjacent side, while
    // the x-component is the opposite side. Positive x-component is to the right, while negative
    // x-component is to the left.
    public void AdjustHorizontalAngle(bool right)
    {
        // One call of this function increases or decreases the horizontal angle
        // by 1 degree. Limited to being between (-45)-(+45) degrees.

        // Logic if angle is being adjusted to the right
        if (right)
        {
            // Check upper limit of angle, then do calculations, otherwise do nothing
            if (m_iHorizontalAngle < 45)
            {
                m_iHorizontalAngle+=3;
                double hypAngle = (Math.PI / 180) * 90;
                double adjAngle = (Math.PI / 180)  * (90 - Math.Abs(m_iHorizontalAngle));
                double oppAngle = (Math.PI / 180) * m_iHorizontalAngle;
                // Calculations for components
                m_vImpulseDir.z = (float)(Math.Sin(adjAngle) / Math.Sin(hypAngle));
                m_vImpulseDir.x = (float)(Math.Sin(oppAngle) / Math.Sin(hypAngle));
            }
        }
        // Logic if angle is being adjusted to the left
        else if (!right)
        {
            // Check lower limit of angle, then do calculations, otherwise do nothing
            if (m_iHorizontalAngle > -45)
            {
                m_iHorizontalAngle-=3;
                double hypAngle = (Math.PI / 180) * 90;
                double adjAngle = (Math.PI / 180) * (90 - Math.Abs(m_iHorizontalAngle));
                double oppAngle = (Math.PI / 180) * m_iHorizontalAngle;
                // Calculations for components
                m_vImpulseDir.z = (float)(Math.Sin(adjAngle) / Math.Sin(hypAngle));
                m_vImpulseDir.x = (float)(Math.Sin(oppAngle) / Math.Sin(hypAngle));
            }
        }
    }

    // Resets the position and related variables on the ball to their starting points.
    // Could expand upon this to make goalie move to a random position on reset
    public void Reset()
    {
        // First, reset all parameters on the ball
        m_fLaunchPower = 0.0f;
        m_iHorizontalAngle = 0;
        m_iVerticalAngle = 0;
        m_vImpulseDir = new Vector3(0.0f, 0.0f, 1.0f);
        // Then, reset the ball's kinematics
        m_rb.angularVelocity = Vector3.zero;
        m_rb.velocity = Vector3.zero;
        // Last, reset the ball's position
        this.transform.position = new Vector3(0.0f, 0.5f, -5.5f);

        // TODO: Add simple logic that either increases the goals scored or the misses based on a boolean
    }
    #endregion
}
