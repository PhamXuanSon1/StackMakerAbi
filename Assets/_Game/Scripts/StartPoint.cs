using UnityEngine;

public class StartPoint : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (spawnPoint != null && player != null)
        {
            //xets huownsg vs xet vi tri cua player la diem spawn point
            player.transform.position = spawnPoint.position;
            player.transform.rotation = spawnPoint.rotation;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
