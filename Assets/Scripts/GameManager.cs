using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject hazardPrefab;
    public int maxHazardsToSpawn = 3;
    public TMPro.TextMeshPro scoreText;
    public int score;
    public float time = 0;
    private static bool gameOver; 
    
    void Start()
    {
        StartCoroutine(SpawnHazards());
    }

    private void Update()
    {
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
