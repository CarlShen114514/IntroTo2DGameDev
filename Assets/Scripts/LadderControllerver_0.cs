using UnityEngine;
using Maything.UI.MenuStripUI;

public class LadderControllerver_0 : MonoBehaviour
{
    public GameObject thisGameObject;
    private Camera mainCamera;

    // Static variable to track whether a prefab has been pasted
    private static bool _hasPastedPrefab = false;

    private void Start()
    {
        MenuStripEventSystem.instance.onMenuClick.AddListener(OnMenuClicked);
    }

    void Update()
    {
        
    }

    public void OnMenuClicked(MenuStripItemData itemData)
    {
        Debug.Log("clicked on " + itemData.name);
        switch (itemData.name)
        {
            case "Copy Ladder":
                CopyLadder();
                break;
            case "Paste Ladder":
                PasteLadder();
                break;
            case "Delete Ladder":
                Destroy(thisGameObject);
                break;
            default:
                break;
        }
    }

    // Static variable to hold the reference to the prefab to be copied
    private static GameObject _prefabToCopy;

    private void CopyLadder()
    {
        // Store the reference to the prefab instead of instantiating it
        _prefabToCopy = thisGameObject;
        _hasPastedPrefab = false;
    }

    private void PasteLadder()
    {
        // Check if a prefab has been copied and not yet pasted
        if (_prefabToCopy != null && !_hasPastedPrefab)
        {
            // Instantiate the prefab at a new position
            GameObject newLadder = Instantiate(_prefabToCopy, GetRandomPositionNearby(), Quaternion.identity);
            _hasPastedPrefab = true; // Set flag to prevent further pasting
        }
        else if (_prefabToCopy == null)
        {
            Debug.LogError("No prefab has been copied.");
        }
        else
        {
            Debug.LogWarning("Prefab has already been pasted.");
        }
    }

    private Vector3 GetRandomPositionNearby()
    {
        // Generate a random position within a 5 unit radius around the original ladder
        Vector3 originalPosition = thisGameObject.transform.position;
        float randomRadius = Random.Range(-40f, 40f);
        float randomAngle = Random.Range(0f, 360f);
        Vector3 randomOffset = new Vector3(Mathf.Cos(randomAngle) * randomRadius, Mathf.Sin(randomAngle) * randomRadius, 0f);
        return originalPosition + randomOffset;
    }
}