using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    private const float teleportInterval = 0.03f;
    private float lastTeleportTime = -teleportInterval;
 
    void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.gameObject.name == "Portal_blue") && (TimeTillNext() <= 0))
        {
            Vector3 portalPosition = GameObject.Find("Portal_red").transform.localPosition;
            transform.localPosition = portalPosition;
            lastTeleportTime = Time.realtimeSinceStartup;
        }
        if ((other.gameObject.name == "Portal_red") && (TimeTillNext() <= 0))
        {
            Vector3 portalPosition = GameObject.Find("Portal_blue").transform.localPosition;
            transform.localPosition = portalPosition;
            lastTeleportTime = Time.realtimeSinceStartup;
        }
    }

    public float TimeTillNext()
    {
        return teleportInterval + lastTeleportTime - Time.realtimeSinceStartup;
    }
}
