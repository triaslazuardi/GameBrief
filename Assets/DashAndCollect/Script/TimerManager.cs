using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class TimerManager : MonoBehaviour
{
    public TMP_Text timerText;   // drag UI Text ke sini
    public int startTime = 60; // mulai dari 60 detik

    public float currentTime;
    private bool isRunning = true; // kontrol jalan/stop

    private void Start()
    {
        timerText.text = "00:00";
    }

    public void PlayTimer() {
        isRunning = true;
        timerText.text = "0:00";
        currentTime = startTime;
        StartCoroutine(TimerCoroutine());
    }

    private IEnumerator TimerCoroutine()
    {
        while (currentTime > 0)
        {
            if (isRunning) // hanya jalan kalau tidak di-pause
            {
                UpdateTimerText();
                yield return new WaitForSeconds(1f);
                currentTime--;
            }
            else
            {
                // kalau lagi stop, tunggu 1 frame
                yield return null;
            }
        }

        currentTime = 0;
        UpdateTimerText();
        GameManage.instance.TimerDone();
    }

    private void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // 🔹 Fungsi untuk kontrol dari luar
    public void StopTimer()
    {
        isRunning = false;
    }
}
