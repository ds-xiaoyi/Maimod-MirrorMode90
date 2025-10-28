using HarmonyLib;
using MelonLoader;
using Manager.UserDatas;
using DB;
using System.Reflection;

namespace MirrorMode90UI
{
    //用于游戏设置镜像UI
    public class MirrorMode90UIMod : MelonMod
    {
        //镜像类型表
        private static OptionMirrorTableRecord[] _extendedRecords = null;

        public override void OnInitializeMelon()
        {
            CreateExtendedRecords();
        }

        private static void CreateExtendedRecords()
        {
            _extendedRecords = new OptionMirrorTableRecord[6];
            
            // 0: NORMAL
            _extendedRecords[0] = new OptionMirrorTableRecord(
                0, "Normal", "OFF", "", "通常の配置です", "", "UI_OPT_B_06_01", 1);
            
            // 1: LR
            _extendedRecords[1] = new OptionMirrorTableRecord(
                1, "LR", "⇄", "", "左右反転します", "", "UI_OPT_B_06_02", 0);
            
            // 2: UD
            _extendedRecords[2] = new OptionMirrorTableRecord(
                2, "UD", "⇅", "", "上下反転します", "", "UI_OPT_B_06_04", 0);
            
            // 3: UDLR
            _extendedRecords[3] = new OptionMirrorTableRecord(
                3, "UDLR", "↻", "", "180°回転します", "", "UI_OPT_B_06_03", 0);
            
            // 4: CW90
            _extendedRecords[4] = new OptionMirrorTableRecord(
                4, "CW90", "↻90°", "", "右へ90°回転します", "", "UI_OPT_B_06_03", 0);
            
            // 5: CCW90
            _extendedRecords[5] = new OptionMirrorTableRecord(
                5, "CCW90", "↺90°", "", "左へ90°回転します", "", "UI_OPT_B_06_03", 0);
        }

        [HarmonyPatch(typeof(OptionMirrorIDEnum), "GetEnd", new System.Type[] { })]
        public class GetEndStaticPatch
        {
            [HarmonyPostfix]
            public static void Postfix(ref int __result)
            {
                __result = 6;
            }
        }

        [HarmonyPatch(typeof(OptionMirrorIDEnum), "GetEnd", new System.Type[] { typeof(OptionMirrorID) })]
        public class GetEndInstancePatch
        {
            [HarmonyPostfix]
            public static void Postfix(ref int __result)
            {
                __result = 6;
            }
        }


        [HarmonyPatch(typeof(OptionMirrorIDEnum), "IsValid")]
        public class IsValidPatch
        {
            [HarmonyPostfix]
            public static void Postfix(OptionMirrorID self, ref bool __result)
            {
                if (!__result && ((int)self == 4 || (int)self == 5))
                {
                    __result = true;
                }
            }
        }

        [HarmonyPatch(typeof(OptionMirrorIDEnum), "GetEnumName")]
        public class GetEnumNamePatch
        {
            [HarmonyPrefix]
            public static bool Prefix(OptionMirrorID self, ref string __result)
            {
                int index = (int)self;
                
                if (_extendedRecords == null)
                {
                    CreateExtendedRecords();
                }
                
                if (index >= 0 && index < 6)
                {
                    __result = _extendedRecords[index].EnumName;
                    return false;
                }
                
                __result = "";
                return false;
            }
        }

        [HarmonyPatch(typeof(OptionMirrorIDEnum), "GetName")]
        public class GetNamePatch
        {
            [HarmonyPrefix]
            public static bool Prefix(OptionMirrorID self, ref string __result)
            {
                int index = (int)self;
                
                if (_extendedRecords == null)
                {
                    CreateExtendedRecords();
                }
                
                if (index >= 0 && index < 6)
                {
                    __result = _extendedRecords[index].Name;
                    return false;
                }
                
                __result = "";
                return false;
            }
        }

        [HarmonyPatch(typeof(OptionMirrorIDEnum), "GetDetail")]
        public class GetDetailPatch
        {
            [HarmonyPrefix]
            public static bool Prefix(OptionMirrorID self, ref string __result)
            {
                int index = (int)self;
                
                if (_extendedRecords == null)
                {
                    CreateExtendedRecords();
                }
                
                if (index >= 0 && index < 6)
                {
                    __result = _extendedRecords[index].Detail;
                    return false;
                }
                
                __result = "";
                return false;
            }
        }

        [HarmonyPatch(typeof(OptionMirrorIDEnum), "GetFilePath")]
        public class GetFilePathPatch
        {
            [HarmonyPrefix]
            public static bool Prefix(OptionMirrorID self, ref string __result)
            {
                int index = (int)self;
                
                if (_extendedRecords == null)
                {
                    CreateExtendedRecords();
                }
                
                if (index >= 0 && index < 6)
                {
                    __result = _extendedRecords[index].FilePath;
                    return false;
                }
                
                __result = "";
                return false;
            }
        }

        [HarmonyPatch(typeof(OptionMirrorIDEnum), "IsDefault")]
        public class IsDefaultPatch
        {
            [HarmonyPrefix]
            public static bool Prefix(OptionMirrorID self, ref bool __result)
            {
                int index = (int)self;
                
                if (_extendedRecords == null)
                {
                    CreateExtendedRecords();
                }
                
                if (index >= 0 && index < 6)
                {
                    __result = _extendedRecords[index].isDefault;
                    return false;
                }
                
                __result = false;
                return false;
            }
        }

        [HarmonyPatch(typeof(OptionMirrorIDEnum), "GetEnumValue")]
        public class GetEnumValuePatch
        {
            [HarmonyPrefix]
            public static bool Prefix(OptionMirrorID self, ref int __result)
            {
                int index = (int)self;
                
                if (_extendedRecords == null)
                {
                    CreateExtendedRecords();
                }
                
                if (index >= 0 && index < 6)
                {
                    __result = _extendedRecords[index].EnumValue;
                    return false;
                }
                
                __result = 0;
                return false;
            }
        }

        [HarmonyPatch(typeof(OptionMirrorIDEnum), "GetNameEx")]
        public class GetNameExPatch
        {
            [HarmonyPrefix]
            public static bool Prefix(OptionMirrorID self, ref string __result)
            {
                int index = (int)self;
                
                if (_extendedRecords == null)
                {
                    CreateExtendedRecords();
                }
                
                if (index >= 0 && index < 6)
                {
                    __result = _extendedRecords[index].NameEx;
                    return false;
                }
                
                __result = "";
                return false;
            }
        }

        [HarmonyPatch(typeof(OptionMirrorIDEnum), "GetDetailEx")]
        public class GetDetailExPatch
        {
            [HarmonyPrefix]
            public static bool Prefix(OptionMirrorID self, ref string __result)
            {
                int index = (int)self;
                
                if (_extendedRecords == null)
                {
                    CreateExtendedRecords();
                }
                
                if (index >= 0 && index < 6)
                {
                    __result = _extendedRecords[index].DetailEx;
                    return false;
                }
                
                __result = "";
                return false;
            }
        }

        [HarmonyPatch(typeof(OptionMirrorIDEnum), "Clamp")]
        public class ClampPatch
        {
            [HarmonyPrefix]
            public static bool Prefix(ref OptionMirrorID self)
            {
                int value = (int)self;
                if (value < 0)
                {
                    self = OptionMirrorID.Normal;
                }
                else if (value >= 6)
                {
                    self = (OptionMirrorID)5;
                }
                return false;
            }
        }

        [HarmonyPatch(typeof(UserOption), "MirrorMode", MethodType.Setter)]
        public class MirrorModeSetterPatch
        {
            [HarmonyPrefix]
            public static bool Prefix(UserOption __instance, ref OptionMirrorID value)
            {
                int intValue = (int)value;
                if (intValue >= 0 && intValue < 6)
                {
                    return true;
                }
                return false;
            }
        }

        [HarmonyPatch(typeof(UserOption), "AddOption")]
        public class AddOptionPatch
        {
            [HarmonyPrefix]
            public static bool Prefix(UserOption __instance, OptionCategoryID category, int currentOptionIndex)
            {
                if (category == OptionCategoryID.GameSetting && currentOptionIndex == 1)
                {
                    OptionMirrorID currentMode = __instance.MirrorMode;
                    int modeValue = (int)currentMode;
                    
                    modeValue++;
                    if (modeValue >= 6)
                    {
                        modeValue = 0;
                    }
                    
                    __instance.MirrorMode = (OptionMirrorID)modeValue;
                    
                    return false;
                }
                
                return true;
            }
        }

        [HarmonyPatch(typeof(UserOption), "SubOption")]
        public class SubOptionPatch
        {
            [HarmonyPrefix]
            public static bool Prefix(UserOption __instance, OptionCategoryID category, int currentOptionIndex)
            {
                if (category == OptionCategoryID.GameSetting && currentOptionIndex == 1)
                {
                    OptionMirrorID currentMode = __instance.MirrorMode;
                    int modeValue = (int)currentMode;
                    
                    modeValue--;
                    if (modeValue < 0)
                    {
                        modeValue = 5;
                    }
                    
                    __instance.MirrorMode = (OptionMirrorID)modeValue;
                    
                    return false;
                }
                
                return true;
            }
        }
    }
}

