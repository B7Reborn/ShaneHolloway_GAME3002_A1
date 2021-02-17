using TMPro;
using UnityEngine;

// This class holds the UI information used for the HUD. Specifically it displays
// score, power, and kicking angle (horizontal/vertical)
public class InfoUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_ScoreText = null;
    [SerializeField]
    private TextMeshProUGUI m_PowerText = null;
    [SerializeField]
    private TextMeshProUGUI m_VAngleText = null;
    [SerializeField]
    private TextMeshProUGUI m_HAngleText = null;
    [SerializeField]
    private TextMeshProUGUI m_GoalText = null;

    // Callback to update the interface
    public void OnRequestUpdateUI(int iGoals, int iMisses, float fPower, int iVertical, int iHorizontal, bool bGoal)
    {
        UpdateParams(iGoals, iMisses, fPower, iVertical, iHorizontal, bGoal);
    }

    // Update the interface internally
    private void UpdateParams(int iGoals, int iMisses, float fPower, int iVertical, int iHorizontal, bool bGoal)
    {
        m_ScoreText.text = "Score: " + iGoals + " - " + iMisses;
        m_PowerText.text = "Power: " + fPower + " Units";
        m_HAngleText.text = "Angle to Net: " + iHorizontal + " Degrees";
        m_VAngleText.text = "Kicking Angle: " + iVertical + " Degrees";
        if (bGoal)
        {
            m_GoalText.text = "GOOOOOOOOAL!";
        }
        else if (!bGoal)
        {
            m_GoalText.text = "";
        }
    }
}
