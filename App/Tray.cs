using System;
using System.Drawing;
using System.Windows.Forms;

namespace RGBSync
{
    class Tray
    {
        ContextMenu niMenu;
        NotifyIcon ni;
        MenuItem exitItem;

        public Tray()
        {
            niMenu = new ContextMenu();
            ni = new NotifyIcon();
            exitItem = new MenuItem();

            niMenu.MenuItems.AddRange(new MenuItem[] { exitItem });

            exitItem.Index = 0;
            exitItem.Text = "Exit";

            ni.ContextMenu = niMenu;

            ni.Icon = new Icon("logo256.ico");
            ni.Visible = true;
            ni.Text = "RGBSync";
        }

        public void addExitHandler(EventHandler handler)
        {
            exitItem.Click += handler;
        }
    }
}
