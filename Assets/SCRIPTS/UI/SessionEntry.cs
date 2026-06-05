using TMPro;
using UnityEngine;

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

    public void SetSessionInfo(string serverName, string gameMode, string mapName, int playersInGame, int maxPlayers)
    {
        this.serverName = serverName;
        this.gameMode = gameMode;
        this.mapName = mapName;
        this.playersInGame = playersInGame;
        this.maxPlayers = maxPlayers;

        serverNameLbl.text = serverName;
        gameModeLbl.text = gameMode;
        mapNameLbl.text = mapName;
        playerCountLbl.text = $"{playersInGame}/{maxPlayers}";
    }


    public void JoinGame()
    {

    }
}
