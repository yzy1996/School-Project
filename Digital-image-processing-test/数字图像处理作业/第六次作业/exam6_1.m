clear;clc;
picture = imread('lena.bmp');
subplot(3,3,1:3);
imshow(picture);
title('原始图像');

% y = gaussmf(x,[sig c]) 
% y1 = exp(-((x-c)^2)/(2*sig^2))

picture_noise1 = imnoise(picture, 'gaussian', 0, 0.01); %现有函数能产生高斯噪声
subplot(3,3,4);
imshow(picture_noise1);
title('高斯噪声-均值0-方差0.01');

picture_noise2 = imnoise(picture, 'gaussian', 0, 0.1); %现有函数能产生高斯噪声
subplot(3,3,5);
imshow(picture_noise2);
title('高斯噪声-均值0-方差0.1');

picture_noise3 = imnoise(picture, 'gaussian', 0.5, 0.01); %现有函数能产生高斯噪声
subplot(3,3,6);
imshow(picture_noise3);
title('高斯噪声-均值0.5-方差0.01');

picture1 = medfilt2(picture_noise1,[5 5]);
subplot(3,3,7);
imshow(picture1);
title('中值滤波');

picture2 = imgaussfilt(picture_noise1, 1.5);   %高斯滤波，指定标准差为1.5
subplot(3,3,8);
imshow(picture2);
title('高斯滤波'); 