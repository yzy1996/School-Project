% 3.进一步把图像按照对源图像直方图的观察，各自自行指定不同源图像的直方图，进行直方图匹配，进行图像增强；
clc;clear;
picture=imread('lena.bmp');
shist=imhist(picture);             %获取模板直方图
[picture1,map1]=imread('lena4.bmp');
p1=ind2gray(picture1,map1);        %得到真是的图像数据
pp=histeq(p1,shist);               %调用函数进行直方图均衡
subplot(2,3,[1,3]);  
imshowpair(p1,pp,'montage');       %显示原图和匹配后的图像
subplot(2,3,4);  
histogram(picture);
title(sprintf('模板直方图'));
subplot(2,3,5);
histogram(p1);
title(sprintf('待变换图像直方图'));
subplot(2,3,6);
histogram(pp);
title(sprintf('变换后直方图'));

