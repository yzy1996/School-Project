%%��lena 512*512ͼ��Ҷȼ��𼶵ݼ�8-1��ʾ

picture=imread('��һ����ҵ\lena.bmp');
subplot(2,4,1);
imshow(picture);
title('256��ԭͼ��');   
for i = 5:5            
    subplot(2,4,i+1);
    a=2^i;
    b=floor(double(picture)/a);
    c=255/(2^(8-i)-1);
    d=uint8(b*c);
    imshow(d);                        
    title(sprintf("%d���Ҷ�ͼ��", 2^(8-i)));
end

