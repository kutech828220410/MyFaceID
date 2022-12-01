using System;

namespace ArcSoftFace.SDKModels
{
    /// <summary>
    /// SDK版本信息结构体
    /// </summary>
    /// <summary>
    /// SDK版本信息结构体
    /// </summary>
    public struct ASF_VERSION
    {
        /// <summary>
        /// 版本号
        /// </summary>
        public IntPtr Version;

        /// <summary>
        /// 构建日期
        /// </summary>
        public IntPtr BuildDate;

        /// <summary>
        /// Copyright
        /// </summary>
        public IntPtr CopyRight;
        public IntPtr StartTime;
        public IntPtr EndTime;
    }
    public class SDKVersion
    {
        /// <summary>
        /// 版本号
        /// </summary>
        public string version { get; set; }

        /// <summary>
        /// 构建日期
        /// </summary>
        public string buildDate { get; set; }

        /// <summary>
        /// Copyright
        /// </summary>
        public string copyRight { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
    }
    public struct ASF_ActiveFileInfo
    {
        public IntPtr startTime;
        public IntPtr endTime;
        public IntPtr activeKey;
        public IntPtr platform;
        public IntPtr sdkType;
        public IntPtr appId;
        public IntPtr sdkKey;
        public IntPtr sdkVersion;
        public IntPtr fileVersion;

    }

    public class SDKActiveFileInfo
    {
        public string startTime { get; set; }
        public string endTime { get; set; }
        public string activeKey { get; set; }
        public string platform { get; set; }
        public string sdkType { get; set; }
        public string appId { get; set; }
        public string sdkKey { get; set; }
        public string sdkVersion { get; set; }
        public string fileVersion { get; set; }
    }


    public struct ASF_DeviceInfo
    {
        public IntPtr info;

    }
    public class SDKDeviceInfo
    {
        public string info { get; set; }
    }
}
