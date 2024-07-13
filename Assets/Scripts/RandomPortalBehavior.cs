using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPortalBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    private const float moveInterval = 3f;
    private float lastMoveTime = -moveInterval;
    private float mainCameraWidth = 0f;
    void Start()
    {

        if (gameObject.GetComponent<DragBehavior>() != null)
        {
            Destroy(gameObject.GetComponent<DragBehavior>());
        }
        mainCameraWidth = Camera.main.orthographicSize * Camera.main.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        if (TimeTillNext() < 0)
        {
            Vector3 prevPosition = transform.localPosition;
            transform.localPosition = new Vector3(Random.Range(-mainCameraWidth, mainCameraWidth), prevPosition.y, 0);
            lastMoveTime = Time.realtimeSinceStartup;
        }
    }

    public float TimeTillNext()
    {
        return moveInterval + lastMoveTime - Time.realtimeSinceStartup;
    }
}
