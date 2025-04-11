using UnityEngine;

public class TriggerNewLevel : MonoBehaviour
{
    [SerializeField] private GameObject levelPrefab;
    [SerializeField] private Transform levelParent;

    private GameObject currentLevel;
    private Vector3 nextLevelPosition;
    private float spawnDistance = 120f;
    private bool trigger = false;

    void Start()
    {
        nextLevelPosition = transform.position + new Vector3(spawnDistance + 10f, -3f, 0f); // Start at the trigger's position
    }

    // This will be called when the player enters the trigger zone
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Ensure that the object colliding with the trigger is the player (or another specific object)
        if (other.CompareTag("Hero") && !trigger) 
        {
            trigger = true;
            SpawnNewLevel();
        }
    }

    private void SpawnNewLevel()
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel); // Destroy the previous level if it exists
        }
        Debug.Log('f');

        // Instantiate the new level at the correct position
        currentLevel = Instantiate(levelPrefab, nextLevelPosition, Quaternion.identity, levelParent);
        
        // Optionally, disable the trigger after spawning the level if you don't want it to trigger again
        // gameObject.SetActive(false); 
    }
}
