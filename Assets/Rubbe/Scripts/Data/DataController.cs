using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DataController : MonoBehaviour
{
    // ---�̱������� ����--- 
    static GameObject _container;
    static GameObject Container
    {
        get
        {
            return _container;
        }
    }
    static DataController _instance;
    public static DataController Instance
    {
        get
        {
            if (!_instance)
            {
                _container = new GameObject();
                _container.name = "DataController";
                _instance = _container.AddComponent(typeof(DataController)) as DataController;
                DontDestroyOnLoad(_container);
            }
            return _instance;
        }
    }

    // --- ���� ������ �����̸� ���� ---
    public string GameDataFileName = "Data.json";

    // "���ϴ� �̸�(����).json"
    public GameData _gameData;
    public GameData gameData
    {
        get
        {
            // ������ ���۵Ǹ� �ڵ����� ����ǵ���
            if (_gameData == null)
            {
                LoadGameData();
                SaveGameData();
            }
            return _gameData;
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // ü���� �ɾ �� �Լ��� �� ������ ȣ��ȴ�.
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //���� �� ó�� ȣ�� �� ��
        if (GameObject.Find("RewardData") == null)
        {
            LoadGameData();
            SaveGameData();
        }
    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // ����� ���� �ҷ�����
    public void LoadGameData()
    {
        string filePath = Application.persistentDataPath + GameDataFileName;

        // ����� ������ �ִٸ�
        if (File.Exists(filePath))
        {
            print("�ҷ����� ����");
            string FromJsonData = File.ReadAllText(filePath);
            _gameData = JsonUtility.FromJson<GameData>(FromJsonData);
        }

        // ����� ������ ���ٸ�
        else
        {
            print("���ο� ���� ����");
            _gameData = new GameData();
        }

        //GameObject.Find("Gold").GetComponent<GoldUpdate>().nowGold = _gameData.nowGold;
        GameObject.Find("GameData").GetComponent<Data>().nowGold = _gameData.nowGold;
        
        //GameObject.Find("Gems").GetComponent<GemUpdate>().nowGems = _gameData.nowGems;
        GameObject.Find("GameData").GetComponent<Data>().nowGems = _gameData.nowGems;

        GameObject.Find("GameData").GetComponent<Data>().stageClearCheck = _gameData.stageClearCheck;
    }

    // ���� �����ϱ�
    public void SaveGameData()
    {
        string ToJsonData = JsonUtility.ToJson(gameData);
        string filePath = Application.persistentDataPath + GameDataFileName;

        // �̹� ����� ������ �ִٸ� �����
        File.WriteAllText(filePath, ToJsonData);

        // �ùٸ��� ����ƴ��� Ȯ��
        print("����Ϸ�");
    }

    // ������ �����ϸ� �ڵ�����ǵ���
    private void OnApplicationQuit()
    {
        _gameData.nowGold = GameObject.Find("GameData").GetComponent<Data>().nowGold;
        _gameData.nowGems = GameObject.Find("GameData").GetComponent<Data>().nowGems;
        _gameData.stageClearCheck = GameObject.Find("GameData").GetComponent<Data>().stageClearCheck;

        SaveGameData();
    }
}