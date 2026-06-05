using Fusion;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SessionManager : MonoBehaviour
{
    List<SessionInfo> sessionList;
    [SerializeField] private GameObject sessionPrefab;
    [SerializeField] private Transform viewportContent;
    public GameObject noSessionMsg;

    private void OnEnable()
    {
        PhotonManager.Instance.onSessionListUpdated += OnSessionListUpdated;
    }
    private void OnDisable()
    {
        PhotonManager.Instance.onSessionListUpdated -= OnSessionListUpdated;

    }

    private void Start()
    {
        
    }

    public void OnSessionListUpdated(List<SessionInfo> sessionList)
    {
        this.sessionList = sessionList;

        if(sessionList.Count == 0)
        {
            noSessionMsg.SetActive(true);
        }
        else
        {
            UpdateSessionListInCanvas(sessionList);
        }
    }


    public void UpdateSessionListInCanvas(List<SessionInfo> sessionList)
    {
        noSessionMsg.SetActive(false);

        for (int session = 0;  session < sessionList.Count; session++)
        {
            GameObject sessionInfo = Instantiate(sessionPrefab, viewportContent);
            SessionEntry sEntry = sessionInfo.GetComponent<SessionEntry>();
            if (sEntry != null)
            {
                sEntry.SetSessionInfo(sessionList[session].Name, sessionList[session].Region, "mapName", sessionList[session].PlayerCount, sessionList[session].MaxPlayers);
            }
        }
    }





}
