#include "ArcFaceDetection.h"
#include <QtWidgets/QApplication>
#include "UiStyle.h"

//typedef struct {
//	MInt32*                 faceOrient;             // ����ͼ��ĽǶȣ����Բο� ArcFace_OrientCode .
//}FaceInfo, *LPFaceInfo;
//
//void setMem(FaceInfo faceInfo)
//{
//	faceInfo.faceOrient = (MInt32*)malloc(sizeof(MInt32) * 3);
//}

int main(int argc, char *argv[])
{


	QApplication a(argc, argv);

	CommonHelper::setStyle(":/Resources/style.qss");

	ArcFaceDetection w;
	w.show();
	return a.exec();
}
