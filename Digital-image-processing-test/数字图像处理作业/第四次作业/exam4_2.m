%高斯滤波
clc;clear;
picture = imread('test2.tif');
subplot(1,3,1);
imshow(picture);
title('原始图像3×3');

%%自己写的函数
sigma = 1.5;           %设定高斯滤波器的标准差
blockSize = 3;         %设置滤波器模板大小n*n
blockSize1 = (blockSize - 1) / 2;    %做一个预处理，后面经常会用到
blockSize2 = (blockSize + 1) / 2;    %做一个预处理，后面经常会用到

%%%生成高斯滤波器模板
sum1 = 0;               %为了高斯模板归一化
gauss = @(x,y)(1 / (2 * pi * sigma^2)) * exp(-(x^2 + y^2) / (2 * sigma^2));      %二维高斯函数
gaussMatrix = zeros(blockSize,blockSize);   %声明矩阵大小
for i = blockSize1 : -1 : -blockSize1       %倒着循环
    for j = -blockSize1 : blockSize1        %例如3*3的就是从-1到1
        gaussMatrix(blockSize2 - i,j + blockSize2) = gauss(j,i);
        sum1 = sum1 + gaussMatrix(blockSize2 - i,j + blockSize2);
    end
end
gaussMatrix = gaussMatrix / sum1;    %归一化处理

pictureSize = size(picture);             %获得图像的大小
pictureFilter = zeros(pictureSize(1),pictureSize(2));    %滤波后的图像
pictureExpand = imresize(picture,[pictureSize(1) + ((blockSize - 1)),pictureSize(2) + ((blockSize - 1))],'bicubic');   %将原图像进行扩充

for i = 1:pictureSize(1)
    for j = 1:pictureSize(2)
        block = pictureExpand(i:i + blockSize - 1,j:j + blockSize - 1);    %提取一个拓展后的图像块
        value = gaussMatrix .* double(block);              %做卷积运算 
        pictureFilter(i,j) =  sum(value(:));
    end
end
subplot(1,3,2);
imshow(uint8(pictureFilter));
title('自己写—高斯滤波')

%%调用自带的函数
picture1 = imgaussfilt(picture, 1.5);   %高斯滤波，指定标准差为1.5
subplot(1,3,3);
imshow(picture1);
title('调用函数—高斯滤波'); 