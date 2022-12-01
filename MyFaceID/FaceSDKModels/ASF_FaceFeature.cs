using System;
using ArcSoftFace.Utils;
using Basic;
namespace ArcSoftFace.SDKModels
{
    [Serializable]
    public struct ASF_FaceFeatureInfo
    {
        public int searchId { get; set; }
        public IntPtr feature { get; set; }//ASF_FaceFeature
    }
    [Serializable]
    public struct ASF_FaceFeature
    {
        public IntPtr feature { get; set; }
        public int featureSize { get; set; }
        public void Dispose()
        {
            MemoryUtil.Free(feature);
        }
    }
    [Serializable]
    unsafe public struct FaceFeature
    {
        public byte[] feature { get; set; }
        public int featureSize { get; set; }
        public void Dispose()
        {
            if (feature != null) feature = null;
        }


    }
    [Serializable]
    public enum ASF_RegisterOrNot :int
    {
        ASF_RECOGNITION = 0x0, //用于识别照人脸特征提取
        ASF_REGISTER = 0x1 //用于注册照人脸特征提取
    };
}
