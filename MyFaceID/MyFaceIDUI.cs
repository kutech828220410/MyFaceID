using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using Basic;
using MyUI;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using ArcSoftFace.SDKModels;
using ArcSoftFace.SDKUtil;
using ArcSoftFace.Utils;
using ArcSoftFace.Entity;
using System.Runtime.InteropServices;
namespace MyFaceID
{



    public partial class MyFaceIDUI : UserControl
    {

        static bool IsConsoleWrtie = false;
        private List<ASF_FaceFeature> imagesFeatureList = new List<ASF_FaceFeature>();
        private string appID = "";
        private string sdkKey = "";
        private string activeKey = "";
        private string offline_dat = "";
        private int cameraIndex = 0;
        private bool horizontal = false;
        private bool vertical = false;
        private int frameWidth = 1920;
        private int frameHeight = 1080;
        private TxRotateType rotateType = TxRotateType._0;
        public enum TxRotateType
        {
            _0 = 0,
            _90 = 90,
            _180 = 180,
            _270 = 270,

        }

        #region 自訂屬性
        [ReadOnly(false), Browsable(true), Category("Offline file path"), Description(""), DefaultValue("")]
        public string Offline_dat { get => offline_dat; set => offline_dat = value; }
        [ReadOnly(false), Browsable(true), Category("SN Code"), Description(""), DefaultValue("")]
        public string AppID { get => appID; set => appID = value; }
        [ReadOnly(false), Browsable(true), Category("SN Code"), Description(""), DefaultValue("")]
        public string SdkKey { get => sdkKey; set => sdkKey = value; }
        [ReadOnly(false), Browsable(true), Category("SN Code"), Description(""), DefaultValue("")]
        public string ActiveKey { get => activeKey; set => activeKey = value; }
        [ReadOnly(false), Browsable(true), Category("Camera Config"), Description(""), DefaultValue("")]
        public int CameraIndex { get => cameraIndex; set => cameraIndex = value; }
        [ReadOnly(false), Browsable(true), Category("Camera Config"), Description(""), DefaultValue("")]
        public bool Horizontal { get => horizontal; set => horizontal = value; }
        [ReadOnly(false), Browsable(true), Category("Camera Config"), Description(""), DefaultValue("")]
        public bool Vertical { get => vertical; set => vertical = value; }
        [ReadOnly(false), Browsable(true), Category("Camera Config"), Description(""), DefaultValue("")]
        public TxRotateType RotateType { get => rotateType; set => rotateType = value; }
        [ReadOnly(false), Browsable(true), Category("Camera Config"), Description(""), DefaultValue("")]
        public int FrameWidth { get => frameWidth; set => frameWidth = value; }
        [ReadOnly(false), Browsable(true), Category("Camera Config"), Description(""), DefaultValue("")]
        public int FrameHeight { get => frameHeight; set => frameHeight = value; }
        #endregion
        public double Zoom_X
        {
            get
            {
                return this.canvas.ZoomX;
            }
        }
        public double Zoom_Y
        {
            get
            {
                return this.canvas.ZoomY;
            }
        }
        public List<ASF_FaceFeature> ImagesFeatureList { get => imagesFeatureList; set => imagesFeatureList = value; }
        public bool ShowRegisterROI = false;
        private string version = "";
        public string Version
        {
            get
            {
                return version;
            }
            private set
            {
                version = value;
            }
        }


        public Capture Camera;


        public class LivenessClass
        {
            public delegate void FaceFeatureExtractEventHandler(FaceFeature faceFeature, ASF_SingleFace aSF_SingleFace);
            public event FaceFeatureExtractEventHandler FaceFeatureExtractEvent;
            public delegate void StartCompareEventHandler(FaceFeature faceFeature, ref double Score, ref int CompareIndex);
            public event StartCompareEventHandler StartCompareEvent;

            private MyTimer MyTimer_cycleTime = new MyTimer();
            private IntPtr pLivenessEngine = IntPtr.Zero;
            private MyThread myThread;
            public bool IsBusy
            {
                get
                {
                    return this.myThread.IsBusy;
                }
            }
            private bool isDone = true;
            public bool IsDone
            {
                get
                {
                    return isDone;
                }
                private set
                {
                    isDone = value;
                }
            }
            private ImageInfo imageInfo_DetectFace;
            private ImageInfo imageInfo_Liveness;
            private ImageInfo imageInfo_FaceFeature;
            private ASF_MultiFaceInfo aSF_MultiFaceInfo;
            public List<ASF_SingleFace> list_ASF_SingleFace = new List<ASF_SingleFace>();
            public float Threshold = 0.95F;
            public ASF_SingleFace ASF_SingleFace_Result = new ASF_SingleFace();
            private ASF_RegisterOrNot RegisterOrNot = ASF_RegisterOrNot.ASF_RECOGNITION;
            public FaceFeature FaceFeature;
            public string FaceFeatureJson = "";


            public void Init(IntPtr Engine)
            {
                this.myThread = new MyThread();
                this.myThread.AutoRun(false);
                this.myThread.Add_Method(sub_program);
                this.myThread.IsBackGround = true;
                this.pLivenessEngine = Engine;
            }
            public void Trigger(Bitmap bitmap)
            {
                this.ASF_SingleFace_Result = new ASF_SingleFace();
                this.FaceFeatureJson = "";
                this.imageInfo_DetectFace = ImageUtil.ReadBMP(bitmap);
                this.imageInfo_Liveness = ImageUtil.ReadBMP(bitmap);
                this.imageInfo_FaceFeature = ImageUtil.ReadBMP(bitmap);
                this.myThread.Trigger();
            }
            public ASF_SingleFace GetMaxFace()
            {
                return MyFaceIDUI.GetMaxFace(this.list_ASF_SingleFace);
            }

            private void sub_program()
            {
                IsDone = false;
                this.MyTimer_cycleTime.TickStop();
                this.MyTimer_cycleTime.StartTickTime(500000);
                this.aSF_MultiFaceInfo = DetectFace(this.pLivenessEngine, imageInfo_DetectFace);
                this.list_ASF_SingleFace = LivenessInfo(pLivenessEngine, imageInfo_Liveness, aSF_MultiFaceInfo);
                this.ASF_SingleFace_Result = this.GetMaxFace();
                this.FaceFeature = FaceFeatureExtract(this.pLivenessEngine, imageInfo_FaceFeature, this.ASF_SingleFace_Result, RegisterOrNot, false);
                if (this.FaceFeatureExtractEvent != null) FaceFeatureExtractEvent(this.FaceFeature , this.ASF_SingleFace_Result);
                if (this.StartCompareEvent != null) StartCompareEvent(this.FaceFeature, ref this.ASF_SingleFace_Result.Score, ref this.ASF_SingleFace_Result.CompareIndex);
                if (this.ASF_SingleFace_Result.Score <= Threshold)
                {
                    this.ASF_SingleFace_Result.CompareIndex = -1;
                }
               if(IsConsoleWrtie) Console.WriteLine(string.Format("LivenessClass Time : {0} ms", this.MyTimer_cycleTime.GetTickTime().ToString("0.000")));
                if (IsConsoleWrtie) Console.WriteLine(string.Format("LivenessClass Score : {0} %", (this.ASF_SingleFace_Result.Score * 100).ToString("0.000")));
                if (IsConsoleWrtie) Console.WriteLine(string.Format("LivenessClass CompareIndex : {0}", (this.ASF_SingleFace_Result.CompareIndex).ToString("")));

                this.FaceFeature.Dispose();
                IsDone = true;
            }

        }

        private void LivenessClass_StartCompareEvent(FaceFeature faceFeature, ref double Score, ref int CompareIndex)
        {
            float score = 0F;
            CompareIndex = this.ASF_FeatureCompare(this.pLivenessEngine, faceFeature.ToASF_FaceFeature().ToPointer(), out score, 0.7F);
            Score = score;
        }

        public LivenessClass livenessClass = new LivenessClass();
        private List<ASF_SingleFace> List_ASF_SingleFace = new List<ASF_SingleFace>();
        public IntPtr pLivenessEngine = IntPtr.Zero;
        public IntPtr pVideoRGBImageEngine = IntPtr.Zero;
        private HsBase.ImageC24 ImageC24_ProcessFrame;
        private HsBase.ROIC24 ROIC24_Register;
        private void MyFaceIDUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.dispose();
        }
        private void dispose()
        {
            if (pLivenessEngine != IntPtr.Zero) ASFFunctions.ASFUninitEngine(pLivenessEngine);
            if (pVideoRGBImageEngine != IntPtr.Zero) ASFFunctions.ASFUninitEngine(pVideoRGBImageEngine);
        }

        public MyFaceIDUI()
        {
            InitializeComponent();
        }
        public void Init(Capture Camera)
        {
            this.Init(Camera, false);
        }
        public void Init(Capture Camera, bool InitEngie)
        {
            this.Camera = Camera;
            this.InitFaceEngines();
            this.ASFSetLivenessParam(pLivenessEngine, 0.5F, 0.5F, 0.65F);

            this.Init();
            this.livenessClass.Init(this.pLivenessEngine);
            this.livenessClass.StartCompareEvent += LivenessClass_StartCompareEvent;
        }
        public void Init(int CamNum)
        {

            this.Camera = new Capture(CamNum);
            this.Camera.SetCaptureProperty(CapProp.FrameWidth, this.FrameWidth);
            this.Camera.SetCaptureProperty(CapProp.FrameHeight, this.FrameHeight);
            this.InitFaceEngines();
            this.ASFSetLivenessParam(pLivenessEngine, 0.5F, 0.5F, 0.65F);

            this.Init();
            this.livenessClass.Init(this.pLivenessEngine);
            this.livenessClass.StartCompareEvent += LivenessClass_StartCompareEvent;
        }



        private void Init()
        {
            HsBasic.Memory.StackMemory = 1000;
            HsBasic.Memory.StackNum = 1;
            HsBasic.Memory.MemoryInit();
            this.ImageC24_ProcessFrame = new HsBase.ImageC24();
            this.ROIC24_Register = new HsBase.ROIC24();

            this.canvas.CanvasWidth = this.canvas.Width;
            this.canvas.CanvasHeight = this.canvas.Height;
            this.canvas.SetScale(this.Camera.Width, this.Camera.Height);
            this.ROIC24_Register.SetDefultEvent(this.canvas, this.canvas.ZoomX, this.canvas.ZoomY);
            this.ROIC24_Register.OrgX = 50;
            this.ROIC24_Register.OrgY = 50;

            this.FindForm().FormClosing += MyFaceIDUI_FormClosing;
        }
        public bool InitFaceEngines()
        {
            SDKVersion sDKVersion = this.ASFGetVersion();
            this.Version = sDKVersion.version;
            int retCode = 0;
            int retCode_LivenessEngine = 0;
            int retCode_RGBImageEngine = 0;
            try
            {
                if (Offline_dat.StringIsEmpty())
                {
                    retCode = ASFFunctions.ASFOnlineActivation(appID, sdkKey, activeKey);
                    if (retCode == 90114) retCode = 0;
                }
                else
                {
                    retCode = ASFFunctions.ASFOfflineActivation(Offline_dat);
                    if (retCode == 90114) retCode = 0;
                }
            }
            catch (Exception ex)
            {
                //禁用相关功能按钮
                if (ex.Message.Contains("無法載入 DLL"))
                {
                    MessageBox.Show("請將sdk相關DLL放入bin對應的x86或x64下的文件夾中!", "Asterisk", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    MessageBox.Show("激活引擎失敗!", "Asterisk", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                return false;
            }
            //初始化引擎
            int combinedMask = 0;
            combinedMask = FaceEngineMask.ASF_FACE_DETECT | FaceEngineMask.ASF_FACERECOGNITION | FaceEngineMask.ASF_LIVENESS;
            retCode_LivenessEngine = ASFFunctions.ASFInitEngine(DetectionMode.ASF_DETECT_MODE_IMAGE, ASF_OrientPriority.ASF_OP_0_ONLY, 5, combinedMask, ref pLivenessEngine);
            combinedMask = FaceEngineMask.ASF_FACE_DETECT | FaceEngineMask.ASF_FACERECOGNITION | FaceEngineMask.ASF_LIVENESS | FaceEngineMask.ASF_MASKDETECT;
            retCode_RGBImageEngine = ASFFunctions.ASFInitEngine(DetectionMode.ASF_DETECT_MODE_IMAGE, ASF_OrientPriority.ASF_OP_0_ONLY, 5, combinedMask, ref pVideoRGBImageEngine);
            if (retCode != 0 || retCode_LivenessEngine != 0 || retCode_RGBImageEngine != 0)
            {
                string code = "";
                if (retCode != 0) code += String.Format("初始化激活失敗! 錯誤碼:{0}\n\r", retCode.ToString());
                if (retCode_LivenessEngine != 0) code += String.Format("VideoEngine啟用失敗! 錯誤碼:{0}\n\r", retCode_LivenessEngine.ToString());
                if (retCode_RGBImageEngine != 0) code += String.Format("RGBImageEngine啟用失敗! 錯誤碼:{0}\n\r", retCode_RGBImageEngine.ToString());
                MessageBox.Show(code, "Asterisk", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }
            return true;
        }

        public Bitmap GetBitmapFromCam()
        {
            Mat mat = this.Camera.QueryFrame();
            Image<Rgb, byte> frameEmguCv = new Image<Rgb, byte>(mat.Bitmap);
            Bitmap frame_Bitmap = frameEmguCv.Bitmap;
            if (RotateType != TxRotateType._0)
            {
                frameEmguCv = new Image<Rgb, byte>(frame_Bitmap);
                frame_Bitmap = frameEmguCv.Rotate((int)RotateType, new PointF(frame_Bitmap.Width / 2, frame_Bitmap.Height / 2), Inter.Cubic, new Rgb(0, 0, 0), false).Bitmap;
                frameEmguCv.Dispose();
            }
            if (Horizontal)
            {
                frameEmguCv = new Image<Rgb, byte>(frame_Bitmap);
                frame_Bitmap = frameEmguCv.Flip(FlipType.Horizontal).Bitmap;
                frameEmguCv.Dispose();
            }
            if (Vertical)
            {
                frameEmguCv = new Image<Rgb, byte>(frame_Bitmap);
                frame_Bitmap = frameEmguCv.Flip(FlipType.Vertical).Bitmap;
                frameEmguCv.Dispose();
            }
            mat.Dispose();
            mat = null;
            return frame_Bitmap;
        }
        public void DrawBitmapToCanvas(Bitmap bitmap)
        {
            using (Graphics g = GetCanvasGraphics())
            {
                using (Bitmap bitmap_scale = ImageUtil.ScaleImage(bitmap, (float)(this.canvas.ZoomX), (float)(this.canvas.ZoomY)))
                {
                    try
                    {
                        g.DrawImage(bitmap_scale, new PointF());
                    }
                    catch
                    {

                    }
                }

                this.canvas.ReleaseHDC();
            }
            //ImageC24_ProcessFrame.ReadBMP(bitmap);
            //ImageC24_ProcessFrame.DrawImage(this.canvas, this.canvas.ZoomX, this.canvas.ZoomY);
        }
        public Graphics GetCanvasGraphics()
        {

            return Graphics.FromHdc(this.canvas.GetBimapHDC());
        }

        public SDKVersion ASFGetVersion()
        {
            SDKVersion version = new SDKVersion();
            ASF_VERSION asfVersion = ASFFunctions.ASFGetVersion();
            version.version = Marshal.PtrToStringAnsi(asfVersion.Version);
            version.buildDate = Marshal.PtrToStringAnsi(asfVersion.BuildDate);
            version.copyRight = Marshal.PtrToStringAnsi(asfVersion.CopyRight);
            version.startTime = Marshal.PtrToStringAnsi(asfVersion.StartTime);
            version.endTime = Marshal.PtrToStringAnsi(asfVersion.EndTime);
            return version;
        }
        public ASF_MultiFaceInfo DetectFace(Bitmap bitmap)
        {
            ImageInfo imageInfo = ImageUtil.ReadBMP(bitmap);
            ASF_MultiFaceInfo aSF_MultiFaceInfo = DetectFace(pVideoRGBImageEngine, imageInfo);
            this.List_ASF_SingleFace = aSF_MultiFaceInfo.ToSingleFaceInfo();
            return aSF_MultiFaceInfo;
        }
        static public ASF_MultiFaceInfo DetectFace(IntPtr pEngine, ImageInfo imageInfo)
        {
            ASF_MultiFaceInfo aSF_MultiFaceInfo = ASFDetectFace(pEngine, imageInfo);
            MemoryUtil.Free(imageInfo.imgData);
            return aSF_MultiFaceInfo;

        }
        static public ASF_SingleFace GetMaxFace(List<ASF_SingleFace> list_ASF_SingleFace)
        {
            ASF_SingleFace ASF_SingleFace = new ASF_SingleFace();
            int maxArea = 0;
            int index = -1;
            for (int i = 0; i < list_ASF_SingleFace.Count; i++)
            {
                int area = list_ASF_SingleFace[i].area;
                if (maxArea <= area)
                {
                    maxArea = area;
                    index = i;
                }
            }
            if (index != -1)
            {
                ASF_SingleFace = list_ASF_SingleFace[index];
            }
            return ASF_SingleFace;
        }
        public List<ASF_SingleFace> LivenessInfo(Bitmap bitmap, ASF_MultiFaceInfo aSF_MultiFaceInfo)
        {
            return LivenessInfo(pVideoRGBImageEngine, bitmap, aSF_MultiFaceInfo);
        }
        static public List<ASF_SingleFace> LivenessInfo(IntPtr pEngine, Bitmap bitmap, ASF_MultiFaceInfo aSF_MultiFaceInfo)
        {
            ImageInfo imageInfo = ImageUtil.ReadBMP(bitmap);
            List<ASF_SingleFace> list_aSF_SingleFaces = LivenessInfo(pEngine, imageInfo, aSF_MultiFaceInfo);

            return list_aSF_SingleFaces;
        }
        static public List<ASF_SingleFace> LivenessInfo(IntPtr pEngine, ImageInfo imageInfo, List<ASF_SingleFace> list_ASF_SingleFace)
        {
            ASF_MultiFaceInfo aSF_MultiFaceInfo = list_ASF_SingleFace.ToASF_MultiFaceInfo();
            if (aSF_MultiFaceInfo.faceNum > 0)
            {
                list_ASF_SingleFace = LivenessInfo(pEngine, imageInfo, aSF_MultiFaceInfo);
                // aSF_MultiFaceInfo.Disopse();
            }

            // aSF_MultiFaceInfo.Disopse();
            return list_ASF_SingleFace;
        }
        static public List<ASF_SingleFace> LivenessInfo(IntPtr pEngine, ImageInfo imageInfo, ASF_MultiFaceInfo aSF_MultiFaceInfo)
        {
            ASF_LivenessInfo aSF_LivenessInfo = ASFLivenessInfo(pEngine, imageInfo, aSF_MultiFaceInfo);
            MemoryUtil.Free(imageInfo.imgData);
            int num_Live = aSF_LivenessInfo.num;
            List<int> list_isLive = new List<int>();
            int isLive = 0;
            for (int i = 0; i < num_Live; i++)
            {
                isLive = MemoryUtil.PtrToStructure<int>(aSF_LivenessInfo.isLive + MemoryUtil.SizeOf<int>() * i);
                list_isLive.Add(isLive);
            }
            List<ASF_SingleFace> list_aSF_SingleFaces = aSF_MultiFaceInfo.ToSingleFaceInfo();
            for (int i = 0; i < list_aSF_SingleFaces.Count; i++)
            {
                list_aSF_SingleFaces[i].IsLive = list_isLive[i];
            }
            return list_aSF_SingleFaces;
        }

        public FaceFeature FaceFeatureExtract(IntPtr ImageEngine, Bitmap bitmap, ASF_MultiFaceInfo multiFaceInfo, ASF_RegisterOrNot registerOrNot, int faceIndex, bool IsMask)
        {
            FaceFeature faceFeature = new FaceFeature();
            List<ASF_SingleFace> list_ASF_SingleFace = multiFaceInfo.ToSingleFaceInfo();
            if (faceIndex >= list_ASF_SingleFace.Count) return faceFeature;
            faceFeature = FaceFeatureExtract(ImageEngine, bitmap, list_ASF_SingleFace[faceIndex], registerOrNot, IsMask);
            return faceFeature;
        }
        static public FaceFeature FaceFeatureExtract(IntPtr ImageEngine, Bitmap bitmap, ASF_SingleFace singleFace, ASF_RegisterOrNot registerOrNot, bool IsMask)
        {
            ImageInfo imageInfo = ImageUtil.ReadBMP(bitmap);
            return FaceFeatureExtract(ImageEngine, imageInfo, singleFace, registerOrNot, IsMask);
        }
        static public FaceFeature FaceFeatureExtract(IntPtr ImageEngine, ImageInfo imageInfo, ASF_SingleFace singleFace, ASF_RegisterOrNot registerOrNot, bool IsMask)
        {
            FaceFeature faceFeature = new FaceFeature();
            ASF_SingleFaceInfo singleFaceInfo = new ASF_SingleFaceInfo();
            IntPtr pSIngleFaceInfo = MemoryUtil.Malloc(MemoryUtil.SizeOf<ASF_SingleFaceInfo>());
            singleFaceInfo.faceRect = singleFace.faceRect;
            singleFaceInfo.faceOrient = singleFace.faceOrient;
            singleFaceInfo.faceDataInfo = singleFace.faceDataInfo;
            faceFeature = ASFFaceFeatureExtract(ImageEngine, imageInfo, singleFaceInfo, registerOrNot, IsMask);
            MemoryUtil.Free(imageInfo.imgData);
            return faceFeature;
        }
        public Bitmap GetRegisterROIBitmap()
        {
            Bitmap bitmap = new Bitmap(this.ROIC24_Register.Width, this.ROIC24_Register.Height);
            this.ROIC24_Register.ParentHandle = ImageC24_ProcessFrame.VegaHandle;
            this.ROIC24_Register.GetBitmap(ref bitmap);
            if (bitmap.Width > 1536 || bitmap.Height > 1536)
            {
                bitmap = ImageUtil.ScaleImage(bitmap, 1536, 1536);
            }
            if (bitmap.Width % 4 != 0 || bitmap.Height % 4 != 0)
            {
                bitmap = ImageUtil.ScaleImage(bitmap, bitmap.Width - (bitmap.Width % 4), bitmap.Height - (bitmap.Height % 4));
            }
            return bitmap;
        }
        public bool RegisterFaceList(ASF_FaceFeature feature)
        {
            imagesFeatureList.Add(feature);
            return true;
        }
        public bool RegisterFaceList(Bitmap bitmap)
        {
            ImageInfo imageInfo = ImageUtil.ReadBMP(bitmap);
            ASF_MultiFaceInfo aSF_MultiFaceInfo = DetectFace(pVideoRGBImageEngine, imageInfo);
            if (aSF_MultiFaceInfo.faceNum <= 0)
            {
                return false;
            }
            ASF_SingleFace aSF_SingleFace = GetMaxFace(aSF_MultiFaceInfo.ToSingleFaceInfo());
            imageInfo = ImageUtil.ReadBMP(bitmap);
            FaceFeature faceFeature = FaceFeatureExtract(pVideoRGBImageEngine, imageInfo, aSF_SingleFace, ASF_RegisterOrNot.ASF_REGISTER, false);
            MemoryUtil.Free(imageInfo.imgData);
            imagesFeatureList.Add(faceFeature.ToASF_FaceFeature());

            return true;
        }
        public void ClearFaceList()
        {
            for (int i = 0; i < imagesFeatureList.Count; i++)
            {
                MemoryUtil.Free(imagesFeatureList[i].feature);
            }
            imagesFeatureList.Clear();
        }

        public void Draw_FaceRect(ASF_SingleFace ASF_SingleFace, Color color, float pen_width)
        {
            using (Graphics g = GetCanvasGraphics())
            {
                MRECT rect = ASF_SingleFace.faceRect;
                float x = rect.left;
                float width = rect.right - x;
                float y = rect.top;
                float height = rect.bottom - y;
                Rectangle rectangle = new Rectangle((int)x, (int)y, (int)width, (int)height);
                x = (float)(rectangle.X * this.Zoom_X);
                width = (float)(rectangle.Width * this.Zoom_X);
                y = (float)(rectangle.Y * this.Zoom_Y);
                height = (float)(rectangle.Height * this.Zoom_Y);
                Pen pen = new Pen(color, pen_width);
                g.DrawRectangle(pen, x, y, width, height);
                this.canvas.ReleaseHDC();
            }
        }
        public void Draw_DetectFace(ASF_SingleFace ASF_SingleFace, Color color, float pen_width, string str_value)
        {
            this.Draw_DetectFace(ASF_SingleFace, color, pen_width, str_value, new Font("微軟正黑體", 12));
        }
        public void Draw_DetectFace(ASF_SingleFace ASF_SingleFace, float pen_width, Font font)
        {
            this.Draw_DetectFace(ASF_SingleFace, "", pen_width, font);
        }
        public void Draw_DetectFace(ASF_SingleFace ASF_SingleFace, string Name, float pen_width, Font font)
        {
            Color color;
            string str_value = "";
            if (ASF_SingleFace.IsLive == 1)
            {
                if (ASF_SingleFace.CompareIndex >= 0)
                {
                    str_value = string.Format("({0}){1},Score: {2}%", ASF_SingleFace.CompareIndex.ToString(), Name, ASF_SingleFace.Score.ToString("0.00"));
                }
                color = Color.Lime;
            }
            else
            {
                color = Color.Yellow;
            }

            this.Draw_DetectFace(ASF_SingleFace, color, pen_width, str_value, font);
        }
        public void Draw_DetectFace(ASF_SingleFace ASF_SingleFace, Color color, float pen_width, string str_value, Font font)
        {
            using (Graphics g = GetCanvasGraphics())
            {
                MRECT rect = ASF_SingleFace.faceRect;
                float x = rect.left;
                float width = rect.right - x;
                float y = rect.top;
                float height = rect.bottom - y;
                Rectangle rectangle = new Rectangle((int)x, (int)y, (int)width, (int)height);
                x = (float)(rectangle.X * this.Zoom_X);
                width = (float)(rectangle.Width * this.Zoom_X);
                y = (float)(rectangle.Y * this.Zoom_Y);
                height = (float)(rectangle.Height * this.Zoom_Y);
                Pen pen = new Pen(color, pen_width);
                g.DrawRectangle(pen, x, y, width, height);
                SizeF textSize = TextRenderer.MeasureText(" ", font);
                g.DrawString(str_value, font, Brushes.Lime, x, y - textSize.Height);
                this.canvas.ReleaseHDC();
            }
        }
        public void RrfreshCanvas()
        {
            if (this.ShowRegisterROI)
            {

                this.ROIC24_Register.ROIWidth = 300;
                this.ROIC24_Register.ROIHeight = 300;
                this.ROIC24_Register.DrawFrame(this.canvas, this.canvas.ZoomX, this.canvas.ZoomY, Color.Red);
            }

            this.canvas.CanvasRefresh();
        }
        public void ClearCanvas()
        {
            this.canvas.CavasClear();
        }
        private int ASF_FeatureCompare(IntPtr pEngine, IntPtr feature0, out float similarity, float threshold)
        {
            similarity = 0f;
            List<object[]> list_value = new List<object[]>();

            //如果人脸库不为空，则进行人脸匹配
            if (imagesFeatureList != null && imagesFeatureList.Count > 0)
            {
                for (int i = 0; i < imagesFeatureList.Count; i++)
                {
                    //调用人脸匹配方法，进行匹配
                    IntPtr feature1 = imagesFeatureList[i].ToPointer();
                    ASFFunctions.ASFFaceFeatureCompare(pEngine, feature0, feature1, ref similarity);
                    if (similarity >= threshold)
                    {
                        list_value.Add(new object[] { i, similarity });
                    }
                }
            }
            list_value.Sort(new ICP_compareFeature());
            if (list_value.Count == 0) return -1;
            similarity = (float)list_value[0][1];
            return (int)list_value[0][0];
        }
        static public ASF_MultiFaceInfo ASFDetectFace(IntPtr pEngine, ImageInfo imageInfo)
        {
            int SIZE = MemoryUtil.SizeOf<ASF_MultiFaceInfo>();
            IntPtr pMultiFaceInfo = MemoryUtil.Malloc(SIZE);

            int retCode = ASFFunctions.ASFDetectFaces(pEngine, imageInfo.width, imageInfo.height, imageInfo.format, imageInfo.imgData, pMultiFaceInfo);
            if (retCode != 0)
            {
                MemoryUtil.Free(pMultiFaceInfo);
                return new ASF_MultiFaceInfo();
            }
            ASF_MultiFaceInfo multiFaceInfo = MemoryUtil.PtrToStructure<ASF_MultiFaceInfo>(pMultiFaceInfo);
            MemoryUtil.Free(pMultiFaceInfo);
            return multiFaceInfo;

        }
        public void ASFSetLivenessParam(IntPtr pEngine, float rgbThreshold, float irThreshole, float fqThreshole)
        {
            ASF_LivenessThreshold livebessThreshold = new ASF_LivenessThreshold();
            livebessThreshold.thresholdmodel_BGR = (rgbThreshold >= 0 && rgbThreshold <= 1) ? rgbThreshold : 0.5f;
            livebessThreshold.thresholdmodel_IR = (irThreshole >= 0 && irThreshole <= 1) ? irThreshole : 0.5f;
            livebessThreshold.thresholdmodel_FQ = (fqThreshole >= 0 && fqThreshole <= 1) ? fqThreshole : 0.65f;
            IntPtr pLivenessThreshold = MemoryUtil.Malloc(MemoryUtil.SizeOf<ASF_LivenessThreshold>());
            MemoryUtil.StructureToPtr(livebessThreshold, pLivenessThreshold);
            int retCode = ASFFunctions.ASFSetLivenessParam(pEngine, pLivenessThreshold);
            MemoryUtil.Free(pLivenessThreshold);
        }
        static public ASF_LivenessInfo ASFLivenessInfo(IntPtr pEngine, ImageInfo imageInfo, ASF_MultiFaceInfo multiFaceInfo)
        {
            int retCode = -99;
            IntPtr pMultiFaceInfo = MemoryUtil.Malloc(MemoryUtil.SizeOf<ASF_MultiFaceInfo>());
            MemoryUtil.StructureToPtr(multiFaceInfo, pMultiFaceInfo);

            if (multiFaceInfo.faceNum == 0)
            {
                retCode = -1;
                //释放内存
                MemoryUtil.Free(pMultiFaceInfo);
                return new ASF_LivenessInfo();
            }

            try
            {
                //人脸信息处理
                retCode = ASFFunctions.ASFProcess(pEngine, imageInfo.width, imageInfo.height, imageInfo.format, imageInfo.imgData, pMultiFaceInfo, FaceEngineMask.ASF_LIVENESS);
                if (retCode == 0)
                {
                    //获取活体检测结果
                    IntPtr pLivenessInfo = MemoryUtil.Malloc(MemoryUtil.SizeOf<ASF_LivenessInfo>());
                    retCode = ASFFunctions.ASFGetLivenessScore(pEngine, pLivenessInfo);
                    ASF_LivenessInfo livenessInfo = MemoryUtil.PtrToStructure<ASF_LivenessInfo>(pLivenessInfo);

                    //释放内存
                    MemoryUtil.Free(pMultiFaceInfo);
                    MemoryUtil.Free(pLivenessInfo);
                    return livenessInfo;
                }
                else
                {
                    //释放内存
                    MemoryUtil.Free(pMultiFaceInfo);
                    return new ASF_LivenessInfo();
                }
            }
            catch
            {
                retCode = -1;
                //释放内存
                MemoryUtil.Free(pMultiFaceInfo);
                return new ASF_LivenessInfo();
            }
        }
        static public FaceFeature ASFFaceFeatureExtract(IntPtr pEngine, ImageInfo imageInfo, ASF_SingleFaceInfo singleFaceInfo, ASF_RegisterOrNot registerOrNot, bool IsMask)
        {
            FaceFeature faceFeature = new FaceFeature();
            IntPtr pSIngleFaceInfo = MemoryUtil.Malloc(MemoryUtil.SizeOf<ASF_SingleFaceInfo>());
            MemoryUtil.StructureToPtr(singleFaceInfo, pSIngleFaceInfo);
            int size = MemoryUtil.SizeOf<ASF_FaceFeature>();
            IntPtr pAsfFaceFeature = MemoryUtil.Malloc(size);
            int retCode = ASFFunctions.ASFFaceFeatureExtract(pEngine, imageInfo.width, imageInfo.height, imageInfo.format, imageInfo.imgData, pSIngleFaceInfo, registerOrNot, IsMask ? 1 : 0, pAsfFaceFeature);
            if (retCode != 0)
            {
                MemoryUtil.FreeArray(pSIngleFaceInfo, pAsfFaceFeature);
                return faceFeature;
            }
            //获取特征结构体，并转化
            ASF_FaceFeature asfFeature = MemoryUtil.PtrToStructure<ASF_FaceFeature>(pAsfFaceFeature);
            byte[] feature = new byte[asfFeature.featureSize];
            MemoryUtil.Copy(asfFeature.feature, feature, 0, asfFeature.featureSize);
            faceFeature.featureSize = asfFeature.featureSize;
            faceFeature.feature = feature;
            MemoryUtil.FreeArray(pSIngleFaceInfo, pAsfFaceFeature);
            return faceFeature;
        }
        static public void ASFRegisterFaceFeature(IntPtr pEngine, int ID, FaceFeature faceFeature)
        {
            IntPtr pFaceFeature = IntPtr.Zero;
            IntPtr pfeature = IntPtr.Zero;
            ASF_FaceFeatureInfo faceList = new ASF_FaceFeatureInfo();
            ASF_FaceFeature aSF_FaceFeature = new ASF_FaceFeature();

            pfeature = MemoryUtil.Malloc(faceFeature.featureSize);
            MemoryUtil.Copy(faceFeature.feature, 0, pfeature, faceFeature.featureSize);
            aSF_FaceFeature.feature = pfeature;
            aSF_FaceFeature.featureSize = faceFeature.featureSize;
            pFaceFeature = MemoryUtil.Malloc(MemoryUtil.SizeOf<ASF_FaceFeature>());
            MemoryUtil.StructureToPtr(aSF_FaceFeature, pFaceFeature);
            faceList.searchId = ID;
            faceList.feature = pFaceFeature;

            IntPtr pfaceList = MemoryUtil.Malloc(MemoryUtil.SizeOf<ASF_FaceFeatureInfo>());
            MemoryUtil.StructureToPtr(faceList, pfaceList);
            int retCode = ASFFunctions.ASFRegisterFaceFeature(pEngine, pfaceList, 1);
            MemoryUtil.FreeArray(pfaceList, pFaceFeature, pfeature);
        }
        static public void ASFGetFaceCount(IntPtr pEngine, out int num)
        {
            IntPtr pnum = MemoryUtil.Malloc(MemoryUtil.SizeOf<int>());
            int retCode = ASFFunctions.ASFGetFaceCount(pEngine, pnum);
            num = MemoryUtil.PtrToStructure<int>(pnum);
        }
        static public void ASFGetFaceFeature(IntPtr pEngine, int ID)
        {
            IntPtr pfeatureInfo = MemoryUtil.Malloc(MemoryUtil.SizeOf<ASF_FaceFeatureInfo>());
            int retCode = ASFFunctions.ASFGetFaceFeature(pEngine, ID, pfeatureInfo);
            if (retCode != 151553 && retCode != 151554)
            {
                ASF_FaceFeatureInfo aSF_FaceFeatureInfo = MemoryUtil.PtrToStructure<ASF_FaceFeatureInfo>(pfeatureInfo);
                IntPtr pfeature = aSF_FaceFeatureInfo.feature;
                ASF_FaceFeature aSF_FaceFeature = MemoryUtil.PtrToStructure<ASF_FaceFeature>(pfeature);
            }
            MemoryUtil.Free(pfeatureInfo);
        }
        static public int ASFOfflineActivation(string filepath)
        {
            return ASFFunctions.ASFOfflineActivation(filepath);
        }
        static public int ASFGetActiveDeviceInfo(out string Deviceinfo)
        {
            int code = -1;

            unsafe
            {
                IntPtr pASF_DeviceInfo = MemoryUtil.Malloc(MemoryUtil.SizeOf<ASF_DeviceInfo>());
                code = ASFFunctions.ASFGetActiveDeviceInfo(pASF_DeviceInfo);
                ASF_DeviceInfo ASF_DeviceInfo = MemoryUtil.PtrToStructure<ASF_DeviceInfo>(pASF_DeviceInfo);

                SDKDeviceInfo sDKDeviceInfo = new SDKDeviceInfo();
                sDKDeviceInfo.info = Marshal.PtrToStringAnsi(ASF_DeviceInfo.info);
                Deviceinfo = sDKDeviceInfo.info;
                MemoryUtil.Free(pASF_DeviceInfo);
            }
            return code;
        }
        static public int ASFGetActiveFileInfo()
        {
            SDKActiveFileInfo sDKActiveFileInfo = new SDKActiveFileInfo();
            IntPtr pActiveFileInfo = MemoryUtil.Malloc(MemoryUtil.SizeOf<ASF_ActiveFileInfo>());
            int code = ASFFunctions.ASFGetActiveFileInfo(pActiveFileInfo);
            ASF_ActiveFileInfo ActiveFileInfo = MemoryUtil.PtrToStructure<ASF_ActiveFileInfo>(pActiveFileInfo);
            sDKActiveFileInfo.startTime = Marshal.PtrToStringAnsi(ActiveFileInfo.startTime);
            sDKActiveFileInfo.endTime = Marshal.PtrToStringAnsi(ActiveFileInfo.endTime);
            sDKActiveFileInfo.activeKey = Marshal.PtrToStringAnsi(ActiveFileInfo.activeKey);
            sDKActiveFileInfo.platform = Marshal.PtrToStringAnsi(ActiveFileInfo.platform);
            sDKActiveFileInfo.sdkType = Marshal.PtrToStringAnsi(ActiveFileInfo.sdkType);
            sDKActiveFileInfo.appId = Marshal.PtrToStringAnsi(ActiveFileInfo.appId);
            sDKActiveFileInfo.sdkKey = Marshal.PtrToStringAnsi(ActiveFileInfo.sdkKey);
            sDKActiveFileInfo.sdkVersion = Marshal.PtrToStringAnsi(ActiveFileInfo.sdkVersion);
            sDKActiveFileInfo.fileVersion = Marshal.PtrToStringAnsi(ActiveFileInfo.fileVersion);
            MemoryUtil.Free(pActiveFileInfo);
            return code;
        }
        

        private class ICP_compareFeature : IComparer<object[]>
        {
            public int Compare(object[] x, object[] y)
            {
                if ((float)x[1] > (float)y[1]) return -1;
                else if ((float)x[1] < (float)y[1]) return 1;
                else return 0;
            }
        }

    }

    public static class TypeConvert
    {
        static public List<ASF_SingleFace> ToSingleFaceInfo(this ASF_MultiFaceInfo aSF_MultiFaceInfo)
        {
            List<ASF_SingleFace> list_ASF_SingleFace = new List<ASF_SingleFace>();
            for (int i = 0; i < aSF_MultiFaceInfo.faceNum; i++)
            {
                ASF_SingleFace singleFaceInfo = new ASF_SingleFace();
                try
                {
                    singleFaceInfo.faceID = MemoryUtil.PtrToStructure<int>(aSF_MultiFaceInfo.faceID + MemoryUtil.SizeOf<int>() * i);
                    singleFaceInfo.faceIsWithinBoundary = MemoryUtil.PtrToStructure<int>(aSF_MultiFaceInfo.faceIsWithinBoundary + MemoryUtil.SizeOf<int>() * i);
                    singleFaceInfo.faceRect = MemoryUtil.PtrToStructure<MRECT>(aSF_MultiFaceInfo.faceRect + MemoryUtil.SizeOf<MRECT>() * i);
                    singleFaceInfo.faceOrient = MemoryUtil.PtrToStructure<int>(aSF_MultiFaceInfo.faceOrient + MemoryUtil.SizeOf<int>() * i);
                    singleFaceInfo.wearGlasses = MemoryUtil.PtrToStructure<int>(aSF_MultiFaceInfo.faceAttributeInfo.wearGlasses + MemoryUtil.SizeOf<int>() * i);
                    singleFaceInfo.leftEyeOpen = MemoryUtil.PtrToStructure<int>(aSF_MultiFaceInfo.faceAttributeInfo.leftEyeOpen + MemoryUtil.SizeOf<int>() * i);
                    singleFaceInfo.rightEyeOpen = MemoryUtil.PtrToStructure<int>(aSF_MultiFaceInfo.faceAttributeInfo.rightEyeOpen + MemoryUtil.SizeOf<int>() * i);
                    singleFaceInfo.mouthClose = MemoryUtil.PtrToStructure<int>(aSF_MultiFaceInfo.faceAttributeInfo.mouthClose + MemoryUtil.SizeOf<int>() * i);
                    singleFaceInfo.roll = MemoryUtil.PtrToStructure<int>(aSF_MultiFaceInfo.face3DAngleInfo.roll + MemoryUtil.SizeOf<int>() * i);
                    singleFaceInfo.yaw = MemoryUtil.PtrToStructure<int>(aSF_MultiFaceInfo.face3DAngleInfo.yaw + MemoryUtil.SizeOf<int>() * i);
                    singleFaceInfo.pitch = MemoryUtil.PtrToStructure<int>(aSF_MultiFaceInfo.face3DAngleInfo.pitch + MemoryUtil.SizeOf<int>() * i);
                    singleFaceInfo.faceDataInfo = MemoryUtil.PtrToStructure<ASF_FaceDataInfo>(aSF_MultiFaceInfo.faceDataInfoList + MemoryUtil.SizeOf<ASF_FaceDataInfo>() * i);
                    singleFaceInfo.foreheadRect = MemoryUtil.PtrToStructure<MRECT>(aSF_MultiFaceInfo.foreheadRect + MemoryUtil.SizeOf<MRECT>() * i);
                    singleFaceInfo.area = (singleFaceInfo.faceRect.right - singleFaceInfo.faceRect.left) * (singleFaceInfo.faceRect.bottom - singleFaceInfo.faceRect.top);


                    list_ASF_SingleFace.Add(singleFaceInfo);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return list_ASF_SingleFace;
        }
        static public ASF_MultiFaceInfo ToASF_MultiFaceInfo(this List<ASF_SingleFace> list_ASF_SingleFace)
        {
            ASF_MultiFaceInfo aSF_MultiFaceInfo = new ASF_MultiFaceInfo();

            int count = list_ASF_SingleFace.Count;
            int size_int = MemoryUtil.SizeOf<int>();
            int size_ASF_FaceDataInfo = MemoryUtil.SizeOf<ASF_FaceDataInfo>();
            int size_ASF_FaceAttributeInfo = MemoryUtil.SizeOf<int>() * 4;
            int size_ASF_Face3DAngleInfo = MemoryUtil.SizeOf<float>() * 3;
            int size_MRECT = MemoryUtil.SizeOf<MRECT>();
            int size_ASF_MultiFaceInfo = size_int + (size_MRECT + size_int * 4 + size_ASF_FaceDataInfo + size_ASF_FaceAttributeInfo + size_ASF_Face3DAngleInfo) * count;
            IntPtr Start_ptr = MemoryUtil.Malloc(size_ASF_MultiFaceInfo);
            IntPtr ptr = Start_ptr;
            IntPtr pfaceRect;
            IntPtr pfaceOrient;
            IntPtr pfaceID;
            IntPtr pfaceDataInfoList;
            IntPtr pfaceIsWithinBoundary;
            IntPtr pforeheadRect;
            IntPtr pwearGlasses;
            IntPtr pleftEyeOpen;
            IntPtr prightEyeOpen;
            IntPtr pmouthClose;
            IntPtr proll;
            IntPtr pyaw;
            IntPtr ppitch;
            MemoryUtil.StructureToPtr<int>(count, ref ptr);
            pfaceRect = ptr;
            for (int i = 0; i < count; i++)
            {
                MemoryUtil.StructureToPtr<MRECT>(list_ASF_SingleFace[i].faceRect, ref ptr);
            }
            pfaceOrient = ptr;
            for (int i = 0; i < count; i++)
            {
                MemoryUtil.StructureToPtr<int>(list_ASF_SingleFace[i].faceOrient, ref ptr);
            }
            pfaceID = ptr;
            for (int i = 0; i < count; i++)
            {
                MemoryUtil.StructureToPtr<int>(list_ASF_SingleFace[i].faceID, ref ptr);
            }
            pfaceDataInfoList = ptr;
            for (int i = 0; i < count; i++)
            {
                MemoryUtil.StructureToPtr<ASF_FaceDataInfo>(list_ASF_SingleFace[i].faceDataInfo, ref ptr);
            }
            pfaceIsWithinBoundary = ptr;
            for (int i = 0; i < count; i++)
            {
                MemoryUtil.StructureToPtr<int>(list_ASF_SingleFace[i].faceIsWithinBoundary, ref ptr);
            }
            pforeheadRect = ptr;
            for (int i = 0; i < count; i++)
            {
                MemoryUtil.StructureToPtr<MRECT>(list_ASF_SingleFace[i].foreheadRect, ref ptr);
            }
            pleftEyeOpen = ptr;
            for (int i = 0; i < count; i++)
            {
                MemoryUtil.StructureToPtr<int>(list_ASF_SingleFace[i].leftEyeOpen, ref ptr);
            }
            pwearGlasses = ptr;
            for (int i = 0; i < count; i++)
            {
                MemoryUtil.StructureToPtr<int>(list_ASF_SingleFace[i].wearGlasses, ref ptr);
            }
            prightEyeOpen = ptr;
            for (int i = 0; i < count; i++)
            {
                MemoryUtil.StructureToPtr<int>(list_ASF_SingleFace[i].rightEyeOpen, ref ptr);
            }
            pmouthClose = ptr;
            for (int i = 0; i < count; i++)
            {
                MemoryUtil.StructureToPtr<int>(list_ASF_SingleFace[i].mouthClose, ref ptr);
            }
            proll = ptr;
            for (int i = 0; i < count; i++)
            {
                MemoryUtil.StructureToPtr<float>(list_ASF_SingleFace[i].roll, ref ptr);
            }
            pyaw = ptr;
            for (int i = 0; i < count; i++)
            {
                MemoryUtil.StructureToPtr<float>(list_ASF_SingleFace[i].yaw, ref ptr);
            }
            ppitch = ptr;
            for (int i = 0; i < count; i++)
            {
                MemoryUtil.StructureToPtr<float>(list_ASF_SingleFace[i].pitch, ref ptr);
            }
            aSF_MultiFaceInfo.faceNum = count;
            aSF_MultiFaceInfo.faceRect = pfaceRect;
            aSF_MultiFaceInfo.faceOrient = pfaceOrient;
            aSF_MultiFaceInfo.faceID = pfaceID;
            aSF_MultiFaceInfo.faceDataInfoList = pfaceDataInfoList;
            aSF_MultiFaceInfo.faceIsWithinBoundary = pfaceIsWithinBoundary;
            aSF_MultiFaceInfo.foreheadRect = pforeheadRect;
            aSF_MultiFaceInfo.faceAttributeInfo.wearGlasses = pwearGlasses;
            aSF_MultiFaceInfo.faceAttributeInfo.leftEyeOpen = pleftEyeOpen;
            aSF_MultiFaceInfo.faceAttributeInfo.rightEyeOpen = prightEyeOpen;
            aSF_MultiFaceInfo.faceAttributeInfo.mouthClose = pmouthClose;
            aSF_MultiFaceInfo.face3DAngleInfo.roll = proll;
            aSF_MultiFaceInfo.face3DAngleInfo.yaw = pyaw;
            aSF_MultiFaceInfo.face3DAngleInfo.pitch = ppitch;
            return aSF_MultiFaceInfo;
        }
        static public ASF_FaceFeature ToASF_FaceFeature(this FaceFeature faceFeature)
        {
            ASF_FaceFeature aSF_FaceFeature = new ASF_FaceFeature();
            if (faceFeature.featureSize > 0)
            {
                IntPtr pfeature = MemoryUtil.Malloc(faceFeature.featureSize);
                MemoryUtil.Copy(faceFeature.feature, 0, pfeature, faceFeature.featureSize);
                aSF_FaceFeature.featureSize = faceFeature.featureSize;
                aSF_FaceFeature.feature = pfeature;
            }

            return aSF_FaceFeature;
        }
        static public IntPtr ToPointer(this ASF_FaceFeature aSF_FaceFeature)
        {
            IntPtr ptr = MemoryUtil.Malloc(MemoryUtil.SizeOf<ASF_FaceFeature>());
            MemoryUtil.StructureToPtr<ASF_FaceFeature>(aSF_FaceFeature, ptr);
            return ptr;
        }
        static public FaceFeature ToFaceFeature(this ASF_FaceFeature aSF_FaceFeature)
        {
            FaceFeature faceFeature = new FaceFeature();
            byte[] byte_array = new byte[aSF_FaceFeature.featureSize];
            MemoryUtil.Copy(aSF_FaceFeature.feature, byte_array, 0, aSF_FaceFeature.featureSize);
            faceFeature.feature = byte_array;
            faceFeature.featureSize = byte_array.Length;
            aSF_FaceFeature.Dispose();
            return faceFeature;
        }

    }
}
