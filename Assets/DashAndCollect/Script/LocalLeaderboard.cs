using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class LocalLeaderboard : MonoBehaviour
{
    public GameObject leaderboardPnl;
    public TMP_Text leaderboardText;
    private List<int> scores = new List<int>();

    private const string Key = "LocalLeaderboard";

    private void Start()
    {
        
    }

    public void OpenPanel() {
        LoadScores();
        ShowLeaderboard();
        leaderboardPnl.SetActive(true);
    }

    public void ClosePanel()
    {
        leaderboardPnl.SetActive(false);
    }

    public void SubmitScore(int score)
    {
        scores.Add(score);
        scores = scores.OrderBy(s => s).ToList(); // kecil lebih baik
        if (scores.Count > 10) scores = scores.Take(10).ToList(); // simpan 10 besar

        SaveScores();
        ShowLeaderboard();
    }

    private void ShowLeaderboard()
    {
        leaderboardText.text = "=== Leaderboard (Simulasi Nakama) ===\n";
        for (int i = 0; i < scores.Count; i++)
        {
            leaderboardText.text += $"{i + 1}. Time: {scores[i]} ms\n";
        }
    }

    private void SaveScores()
    {
        PlayerPrefs.SetString(Key, string.Join(",", scores));
        PlayerPrefs.Save();
    }

    private void LoadScores()
    {
        string saved = PlayerPrefs.GetString(Key, "");
        if (!string.IsNullOrEmpty(saved))
        {
            scores = saved.Split(',').Select(int.Parse).ToList();
        }
    }
}
