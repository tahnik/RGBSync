using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Media;
using SelLEDControl;
using System.ComponentModel;

namespace SDKs
{
    public class LedCommand
    {
        [DefaultValue(-1)]
        public sbyte AreaId { get; set; }
        [DefaultValue(0)]
        public sbyte NewMode { get; set; }
        public Color NewColor { get; set; }
        [DefaultValue(5)]
        public sbyte Speed { get; set; }
        [DefaultValue(9)]
        public sbyte Bright { get; set; }
    }
    public class RGBFusion
    {

        private Comm_LED_Fun _ledFun;
        private bool _scanDone;
        private List<CommUI.Area_class> _allAreaInfo;
        private List<CommUI.Area_class> _allExtAreaInfo;
        private readonly sbyte _changeOperationDelay = 60;
        private bool _initialized;

        public bool IsInitialized()
        {
            return _ledFun != null && _initialized;
        }

        private void FillAllAreaInfo()
        {
            _allAreaInfo = GetAllAreaInfo();
        }


        private List<CommUI.Area_class> GetAllAreaInfo(int profileId = -1)
        {
            var str = "Pro1.xml";
            return CommUI.Inport_from_xml(str, But_Style: null);
        }

        private void Fill_ExtArea_info()
        {
            _allExtAreaInfo = GetAllExtAreaInfo();
        }

        private List<CommUI.Area_class> GetAllExtAreaInfo(int profileId = -1)
        {
            List<CommUI.Area_class> allExtAreaInfo;
            var str = "ExtPro1.xml";
            allExtAreaInfo = CommUI.Inport_from_xml(str, But_Style: null);
            return allExtAreaInfo;
        }

        public void Init()
        {
            DoInit();
        }

        public void SetAllAreas(object obj)
        {
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

            var allAreaInfo = _allAreaInfo.Select(areaInfo => new CommUI.Area_class(patternCombItem, areaInfo.Area_index, mBut_Style: null)).ToList();

            var allExtAreaInfo = _allExtAreaInfo.Select(areaInfo => new CommUI.Area_class(patternCombItem, areaInfo.Area_index, mBut_Style: null) { Ext_Area_id = areaInfo.Ext_Area_id }).ToList();

            allAreaInfo.AddRange(allExtAreaInfo);
            _ledFun.Set_Adv_mode(allAreaInfo, Run_Direct: true);
        }

        private void CallBackLedFunApplyScanPeripheralSuccess() => _scanDone = true;

        private void DoInit()
        {
            if (_ledFun != null)
                return;

            _ledFun = new Comm_LED_Fun(false);
            _ledFun.Apply_ScanPeriphera_Scuuess += CallBackLedFunApplyScanPeripheralSuccess;

            _ledFun.Ini_LED_Fun();

            _ledFun = CommUI.Get_Easy_Pattern_color_Key(_ledFun);

            _ledFun.LEd_Layout.Set_Support_Flag();
            do
            {
                Thread.Sleep(millisecondsTimeout: 10);
            }
            while (!_scanDone);

            _ledFun.Current_Mode = 0; // 1= Advanced 0 = Simple or Ez

            _ledFun.Led_Ezsetup_Obj.PoweronStatus = 1;
            _initialized = true;
            _ledFun.Set_Sync(false);
            FillAllAreaInfo();
            Fill_ExtArea_info();
        }
    }
}
