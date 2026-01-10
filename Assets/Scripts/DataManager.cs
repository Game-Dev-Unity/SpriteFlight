using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    DataManager Instance;
    void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public UserData LoadData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            UserData userData = JsonUtility.FromJson<UserData>(json);
            Debug.Log(json);
            return userData;
        }
        else
        {
            return null;
        }
    }
    public void SaveData(string score)
    {
        UserData userData = new UserData();
        userData.highscore = score;

        string json = JsonUtility.ToJson(userData);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }
    public class UserData
    {
        public string highscore;
    }
}
