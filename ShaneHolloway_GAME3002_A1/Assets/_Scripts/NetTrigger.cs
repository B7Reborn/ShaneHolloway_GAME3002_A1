using UnityEngine.Assertions;
using UnityEngine;

// This class is simply used as a locational trigger for the ball
public class NetTrigger : MonoBehaviour
{
    // Store a reference to the ball so the trigger class can change the goal bool
    [SerializeField]
    private BallBehaviour m_ball = null;

    
    void Start()
    {
        // Ensure the trigger has a reference to the ball
        Assert.IsNotNull(m_ball, "ERROR: No ball reference added to net trigger!");
    }

    // When the ball enters the net, the goal bool is set to true.
    private void OnTriggerEnter(Collider other)
    {
        m_ball.m_bGoalScored = true;
    }
}
