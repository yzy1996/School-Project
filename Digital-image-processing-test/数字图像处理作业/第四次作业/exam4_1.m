%中值滤波
clc;clear;
picture = imread('test1.pgm');
subplot(1,3,1);
imshow(picture);
title('原始图像7×7');

%%自己写的滤波器
blockSize = 7;                           %设置滤波器模板大小n*n
pictureSize = size(picture);             %获得图像的大小
pictureFilter = zeros(pictureSize(1),pictureSize(2));
pictureExpand = imresize(picture,[pictureSize(1) + ((blockSize - 1)),pictureSize(2) + ((blockSize - 1))],'bicubic');   %将原图像进行扩充

for i = 1:pictureSize(1)
    for j = 1:pictureSize(2)
        block = pictureExpand(i:i + blockSize - 1,j:j + blockSize - 1);   %提取一个拓展后的图像块
        block_sort = sort(block(:));                            %先将block中的每一个元素从小到大排列
        value_middle = block_sort((blockSize^2 - 1) / 2);      %然后取中值
        pictureFilter(i,j) = value_middle;
    end
end
subplot(1,3,2);
imshow(uint8(pictureFilter));
title('自己写—中值滤波')

%%调用函数medfilt2()实现的中值滤波
picture1 = medfilt2(picture,[blockSize blockSize]);
subplot(1,3,3);
imshow(picture1);
title('调用函数—中值滤波');