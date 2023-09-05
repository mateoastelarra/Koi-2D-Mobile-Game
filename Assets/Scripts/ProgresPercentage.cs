using UnityEngine;
using TMPro;

public class ProgresPercentage : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI progressText;
    [SerializeField] int totalLevels;

    private int progressPercentage;

    void Start()
    {
        progressPercentage = CalculateProgress(PlayerPrefs.GetInt("Progress"));
        progressText.text = $"Progreso {progressPercentage}%";
    }


    public int CalculateProgress(int progress)
    {
        return progress * 100 / totalLevels;
    }
}
