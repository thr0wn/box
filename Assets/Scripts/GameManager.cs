using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject hazardPrefab;
    public int maxHazardsToSpawn = 3;
    public TMPro.TextMeshProUGUI scoreText;
    public int score;
    public float time = 0;
    public Image backgroundMenu;
    private static bool gameOver;

    private static GameManager instance;
    public static GameManager Instance => instance;

    void Start()
    {
        instance = this;
        StartCoroutine(SpawnHazards());
    }

    public void Enable() {
        gameObject.SetActive(true);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            if(Time.timeScale == 0) {
                Time.timeScale = 1f;
                backgroundMenu.gameObject.SetActive(false);
            } else {
                Time.timeScale = 0f;
                backgroundMenu.gameObject.SetActive(true);
            }
        }
        if (gameOver)
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

    public static void GameOver()
    {
        gameOver = true;
    }
}
