clear;clc;
picture = imread('lena.bmp');
subplot(2,3,1);
imshow(picture);
title('原始图像');

picture = double(picture);
P = fft2(picture);
P = fftshift(P);
[U, V] = size(P);
a = 0.1; b = 0.1; T = 1;
for u = 1:U
    for v = 1:V
        H(u,v) = (T/(pi*(u*a+v*b)))*sin(pi*(u*a+v*b))*exp(-sqrt(-1)*pi*(u*a+v*b));
        G(u,v) = H(u,v)*P(u,v);
    end
end
G = ifftshift(G);
g = ifft2(G);
g=256.*g./max(max(g));
g=uint8(real(g));

subplot(2,3,2);
change = fspecial('motion',50,45);
picture1 = imfilter(g,change,'circular','conv');
imshow(picture1);
title('运动模糊图像');

picture2 = imnoise(picture1,'gaussian',0,0.01);
subplot(2,3,3);
imshow(picture2);
title('运动模糊+高斯噪声图像');

pp = im2double(imread('lena.bmp'));
blurred = imfilter(pp, change, 'conv', 'circular');
picture3 = mydeconvwnr(blurred, change, 0);
subplot(2,3,4);
imshow(picture3);
title('维纳滤波');
