using ErrorHandling;
using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace WindowHandleConsole
{
    class Program
    {
        #region

        [DllImport("kernel32.dll")]
        static extern int GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(int hWnd, int nCmdShow);

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string className, string windowTitle);

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int uMsg, int wParam, string lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private const int WM_SETTEXT = 0X000C;
        private const int WM_LBUTTONDOWN = 0x201;
        private const int WM_LBUTTONUP = 0x202;
        private const int OMITTED_VALUE = 0;
        private const string EDIT_NAME = "Edit";
        private const string BUTTON_CLASS = "Button";
        private const string BUTTON_OK = "OK";
        private const int WAIT_POP_UP_MESSAGEBOX_IN_MILISOCONDS = 5000;
        private const int WAIT_FOR_SETTING_EDIT_IN_MILISOCONDS = 1000;
        private const int WAIT_FOR_ULINK_POP_UP_IN_MILISOCONDS = 30000; // Wait for 30s

        #endregion


        static void Main(string[] args)
        {
            if (1 == args.Length)
            {
                ShowWindow(GetConsoleWindow(), 0); // 0:Hiden, 6:Min, 
                Thread.Sleep(WAIT_FOR_ULINK_POP_UP_IN_MILISOCONDS);
                string idConfiguration = args[0];
                ConfigurationBaseId configuration = new ConfigurationBaseId();
                configuration = XMLParserConsole.ParseConfiguration(idConfiguration);
                foreach (Option opt in configuration.ListOption)
                {
                    SetMessageBoxAuto(opt.Title, opt.Lable, EDIT_NAME, opt.Text, BUTTON_CLASS, BUTTON_OK);
                }
            }
            else
            {
                LogTextInformation.LogFileWrite(string.Format("[DEBUG]: Window Handle Console : Arguments not match "));
                return;
            }
        }

        #region

        public static void SetMessageBoxAuto(string iTitleName, string iLableName, string iEditName, string iValueEdit, string iButtonClass, string iButtonName)
        {
            LogTextInformation.LogFileWrite(string.Format("[INFO]: Running...{0} {1} ", iTitleName, iLableName));

            try
            {

                Thread.Sleep(WAIT_POP_UP_MESSAGEBOX_IN_MILISOCONDS);
                IntPtr titleBoxId = FindWindow(null, iTitleName);
                IntPtr lableId = FindWindowEx(titleBoxId, IntPtr.Zero, null, iLableName);
                IntPtr editId = FindWindowEx(titleBoxId, IntPtr.Zero, iEditName, null);
                IntPtr buttonId = FindWindowEx(titleBoxId, IntPtr.Zero, iButtonClass, iButtonName);

                if (lableId != IntPtr.Zero)
                {
                    // Set value to edit control in message_box
                    SendMessage(editId, WM_SETTEXT, OMITTED_VALUE, iValueEdit);
                    Thread.Sleep(WAIT_FOR_SETTING_EDIT_IN_MILISOCONDS);

                    // Press up and down button by SendMessage in API 
                    SendMessage(buttonId, WM_LBUTTONDOWN, OMITTED_VALUE, OMITTED_VALUE);
                    SendMessage(buttonId, WM_LBUTTONUP, OMITTED_VALUE, OMITTED_VALUE);
                    SendMessage(buttonId, WM_LBUTTONDOWN, OMITTED_VALUE, OMITTED_VALUE);
                    SendMessage(buttonId, WM_LBUTTONUP, OMITTED_VALUE, OMITTED_VALUE);
                }
                else
                {
                    LogTextInformation.LogFileWrite(string.Format("[DEBUG]: {0} Not found ", iLableName));
                }
            }
            catch (Exception ex)
            {
                string logger = LogTextInformation.CreateErrorMessage(ex);
                LogTextInformation.LogFileWrite(string.Format("[DEBUG]: Exception Set MessageBox Auto Console : {0} ", logger));
            }
        }

        #endregion      
    }
}
