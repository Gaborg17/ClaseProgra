using Fusion;
using Fusion.Sockets;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PhotonManager : MonoBehaviour, INetworkRunnerCallbacks
{

    public NetworkRunner runner;

    [SerializeField] private UnityEvent Joined;
    

    [SerializeField] private NetworkPrefabRef playerPrefab;
    [SerializeField] private Transform spawnPoint;

    private void Awake()
    {
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
            runner.Spawn(playerPrefab, Vector3.zero, Quaternion.identity, player);
            //Cursor.lockState = CursorLockMode.Locked;
            
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
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
    }

    public async Task StartGame(GameMode mode)
    {
        runner.ProvideInput = true;

        var scene = SceneRef.FromIndex(1);

        var sceneInfo = new NetworkSceneInfo();
        sceneInfo.AddSceneRef(scene, LoadSceneMode.Additive);

        await runner.StartGame(new StartGameArgs
        {
            GameMode = mode,
            SessionName = "MiPartida",
            Scene = sceneInfo,
            PlayerCount = 2,
        });

        


    }

    public void CreateGame()
    {
        StartGame(GameMode.Host);
        Debug.Log("Joined as Host");
    }

    public void JoinGame()
    {
        StartGame(GameMode.Client);
        Debug.Log("Joined as Client");

    }


}
