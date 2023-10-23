using System;
using UnityEngine;
using UnityEngine.SceneManagement;

class GameManager : MonoBehaviour
{

    public static GameManager instance; 
    
    [SerializeField] private GameObject[] characters;

    private int _charIndex;

    public int CharIndex
    {
        get { return _charIndex; }
        set { _charIndex = value; }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishLoadding;
    }
    
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishLoadding;
    }

    void OnLevelFinishLoadding(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Gameplay")
        {
            Instantiate(characters[CharIndex]);
        }
    }
} //class 