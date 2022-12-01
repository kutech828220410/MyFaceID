#pragma once
#include "arcsoft_face_sdk.h"
#include "merror.h"
#include "opencv/cv.h"
#include "opencv/highgui.h"
#include "config.h"

#define SafeFree(p) { if ((p)) free(p); (p) = NULL; }
#define SafeArrayDelete(p) { if ((p)) delete [] (p); (p) = NULL; } 
#define SafeDelete(p) { if ((p)) delete (p); (p) = NULL; } 

typedef struct FqThreshold
{
	float fqRegisterThreshold;
	float fqRecognitionMaskThreshold;
	float fqRecognitionNoMaskThreshold;
};

//ͼ��ü�
void PicCutOut(IplImage* src, IplImage* dst, int x, int y);
// ͼ����ɫ�ռ�ת��
int ColorSpaceConversion(MInt32 format, IplImage* img, ASVLOFFSCREEN& offscreen);
//���������
void copyMultiFaceInfo(ASF_MultiFaceInfo& dstMultiFaceInfo, ASF_MultiFaceInfo srcMultiFaceInfo);
void multiFaceInfoMemoryAllocation(ASF_MultiFaceInfo* multiFaceInfo);
void multiFaceInfoMemoryRelease(ASF_MultiFaceInfo* multiFaceInfo);

class FaceEngine
{
public:
	FaceEngine();
	~FaceEngine();

public:
	void initData();

	//����SDK
	MRESULT ActiveSDK(char* appId, char* sdkKey, char* activeKey);
	//��ȡ������Ϣ
	MRESULT GetActiveFileInfo(ASF_ActiveFileInfo& activeFileInfo);
	//�����ʼ��
	MRESULT InitEngine(ASF_DetectMode detectMode, ASF_OrientPriority detectFaceOrientPriority, MInt32 mask);
	//�ͷ�����
	MRESULT UnInitEngine();
	MRESULT SetFaceAttributeParam(MFloat eyeopenThreshold, MFloat mouthcloseThreshold,
		MFloat wearGlassesThreshold);
	//�������
	MRESULT PreDetectFace(IplImage* image, ASF_MultiFaceInfo& multiFaceInfo, bool isRgb);
	//ͼ���������
	MRESULT PreImageQualityDetect(IplImage* image, ASF_SingleFaceInfo singleFaceInfo,
		ASF_RegisterOrNot registerOrNot, MInt32 isMask, FqThreshold FqThreshold);
	//����FaceData����
	MRESULT UpdateFaceData(IplImage* image, ASF_MultiFaceInfo multiFaceInfo);
	//����������ȡ
	MRESULT PreExtractFeature(IplImage* image, ASF_SingleFaceInfo& faceRect, ASF_FaceFeature& feature, 
		ASF_RegisterOrNot registerOrNot = ASF_RECOGNITION, MInt32 mask = 0);
	//�����ȶ�
	MRESULT FacePairMatching(MFloat &confidenceLevel, ASF_FaceFeature feature1, ASF_FaceFeature feature2, 
		ASF_CompareModel compareModel = ASF_LIFE_PHOTO);
	//���û�����ֵ
	MRESULT SetLivenessThreshold(MFloat	rgbLiveThreshold, MFloat irLiveThreshold);
	//�������Լ�⣨���䡢�Ա𡢻��塢3D�Ƕȣ�
	MRESULT FaceProcess(ASF_MultiFaceInfo detectedFaces, IplImage *img, ASF_AgeInfo &ageInfo,
		ASF_GenderInfo &genderInfo, ASF_LivenessInfo& liveNessInfo);
	//������
	MRESULT livenessProcess(ASF_MultiFaceInfo detectedFaces, IplImage *img, ASF_LivenessInfo& livenessInfo, bool isRgb);
	//���ְ汾���Լ���⣨���֡��ڵ�����ͷ����
	MRESULT FaceProcessMask(ASF_MultiFaceInfo detectedFaces, IplImage *img, ASF_MaskInfo& maskInfo);
	//��������
	MRESULT searchFaceFeature(LPASF_FaceFeature feature, MFloat &confidenceLevel, MInt32 &index, ASF_CompareModel compareModel = ASF_LIFE_PHOTO);
	//����ע��
	MRESULT registerFace(MInt32 searchId, LPASF_FaceFeature faceFeature);


private:
	MHandle m_hEngine;
};


