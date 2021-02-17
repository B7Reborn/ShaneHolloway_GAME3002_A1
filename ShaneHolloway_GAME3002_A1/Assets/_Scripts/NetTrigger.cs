using UnityEngine.Assertions;
using UnityEngine;

public class NetTrigger : MonoBehaviour
{
    [SerializeField]
    private BallBehaviour m_ball = null;

    // Start is called before the first frame update
    void Start()
    {
        Assert.IsNotNull(m_ball, "ERROR: No ball reference added to net trigger!");
    }

    private void OnTriggerEnter(Collider other)
    {
        m_ball.m_bGoalScored = true;
    }
}
