using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Media;
using SelLEDControl;

namespace SDKs
{
    public class RGBFusion
    {

        private Comm_LED_Fun ledFun_;
        private bool initialized_;

        public bool IsInitialized()
        {
            return ledFun_ != null && initialized_;
        }


        public void Init()
        {
            if (IsInitialized())
                return;

            bool scanDone = false;

            ledFun_ = new Comm_LED_Fun(false);
            ledFun_.Apply_ScanPeriphera_Scuuess += () => scanDone = true;

            ledFun_.Ini_LED_Fun();

            ledFun_ = CommUI.Get_Easy_Pattern_color_Key(ledFun_);
            ledFun_.LEd_Layout.Set_Support_Flag();

            do
            {
                Thread.Sleep(millisecondsTimeout: 10);
            }
            while (!scanDone);

            ledFun_.Current_Mode = 0;
            ledFun_.Led_Ezsetup_Obj.PoweronStatus = 1;
            ledFun_.Set_Sync(false);

            initialized_ = true;
        }

        public void SetAllAreas(Color color)
        {
            ledFun_.Set_CS_Effect(0, 9, 9, color.R, color.G, color.B);
        }

    }
}
