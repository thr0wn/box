using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject hazardPrefab;
    public int maxHazardsToSpawn = 3;

    void Start()
    {
        StartCoroutine(SpawnHazards());
    }

    private IEnumerator SpawnHazards()
    {
	var hazardsToSpawn = Random.Range(1, maxHazardsToSpawn);
	for (int i = 0; i <= hazardsToSpawn; i++)
	{
	    var x = Random.Range(-6, 6);
            var drag = Random.Range(0f, 2f);
	    var hazard = Instantiate(hazardPrefab, new Vector3(x, 12, 0), Quaternion.identity);
	    hazard.GetComponent<Rigidbody>().drag = drag;
	}
	yield return new WaitForSeconds(1f);

	yield return SpawnHazards();
    }
 
}
