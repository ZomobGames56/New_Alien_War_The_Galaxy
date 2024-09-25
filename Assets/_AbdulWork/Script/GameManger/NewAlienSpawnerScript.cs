using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class NewAlienSpawnerScript : MonoBehaviour
{
    private float spawnTime = 3;
    private List<GameObject> deactiveAliens = new List<GameObject>();
    private List<GameObject> activeAliens = new List<GameObject>();
    private Vector3 spawnPos = Vector3.zero;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject[] AliensPrefab;
    [SerializeField] private int alienSpawnStrength = 50;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 1; i<=alienSpawnStrength; i++)
        {
            GameObject _gameObject = Instantiate(AliensPrefab[Random.Range(0, AliensPrefab.Length - 1)]);
            _gameObject.GetComponent<AlienClass>().SetAlienSpawner(this);
            _gameObject.transform.position = Vector3.zero;
            _gameObject.SetActive(false);
            deactiveAliens.Add(_gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        spawnTime -= Time.deltaTime;
        if(spawnTime < 0)
        {
            spawnTime = 1;
            if(deactiveAliens.Count < 4  && deactiveAliens.Count>0)
            {
                SpawnIndividual();
            }
            else if(deactiveAliens.Count >0)
            {
                int i = Random.Range(0, 5);
                if(i<=3)
                {
                    SpawnIndividual();
                }
                else
                {
                    SpawnGroup();
                }
            }
        }
    }
    private void SpawnIndividual()
    {
        Vector3 spawnPoint = player.transform.position + new Vector3(Random.Range(-20, 20), 0, Random.Range(30, 90));
        spawnPoint = CheckPosition(spawnPoint);
        Spawn(spawnPoint);
    }
    private void SpawnGroup()
    {
        if (deactiveAliens.Count>4)
        {
            int spawnCount = Random.Range(2, 4);
            Vector3 groupPoint = player.transform.position + new Vector3(Random.Range(-20 , 20), 0, Random.Range(30, 90));
            for (int i = 0; i <= spawnCount; i++)
            {
                Vector3 spawnPoint = groupPoint + new Vector3((float)Random.Range(-200, 200)/100, 0, (float)Random.Range(-200, 200)/100);
                Spawn(spawnPoint);
            }
        }
        else
        {
            int spawnCount = Random.Range(2 , deactiveAliens.Count-1);
            Vector3 groupPoint = player.transform.position + new Vector3(Random.Range(-20, 20), 0, Random.Range(30, 90));
            groupPoint = CheckPosition(groupPoint);
            for(int i = 0;i<=spawnCount;i++)
            {
                Vector3 spawnPoint = groupPoint + new Vector3(Random.Range(-3, 3), 0, Random.Range(-3, 3));
                Spawn(spawnPoint);
            }
        }
    }
    private Vector3 CheckPosition(Vector3 point)
    {
        Vector3 newPoint = point;
        RaycastHit hit;
        if (Physics.Raycast(point + new Vector3(0, 10, 0), Vector3.down, out hit))
        {
            if (hit.collider.tag != "Ground")
            {
               point = player.transform.position + new Vector3(Random.Range(-30, 30), 0, Random.Range(30, 90));
            }
            else
            {
                newPoint = hit.point;
            }
        }
        return newPoint;
    }
    private void Spawn(Vector3 point)
    {
        deactiveAliens[0].transform.position = point;
        deactiveAliens[0].transform.rotation = Quaternion.Euler(0, Random.Range(0,360), 0);
        deactiveAliens[0].SetActive(true);
        activeAliens.Add(deactiveAliens[0]);
        deactiveAliens.Remove(deactiveAliens[0]);
    }
    public void BackToList(GameObject alien)
    {
        activeAliens.Remove(alien);
        deactiveAliens.Add(alien);
        alien.SetActive(false);
    }
}
