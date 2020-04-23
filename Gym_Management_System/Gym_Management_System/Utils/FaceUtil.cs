using System;
using Gym_Management_System.SDKModels;
using Gym_Management_System.SDKUtil;
using Gym_Management_System.Entity;
using System.Drawing;

namespace Gym_Management_System.Utils
{
    public class FaceUtil
    {
        /// <summary>
        /// Face detection
        /// </summary>
        /// <param name="pEngine">Engine Handle</param>
        /// <param name="imageInfo">image data</param>
        /// <returns>Face detection results</returns>
        public static ASF_MultiFaceInfo DetectFace(IntPtr pEngine, ImageInfo imageInfo)
        {
            ASF_MultiFaceInfo multiFaceInfo = new ASF_MultiFaceInfo();
            IntPtr pMultiFaceInfo = MemoryUtil.Malloc(MemoryUtil.SizeOf<ASF_MultiFaceInfo>());
            int retCode = ASFFunctions.ASFDetectFaces(pEngine, imageInfo.width, imageInfo.height, imageInfo.format, imageInfo.imgData, pMultiFaceInfo);
            if (retCode != 0)
            {
                MemoryUtil.Free(pMultiFaceInfo);
                return multiFaceInfo;
            }
            multiFaceInfo = MemoryUtil.PtrToStructure<ASF_MultiFaceInfo>(pMultiFaceInfo);
            MemoryUtil.Free(pMultiFaceInfo);
            return multiFaceInfo;
        }

        /// <summary>
        /// Face detection
        /// </summary>
        /// <param name="pEngine">Engine Handle</param>
        /// <param name="image">image</param>
        /// <returns></returns>
        private static object locks = new object();
        public static ASF_MultiFaceInfo DetectFace(IntPtr pEngine, Image image)
        {
            lock (locks)
            {
                ASF_MultiFaceInfo multiFaceInfo = new ASF_MultiFaceInfo();
                if (image != null)
                {
                    if(image == null)
                    {
                        return multiFaceInfo;
                    }
                    ImageInfo imageInfo = ImageUtil.ReadBMP(image);
                    if(imageInfo == null)
                    {
                        return multiFaceInfo;
                    }
                    multiFaceInfo = DetectFace(pEngine, imageInfo);
                    MemoryUtil.Free(imageInfo.imgData);
                    return multiFaceInfo;

                }
                else
                {
                    return multiFaceInfo;
                }
            }
        }

        /// <summary>
        /// Extracting facial features
        /// </summary>
        /// <param name="pEngine">Engine Handle</param>
        /// <param name="imageInfo">picture data</param>
        /// <param name="multiFaceInfo">Face detection results</param>
        /// <returns>Save the face feature structure pointer</returns>
        public static IntPtr ExtractFeature(IntPtr pEngine, ImageInfo imageInfo, ASF_MultiFaceInfo multiFaceInfo, out ASF_SingleFaceInfo singleFaceInfo)
        {
            singleFaceInfo = new ASF_SingleFaceInfo();
            if(multiFaceInfo.faceRects == null)
            {
                ASF_FaceFeature emptyFeature = new ASF_FaceFeature();
                IntPtr pEmptyFeature = MemoryUtil.Malloc(MemoryUtil.SizeOf<ASF_FaceFeature>());
                MemoryUtil.StructureToPtr(emptyFeature, pEmptyFeature);
                return pEmptyFeature;
            }
            singleFaceInfo.faceRect = MemoryUtil.PtrToStructure<MRECT>(multiFaceInfo.faceRects);
            singleFaceInfo.faceOrient = MemoryUtil.PtrToStructure<int>(multiFaceInfo.faceOrients);
            IntPtr pSingleFaceInfo = MemoryUtil.Malloc(MemoryUtil.SizeOf<ASF_SingleFaceInfo>());
            MemoryUtil.StructureToPtr(singleFaceInfo, pSingleFaceInfo);

            IntPtr pFaceFeature = MemoryUtil.Malloc(MemoryUtil.SizeOf<ASF_FaceFeature>());
            int retCode = ASFFunctions.ASFFaceFeatureExtract(pEngine, imageInfo.width, imageInfo.height, imageInfo.format, imageInfo.imgData, pSingleFaceInfo, pFaceFeature);
            Console.WriteLine("FR Extract Feature result:" + retCode);

            if (retCode != 0)
            {
                //Release the pointer
                MemoryUtil.Free(pSingleFaceInfo);
                MemoryUtil.Free(pFaceFeature);
                ASF_FaceFeature emptyFeature = new ASF_FaceFeature();
                IntPtr pEmptyFeature = MemoryUtil.Malloc(MemoryUtil.SizeOf<ASF_FaceFeature>());
                MemoryUtil.StructureToPtr(emptyFeature, pEmptyFeature);
                return pEmptyFeature;
            }

            //Face feature feature filtering
            ASF_FaceFeature faceFeature = MemoryUtil.PtrToStructure<ASF_FaceFeature>(pFaceFeature);
            byte[] feature = new byte[faceFeature.featureSize];
            MemoryUtil.Copy(faceFeature.feature, feature, 0, faceFeature.featureSize);

            ASF_FaceFeature localFeature = new ASF_FaceFeature();
            localFeature.feature = MemoryUtil.Malloc(feature.Length);
            MemoryUtil.Copy(feature, 0, localFeature.feature, feature.Length);
            localFeature.featureSize = feature.Length;
            IntPtr pLocalFeature = MemoryUtil.Malloc(MemoryUtil.SizeOf<ASF_FaceFeature>());
            MemoryUtil.StructureToPtr(localFeature, pLocalFeature);

            //Release pointer
            MemoryUtil.Free(pSingleFaceInfo);
            MemoryUtil.Free(pFaceFeature);

            return pLocalFeature;
        }


        /// <summary>
        /// Extracting facial features
        /// </summary>
        /// <param name="pEngine">Engine Handle</param>
        /// <param name="image">image</param>
        /// <returns>Save the face feature structure pointer</returns>
        public static IntPtr ExtractFeature(IntPtr pEngine, Image image, out ASF_SingleFaceInfo singleFaceInfo)
        {
            if (image.Width > 1536 || image.Height > 1536)
            {
                image = ImageUtil.ScaleImage(image, 1536, 1536);
            }
            else
            {
                image = ImageUtil.ScaleImage(image, image.Width, image.Height);
            }
            if(image == null)
            {
                singleFaceInfo = new ASF_SingleFaceInfo();
                ASF_FaceFeature emptyFeature = new ASF_FaceFeature();
                IntPtr pEmptyFeature = MemoryUtil.Malloc(MemoryUtil.SizeOf<ASF_FaceFeature>());
                MemoryUtil.StructureToPtr(emptyFeature, pEmptyFeature);
                return pEmptyFeature;
            }
            ImageInfo imageInfo = ImageUtil.ReadBMP(image);
            if(imageInfo == null)
            {
                singleFaceInfo = new ASF_SingleFaceInfo();
                ASF_FaceFeature emptyFeature = new ASF_FaceFeature();
                IntPtr pEmptyFeature = MemoryUtil.Malloc(MemoryUtil.SizeOf<ASF_FaceFeature>());
                MemoryUtil.StructureToPtr(emptyFeature, pEmptyFeature);
                return pEmptyFeature;
            }
            ASF_MultiFaceInfo multiFaceInfo = DetectFace(pEngine, imageInfo);
            singleFaceInfo = new ASF_SingleFaceInfo();
            IntPtr pFaceModel = ExtractFeature(pEngine, imageInfo, multiFaceInfo, out singleFaceInfo);
            MemoryUtil.Free(imageInfo.imgData);
            return pFaceModel;
        }


        /// <summary>
        /// Extract single face feature
        /// </summary>
        /// <param name="pEngine">Engine Handle</param>
        /// <param name="image">photograph</param>
        /// <param name="singleFaceInfo">Single face information</param>
        /// <returns>Features of single face</returns>
        public static IntPtr ExtractFeature(IntPtr pEngine, Image image, ASF_SingleFaceInfo singleFaceInfo)
        {
            ImageInfo imageInfo = ImageUtil.ReadBMP(image);
            if(imageInfo == null)
            {
                ASF_FaceFeature emptyFeature = new ASF_FaceFeature();
                IntPtr pEmptyFeature = MemoryUtil.Malloc(MemoryUtil.SizeOf<ASF_FaceFeature>());
                MemoryUtil.StructureToPtr(emptyFeature, pEmptyFeature);
                return pEmptyFeature;
            }
            IntPtr pSingleFaceInfo = MemoryUtil.Malloc(MemoryUtil.SizeOf<ASF_SingleFaceInfo>());
            MemoryUtil.StructureToPtr(singleFaceInfo, pSingleFaceInfo);

            IntPtr pFaceFeature = MemoryUtil.Malloc(MemoryUtil.SizeOf<ASF_FaceFeature>());
            int retCode = -1;
            try
            {
                retCode = ASFFunctions.ASFFaceFeatureExtract(pEngine, imageInfo.width, imageInfo.height, imageInfo.format, imageInfo.imgData, pSingleFaceInfo, pFaceFeature);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine("FR Extract Feature result:" + retCode);

            if (retCode != 0)
            {
                //Release the pointer
                MemoryUtil.Free(pSingleFaceInfo);
                MemoryUtil.Free(pFaceFeature);
                MemoryUtil.Free(imageInfo.imgData);

                ASF_FaceFeature emptyFeature = new ASF_FaceFeature();
                IntPtr pEmptyFeature = MemoryUtil.Malloc(MemoryUtil.SizeOf<ASF_FaceFeature>());
                MemoryUtil.StructureToPtr(emptyFeature, pEmptyFeature);
                return pEmptyFeature;
            }

            //Face feature feature filtering
            ASF_FaceFeature faceFeature = MemoryUtil.PtrToStructure<ASF_FaceFeature>(pFaceFeature);
            byte[] feature = new byte[faceFeature.featureSize];
            MemoryUtil.Copy(faceFeature.feature, feature, 0, faceFeature.featureSize);

            ASF_FaceFeature localFeature = new ASF_FaceFeature();
            localFeature.feature = MemoryUtil.Malloc(feature.Length);
            MemoryUtil.Copy(feature, 0, localFeature.feature, feature.Length);
            localFeature.featureSize = feature.Length;
            IntPtr pLocalFeature = MemoryUtil.Malloc(MemoryUtil.SizeOf<ASF_FaceFeature>());
            MemoryUtil.StructureToPtr(localFeature, pLocalFeature);

            //Release the pointer
            MemoryUtil.Free(pSingleFaceInfo);
            MemoryUtil.Free(pFaceFeature);
            MemoryUtil.Free(imageInfo.imgData);

            return pLocalFeature;
        }



        /// <summary>
        /// Age detection
        /// </summary>
        /// <param name="pEngine">Engine Handle</param>
        /// <param name="imageInfo">Image data</param>
        /// <param name="multiFaceInfo">Face detection results</param>
        /// <returns>Age detection structures</returns>
        public static ASF_AgeInfo AgeEstimation(IntPtr pEngine, ImageInfo imageInfo, ASF_MultiFaceInfo multiFaceInfo, out int retCode)
        {
            retCode = -1;
            IntPtr pMultiFaceInfo = MemoryUtil.Malloc(MemoryUtil.SizeOf<ASF_MultiFaceInfo>());
            MemoryUtil.StructureToPtr(multiFaceInfo, pMultiFaceInfo);

            if (multiFaceInfo.faceNum == 0)
            {
                return new ASF_AgeInfo();
            }

            //Face information processing
            retCode = ASFFunctions.ASFProcess(pEngine, imageInfo.width, imageInfo.height, imageInfo.format, imageInfo.imgData, pMultiFaceInfo, FaceEngineMask.ASF_AGE);
            if (retCode == 0)
            {
                //Get age information
                IntPtr pAgeInfo = MemoryUtil.Malloc(MemoryUtil.SizeOf<ASF_AgeInfo>());
                retCode = ASFFunctions.ASFGetAge(pEngine, pAgeInfo);
                Console.WriteLine("Get Age Result:" + retCode);
                ASF_AgeInfo ageInfo = MemoryUtil.PtrToStructure<ASF_AgeInfo>(pAgeInfo);

                //Release memory
                MemoryUtil.Free(pMultiFaceInfo);
                MemoryUtil.Free(pAgeInfo);
                return ageInfo;
            }
            else
            {
                return new ASF_AgeInfo();
            }
        }


        /// <summary>
        /// Single face age test
        /// </summary>
        /// <param name="pEngine">Engine Handle</param>
        /// <param name="image">Image</param>
        /// <param name="singleFaceInfo">Single face information</param>
        /// <returns>Age test results</returns>
        public static ASF_AgeInfo AgeEstimation(IntPtr pEngine, Image image, ASF_SingleFaceInfo singleFaceInfo)
        {
            ImageInfo imageInfo = ImageUtil.ReadBMP(image);
            if(imageInfo == null)
            {
                return new ASF_AgeInfo();
            }
            ASF_MultiFaceInfo multiFaceInfo = new ASF_MultiFaceInfo();
            multiFaceInfo.faceRects = MemoryUtil.Malloc(MemoryUtil.SizeOf<MRECT>());
            MemoryUtil.StructureToPtr<MRECT>(singleFaceInfo.faceRect, multiFaceInfo.faceRects);
            multiFaceInfo.faceOrients = MemoryUtil.Malloc(MemoryUtil.SizeOf<int>());
            MemoryUtil.StructureToPtr<int>(singleFaceInfo.faceOrient, multiFaceInfo.faceOrients);
            multiFaceInfo.faceNum = 1;
            ASF_AgeInfo ageInfo = AgeEstimation(pEngine, imageInfo, multiFaceInfo);
            MemoryUtil.Free(imageInfo.imgData);
            return ageInfo;
        }

        /// <summary>
        /// Gender tests
        /// </summary>
        /// <param name="pEngine">Engine Handle</param>
        /// <param name="imageInfo">Image data</param>
        /// <param name="multiFaceInfo">Face detection results</param>
        /// <returns>Save the gender detection result structure</returns>
        public static ASF_GenderInfo GenderEstimation(IntPtr pEngine, ImageInfo imageInfo, ASF_MultiFaceInfo multiFaceInfo, out int retCode)
        {
            retCode = -1;
            IntPtr pMultiFaceInfo = MemoryUtil.Malloc(MemoryUtil.SizeOf<ASF_MultiFaceInfo>());
            MemoryUtil.StructureToPtr(multiFaceInfo, pMultiFaceInfo);

            if (multiFaceInfo.faceNum == 0)
            {
                return new ASF_GenderInfo();
            }

            //Face information processing
            retCode = ASFFunctions.ASFProcess(pEngine, imageInfo.width, imageInfo.height, imageInfo.format, imageInfo.imgData, pMultiFaceInfo, FaceEngineMask.ASF_GENDER);
            if (retCode == 0)
            {
                //Access to gender information
                IntPtr pGenderInfo = MemoryUtil.Malloc(MemoryUtil.SizeOf<ASF_GenderInfo>());
                retCode = ASFFunctions.ASFGetGender(pEngine, pGenderInfo);
                Console.WriteLine("Get Gender Result:" + retCode);
                ASF_GenderInfo genderInfo = MemoryUtil.PtrToStructure<ASF_GenderInfo>(pGenderInfo);

                //Release memory
                MemoryUtil.Free(pMultiFaceInfo);
                MemoryUtil.Free(pGenderInfo);

                return genderInfo;
            }
            else
            {
                return new ASF_GenderInfo();
            }
        }

        /// <summary>
        /// Face 3D Angle detection
        /// </summary>
        /// <param name="pEngine">Engine Handle</param>
        /// <param name="imageInfo">Image data</param>
        /// <param name="multiFaceInfo">Face detection results</param>
        /// <returns>Save face 3D Angle detection results structure</returns>
        public static ASF_Face3DAngle Face3DAngleDetection(IntPtr pEngine, ImageInfo imageInfo, ASF_MultiFaceInfo multiFaceInfo,out int retCode)
        {
            IntPtr pMultiFaceInfo = MemoryUtil.Malloc(MemoryUtil.SizeOf<ASF_MultiFaceInfo>());
            MemoryUtil.StructureToPtr(multiFaceInfo, pMultiFaceInfo);

            if (multiFaceInfo.faceNum == 0)
            {
                retCode = -1;
                return new ASF_Face3DAngle();
            }

            //Face information processing
            retCode = ASFFunctions.ASFProcess(pEngine, imageInfo.width, imageInfo.height, imageInfo.format, imageInfo.imgData, pMultiFaceInfo, FaceEngineMask.ASF_FACE3DANGLE);
            if (retCode == 0)
            {
                //Get the 3D Angle of the face
                IntPtr pFace3DAngleInfo = MemoryUtil.Malloc(MemoryUtil.SizeOf<ASF_Face3DAngle>());
                retCode = ASFFunctions.ASFGetFace3DAngle(pEngine, pFace3DAngleInfo);
                Console.WriteLine("Get Face3D Angle Result:" + retCode);
                ASF_Face3DAngle face3DAngle = MemoryUtil.PtrToStructure<ASF_Face3DAngle>(pFace3DAngleInfo);

                //Release memory
                MemoryUtil.Free(pMultiFaceInfo);
                MemoryUtil.Free(pFace3DAngleInfo);

                return face3DAngle;
            }else
            {
                return new ASF_Face3DAngle();
            }
        }

        /// <summary>
        /// RGB Vivo detection
        /// </summary>
        /// <param name="pEngine">Engine Handle</param>
        /// <param name="imageInfo">Image data</param>
        /// <param name="multiFaceInfo">In vivo test results</param>
        /// <returns>Preserve the structure of in vivo detection results</returns>
        public static ASF_LivenessInfo LivenessInfo_RGB(IntPtr pEngine, ImageInfo imageInfo, ASF_MultiFaceInfo multiFaceInfo, out int retCode)
        {
            IntPtr pMultiFaceInfo = MemoryUtil.Malloc(MemoryUtil.SizeOf<ASF_MultiFaceInfo>());
            MemoryUtil.StructureToPtr(multiFaceInfo, pMultiFaceInfo);

            if (multiFaceInfo.faceNum == 0)
            {
                retCode = -1;
                //Release memory
                MemoryUtil.Free(pMultiFaceInfo);
                return new ASF_LivenessInfo();
            }

            try
            {
                //Face information processing
                retCode = ASFFunctions.ASFProcess(pEngine, imageInfo.width, imageInfo.height, imageInfo.format, imageInfo.imgData, pMultiFaceInfo, FaceEngineMask.ASF_LIVENESS);
                if (retCode == 0)
                {
                    //Obtain in vivo test results
                    IntPtr pLivenessInfo = MemoryUtil.Malloc(MemoryUtil.SizeOf<ASF_LivenessInfo>());
                    retCode = ASFFunctions.ASFGetLivenessScore(pEngine, pLivenessInfo);
                    Console.WriteLine("Get Liveness Result:" + retCode);
                    ASF_LivenessInfo livenessInfo = MemoryUtil.PtrToStructure<ASF_LivenessInfo>(pLivenessInfo);

                    //Release memory
                    MemoryUtil.Free(pMultiFaceInfo);
                    MemoryUtil.Free(pLivenessInfo);
                    return livenessInfo;
                }
                else
                {
                    //Release memory
                    MemoryUtil.Free(pMultiFaceInfo);
                    return new ASF_LivenessInfo();
                }
            }
            catch
            {
                retCode = -1;
                //Release memory
                MemoryUtil.Free(pMultiFaceInfo);
                return new ASF_LivenessInfo();
            }
        }

        /// <summary>
        /// Infrared in vivo detection
        /// </summary>
        /// <param name="pEngine">Engine Handle</param>
        /// <param name="imageInfo">Image data</param>
        /// <param name="multiFaceInfo">In vivo test results</param>
        /// <returns>Preserve the structure of in vivo detection results</returns>
        public static ASF_LivenessInfo LivenessInfo_IR(IntPtr pEngine, ImageInfo imageInfo, ASF_MultiFaceInfo multiFaceInfo, out int retCode)
        {
            IntPtr pMultiFaceInfo = MemoryUtil.Malloc(MemoryUtil.SizeOf<ASF_MultiFaceInfo>());
            MemoryUtil.StructureToPtr(multiFaceInfo, pMultiFaceInfo);

            if (multiFaceInfo.faceNum == 0)
            {
                retCode = -1;
                //Release memory
                MemoryUtil.Free(pMultiFaceInfo);
                return new ASF_LivenessInfo();
            }

            try
            {
                //Face information processing
                retCode = ASFFunctions.ASFProcess_IR(pEngine, imageInfo.width, imageInfo.height, imageInfo.format, imageInfo.imgData, pMultiFaceInfo, FaceEngineMask.ASF_IR_LIVENESS);
                if (retCode == 0)
                {
                    //Obtain in vivo test results
                    IntPtr pLivenessInfo = MemoryUtil.Malloc(MemoryUtil.SizeOf<ASF_LivenessInfo>());
                    retCode = ASFFunctions.ASFGetLivenessScore_IR(pEngine, pLivenessInfo);
                    Console.WriteLine("Get Liveness Result:" + retCode);
                    ASF_LivenessInfo livenessInfo = MemoryUtil.PtrToStructure<ASF_LivenessInfo>(pLivenessInfo);

                    //Release memory
                    MemoryUtil.Free(pMultiFaceInfo);
                    MemoryUtil.Free(pLivenessInfo);
                    return livenessInfo;
                }
                else
                {
                    //Release memory
                    MemoryUtil.Free(pMultiFaceInfo);
                    return new ASF_LivenessInfo();
                }
            }
            catch
            {
                retCode = -1;
                //Release memory
                MemoryUtil.Free(pMultiFaceInfo);
                return new ASF_LivenessInfo();
            }
        }

        /// <summary>
        /// Gender tests
        /// </summary>
        /// <param name="pEngine">Engine Handle</param>
        /// <param name="imageInfo">Image data</param>
        /// <param name="multiFaceInfo">Face detection results</param>
        /// <returns>Preserve the gender estimation result structure</returns>
        public static ASF_GenderInfo GenderEstimation(IntPtr pEngine, ImageInfo imageInfo, ASF_MultiFaceInfo multiFaceInfo)
        {
            IntPtr pMultiFaceInfo = MemoryUtil.Malloc(MemoryUtil.SizeOf<ASF_MultiFaceInfo>());
            MemoryUtil.StructureToPtr(multiFaceInfo, pMultiFaceInfo);

            if (multiFaceInfo.faceNum == 0)
            {
                return new ASF_GenderInfo();
            }

            //Face information processing
            int retCode = ASFFunctions.ASFProcess(pEngine, imageInfo.width, imageInfo.height, imageInfo.format, imageInfo.imgData, pMultiFaceInfo, FaceEngineMask.ASF_GENDER);

            //Access to gender information
            IntPtr pGenderInfo = MemoryUtil.Malloc(MemoryUtil.SizeOf<ASF_GenderInfo>());
            retCode = ASFFunctions.ASFGetGender(pEngine, pGenderInfo);
            Console.WriteLine("Get Gender Result:" + retCode);
            ASF_GenderInfo genderInfo = MemoryUtil.PtrToStructure<ASF_GenderInfo>(pGenderInfo);

            //Release memory
            MemoryUtil.Free(pMultiFaceInfo);
            MemoryUtil.Free(pGenderInfo);

            return genderInfo;
        }



        /// <summary>
        /// Single face gender test
        /// </summary>
        /// <param name="pEngine">Engine Handle</param>
        /// <param name="image">Image</param>
        /// <param name="singleFaceInfo">Single face information</param>
        /// <returns>Gender estimation results</returns>
        public static ASF_GenderInfo GenderEstimation(IntPtr pEngine, Image image, ASF_SingleFaceInfo singleFaceInfo)
        {
            ImageInfo imageInfo = ImageUtil.ReadBMP(image);
            if(imageInfo == null)
            {
                return new ASF_GenderInfo();
            }
            ASF_MultiFaceInfo multiFaceInfo = new ASF_MultiFaceInfo();
            multiFaceInfo.faceRects = MemoryUtil.Malloc(MemoryUtil.SizeOf<MRECT>());
            MemoryUtil.StructureToPtr<MRECT>(singleFaceInfo.faceRect, multiFaceInfo.faceRects);
            multiFaceInfo.faceOrients = MemoryUtil.Malloc(MemoryUtil.SizeOf<int>());
            MemoryUtil.StructureToPtr<int>(singleFaceInfo.faceOrient, multiFaceInfo.faceOrients);
            multiFaceInfo.faceNum = 1;
            ASF_GenderInfo genderInfo = GenderEstimation(pEngine, imageInfo, multiFaceInfo);
            MemoryUtil.Free(imageInfo.imgData);
            return genderInfo;
        }

        /// <summary>
        /// Age detection
        /// </summary>
        /// <param name="pEngine">Engine Handle</param>
        /// <param name="imageInfo">Image data</param>
        /// <param name="multiFaceInfo">Face detection results</param>
        /// <returns>Age detection structures</returns>
        public static ASF_AgeInfo AgeEstimation(IntPtr pEngine, ImageInfo imageInfo, ASF_MultiFaceInfo multiFaceInfo)
        {
            IntPtr pMultiFaceInfo = MemoryUtil.Malloc(MemoryUtil.SizeOf<ASF_MultiFaceInfo>());
            MemoryUtil.StructureToPtr(multiFaceInfo, pMultiFaceInfo);

            if (multiFaceInfo.faceNum == 0)
            {
                return new ASF_AgeInfo();
            }

            //Face information processing
            int retCode = ASFFunctions.ASFProcess(pEngine, imageInfo.width, imageInfo.height, imageInfo.format, imageInfo.imgData, pMultiFaceInfo, FaceEngineMask.ASF_AGE);

            //Get age information
            IntPtr pAgeInfo = MemoryUtil.Malloc(MemoryUtil.SizeOf<ASF_AgeInfo>());
            retCode = ASFFunctions.ASFGetAge(pEngine, pAgeInfo);
            Console.WriteLine("Get Age Result:" + retCode);
            ASF_AgeInfo ageInfo = MemoryUtil.PtrToStructure<ASF_AgeInfo>(pAgeInfo);

            //Release memory
            MemoryUtil.Free(pMultiFaceInfo);
            MemoryUtil.Free(pAgeInfo);

            return ageInfo;
        }

        /// <summary>
        /// To obtain the face with the largest area in multiple face detection results
        /// </summary>
        /// <param name="multiFaceInfo">Face detection results</param>
        /// <returns>The largest area of face information</returns>
        public static ASF_SingleFaceInfo GetMaxFace(ASF_MultiFaceInfo multiFaceInfo)
        {
            ASF_SingleFaceInfo singleFaceInfo = new ASF_SingleFaceInfo();
            singleFaceInfo.faceRect = new MRECT();
            singleFaceInfo.faceOrient = 1;

            int maxArea = 0;
            int index = -1;
            for (int i = 0; i < multiFaceInfo.faceNum; i++)
            {
                try
                {
                    MRECT rect = MemoryUtil.PtrToStructure<MRECT>(multiFaceInfo.faceRects + MemoryUtil.SizeOf<MRECT>() * i);
                    int area = (rect.right - rect.left) * (rect.bottom - rect.top);
                    if (maxArea <= area)
                    {
                        maxArea = area;
                        index = i;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            if (index != -1)
            {
                singleFaceInfo.faceRect = MemoryUtil.PtrToStructure<MRECT>(multiFaceInfo.faceRects + MemoryUtil.SizeOf<MRECT>() * index);
                singleFaceInfo.faceOrient = MemoryUtil.PtrToStructure<int>(multiFaceInfo.faceOrients + MemoryUtil.SizeOf<int>() * index);
            }
            return singleFaceInfo;
        }
    }
}
