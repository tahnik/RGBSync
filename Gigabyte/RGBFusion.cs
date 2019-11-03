using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Media;
using SelLEDControl;

namespace SDKs
{
    public class RGBFusion
    {

        private Comm_LED_Fun ledFun_;
        private List<CommUI.Area_class> allAreaInfo_;
        private List<CommUI.Area_class> allExtAreaInfo_;
        private bool initialized_;

        public bool IsInitialized()
        {
            return ledFun_ != null && initialized_;
        }

        private void FillAllAreaInfo()
        {
            string areaFilename_ = "Pro1.xml";
            allAreaInfo_ = CommUI.Inport_from_xml(areaFilename_, But_Style: null);
        }

        private void Fill_ExtArea_info()
        {
            string extAreaFileName_ = "ExtPro1.xml";
            allExtAreaInfo_ = CommUI.Inport_from_xml(extAreaFileName_, But_Style: null);
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

            ledFun_.Current_Mode = 0; // 1= Advanced 0 = Simple or Ez
            ledFun_.Led_Ezsetup_Obj.PoweronStatus = 1;
            ledFun_.Set_Sync(false);

            initialized_ = true;

            FillAllAreaInfo();
            Fill_ExtArea_info();
        }

        public void SetAllAreas(object obj)
        {
            if (allAreaInfo_ == null || allExtAreaInfo_ == null)
            {
                Console.WriteLine("Gigabyte areas are null!");
                return;
            }

            var patternCombItem = new CommUI.Pattern_Comb_Item
            {
                Bg_Brush_Solid = { Color = (Color)obj },
                Sel_Item = { Style = null }
            };

            patternCombItem.Sel_Item.Background = patternCombItem.Bg_Brush_Solid;
            patternCombItem.Sel_Item.Content = string.Empty;
            patternCombItem.But_Args = CommUI.Get_Color_Sceenes_class_From_Brush(patternCombItem.Bg_Brush_Solid);
            patternCombItem.But_Args[0].Scenes_type = 0;
            patternCombItem.But_Args[1].Scenes_type = 0;
            patternCombItem.But_Args[0].TransitionsTeime = -1;
            patternCombItem.But_Args[1].TransitionsTeime = -1;
            patternCombItem.Bri = 9;
            patternCombItem.Speed = 9;
            patternCombItem.Type = 0;

            var allAreaInfo = allAreaInfo_.Select(areaInfo => new CommUI.Area_class(patternCombItem, areaInfo.Area_index, mBut_Style: null)).ToList();

            var allExtAreaInfo = allExtAreaInfo_.Select(areaInfo => new CommUI.Area_class(patternCombItem, areaInfo.Area_index, mBut_Style: null) { Ext_Area_id = areaInfo.Ext_Area_id }).ToList();

            allAreaInfo.AddRange(allExtAreaInfo);
            ledFun_.Set_Adv_mode(allAreaInfo, Run_Direct: true);
        }

    }
}
