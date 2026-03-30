using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public List<string> haveItemKeyData = new List<string>();
    public List<string> collectedRecoveryItems = new List<string>();
    public Vector2 playerPosition;
    public string spawnPoint;
    public int playerHp;
}
public class SaveManager : MonoSingleton<SaveManager>
{
    [SerializeField] private Vector2 _defaultPlayerPos;
    [SerializeField] private int _defaultPlayerHp = 200;

    public Action OnDataLoaded;

    private string path;

    private void Awake()
    {
        path = Path.Combine(Application.persistentDataPath, "database.json");

        transform.parent = null;

        if (FindObjectsByType<SaveManager>(FindObjectsSortMode.None).Length == 1)
        {
            _instance = this;
        }

        if (_instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        JsonLoad();
        OnDataLoaded?.Invoke();
    }

    [ContextMenu("FileReset")]
    public void FileReset()
    {
        SaveData saveData = new SaveData();
        saveData.playerPosition = _defaultPlayerPos;
        saveData.playerHp = _defaultPlayerHp;

        string json = JsonUtility.ToJson(saveData, true);

        File.WriteAllText(Path.Combine(Application.persistentDataPath, "database.json"), json);
    }

    public void JsonLoad()
    {
        SaveData saveData = new SaveData();

        if (!File.Exists(path))
        {
            InitJson(saveData);
        }

        try
        {
            string loadJson = File.ReadAllText(path);
            saveData = JsonUtility.FromJson<SaveData>(loadJson);

            foreach (string key in saveData.collectedRecoveryItems)
            {
                GameManager.Instance.CollectRecoveryItem(key);
            }

            RespawnManager.Instance.LoadRespawnPoint(saveData.spawnPoint);
            GameManager.Instance.LoadPlayerPosition(saveData.playerPosition);
            GameManager.Instance.Player.GetCompo<Health>().SetHp(saveData.playerHp);
            ItemManager.Instance.LoadCollectedItem(saveData.haveItemKeyData);
        }
        catch
        {
            File.CreateText(path);
        }
    }

    private void InitJson(SaveData saveData)
    {
        saveData.playerHp = _defaultPlayerHp;
        saveData.playerPosition = _defaultPlayerPos;

        string initJson = JsonUtility.ToJson(saveData, true);

        File.WriteAllText(path, initJson);
    }

    public void JsonSave()
    {
        SaveData saveData = new SaveData();

        if (!File.Exists(path))
        {
            InitJson(saveData);
        }

        saveData.haveItemKeyData = ItemManager.Instance.GetCollectedItemsKey();
        saveData.collectedRecoveryItems = GameManager.Instance.RecoveryItems;
        saveData.spawnPoint = RespawnManager.Instance.GetCurrentPointkey();
        saveData.playerPosition = GameManager.Instance.Player.transform.position;
        saveData.playerHp = GameManager.Instance.Player.GetCompo<Health>().CurrentHealth;

        List<RespawnPoint> respawnPoints = RespawnManager.Instance.RespawnPoints;

        RespawnPoint lastPoint = respawnPoints[respawnPoints.Count - 1];

        bool isLastSpawnPoint = saveData.spawnPoint.Trim().Equals(lastPoint.key);
        bool isBossRoom = saveData.playerPosition.x > lastPoint.transform.position.x;

        if (isLastSpawnPoint && isBossRoom)
            saveData.playerPosition = lastPoint.transform.position;

        string json = JsonUtility.ToJson(saveData, true);

        File.WriteAllText(path, json);
    }

    private void OnApplicationQuit()
    {
        JsonSave();
    }

}