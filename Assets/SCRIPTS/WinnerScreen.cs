using Fusion;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinnerScreen : MonoBehaviour
{
    private NetworkRunner runner;

    [SerializeField] private GameObject endScreen;

    [SerializeField] private TextMeshProUGUI killsOnGame;
    [SerializeField] private TextMeshProUGUI totalKills;
    [SerializeField] private TextMeshProUGUI deathsOnGame;
    [SerializeField] private TextMeshProUGUI totalDeaths;
    [SerializeField] private TextMeshProUGUI totalWins;

    private PlayerStats playerStats;
    private TeamHandler teamHandler;

    private Team winnerteam;
    private void Start()
    {
        runner = FindAnyObjectByType<NetworkRunner>();


        PlayFabManager.Instance.OnReceivedData += UpdateStatsToDisplay;

        endScreen.SetActive(false);

    }

    private void OnDestroy()
    {
        if (PlayFabManager.Instance != null)
            PlayFabManager.Instance.OnReceivedData -= UpdateStatsToDisplay;
    }

    public void ActivateScreen(Team winner)
    {
        NetworkObject localPlayer = runner.GetPlayerObject(runner.LocalPlayer);
        if (localPlayer == null)
        {
            Debug.LogError("No se encontró el GameObject del jugador local.");
            return;
        }

        playerStats = localPlayer.GetComponent<PlayerStats>();
        teamHandler = localPlayer.GetComponent<TeamHandler>();
        winnerteam = winner;
        
        FetchData();

        endScreen.SetActive(true);
    }

    public void DeactivateScreen()
    {
        endScreen.SetActive(false);
    }

    private void FetchData()
    {
        PlayFabManager.Instance.GetPlayerData();
    }

    private void UpdateData()
    {
        if(teamHandler.team == winnerteam)
        {
            PlayFabManager.Instance.victories += 1;
            totalWins.text = PlayFabManager.Instance.victories.ToString();
        }
        
        PlayFabManager.Instance.UpdatePlayerData();
    }


    private void UpdateStatsToDisplay(Dictionary<string, string> dataToShow)
    {
        killsOnGame.text = playerStats.Kills.ToString();
        deathsOnGame.text = playerStats.Deaths.ToString();

        int kills = dataToShow.ContainsKey("Kills") ? int.Parse(dataToShow["Kills"]) : 0;
        int deaths = dataToShow.ContainsKey("Deaths") ? int.Parse(dataToShow["Deaths"]) : 0;
        int wins = dataToShow.ContainsKey("Victories") ? int.Parse(dataToShow["Victories"]) : 0;


        PlayFabManager.Instance.kills = kills + playerStats.Kills;
        PlayFabManager.Instance.deaths = deaths + playerStats.Deaths;
        PlayFabManager.Instance.victories = wins;

        totalKills.text = PlayFabManager.Instance.kills.ToString();
        totalDeaths.text = PlayFabManager.Instance.deaths.ToString();
        totalWins.text = PlayFabManager.Instance.victories.ToString();

        UpdateData();
    }

}
