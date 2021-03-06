﻿using System.Runtime.InteropServices;
using System.Threading;
using System;

namespace UnityEngine.Console {
    namespace console_api {
        public class ConsoleAPI {
            [DllImport("UnityConsoleAPI.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
            public static extern void CreateConsole();

            [DllImport("UnityConsoleAPI.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
            public static extern void CloseConsole();

            [DllImport("UnityConsoleAPI.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
            public static extern void DetachConsole();

            [DllImport("UnityConsoleAPI.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
            public static extern void LogConsole(string msg);

            [DllImport("UnityConsoleAPI.dll", CallingConvention = CallingConvention.StdCall)]
            [return: MarshalAs(UnmanagedType.LPStr)]
            public static extern string ScanConsole();
        }
    }


    public class UnityConsole : MonoBehaviour {
        public static readonly bool CONSOLE_ENABLE = false;
        public static bool CONSOLE_CREATED = false;

        public Thread console_thread;
        public string message;
        public string stacktrace;

        void OnEnable() {
            Application.logMessageReceived += HandleLog;
        }
        void OnDisable() {
            Application.logMessageReceived -= HandleLog;
        }

        void HandleLog(string logString, string stackTrace, LogType type) {
            if (!CONSOLE_ENABLE) return;
            if (!Debug.isDebugBuild) return;

            message = logString;
            stacktrace = stackTrace;

            if (type == LogType.Error) {
                Instance.LogConsole("\n" + logString + "\n" + stackTrace + ":" + Time.time + "-------------------------\n");
            } else {
                Instance.LogConsole(logString + ":" + Time.time + "\n");
            }
        }

        public void LogConsole(string msg) {
            if (!CONSOLE_ENABLE) return;

            console_thread = new Thread(
                () => console_api.ConsoleAPI.LogConsole(msg));
            console_thread.Start(msg);
        }

        public string ScanConsole() {
            if (CONSOLE_CREATED == false) {
                console_api.ConsoleAPI.CreateConsole();
                console_api.ConsoleAPI.LogConsole("[STARTING CONSOLE]\n\n");
                CONSOLE_CREATED = true;
            }
            Instance.LogConsole("\n>> ");

            return console_api.ConsoleAPI.ScanConsole();
        }

        private static UnityConsole s_instance;
        public static UnityConsole Instance {
            get {
                if (s_instance == null) {
                    if (!CONSOLE_ENABLE) return new GameObject("Console").AddComponent<UnityConsole>();

                    console_api.ConsoleAPI.CreateConsole();
                    console_api.ConsoleAPI.LogConsole("[STARTING CONSOLE]\n\n");
                    CONSOLE_CREATED = true;
                    return new GameObject("Console").AddComponent<UnityConsole>();
                } else { return s_instance; }
            }
        }

        private void Awake() {
            if (s_instance != null) {
                Destroy(gameObject);
                return;
            }
            s_instance = this;

            DontDestroyOnLoad(gameObject);
        }

        private void OnApplicationQuit() {
            if (!CONSOLE_ENABLE) return;

            console_api.ConsoleAPI.LogConsole("[CLOSING CONSOLE]\n\n");
#if UNITY_EDITOR
            console_api.ConsoleAPI.CloseConsole();
#else
            console_api.ConsoleAPI.DetachConsole();
#endif
        }
    }
}