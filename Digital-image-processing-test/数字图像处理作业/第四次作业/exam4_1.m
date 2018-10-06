%��ֵ�˲�
clc;clear;
picture = imread('test1.pgm');
subplot(1,3,1);
imshow(picture);
title('ԭʼͼ��7��7');

%%�Լ�д���˲���
blockSize = 7;                           %�����˲���ģ���Сn*n
pictureSize = size(picture);             %���ͼ��Ĵ�С
pictureFilter = zeros(pictureSize(1),pictureSize(2));
pictureExpand = imresize(picture,[pictureSize(1) + ((blockSize - 1)),pictureSize(2) + ((blockSize - 1))],'bicubic');   %��ԭͼ���������

for i = 1:pictureSize(1)
    for j = 1:pictureSize(2)
        block = pictureExpand(i:i + blockSize - 1,j:j + blockSize - 1);   %��ȡһ����չ���ͼ���
        block_sort = sort(block(:));                            %�Ƚ�block�е�ÿһ��Ԫ�ش�С��������
        value_middle = block_sort((blockSize^2 - 1) / 2);      %Ȼ��ȡ��ֵ
        pictureFilter(i,j) = value_middle;
    end
end
subplot(1,3,2);
imshow(uint8(pictureFilter));
title('�Լ�д����ֵ�˲�')

%%���ú���medfilt2()ʵ�ֵ���ֵ�˲�
picture1 = medfilt2(picture,[blockSize blockSize]);
subplot(1,3,3);
imshow(picture1);
title('���ú�������ֵ�˲�');