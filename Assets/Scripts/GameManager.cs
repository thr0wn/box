using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject hazardPrefab;
    public int maxHazardsToSpawn = 3;
    public TMPro.TextMeshProUGUI scoreText;
    private const string highScorePrefsKey = "HighScore";
    public TMPro.TextMeshProUGUI maxScoreText;
    public int score;
    public int highScore;
    public float time = 0;
    public Image backgroundMenu;
    private static GameManager Instance;
    private static bool Running = false;
    private CinemachineImpulseSource cinemachineImpulseSource;
    public GameObject mainVCam;
    public GameObject zoomVCam;

    private static Coroutine SpawnRoutine;

    public GameObject gameOverMenu;

    void Awake()
    {
        Instance = this;
        cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
        highScore = PlayerPrefs.GetInt(highScorePrefsKey);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 0)
            {
                Time.timeScale = 1f;
                backgroundMenu.gameObject.SetActive(false);
            }
            else
            {
                Time.timeScale = 0f;
                backgroundMenu.gameObject.SetActive(true);
            }
        }
        if (!Running)
        {
            return;
        }

        time += Time.deltaTime;

        if (time >= 1f)
        {
            score++;
            scoreText.text = score.ToString();
            time = 0;
        }

        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt(highScorePrefsKey, highScore);
            maxScoreText.text = highScore.ToString();
        }
    }

    private IEnumerator SpawnHazards()
    {
        var hazardsToSpawn = Random.Range(1, maxHazardsToSpawn);
        for (int i = 0; i <= hazardsToSpawn; i++)
        {
            var x = Random.Range(-6, 6);
            var z = player.GetComponent<Transform>().position.z;
            var drag = Random.Range(0f, 2f);
            var hazard = Instantiate(hazardPrefab, new Vector3(x, 12, z), Quaternion.identity);
            hazard.GetComponent<Rigidbody>().drag = drag;
        }
        yield return new WaitForSeconds(1f);

        yield return SpawnHazards();
    }

    public void RunInstance()
    {
        gameObject.SetActive(true);
        player.SetActive(true);
        mainVCam.SetActive(true);
        zoomVCam.SetActive(false);
        gameOverMenu.SetActive(false);
        Running = true;
        score = 0;
        SpawnRoutine = StartCoroutine(SpawnHazards());
    }

    public static void Run()
    {
        Instance.RunInstance();
    }

    private void StopInstance()
    {
        Running = false;
        player.SetActive(false);
        cinemachineImpulseSource.GenerateImpulse();
        mainVCam.SetActive(false);
        zoomVCam.SetActive(true);
        gameOverMenu.SetActive(true);
        maxScoreText.text = highScore.ToString();
        StopCoroutine(SpawnRoutine);
    }

    public static void Stop()
    {
        Instance.StopInstance();
    }


    public static bool IsRunning()
    {
        return Running;
    }
}
