%%��lena��elainͼ��ֱ����ˮƽshear������������Ϊ1.5����������ѡ�񣩺���ת30�ȣ��������ý��ڡ�˫���Ժ�˫���β�ֵ��zoom��2048*2048

picture = imread('��һ����ҵ\lena.bmp');                 %��ȡͼƬ
picture2 = imread('��һ����ҵ\elain1.bmp');

%�Լ�ʵ�ֵĺ�����
A = [1 1.5 0;0 1 0;0 0 1];        %����任���󣬲�������Ϊ1.5
theta=pi/6;                         %������ת�Ƕ�30��
B = [cos(theta) sin(theta) 0;-sin(theta) cos(theta) 0;0 0 1];     %��ת�任����

for i=1:512
    for j=1:512
        c=[i j 1]*A;           %���б任
        d=[i j 1]*B;          %��ת�任
        picture31(round(c(1)),round(c(2)))=picture(i,j);
        %picture32(round(d(1)),round(d(2)))=picture(i,j);
    end
end

%���к���ʵ��
tform = affine2d(A);              %����任
AA = imwarp(picture,tform);        %�����α任Ӧ����ͼ��
rotate = imrotate(picture2,30);    %��ת30��

subplot(3,3,1);
picture11 = imresize(picture31,[2048,2048],'nearest');    %��������ֵ;
imshow(picture11);
title('�Լ�д ˮƽshear ��������ֵ');

subplot(3,3,2);
picture12 = imresize(picture31,[2048,2048],'bilinear');   %˫���Բ�ֵ;
imshow(picture12);
title('�Լ�д ˮƽshear ˫���Բ�ֵ');

subplot(3,3,3);
picture13 = imresize(picture31,[2048,2048],'bicubic');    %˫���β�ֵ;
imshow(picture13);
title('�Լ�д ˮƽshear ˫���β�ֵ');

subplot(3,3,4);
picture11 = imresize(AA,[2048,2048],'nearest');    %��������ֵ;
imshow(picture11);
title('����д ˮƽshear ��������ֵ');

subplot(3,3,5);
picture12 = imresize(AA,[2048,2048],'bilinear');   %˫���Բ�ֵ;
imshow(picture12);
title('����д ˮƽshear ˫���Բ�ֵ');

subplot(3,3,6);
picture13 = imresize(AA,[2048,2048],'bicubic');    %˫���β�ֵ;
imshow(picture13);
title('����д ˮƽshear ˫���β�ֵ');

subplot(3,3,7);
picture21 = imresize(rotate,[2048,2048],'nearest');    %��������ֵ;
imshow(picture21);
title('��ת30�� ��������ֵ');

subplot(3,3,8);
picture22 = imresize(rotate,[2048,2048],'bilinear');   %˫���Բ�ֵ;
imshow(picture22);
title('��ת30�� ˫���Բ�ֵ');

subplot(3,3,9);
picture23 = imresize(rotate,[2048,2048],'bicubic');    %˫���β�ֵ;
imshow(picture23);
title('��ת30�� ˫���β�ֵ');


