using Fusion;
using Fusion.Sockets;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhotonManager : MonoBehaviour, INetworkRunnerCallbacks
{
    public static PhotonManager Instance;

    public NetworkRunner runner;



    public event Action Joined;
    public event Action<List<SessionInfo>> onSessionListUpdated;



    [SerializeField] private NetworkPrefabRef playerPrefab;
    [SerializeField] private Transform spawnPoint;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        runner = FindAnyObjectByType<NetworkRunner>();
        runner.AddCallbacks(this);
    }

    public void OnConnectedToServer(NetworkRunner runner)
    {
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
    }

    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
    {
    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        InputInfo info = new InputInfo()
        {
            playerPosition = InputManager.Instance.GetMoveInput(),
            lookDirection = InputManager.Instance.GetMouseDelta(),
            isMoving = InputManager.Instance.IsMoveInputPressed(),
            isRunInputPressed = InputManager.Instance.WasRunInputPressed(),
            isMovingBackwards = InputManager.Instance.IsMovingBackwards(),
            isMovingOnXAxis = InputManager.Instance.IsMovingOnXAxis(),
            isFirePressed = InputManager.Instance.IsMainFirePressed(),
            isReloadPressed = InputManager.Instance.IsReloadPressed()
        };
        input.Set(info);

    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
    }

    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {
    }

    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {



        if (runner.IsServer)
        {
            var playerObject = runner.Spawn(playerPrefab, Vector3.zero, Quaternion.identity, player);
            //Cursor.lockState = CursorLockMode.Locked;
            runner.SetPlayerObject(player, playerObject);
        }



    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
    }

    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress)
    {
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data)
    {
    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {


        if (runner.LocalPlayer != null)
        {
            Joined?.Invoke();
        }
    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {

        onSessionListUpdated?.Invoke(sessionList);
    }

    public async void ConnectToPhotonLobby()
    {
        
       

        await runner.JoinSessionLobby(SessionLobby.ClientServer);

    }


    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
    }

    public async void StartRandomGame()
    {

        runner.ProvideInput = true;

        var scene = SceneRef.FromIndex(1);

        var sceneInfo = new NetworkSceneInfo();
        sceneInfo.AddSceneRef(scene, LoadSceneMode.Additive);
        sessionGeneratedName = RandomServerName();
        await runner.StartGame(new StartGameArgs
        {
            GameMode = GameMode.Host,
            SessionName = sessionGeneratedName,
            Scene = sceneInfo,
            CustomLobbyName = "Server: " + sessionGeneratedName,
            PlayerCount = 2,
        });

    }
    public async void StartCustomGame()
    {

        runner.ProvideInput = true;

        var scene = SceneRef.FromIndex(1);

        var sceneInfo = new NetworkSceneInfo();
        sceneInfo.AddSceneRef(scene, LoadSceneMode.Additive);

        await runner.StartGame(new StartGameArgs
        {
            GameMode = GameMode.Host,
            SessionName = "MiPartida",
            Scene = sceneInfo,
            CustomLobbyName = "Ea",
            PlayerCount = 2,
        });

    }

    public void CreateRandomGame()
    {
        StartRandomGame();
    }

    public void JoinGame()
    {


    }

    [SerializeField] private int randomNameMaxLength;
    private string sessionGeneratedName;

    [ContextMenu("Random")]
    public string RandomServerName()
    {
        string characters = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz0123456789";
        char[] character = characters.ToCharArray();


        StringBuilder sessionName = new StringBuilder();

        for (int i = 0; i < randomNameMaxLength; i++)
        {
            int charPos = UnityEngine.Random.Range(0, character.Length);
            sessionName.Append(character[charPos]);
        }

        Debug.Log(sessionName.ToString());

        return sessionName.ToString();
    }
}
