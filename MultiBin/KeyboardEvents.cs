using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

class eventHandler
    {
        public bool Triggered { get { return triggered; } set { triggered = value; } }
        public string KeyStroke { get { return keyStroke; } }

        private string name;
        private object instance;
        private object[] parameters;
        private string method;
        private string keyStroke;
        private bool triggered = false;
        public eventHandler(string Name, string KeyStroke, object Instance, string Method, object[] Parameters)
        {
            name = Name;
            keyStroke = KeyStroke;
            instance = Instance;
            parameters = Parameters;
            method = Method;
        }
        public void call()
        {
            instance.GetType().GetMethod(method, 
                System.Reflection.BindingFlags.Public |
                System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.NonPublic |
                System.Reflection.BindingFlags.Static
                ).Invoke(instance, null);
        }
    }

class KeyboardEvents
{
    private const bool LOG_KEYS = true; //< Logs keys in console if set to true

    private const int WH_KEYBOARD_LL = 13;
    private const int WM_KEYDOWN = 0x0100;
    private const int WM_KEYUP = 0x0101;
    private static List<Keys> keysDown;
    private static LowLevelKeyboardProc _proc = HookCallback;
    private static IntPtr _hookID = IntPtr.Zero;
    private static List<eventHandler> events;

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool UnhookWindowsHookEx(IntPtr hhk);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr GetModuleHandle(string lpModuleName);

    public KeyboardEvents()
    {
        keysDown = new List<Keys>();
        events = new List<eventHandler>();
    }

    public void AddEvent(string name, Keys[] keyStroke, object instance, string method, object[] parameters)
    {
        eventHandler evnt = new eventHandler(name, getKeyStrokeString(keyStroke), instance, method, parameters);
        events.Add(evnt);
    }

    public void CaptureKeyboard()
    {
        _hookID = SetHook(_proc);
    }
    public void StopCapturingKeyboard()
    {
        UnhookWindowsHookEx(_hookID);
    }

    private static IntPtr SetHook(LowLevelKeyboardProc proc)
    {
        using (Process curProcess = Process.GetCurrentProcess())
        using (ProcessModule curModule = curProcess.MainModule)
        {
            return SetWindowsHookEx(WH_KEYBOARD_LL, proc,
                GetModuleHandle(curModule.ModuleName), 0);
        }
    }
    private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
    private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
    {
        if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
        {
            int vkCode = Marshal.ReadInt32(lParam);
            Keys vk = (Keys)vkCode;
            if (!keysDown.Contains(vk))
            {
                keysDown.Add((Keys)vkCode);
                string keyStrokeString = getKeyStrokeString(keysDown.ToArray());
                eventHandler e = events.Find(evnt => (evnt.Triggered == false && evnt.KeyStroke == keyStrokeString));
                if (e != null) e.call();

                if (LOG_KEYS)
                    Console.WriteLine("KeyDown : " + vk);
            }
        }
        else if (nCode >= 0 && wParam == (IntPtr)WM_KEYUP)
        {
            int vkCode = Marshal.ReadInt32(lParam);
            Keys vk = (Keys)vkCode;
            if (keysDown.Contains(vk))
            {
                string keyStrokeString = getKeyStrokeString(keysDown.ToArray());
                if (keyStrokeString == "")
                {
                    foreach (eventHandler ev in events.FindAll(evnt => (evnt.Triggered == true)))
                    {
                        ev.Triggered = false;
                    }
                }
                else
                {
                    eventHandler e = events.Find(evnt => (evnt.Triggered == true && evnt.KeyStroke == keyStrokeString));
                    if (e != null) e.Triggered = false;
                }

                keysDown.Remove((Keys)vk);

                if (LOG_KEYS)
                    Console.WriteLine("KeyUp : " + (Keys)vkCode);
            }
        }

        return CallNextHookEx(_hookID, nCode, wParam, lParam);
    }
    private static string getKeyStrokeString(Keys[] keys)
    {
        string keyStrokeString = "";
        foreach (Keys k in keys)
        {
            keyStrokeString = keyStrokeString + ((int)k).ToString();
        }
        return keyStrokeString;
    }
}
