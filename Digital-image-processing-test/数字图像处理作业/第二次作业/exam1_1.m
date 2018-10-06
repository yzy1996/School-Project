%%把lena 512*512图像灰度级逐级递减8-1显示

picture=imread('第一次作业\lena.bmp');
subplot(2,4,1);
imshow(picture);
title('256级原图像');   
for i = 5:5            
    subplot(2,4,i+1);
    a=2^i;
    b=floor(double(picture)/a);
    c=255/(2^(8-i)-1);
    d=uint8(b*c);
    imshow(d);                        
    title(sprintf("%d级灰度图像", 2^(8-i)));
end

