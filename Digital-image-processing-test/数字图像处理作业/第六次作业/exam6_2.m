clear;clc;
picture = imread('lena.bmp');
subplot(3,3,1:3);
imshow(picture);
title('ԭʼͼ��');

picture_noise1 = imnoise(picture,'salt & pepper',0.1); %���к����ܲ�����˹����
subplot(3,3,5);
imshow(picture_noise1);
title('��������');

picture1 = medfilt2(picture_noise1,[5 5]);
subplot(3,3,7);
imshow(picture1);
title('��ֵ�˲�');

picture2 = imgaussfilt(picture_noise1, 1.5);  

subplot(3,3,8);

imshow(picture2);
title('��˹�˲�'); 
