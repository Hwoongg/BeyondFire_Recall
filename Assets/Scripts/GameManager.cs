using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//
// 게임 전역 시스템 관리 클래스. 싱글톤
// 

public class GameManager : MonoBehaviour
{
    static GameManager instance;
    static GameObject container;

    // 플레이중인 씬 이름
    string lastSceneName;

    static public GameManager Instance()
    {
        if(instance == null)
        {
            container = new GameObject();
            instance = container.AddComponent<GameManager>();
        }

        return instance;
    }

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        instance = this;
        System.Type ty = GetType();
        gameObject.name = ty.ToString();
    }

    public void SetNowScene()
    {
        lastSceneName = SceneManager.GetActiveScene().name;
    }

    public string GetLastScene()
    {
        return lastSceneName;
    }
    
    
}
