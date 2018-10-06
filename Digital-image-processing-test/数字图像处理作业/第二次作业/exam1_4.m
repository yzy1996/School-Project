%%把lena和elain图像分别进行水平shear（参数可设置为1.5，或者自行选择）和旋转30度，并采用用近邻、双线性和双三次插值法zoom到2048*2048

picture = imread('第一次作业\lena.bmp');                 %读取图片
picture2 = imread('第一次作业\elain1.bmp');

%自己实现的函数：
A = [1 1.5 0;0 1 0;0 0 1];        %仿射变换矩阵，参数设置为1.5
theta=pi/6;                         %设置旋转角度30°
B = [cos(theta) sin(theta) 0;-sin(theta) cos(theta) 0;0 0 1];     %旋转变换矩阵

for i=1:512
    for j=1:512
        c=[i j 1]*A;           %错切变换
        d=[i j 1]*B;          %旋转变换
        picture31(round(c(1)),round(c(2)))=picture(i,j);
        %picture32(round(d(1)),round(d(2)))=picture(i,j);
    end
end

%现有函数实现
tform = affine2d(A);              %仿射变换
AA = imwarp(picture,tform);        %将几何变换应用于图形
rotate = imrotate(picture2,30);    %旋转30度

subplot(3,3,1);
picture11 = imresize(picture31,[2048,2048],'nearest');    %最近邻域插值;
imshow(picture11);
title('自己写 水平shear 最近邻域插值');

subplot(3,3,2);
picture12 = imresize(picture31,[2048,2048],'bilinear');   %双线性插值;
imshow(picture12);
title('自己写 水平shear 双线性插值');

subplot(3,3,3);
picture13 = imresize(picture31,[2048,2048],'bicubic');    %双三次插值;
imshow(picture13);
title('自己写 水平shear 双三次插值');

subplot(3,3,4);
picture11 = imresize(AA,[2048,2048],'nearest');    %最近邻域插值;
imshow(picture11);
title('函数写 水平shear 最近邻域插值');

subplot(3,3,5);
picture12 = imresize(AA,[2048,2048],'bilinear');   %双线性插值;
imshow(picture12);
title('函数写 水平shear 双线性插值');

subplot(3,3,6);
picture13 = imresize(AA,[2048,2048],'bicubic');    %双三次插值;
imshow(picture13);
title('函数写 水平shear 双三次插值');

subplot(3,3,7);
picture21 = imresize(rotate,[2048,2048],'nearest');    %最近邻域插值;
imshow(picture21);
title('旋转30度 最近邻域插值');

subplot(3,3,8);
picture22 = imresize(rotate,[2048,2048],'bilinear');   %双线性插值;
imshow(picture22);
title('旋转30度 双线性插值');

subplot(3,3,9);
picture23 = imresize(rotate,[2048,2048],'bicubic');    %双三次插值;
imshow(picture23);
title('旋转30度 双三次插值');


