using UnityEngine;
using System.Collections.Generic;

public class FixedCameraController : MonoBehaviour
{
    public List<FixedCamera> fixedCameras;
    private GameObject player;
    private Camera mainCamera;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        mainCamera = Camera.main;
    }

    void Update()
    {
        FixedCamera closestCamera = FindClosestCamera();
        float distanceToClosestCamera = Vector3.Distance(player.transform.position, closestCamera.transform.position);

        if (distanceToClosestCamera <= closestCamera.switchRadius)
        {
            mainCamera.transform.position = closestCamera.transform.position;
            mainCamera.transform.rotation = closestCamera.transform.rotation;
        }
    }

    FixedCamera FindClosestCamera()
    {
        FixedCamera closestCamera = null;
        float closestDistance = Mathf.Infinity;

        foreach (FixedCamera camera in fixedCameras)
        {
            float distanceToPlayer = Vector3.Distance(camera.transform.position, player.transform.position);
            if (distanceToPlayer < closestDistance)
            {
                closestDistance = distanceToPlayer;
                closestCamera = camera;
            }
        }

        return closestCamera;
    }

    void OnDrawGizmos()
    {
        if (fixedCameras != null)
        {
            Gizmos.color = Color.green;

            foreach (FixedCamera camera in fixedCameras)
            {
                if (camera != null)
                {
                    Gizmos.DrawWireSphere(camera.transform.position, camera.switchRadius);
                }
            }
        }
    }
}
