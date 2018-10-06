%%计算lena图像的均值方差

picture=imread('第一次作业\lena.bmp');

a=mean2(picture);      %计算均值
fprintf('均值为： %8.5f\n',a)

b=std2(picture);       %计算标准差
c=b^2;                 %计算方差
fprintf('方差为： %8.5f\n',c)
