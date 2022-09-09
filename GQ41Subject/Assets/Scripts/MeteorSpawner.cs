using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    public bool IsActive = false;

    [SerializeField] GameObject meteorPrefab = null;
    [SerializeField, Min(0)] float minMeteorSpeed = 1;
    [SerializeField, Min(0)] float maxMeteorSpeed = 4;
    [SerializeField, Range(-90, 90)] float minAngleZ = 0;
    [SerializeField, Range(-90, 90)] float maxAngleZ = 0;
    [SerializeField, Min(0.1f)] float minSpawnInterval = 1;
    [SerializeField, Min(0.1f)] float maxSpawnInterval = 3;

    bool spawing = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsActive && !spawing)
		{
            StartCoroutine(nameof(SpawnTimer));
		}
    }

    IEnumerator SpawnTimer()
	{
        spawing = true;

        yield return new WaitForSeconds(Random.Range(minSpawnInterval, maxSpawnInterval));

        SpawnMeteor();

        spawing = false;
	}

    void SpawnMeteor()
	{
        GameObject meteorObj;
        MeteorController meteorController;
        Vector3 scale = new Vector3(1.5f, 1.5f, 0);
        
        //  Ë¶êŒÇÃî≠éÀäpìx
        Quaternion rotation = Quaternion.Euler(Vector3.forward * Random.Range(minAngleZ, maxAngleZ));

        meteorObj = Instantiate(meteorPrefab, transform.position, rotation * transform.rotation);
        meteorObj.transform.localScale = scale;
        meteorController = meteorObj.GetComponent<MeteorController>();
        meteorController.Speed = Random.Range(minMeteorSpeed, maxMeteorSpeed);

	}
}
