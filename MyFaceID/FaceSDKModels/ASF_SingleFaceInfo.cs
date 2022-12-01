using System;
using ArcSoftFace.Utils;
namespace ArcSoftFace.SDKModels
{
    [Serializable]
    public struct ASF_SingleFaceInfo
    {
        public MRECT faceRect;
        public int faceOrient; // 人脸角度
        public ASF_FaceDataInfo faceDataInfo;
    }
    [Serializable]
    public class ASF_SingleFace
    {
        public int IsLive = -99;
        public int CompareIndex = -1;
        public double Score = 0;
        

        public int faceID = -1; //人臉序號
        public int faceIsWithinBoundary;// 是否在邊界 ( 0: 溢出 ,1: 未溢出 )
        public MRECT faceRect; // 人臉框
        public int faceOrient; // 人脸角度
        public ASF_FaceDataInfo faceDataInfo; // 單張人臉數據
        public MRECT foreheadRect; // 單張人臉數據
        public int wearGlasses; // 戴眼鏡
        public int leftEyeOpen; // 左眼睜開
        public int rightEyeOpen; // 右眼睜開
        public int mouthClose; // 嘴巴閉上
        public float roll; // 横滚角
        public float yaw; // 偏航角
        public float pitch; // 俯仰角
        public int area;//面積

        public void Dispose()
        {
            MemoryUtil.Free(faceDataInfo.data);
        }

    }
    [Serializable]
    public struct ASF_FaceDataInfo
    {
        public IntPtr data;
        public int dataSize;
    }

}
