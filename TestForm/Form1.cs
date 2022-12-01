using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyUI;
using Basic;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using ArcSoftFace.SDKModels;
using ArcSoftFace.SDKUtil;
using ArcSoftFace.Utils;
using ArcSoftFace.Entity;
using MyFaceID;
namespace TestForm
{
    public partial class Form1 : Form
    {
        private MyThread MyThread_test;
        private bool flag_init = false;


        public Form1()
        {
            InitializeComponent();
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            this.button_初始化.Click += Button_初始化_Click;
            this.button_產生離線文件.Click += Button_產生離線文件_Click;
            this.button_離線激活.Click += Button_離線激活_Click;

            MyMessageBox.form = this.FindForm();
            MyMessageBox.音效 = false;
        }

       
        private bool falg_regist = false;
        private int index = 0;
        private ASF_SingleFace Liveness_SingleFace;
        private void sub_program()
        {
            using (Bitmap bitmap = myFaceIDUI_SRC.GetBitmapFromCam())
            {
                MyTimer myTimer = new MyTimer();
                myTimer.StartTickTime(50000);
                myFaceIDUI_SRC.ShowRegisterROI = true;
                ASF_MultiFaceInfo aSF_MultiFaceInfo;

                List<ASF_SingleFace> list_ASF_SingleFace = new List<ASF_SingleFace>();
                myFaceIDUI_SRC.DrawBitmapToCanvas(bitmap);
                aSF_MultiFaceInfo = myFaceIDUI_SRC.DetectFace(bitmap);
                list_ASF_SingleFace = aSF_MultiFaceInfo.ToSingleFaceInfo();


                if (myFaceIDUI_SRC.livenessClass.IsDone)
                {
                    Liveness_SingleFace = myFaceIDUI_SRC.livenessClass.ASF_SingleFace_Result;
                    myFaceIDUI_SRC.livenessClass.Trigger(bitmap);

                }
                if (Liveness_SingleFace != null)
                {

                    myFaceIDUI_SRC.Draw_DetectFace(Liveness_SingleFace, 2, new Font("微軟正黑體", 12));

                }

                myFaceIDUI_SRC.RrfreshCanvas();
                index++;



            }
            using (Bitmap bitmap = myFaceIDUI_Clone.GetBitmapFromCam())
            {
                MyTimer myTimer = new MyTimer();
                myTimer.StartTickTime(50000);
                myFaceIDUI_Clone.ShowRegisterROI = true;
                ASF_MultiFaceInfo aSF_MultiFaceInfo;

                List<ASF_SingleFace> list_ASF_SingleFace = new List<ASF_SingleFace>();
                myFaceIDUI_Clone.DrawBitmapToCanvas(bitmap);
                aSF_MultiFaceInfo = myFaceIDUI_Clone.DetectFace(bitmap);
                list_ASF_SingleFace = aSF_MultiFaceInfo.ToSingleFaceInfo();


                if (myFaceIDUI_Clone.livenessClass.IsDone)
                {
                    Liveness_SingleFace = myFaceIDUI_Clone.livenessClass.ASF_SingleFace_Result;
                    myFaceIDUI_Clone.livenessClass.Trigger(bitmap);

                }
                if (Liveness_SingleFace != null)
                {

                    myFaceIDUI_Clone.Draw_DetectFace(Liveness_SingleFace, 2, new Font("微軟正黑體", 12));

                }

                myFaceIDUI_Clone.RrfreshCanvas();
            }
        }

        private void button_GetFaceFeature_Click(object sender, EventArgs e)
        {
            this.myFaceIDUI_SRC.RegisterFaceList(this.myFaceIDUI_SRC.GetRegisterROIBitmap());
        }
        private void Button_離線激活_Click(object sender, EventArgs e)
        {
            int code = -1;
            if(this.openFileDialog.ShowDialog() == DialogResult.OK)
            {
                code = MyFaceID.MyFaceIDUI.ASFOfflineActivation(this.openFileDialog.FileName);
            }
         
        }

        private void Button_產生離線文件_Click(object sender, EventArgs e)
        {
            string deviceinfo = "";

            int code = MyFaceID.MyFaceIDUI.ASFGetActiveFileInfo();
            MyFaceID.MyFaceIDUI.ASFGetActiveDeviceInfo(out deviceinfo);
            this.textBox_序號.Text = deviceinfo;

            if(this.saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                MyFileStream.SaveFile(this.saveFileDialog.FileName, this.textBox_序號.Text);
                MyMessageBox.ShowDialog("存檔完成!");
            }

        }

        private void Button_初始化_Click(object sender, EventArgs e)
        {
            if (!flag_init)
            {
                myFaceIDUI_SRC.Init(0);
                myFaceIDUI_Clone.Init(myFaceIDUI_SRC.Camera, true);
                myFaceIDUI_SRC.livenessClass.FaceFeatureExtractEvent += LivenessClass_FaceFeatureExtractEvent;
                MyThread_test = new MyThread();
                MyThread_test.AutoRun(true);
                MyThread_test.Add_Method(sub_program);
                MyThread_test.Trigger();
            }
            flag_init = true;
        }

        private void LivenessClass_FaceFeatureExtractEvent(FaceFeature faceFeature, ASF_SingleFace aSF_SingleFace)
        {
            if (falg_regist)
            {
                myFaceIDUI_SRC.RegisterFaceList(faceFeature.ToASF_FaceFeature());
                falg_regist = false;
            }

        }

    }
}
