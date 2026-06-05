using Fusion;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] dontDestroy;

    private void Awake()
    {
        foreach (GameObject go in dontDestroy)
        {
            DontDestroyOnLoad(go);
        }
    }
    private void OnEnable()
    {
        
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PhotonManager.Instance.Joined += CloseScene;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CloseScene()
    {
       
        SceneManager.UnloadSceneAsync(0);
    }
}
