using System.Runtime.InteropServices;

namespace RHGMTool.Helper
{
    public class FormUtils
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HTCAPTION = 0x2;
        [DllImport("User32.dll")]
        private static extern bool ReleaseCapture();
        [DllImport("User32.dll")]
        private static extern int SendMessage(nint hWnd, int Msg, int wParam, int lParam);

        public static void MoveForm(nint handle)
        {
            ReleaseCapture();
            SendMessage(handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
        }

    }
}