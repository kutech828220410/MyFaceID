using System;
using ArcSoftFace.Utils;
namespace ArcSoftFace.SDKModels
{
    /// <summary>
    /// 多人脸检测结构体
    /// </summary>
    [Serializable]
    public struct ASF_MultiFaceInfo
    {
        public int faceNum; // 檢測到人臉數目
        public IntPtr faceRect; // 人臉框數組
        public IntPtr faceOrient; // 人臉角度
        public IntPtr faceID; // 人臉KEY
        public IntPtr faceDataInfoList; //多張人臉信息
        public IntPtr faceIsWithinBoundary; // 是否在邊界 ( 0: 溢出 ,1: 未溢出 )
        public IntPtr foreheadRect; // 額頭區域
        public ASF_FaceAttributeInfo faceAttributeInfo; // 人臉屬性
        public ASF_Face3DAngleInfo face3DAngleInfo; // 人臉角度
        public void Disopse()
        {
            MemoryUtil.FreeArray(faceRect, faceOrient, faceOrient, faceID, faceDataInfoList, faceIsWithinBoundary, foreheadRect);
            MemoryUtil.FreeArray(faceAttributeInfo.leftEyeOpen, faceAttributeInfo.mouthClose, faceAttributeInfo.rightEyeOpen, faceAttributeInfo.wearGlasses);
            MemoryUtil.FreeArray(face3DAngleInfo.pitch, face3DAngleInfo.roll, face3DAngleInfo.yaw);
        }
    }

    [Serializable]
    public struct ASF_FaceAttributeInfo
    {
        public IntPtr wearGlasses;
        public IntPtr leftEyeOpen;
        public IntPtr rightEyeOpen;
        public IntPtr mouthClose;
    }
    [Serializable]
    public struct ASF_Face3DAngleInfo
    {
        public IntPtr roll;
        public IntPtr yaw;
        public IntPtr pitch;
    }
}
