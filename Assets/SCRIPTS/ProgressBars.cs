using Fusion;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBars : MonoBehaviour
{
    private bool barsInitialized = false;

    [SerializeField] private Slider localProgressBar;
    [SerializeField] private Image localProgress;
    [SerializeField] private Slider otherProgressBar;
    [SerializeField] private Image otherProgress;


    [Header("Team Red")]
    [SerializeField] private Color redTeamColor;

    [Header("Team Blue")]
    [SerializeField] private Color blueTeamColor;

    [SerializeField]private CaptureZone captureZone;

    private NetworkObject player;
    private TeamHandler tHandler;

    public void ChangeBars()
    {

        var runner = captureZone.Runner;

        if (runner == null || !runner.IsRunning) return;

        player = runner.GetPlayerObject(runner.LocalPlayer);

        tHandler = player.GetComponent<TeamHandler>();

        if (tHandler.team == Team.Blue)
        {
            localProgress.color = blueTeamColor;
            otherProgress.color = redTeamColor;

        }
        else
        {
            localProgress.color = redTeamColor;
            otherProgress.color = blueTeamColor;
        }


    }


    private void Update()
    {
        if (!barsInitialized)
        {
            var runner = captureZone != null ? captureZone.Runner : null;
            if (runner != null && runner.IsRunning && runner.GetPlayerObject(runner.LocalPlayer) != null)
            {
                ChangeBars();
                barsInitialized = true;
            }
            return;
        }

        float bluePer = (captureZone.TeamBlueTime / captureZone.winTime);
        float redPer = (captureZone.TeamRedTime / captureZone.winTime);
        bluePer = Mathf.Clamp01(bluePer);
        redPer = Mathf.Clamp01(redPer);

        if (tHandler.team == Team.Blue)
        {

            localProgressBar.value = bluePer;
            otherProgressBar.value = redPer;

        }
        else
        {
            localProgressBar.value = redPer;
            otherProgressBar.value = bluePer;
        }
    }


}
