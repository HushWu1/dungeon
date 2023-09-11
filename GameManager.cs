using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static AssetModule Asset { get => TGameFrameWork.Instance.GetModule<AssetModule>(); }

    public static ProcedureModule Procedure { get => TGameFrameWork.Instance.GetModule<ProcedureModule>(); }

    public static UIModule UI { get => TGameFrameWork.Instance.GetModule<UIModule>(); }

    public static TimeModule Time { get => TGameFrameWork.Instance.GetModule<TimeModule>(); }

    public static AudioModule Audio { get => TGameFrameWork.Instance.GetModule<AudioModule>(); }

    public static MessageModule Message { get => TGameFrameWork.Instance.GetModule<MessageModule>(); }

    public static ECSModule ECS { get => TGameFrameWork.Instance.GetModule<ECSModule>(); }

    public static SaveModule Save { get => TGameFrameWork.Instance.GetModule<SaveModule>(); }

    public static ScheduleModule Schedule { get => TGameFrameWork.Instance.GetModule<ScheduleModule>(); }

    private bool activing;

    private void Awake()
    {
        if (TGameFrameWork.Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        activing = true;
        DontDestroyOnLoad(gameObject);
//#if UNITY_EDITOR
//        UnityLog.StartupEditor();
//#else
//            UnityLog.Startup();
//#endif

        Application.logMessageReceived += OnReceiveLog;
        TGameFrameWork.Initialize();
        //StartupModules();
        TGameFrameWork.Instance.InitModules();
    }

    private void Start()
    {
        TGameFrameWork.Instance.StartModules();
        //Procedure.StartProcedure().Coroutine();
    }

    private void Update()
    {
        TGameFrameWork.Instance.Update();
    }

    private void LateUpdate()
    {
        TGameFrameWork.Instance.LateUpdate();
    }

    private void FixedUpdate()
    {
        TGameFrameWork.Instance.FixedUpdate();
    }

    private void OnDestroy()
    {
        if (activing)
        {
            Application.logMessageReceived -= OnReceiveLog;
            TGameFrameWork.Instance.Destroy();
        }
    }

    private void OnApplicationQuit()
    {
        //UnityLog.Teardown();
    }
    private void OnReceiveLog(string condition, string stackTrace, LogType type)
    {
#if !UNITY_EDITOR
            if (type == LogType.Exception)
            {
                UnityLog.Fatal($"{condition}\n{stackTrace}");
            }
#endif
    }
}
