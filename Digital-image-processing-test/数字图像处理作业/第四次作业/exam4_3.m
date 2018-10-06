clc;clear;
picture = imread('test4 copy.bmp');
pictureSize = size(picture);             %���ͼ��Ĵ�С
subplot(3,3,1);
imshow(picture);
title('ԭʼͼ��');

% %Unsharp masking
picture1 = imgaussfilt(picture, 1.5);   %��˹�˲���ָ����׼��Ϊ1.5
unsharpMask = picture - picture1;
picture2 = picture + unsharpMask;
subplot(3,3,2);
imshow(picture2);
title('��д������UnsharpMasking'); 

picture3 = imsharpen(picture);
subplot(3,3,3);
imshow(picture3);
title('���ú�����UnsharpMasking'); 

%Sobel edge detector
hx = [-1 0 1; -2 0 2; -1 0 1];       %��������
hy = [1 2 1; 0 0 0; -1 -2 -1];       %��������
% picture = double(picture);
Gx = conv2(picture,hx,'same');
Gy = conv2(picture,hy,'same');
F = abs(Gx) + abs(Gy);
subplot(3,3,4);
imshow(uint8(F));
title('��д������SobelEdgeDetector');

picture4 = edge(picture,'sobel');
subplot(3,3,5);
imshow(picture4);
title('���ú�����SobelEdgeDetector'); 

%Laplace edge detection
laplaceModel = [0 1 0; 1 -4 1; 0 1 0];
laplaceTra = conv2(picture,laplaceModel,'same');
subplot(3,3,7);
imshow(uint8(laplaceTra));
title('��д������LaplaceEdgeDetection'); 

%Canny algorithm

picture5 = edge(picture,'canny');
subplot(3,3,9);
imshow(picture5);
title('���ú�����CannyAlgorithm'); 