
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpanwer : Spawn
{
    // Start is called before the first frame update
    [SerializeField] private List<Transform> _spawnLocations = new List<Transform>();
    private string _NOTE = "Note";
    private NoteSpanwer instance;
    static public NoteSpanwer Instance;
    private void Awake()
    {
        if (instance != null) return;
        Instance = this;

    }
    protected override void OnEnable()
    {
        base.OnEnable();
        LoadSpawnPosition();
    }
    void LoadSpawnPosition()
    {
        Transform SpawnPosObject = transform.Find("SpawnPosition");
        foreach (Transform pos in SpawnPosObject)
        {
            _spawnLocations.Add(pos);
        }
    }

    public Transform SpawnRandom()
    {
        // need shuffle or random not same before
        Vector3 randomPosSpawn = _spawnLocations[Random.Range(0, _spawnLocations.Count)].position;
        Transform spawnObject = SpawnThing(randomPosSpawn, Quaternion.identity, _NOTE);
        return spawnObject;
    }
    public Transform SpawnRandomOfSetY(float OfSetY)
    {
        Transform randNotePos = SpawnRandom();
        randNotePos.position = new Vector3(randNotePos.position.x, randNotePos.position.y + OfSetY, 0);
        return randNotePos;

    }
}
