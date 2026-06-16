using Fusion;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using WebSocketSharp;

public class SessionEntry : MonoBehaviour
{
    [SerializeField] TMP_Text serverNameLbl;
    [SerializeField] TMP_Text gameModeLbl;
    [SerializeField] TMP_Text mapNameLbl;
    [SerializeField] TMP_Text playerCountLbl;

    private string serverName;
    private string gameMode;
    private string mapName;
    private int playersInGame;
    private int maxPlayers;

    public void SetSessionInfo(SessionInfo info)
    {
        this.serverName = info.Name;
        this.gameMode = info.Properties["GameMode"];
        this.mapName = info.Properties["MapName"];
        this.playersInGame = info.PlayerCount;
        this.maxPlayers = info.MaxPlayers;

        serverNameLbl.text = serverName;
        gameModeLbl.text = gameMode;
        mapNameLbl.text = mapName;
        playerCountLbl.text = $"{playersInGame}/{maxPlayers}";
    }


    public void JoinGame()
    {
        if (!serverName.IsNullOrEmpty())
            PhotonManager.Instance.JoinGame(serverName);
        else
            Debug.Log("No existe la sesion");
    }
}
