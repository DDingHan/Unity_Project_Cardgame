using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DataController : MonoBehaviour
{
    // ---싱글톤으로 선언--- 
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

    // --- 게임 데이터 파일이름 설정 ---
    public string GameDataFileName = "Data.json";

    // "원하는 이름(영문).json"
    public GameData _gameData;
    public GameData gameData
    {
        get
        {
            // 게임이 시작되면 자동으로 실행되도록
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

    // 체인을 걸어서 이 함수는 매 씬마다 호출된다.
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //씬이 맨 처음 호출 될 때
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

    // 저장된 게임 불러오기
    public void LoadGameData()
    {
        string filePath = Application.persistentDataPath + GameDataFileName;

        // 저장된 게임이 있다면
        if (File.Exists(filePath))
        {
            print("불러오기 성공");
            string FromJsonData = File.ReadAllText(filePath);
            _gameData = JsonUtility.FromJson<GameData>(FromJsonData);
        }

        // 저장된 게임이 없다면
        else
        {
            print("새로운 파일 생성");
            _gameData = new GameData();
        }

        //GameObject.Find("Gold").GetComponent<GoldUpdate>().nowGold = _gameData.nowGold;
        GameObject.Find("GameData").GetComponent<Data>().nowGold = _gameData.nowGold;
        
        //GameObject.Find("Gems").GetComponent<GemUpdate>().nowGems = _gameData.nowGems;
        GameObject.Find("GameData").GetComponent<Data>().nowGems = _gameData.nowGems;

        GameObject.Find("GameData").GetComponent<Data>().stageClearCheck = _gameData.stageClearCheck;
    }

    // 게임 저장하기
    public void SaveGameData()
    {
        string ToJsonData = JsonUtility.ToJson(gameData);
        string filePath = Application.persistentDataPath + GameDataFileName;

        // 이미 저장된 파일이 있다면 덮어쓰기
        File.WriteAllText(filePath, ToJsonData);

        // 올바르게 저장됐는지 확인
        print("저장완료");
    }

    // 게임을 종료하면 자동저장되도록
    private void OnApplicationQuit()
    {
        _gameData.nowGold = GameObject.Find("GameData").GetComponent<Data>().nowGold;
        _gameData.nowGems = GameObject.Find("GameData").GetComponent<Data>().nowGems;
        _gameData.stageClearCheck = GameObject.Find("GameData").GetComponent<Data>().stageClearCheck;

        SaveGameData();
    }
}