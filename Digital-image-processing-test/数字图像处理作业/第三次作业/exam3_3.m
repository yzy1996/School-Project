% 3.��һ����ͼ���ն�Դͼ��ֱ��ͼ�Ĺ۲죬��������ָ����ͬԴͼ���ֱ��ͼ������ֱ��ͼƥ�䣬����ͼ����ǿ��
clc;clear;
picture=imread('lena.bmp');
shist=imhist(picture);             %��ȡģ��ֱ��ͼ
[picture1,map1]=imread('lena4.bmp');
p1=ind2gray(picture1,map1);        %�õ����ǵ�ͼ������
pp=histeq(p1,shist);               %���ú�������ֱ��ͼ����
subplot(2,3,[1,3]);  
imshowpair(p1,pp,'montage');       %��ʾԭͼ��ƥ����ͼ��
subplot(2,3,4);  
histogram(picture);
title(sprintf('ģ��ֱ��ͼ'));
subplot(2,3,5);
histogram(p1);
title(sprintf('���任ͼ��ֱ��ͼ'));
subplot(2,3,6);
histogram(pp);
title(sprintf('�任��ֱ��ͼ'));

