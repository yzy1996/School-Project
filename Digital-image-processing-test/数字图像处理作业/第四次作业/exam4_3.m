clc;clear;
picture = imread('test4 copy.bmp');
pictureSize = size(picture);             %获得图像的大小
subplot(3,3,1);
imshow(picture);
title('原始图像');

% %Unsharp masking
picture1 = imgaussfilt(picture, 1.5);   %高斯滤波，指定标准差为1.5
unsharpMask = picture - picture1;
picture2 = picture + unsharpMask;
subplot(3,3,2);
imshow(picture2);
title('自写函数—UnsharpMasking'); 

picture3 = imsharpen(picture);
subplot(3,3,3);
imshow(picture3);
title('调用函数—UnsharpMasking'); 

%Sobel edge detector
hx = [-1 0 1; -2 0 2; -1 0 1];       %横向算子
hy = [1 2 1; 0 0 0; -1 -2 -1];       %纵向算子
% picture = double(picture);
Gx = conv2(picture,hx,'same');
Gy = conv2(picture,hy,'same');
F = abs(Gx) + abs(Gy);
subplot(3,3,4);
imshow(uint8(F));
title('自写函数—SobelEdgeDetector');

picture4 = edge(picture,'sobel');
subplot(3,3,5);
imshow(picture4);
title('调用函数—SobelEdgeDetector'); 

%Laplace edge detection
laplaceModel = [0 1 0; 1 -4 1; 0 1 0];
laplaceTra = conv2(picture,laplaceModel,'same');
subplot(3,3,7);
imshow(uint8(laplaceTra));
title('自写函数—LaplaceEdgeDetection'); 

%Canny algorithm

picture5 = edge(picture,'canny');
subplot(3,3,9);
imshow(picture5);
title('调用函数—CannyAlgorithm'); 