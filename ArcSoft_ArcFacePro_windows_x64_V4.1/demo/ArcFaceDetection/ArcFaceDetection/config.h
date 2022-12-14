#pragma once


#define SETTING_FILE "setting.ini"
#define CAMERA_REGISTER_IMAGE_PATH "/RegisterPhoto"
#define ACTIVE_SETTING	"ActiveInfo_x86"

#define FACENUM 1

#define COMPARE_THRESHOLD "0.80"
#define FQ_REGISTER_THRESHOLD "0.63"
#define FQ_RECOGNITION_MASK_THRESHOLD "0.29"
#define FQ_RECOGNITION_NO_MASK_THRESHOLD "0.49"
#define IR_LIVE_THRESHOLD "0.5"
#define RGB_LIVE_THRESHOLD "0.5"

//人脸属性阈值
#define EYEOPEN_THRESHOLD 0.5
#define MOUTHCLOSE_THRESHOLD 0.5
#define WEARGLASSES_THRESHOLD 0.5

#define VIDEO_FRAME_DEFAULT_WIDTH 640.0
#define VIDEO_FRAME_DEFAULT_HEIGHT 480.0

#define LIST_WIDGET_ITEM_WIDTH 50
#define LIST_WIDGET_ITEM_HEIGHT 50

#define FACE_ATTRIBUTE_HEIGHT 20	//绘制间隔

#define FACE_FEATURE_SIZE 2056		//特征值长度

#define FACE_DATA_SIZE 5000			//人脸信息数据长度



//双目摄像头偏移量，需根据摄像头偏移的像素进行手动修改调整，根据偏移方向值也可为负值
#define BINOCULAR_CAMERA_OFFSET_LEFT_RIGHT "0"
#define BINOCULAR_CAMERA_OFFSET_TOP_BOTTOM "0"