using MelonLoader;
using UnityEngine;
using Color = System.Drawing.Color;

namespace SGMOD.Core.Common;

public class CustomCameraID {
    public static void PrintCameraIDList() {
        WebCamDevice[] devices = WebCamTexture.devices;
        string cameraList;
        if (devices.Length == 0) {
            MelonLogger.Warning("\n没有检测到摄像头设备！" +
                                "\nNo camera device detected!");
            return;
        }

        cameraList = "\nCamera List" + "\n========================================\n";
        for (int i = 0; i < devices.Length; i++) {
            cameraList += "ID:" + i + " " +devices[i].name + "\n";
        }
        cameraList += "========================================";
        MelonLogger.Msg(Color.Chartreuse,cameraList);
    
    }
}