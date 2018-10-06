%��˹�˲�
clc;clear;
picture = imread('test2.tif');
subplot(1,3,1);
imshow(picture);
title('ԭʼͼ��3��3');

%%�Լ�д�ĺ���
sigma = 1.5;           %�趨��˹�˲����ı�׼��
blockSize = 3;         %�����˲���ģ���Сn*n
blockSize1 = (blockSize - 1) / 2;    %��һ��Ԥ�������澭�����õ�
blockSize2 = (blockSize + 1) / 2;    %��һ��Ԥ�������澭�����õ�

%%%���ɸ�˹�˲���ģ��
sum1 = 0;               %Ϊ�˸�˹ģ���һ��
gauss = @(x,y)(1 / (2 * pi * sigma^2)) * exp(-(x^2 + y^2) / (2 * sigma^2));      %��ά��˹����
gaussMatrix = zeros(blockSize,blockSize);   %���������С
for i = blockSize1 : -1 : -blockSize1       %����ѭ��
    for j = -blockSize1 : blockSize1        %����3*3�ľ��Ǵ�-1��1
        gaussMatrix(blockSize2 - i,j + blockSize2) = gauss(j,i);
        sum1 = sum1 + gaussMatrix(blockSize2 - i,j + blockSize2);
    end
end
gaussMatrix = gaussMatrix / sum1;    %��һ������

pictureSize = size(picture);             %���ͼ��Ĵ�С
pictureFilter = zeros(pictureSize(1),pictureSize(2));    %�˲����ͼ��
pictureExpand = imresize(picture,[pictureSize(1) + ((blockSize - 1)),pictureSize(2) + ((blockSize - 1))],'bicubic');   %��ԭͼ���������

for i = 1:pictureSize(1)
    for j = 1:pictureSize(2)
        block = pictureExpand(i:i + blockSize - 1,j:j + blockSize - 1);    %��ȡһ����չ���ͼ���
        value = gaussMatrix .* double(block);              %��������� 
        pictureFilter(i,j) =  sum(value(:));
    end
end
subplot(1,3,2);
imshow(uint8(pictureFilter));
title('�Լ�д����˹�˲�')

%%�����Դ��ĺ���
picture1 = imgaussfilt(picture, 1.5);   %��˹�˲���ָ����׼��Ϊ1.5
subplot(1,3,3);
imshow(picture1);
title('���ú�������˹�˲�'); 