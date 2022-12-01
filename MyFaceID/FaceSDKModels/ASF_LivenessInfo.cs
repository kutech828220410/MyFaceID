using System;

namespace ArcSoftFace.SDKModels
{
    public struct ASF_LivenessInfo
    {
        /// 0：非真人；1：真人；-1：不确定；-2:传入人脸数>1；
        public IntPtr isLive;
        /// 结果集大小
        public int num;
    }
    public struct ASF_Liveness
    {
        /// 0：非真人；1：真人；-1：不确定；-2:传入人脸数>1；
        public int isLive;
        /// 结果集大小
        public int num;
    }
    public struct ASF_LivenessThreshold
    {
        public float thresholdmodel_BGR;
        public float thresholdmodel_IR;
        public float thresholdmodel_FQ;
    }
}
