
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpanwer : Spawn
{
    // Start is called before the first frame update
    [SerializeField] private List<Transform> _spawnLocations = new List<Transform>();
    private string _NOTE = "Note";
    private string _HOLDNOTE = "HoldNote";
    private string _LOSENOTE = "LostNote";
    private int _prevSpawnIndex = 0;
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
        EventDefine.onMissClickOnNote.AddListener(OnMissClick);
    }
    private void OnDisable()
    {
        EventDefine.onMissClickOnNote.RemoveListener(OnMissClick);
    }

    private void OnMissClick(Transform missClickNote)
    {
        var missNotePos = missClickNote.position;
        var missNoteLength = missClickNote.localScale.y;
        var rand = Random.Range(0, _spawnLocations.Count);
        while (missNotePos.x == _spawnLocations[rand].position.x)
        {
            rand = Random.Range(0, _spawnLocations.Count);
        }

        missNotePos = new Vector3(_spawnLocations[rand].position.x, missNotePos.y, 0);
        SpawnLoseNote(missNotePos, missNoteLength);
    }

    void LoadSpawnPosition()
    {
        Transform SpawnPosObject = transform.Find("SpawnPosition");
        foreach (Transform pos in SpawnPosObject)
        {
            _spawnLocations.Add(pos);
        }
    }

    public Transform SpawnRandom(bool isHoldNote)
    {
        // need shuffle or random not same before
        int rand = Random.Range(0, _spawnLocations.Count);
        while (rand == _prevSpawnIndex)
        {
            rand = Random.Range(0, _spawnLocations.Count);
        }

        _prevSpawnIndex = rand;
        Vector3 randomPosSpawn = _spawnLocations[rand].position;
        Transform spawnObject = SpawnThing(randomPosSpawn, Quaternion.identity, isHoldNote == true ? _HOLDNOTE : _NOTE);
        return spawnObject;
    }

    public Transform SpawnRandomOfSetY(float OfSetY, bool isHoldNote)
    {
        Transform randNotePos = SpawnRandom(isHoldNote);
        randNotePos.position = new Vector3(randNotePos.position.x, randNotePos.position.y + OfSetY, 0);
        return randNotePos;

    }
    public Transform SpawnLoseNote(Vector3 notePos, float scale)
    {
        Transform spawnObject = SpawnThing(notePos, Quaternion.identity, _LOSENOTE);
        spawnObject.localScale = new Vector3(spawnObject.localScale.x, scale, 1);
        spawnObject.gameObject.SetActive(true);
        return spawnObject;
    }
}
