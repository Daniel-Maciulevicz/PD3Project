using UnityEngine;

public class PD3StarsGame : UnityModelBaseClass
{
    public void SpawnBot(GameObject brawler, Transform position)
    {
        GameObject spawnedBrawler = GameObject.Instantiate(brawler, position.position, position.rotation);
        spawnedBrawler.transform.parent = null;
    }
    public void SpawnPlayer(GameObject brawler, GameObject camera, Transform position)
    {
        GameObject spawnedBrawler = GameObject.Instantiate(brawler, position.position, position.rotation);
        spawnedBrawler.transform.parent = null;
        GameObject spawnedCamera = GameObject.Instantiate(camera, position.position, position.rotation);
        spawnedCamera.GetComponent<CameraMovement>()._target = spawnedBrawler.transform;
    }
}