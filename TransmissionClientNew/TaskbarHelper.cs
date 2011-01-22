using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace TransmissionRemoteDotnet
{
    class TaskbarHelper
    {
        private object windowsTaskbar = null;
        private IntPtr WindowHandle;
        private static MainWindow ProgramForm;
        private System.Drawing.Icon iconPause, iconPauseAll, iconStartAll, iconAddTorrent;
        private Assembly MicrosoftWindowsAPICodePackShell = null;

        private object buttonStartAll = null, buttonPauseAll = null, buttonAddTorrent = null;

        public TaskbarHelper()
        {
            ProgramForm = Program.Form;
            WindowHandle = ProgramForm.Handle;
            try
            {
                MicrosoftWindowsAPICodePackShell = Assembly.LoadFrom("Microsoft.WindowsAPICodePack.Shell.dll");

                Type TaskbarManager = MicrosoftWindowsAPICodePackShell.GetType("Microsoft.WindowsAPICodePack.Taskbar.TaskbarManager");
                windowsTaskbar = TaskbarManager.GetProperty("Instance").GetValue(TaskbarManager, null);

                /* windowsTaskbar.ApplicationId = "Transmission Remote Dotnet"; */
                TaskbarManager.GetProperty("ApplicationId").SetValue(windowsTaskbar, "Transmission Remote Dotnet", null);

                /* windowsTaskbar.SetApplicationIdForSpecificWindow(WindowHandle, windowsTaskbar.ApplicationId); */
                MethodInfo SetApplicationIdForSpecificWindow = TaskbarManager.GetMethod("SetApplicationIdForSpecificWindow", new Type[] { WindowHandle.GetType(), typeof(System.String) });
                SetApplicationIdForSpecificWindow.Invoke(windowsTaskbar, new object[] {
                    WindowHandle,
                    TaskbarManager.GetProperty("ApplicationId").GetValue(windowsTaskbar, null)
                });

                iconPause = System.Drawing.Icon.FromHandle(Properties.Resources.pause16.GetHicon());
                iconPauseAll = System.Drawing.Icon.FromHandle(Properties.Resources.player_pause_all.GetHicon());
                iconStartAll = System.Drawing.Icon.FromHandle(Properties.Resources.player_play_all.GetHicon());
                iconAddTorrent = System.Drawing.Icon.FromHandle(Properties.Resources.add16.GetHicon());
                /*
                 * buttonStartAll = new ThumbnailToolbarButton(iconStartAll, OtherStrings.StartAll);
                 * buttonPauseAll = new ThumbnailToolbarButton(iconPauseAll, OtherStrings.PauseAll);
                 * buttonAddTorrent = new ThumbnailToolbarButton(iconAddTorrent, OtherStrings.NewTorrentIs);
                 */
                Type ThumbnailToolbarButton = MicrosoftWindowsAPICodePackShell.GetType("Microsoft.WindowsAPICodePack.Taskbar.ThumbnailToolBarButton");
                buttonStartAll = Activator.CreateInstance(ThumbnailToolbarButton, new object[] { iconStartAll, OtherStrings.StartAll });
                buttonPauseAll = Activator.CreateInstance(ThumbnailToolbarButton, new object[] { iconPauseAll, OtherStrings.PauseAll });
                buttonAddTorrent = Activator.CreateInstance(ThumbnailToolbarButton, new object[] { iconAddTorrent, ProgramForm.AddTorrentString });

                SetConnected(false);

                /*
                 * buttonStartAll.Click+=new EventHandler<ThumbnailButtonClickedEventArgs>(buttonStartAll_Click);
                 * buttonPauseAll.Click += new EventHandler<ThumbnailButtonClickedEventArgs>(buttonPauseAll_Click);
                 * buttonAddTorrent.Click += new EventHandler<ThumbnailButtonClickedEventArgs>(buttonAddTorrent_Click);
                 */
                Type ThumbnailButtonClickedEventArgs = MicrosoftWindowsAPICodePackShell.GetType("Microsoft.WindowsAPICodePack.Taskbar.ThumbnailButtonClickedEventArgs");
                EventInfo ei = ThumbnailToolbarButton.GetEvent("Click");
                ei.AddEventHandler(buttonStartAll, Delegate.CreateDelegate(ei.EventHandlerType, null, this.GetType().GetMethod("buttonStartAll_Click")));
                ei.AddEventHandler(buttonPauseAll, Delegate.CreateDelegate(ei.EventHandlerType, null, this.GetType().GetMethod("buttonPauseAll_Click")));
                ei.AddEventHandler(buttonAddTorrent, Delegate.CreateDelegate(ei.EventHandlerType, null, this.GetType().GetMethod("buttonAddTorrent_Click")));

                /* windowsTaskbar.ThumbnailToolbars.AddButtons(WindowHandle, buttonStartAll, buttonPauseAll, buttonAddTorrent); */
                object ThumbnailToolbars = TaskbarManager.GetProperty("ThumbnailToolBars").GetValue(windowsTaskbar, null);
                Type ThumbnailToolBarManager = MicrosoftWindowsAPICodePackShell.GetType("Microsoft.WindowsAPICodePack.Taskbar.ThumbnailToolBarManager");
                Type ThumbnailToolbarButtonArray = MicrosoftWindowsAPICodePackShell.GetType("Microsoft.WindowsAPICodePack.Taskbar.ThumbnailToolBarButton[]");
                MethodInfo AddButtons = ThumbnailToolBarManager.GetMethod("AddButtons", new Type[] { WindowHandle.GetType(), ThumbnailToolbarButtonArray });
                Array ThumbnailToolbarButtons = Array.CreateInstance(ThumbnailToolbarButton, 3);
                ThumbnailToolbarButtons.SetValue(buttonStartAll, 0);
                ThumbnailToolbarButtons.SetValue(buttonPauseAll, 1);
                ThumbnailToolbarButtons.SetValue(buttonAddTorrent, 2);
                AddButtons.Invoke(ThumbnailToolbars, new object[] { WindowHandle, ThumbnailToolbarButtons });
            }
            catch (TargetInvocationException)
            { // this is normal: this is only supported on Windows 7 or newer.
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.ToString());
            }
        }

        public void ChangeUICulture()
        {
            if (MicrosoftWindowsAPICodePackShell != null)
            {
                Type ThumbnailToolbarButton = MicrosoftWindowsAPICodePackShell.GetType("Microsoft.WindowsAPICodePack.Taskbar.ThumbnailToolBarButton");
                if (buttonStartAll != null) ThumbnailToolbarButton.GetProperty("Tooltip").SetValue(buttonStartAll, OtherStrings.StartAll, null);
                if (buttonPauseAll != null) ThumbnailToolbarButton.GetProperty("Tooltip").SetValue(buttonPauseAll, OtherStrings.PauseAll, null);
                if (buttonAddTorrent != null) ThumbnailToolbarButton.GetProperty("Tooltip").SetValue(buttonAddTorrent, ProgramForm.AddTorrentString, null);
            }
        }
        public void buttonPauseAll_Click(object sender, EventArgs e)
        {
            ProgramForm.Perform_stopAllMenuItem_Click();
        }

        public void buttonAddTorrent_Click(object sender, EventArgs e)
        {
            ProgramForm.addTorrentButton_Click(sender, e);
        }

        public void buttonStartAll_Click(object sender, EventArgs e)
        {
            ProgramForm.Perform_startAllMenuItem_Click();
        }

        public void SetConnected(bool connected)
        {
            if (MicrosoftWindowsAPICodePackShell != null)
            {
                Type ThumbnailToolbarButton = MicrosoftWindowsAPICodePackShell.GetType("Microsoft.WindowsAPICodePack.Taskbar.ThumbnailToolBarButton");
                if (buttonStartAll != null) ThumbnailToolbarButton.GetProperty("Enabled").SetValue(buttonStartAll, connected, null);
                if (buttonPauseAll != null) ThumbnailToolbarButton.GetProperty("Enabled").SetValue(buttonPauseAll, connected, null);
                if (buttonAddTorrent != null) ThumbnailToolbarButton.GetProperty("Enabled").SetValue(buttonAddTorrent, connected, null);
            }
        }

        public void UpdateProgress(decimal value)
        {
            if (windowsTaskbar != null && MicrosoftWindowsAPICodePackShell != null)
            {
                /* windowsTaskbar.SetProgressValue(, 100, WindowHandle); */
                Type TaskbarManager = MicrosoftWindowsAPICodePackShell.GetType("Microsoft.WindowsAPICodePack.Taskbar.TaskbarManager");
                MethodInfo SetProgressValue = TaskbarManager.GetMethod("SetProgressValue", new Type[] { typeof(int), typeof(int), WindowHandle.GetType() });
                SetProgressValue.Invoke(windowsTaskbar, new object[] { (int)value, 100, WindowHandle });
            }
        }

        public void SetPaused()
        {
            if (windowsTaskbar != null && MicrosoftWindowsAPICodePackShell != null)
            {
                Type TaskbarManager = MicrosoftWindowsAPICodePackShell.GetType("Microsoft.WindowsAPICodePack.Taskbar.TaskbarManager");
                //windowsTaskbar.SetProgressState(Microsoft.WindowsAPICodePack.Taskbar.TaskbarProgressBarState.Paused, WindowHandle);
                Type TaskbarProgressBarState = MicrosoftWindowsAPICodePackShell.GetType("Microsoft.WindowsAPICodePack.Taskbar.TaskbarProgressBarState");
                MethodInfo SetProgressState = TaskbarManager.GetMethod("SetProgressState", new Type[] { TaskbarProgressBarState, typeof(IntPtr) });
                FieldInfo Paused = TaskbarProgressBarState.GetField("Paused");
                SetProgressState.Invoke(windowsTaskbar, new object[] { Paused.GetValue(TaskbarProgressBarState), WindowHandle });
                /* windowsTaskbar.SetOverlayIcon(iconPause, "pause"); */
                MethodInfo SetOverlayIcon = TaskbarManager.GetMethod("SetOverlayIcon", new Type[] { typeof(System.Drawing.Icon), typeof(string) });
                SetOverlayIcon.Invoke(windowsTaskbar, new object[] { iconPause, "pause" });
            }
        }

        public void SetNormal(bool none)
        {
            if (windowsTaskbar != null && MicrosoftWindowsAPICodePackShell != null)
            {
                Type TaskbarManager = MicrosoftWindowsAPICodePackShell.GetType("Microsoft.WindowsAPICodePack.Taskbar.TaskbarManager");
                //if (none)
                //  windowsTaskbar.SetProgressState(Microsoft.WindowsAPICodePack.Taskbar.TaskbarProgressBarState.Paused, WindowHandle);
                //else 
                //  windowsTaskbar.SetProgressState(Microsoft.WindowsAPICodePack.Taskbar.TaskbarProgressBarState.Normal, WindowHandle);
                Type TaskbarProgressBarState = MicrosoftWindowsAPICodePackShell.GetType("Microsoft.WindowsAPICodePack.Taskbar.TaskbarProgressBarState");
                MethodInfo SetProgressState = TaskbarManager.GetMethod("SetProgressState", new Type[] { TaskbarProgressBarState, typeof(IntPtr) });
                FieldInfo ProgressState = TaskbarProgressBarState.GetField(none ? "Paused" : "Normal");
                SetProgressState.Invoke(windowsTaskbar, new object[] { ProgressState.GetValue(TaskbarProgressBarState), WindowHandle });

                //windowsTaskbar.SetOverlayIcon(null, null);
                MethodInfo SetOverlayIcon = TaskbarManager.GetMethod("SetOverlayIcon", new Type[] { typeof(System.Drawing.Icon), typeof(string) });
                SetOverlayIcon.Invoke(windowsTaskbar, new object[] { null, null });
            }
        }
        public void SetNoProgress()
        {
            if (windowsTaskbar != null && MicrosoftWindowsAPICodePackShell != null)
            {
                //windowsTaskbar.SetProgressState(Microsoft.WindowsAPICodePack.Taskbar.TaskbarProgressBarState.NoProgress, WindowHandle);
                Type TaskbarManager = MicrosoftWindowsAPICodePackShell.GetType("Microsoft.WindowsAPICodePack.Taskbar.TaskbarManager");
                Type TaskbarProgressBarState = MicrosoftWindowsAPICodePackShell.GetType("Microsoft.WindowsAPICodePack.Taskbar.TaskbarProgressBarState");
                MethodInfo SetProgressState = TaskbarManager.GetMethod("SetProgressState", new Type[] { TaskbarProgressBarState, typeof(IntPtr) });
                FieldInfo NoProgress = TaskbarProgressBarState.GetField("NoProgress");
                SetProgressState.Invoke(windowsTaskbar, new object[] { NoProgress.GetValue(TaskbarProgressBarState), WindowHandle });
            }
        }
    }
}
