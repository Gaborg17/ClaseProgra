using Fusion;
using System.Collections.Generic;
using UnityEngine;

public class SessionManager : MonoBehaviour
{
    List<SessionInfo> sessionList;

    public void OnSessionListUpdated(List<SessionInfo> sessionList)
    {
        this.sessionList = sessionList;
    }


    public void UpdateSessionListInCanvas(List<SessionInfo> sessionList)
    {

    }
}
