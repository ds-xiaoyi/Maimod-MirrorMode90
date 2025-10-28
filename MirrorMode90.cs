using HarmonyLib;
using MelonLoader;
using Manager;
using Note;
using MAI2.Util;

[assembly: MelonInfo(typeof(MirrorMode90.MirrorMode90Mod), "MirrorMode90°", "1.0.0", "ds_xiaoyi")]
[assembly: MelonGame(null, null)]

namespace MirrorMode90
{
    //镜像模式 90° 旋转 Mod
    public class MirrorMode90Mod : MelonMod
    {
        private static readonly int[,] _extendedMirrorPos = new int[6, 8]
        {
            { 0, 1, 2, 3, 4, 5, 6, 7 },
            
            { 7, 6, 5, 4, 3, 2, 1, 0 },
            
            { 3, 2, 1, 0, 7, 6, 5, 4 },
            
            { 4, 5, 6, 7, 0, 1, 2, 3 },
            
            //4.CW
            { 2, 3, 4, 5, 6, 7, 0, 1 },
            
            //5.CCW
            { 6, 7, 0, 1, 2, 3, 4, 5 }
        };

        //TouchE
        private static readonly int[,] _extendedMirrorPosTouchE = new int[6, 8]
        {
            { 0, 1, 2, 3, 4, 5, 6, 7 },
            
            { 0, 7, 6, 5, 4, 3, 2, 1 },
            
            { 4, 3, 2, 1, 0, 7, 6, 5 },
            
            { 4, 5, 6, 7, 0, 1, 2, 3 },
            
            //4.CW
            { 2, 3, 4, 5, 6, 7, 0, 1 },
            
            //5.CCW
            { 6, 7, 0, 1, 2, 3, 4, 5 }
        };

        // Slide类型
        private static readonly SlideType[,] _extendedMirrorSlide = new SlideType[6, 14]
        {
            //0:NORMAL
            {
                SlideType.Slide_None,
                SlideType.Slide_Straight,
                SlideType.Slide_Circle_L,
                SlideType.Slide_Circle_R,
                SlideType.Slide_Curve_L,
                SlideType.Slide_Curve_R,
                SlideType.Slide_Thunder_L,
                SlideType.Slide_Thunder_R,
                SlideType.Slide_Corner,
                SlideType.Slide_Bend_L,
                SlideType.Slide_Bend_R,
                SlideType.Slide_Skip_L,
                SlideType.Slide_Skip_R,
                SlideType.Slide_Fan
            },
            //1:LR
            {
                SlideType.Slide_None,
                SlideType.Slide_Straight,
                SlideType.Slide_Circle_R,
                SlideType.Slide_Circle_L,
                SlideType.Slide_Curve_R,
                SlideType.Slide_Curve_L,
                SlideType.Slide_Thunder_R,
                SlideType.Slide_Thunder_L,
                SlideType.Slide_Corner,
                SlideType.Slide_Bend_R,
                SlideType.Slide_Bend_L,
                SlideType.Slide_Skip_R,
                SlideType.Slide_Skip_L,
                SlideType.Slide_Fan
            },
            //2:UD
            {
                SlideType.Slide_None,
                SlideType.Slide_Straight,
                SlideType.Slide_Circle_R,
                SlideType.Slide_Circle_L,
                SlideType.Slide_Curve_R,
                SlideType.Slide_Curve_L,
                SlideType.Slide_Thunder_R,
                SlideType.Slide_Thunder_L,
                SlideType.Slide_Corner,
                SlideType.Slide_Bend_R,
                SlideType.Slide_Bend_L,
                SlideType.Slide_Skip_R,
                SlideType.Slide_Skip_L,
                SlideType.Slide_Fan
            },
            //3:UDLR
            {
                SlideType.Slide_None,
                SlideType.Slide_Straight,
                SlideType.Slide_Circle_L,
                SlideType.Slide_Circle_R,
                SlideType.Slide_Curve_L,
                SlideType.Slide_Curve_R,
                SlideType.Slide_Thunder_L,
                SlideType.Slide_Thunder_R,
                SlideType.Slide_Corner,
                SlideType.Slide_Bend_L,
                SlideType.Slide_Bend_R,
                SlideType.Slide_Skip_L,
                SlideType.Slide_Skip_R,
                SlideType.Slide_Fan
            },
            //4:CW
            {
                SlideType.Slide_None,
                SlideType.Slide_Straight,
                SlideType.Slide_Circle_L,
                SlideType.Slide_Circle_R,
                SlideType.Slide_Curve_L,
                SlideType.Slide_Curve_R,
                SlideType.Slide_Thunder_L,
                SlideType.Slide_Thunder_R,
                SlideType.Slide_Corner,
                SlideType.Slide_Bend_L,
                SlideType.Slide_Bend_R,
                SlideType.Slide_Skip_L,
                SlideType.Slide_Skip_R,
                SlideType.Slide_Fan
            },
            //5:CCW
            {
                SlideType.Slide_None,
                SlideType.Slide_Straight,
                SlideType.Slide_Circle_L,
                SlideType.Slide_Circle_R,
                SlideType.Slide_Curve_L,
                SlideType.Slide_Curve_R,
                SlideType.Slide_Thunder_L,
                SlideType.Slide_Thunder_R,
                SlideType.Slide_Corner,
                SlideType.Slide_Bend_L,
                SlideType.Slide_Bend_R,
                SlideType.Slide_Skip_L,
                SlideType.Slide_Skip_R,
                SlideType.Slide_Fan
            }
        };

        public override void OnInitializeMelon()
        {
            LoggerInstance.Msg("MirrorMode90° Mod 已加载!");            
        }

        [HarmonyPatch(typeof(NotesReader), "ConvertMirrorPosition")]
        public class ConvertMirrorPositionPatch
        {
            [HarmonyPrefix]
            public static bool Prefix(NotesReader __instance, int position, ref int __result)
            {
                try
                {
                    var playerID = Traverse.Create(__instance).Field("_playerID").GetValue<int>();
                    
                    var mirrorMode = (int)Singleton<GamePlayManager>.Instance
                        .GetGameScore(playerID)
                        .UserOption
                        .MirrorMode;

                    //CW/CCW SKIP
                    if (mirrorMode >= 4 && mirrorMode < 6)
                    {
                        __result = _extendedMirrorPos[mirrorMode, position];
                        return false;
                    }
                }
                catch (System.Exception ex)
                {
                    MelonLogger.Error($"ConvertMirrorPosition 错误: {ex.Message}");
                }

                return true;
            }
        }
        //TOUCH E
        [HarmonyPatch(typeof(NotesReader), "ConvertMirrorTouchEPosition")]
        public class ConvertMirrorTouchEPositionPatch
        {
            [HarmonyPrefix]
            public static bool Prefix(NotesReader __instance, int position, ref int __result)
            {
                try
                {
                    var playerID = Traverse.Create(__instance).Field("_playerID").GetValue<int>();
                    
                    var mirrorMode = (int)Singleton<GamePlayManager>.Instance
                        .GetGameScore(playerID)
                        .UserOption
                        .MirrorMode;

                    if (mirrorMode >= 4 && mirrorMode < 6)
                    {
                        __result = _extendedMirrorPosTouchE[mirrorMode, position];
                        return false;
                    }
                }
                catch (System.Exception ex)
                {
                    MelonLogger.Error($"ConvertMirrorTouchEPosition 错误: {ex.Message}");
                }

                return true;
            }
        }

        [HarmonyPatch(typeof(NotesReader), "ConvertMirrorSlide")]
        public class ConvertMirrorSlidePatch
        {
            [HarmonyPrefix]
            public static bool Prefix(NotesReader __instance, SlideType slide, ref SlideType __result)
            {
                try
                {
                    var playerID = Traverse.Create(__instance).Field("_playerID").GetValue<int>();
                    
                    var mirrorMode = (int)Singleton<GamePlayManager>.Instance
                        .GetGameScore(playerID)
                        .UserOption
                        .MirrorMode;

                    if (mirrorMode >= 4 && mirrorMode < 6)
                    {
                        __result = _extendedMirrorSlide[mirrorMode, (int)slide];
                        return false;
                    }
                }
                catch (System.Exception ex)
                {
                    MelonLogger.Error($"ConvertMirrorSlide 错误: {ex.Message}");
                }

                return true;
            }
        }
    }
}

