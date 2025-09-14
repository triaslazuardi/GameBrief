using Nitzz.Utility;
using TMPro;
using UnityEngine;

public class GameManage : MonoBehaviour
{
    public static GameManage instance;

    public PanelController _scrPanel;
    public CollectSpawner _scrCollect;
    public TimerManager _scrTime;


    [SerializeField]private int collected = 0;
    public int totalCollectibles = 5;
    public int totalLive = 3;
    public int currentLive = 3;

    public TMP_Text tmpLive;
    public TMP_Text tmpCollect;

    public bool isDoneGame = false;


    public GameObject playerPrefab;
    public GameObject enemyPrefab;


    public GameObject playerObj;
    public GameObject enemyObj;

    public Transform playerPos;
    public Transform enemyPos;

    private void Awake()
    {
        instance = this;

        
    }

    private void Start()
    {
        _scrPanel.OperatePanelPlay(true);
        AudioManager.instance.PlayMusic("bgm1");
    }

    public void StartGame()
    {
        isDoneGame = false;
        currentLive = totalLive;
        collected = 0;

        tmpLive.text = "x" + currentLive;
        tmpCollect.text = "0/" + totalCollectibles;
        tmpCollect.text = collected + "/" + totalCollectibles;

        playerObj = Instantiate(playerPrefab, playerPos.position, Quaternion.identity);
        enemyObj = Instantiate(enemyPrefab, enemyPos.position, Quaternion.identity);

        _scrCollect.SpawnCollectibles();
        _scrTime.PlayTimer();
    }

    public void FinishGame() {
        Destroy(playerObj);
        Destroy(enemyObj);

        _scrCollect.ResetSpawner();
        _scrTime.StopTimer();
    }

    public void CollectItem()
    {
        collected++;

        tmpCollect.text = collected + "/" + totalCollectibles;
        //Debug.Log("Collected: " + collected + "/" + totalCollectibles);

        if (collected >= totalCollectibles)
        {
            LevelComplete();
        }
    }

    public void TimerDone() {

        if (collected >= totalCollectibles)
        {
            LevelComplete();
        }
        else {
            PlayerLose();
        }
    }

    public void HitPlayer() {
        currentLive--;

        tmpLive.text = "x" + currentLive;

        if (currentLive <= 0)
        {
            tmpLive.text = "x" + 0;
            PlayerLose();
        }
    }

    void LevelComplete()
    {
        if (isDoneGame) return;
        isDoneGame = true;

        FinishGame();
        GameManage.instance._scrPanel._scrLeaderboard.SubmitScore((int)(
            GameManage.instance._scrTime.startTime - GameManage.instance._scrTime.currentTime));

        _scrPanel.OperatePanelWin(true);
    }


    public void PlayerLose() {
        if (isDoneGame) return;
        isDoneGame = true;

        FinishGame();
        _scrPanel.OperatePanelLose(true);
    }
}
