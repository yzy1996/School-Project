% 5.利用直方图对图像elain和woman进行分割
%采用最简单阈值法的代码
clc;clear;
picture1=imread('elain.bmp');
subplot(2,2,1);
imshow(picture1);
threshold1=150;
[m1,n1]=size(picture1);
pp=zeros(m1,n1);
for i=1:m1
    for j=1:n1
        if(picture1(i,j)>threshold1)
            pp(i,j)=1;
        else
            pp(i,j)=0;
        end
    end
end
subplot(2,2,2);
imshow(pp);


picture2=imread('woman.bmp');
subplot(2,2,3);
imshow(picture2);
threshold2=100;
[m2,n2]=size(picture2);
ppp=zeros(m2,n2);
for i=1:m2
    for j=1:n2
        if(picture2(i,j)>threshold2)
            ppp(i,j)=1;
        else
            ppp(i,j)=0;
        end
    end
end
subplot(2,2,4);
imshow(ppp);

%采用迭代法的代码
clc;clear;
picture = imread('woman.bmp');
subplot(1,2,1);
imshow(picture);             %显示图片
title('原图woman.bmp');
picture = double(picture);   %转换为double类型为了后面累加计算
[m,n] = size(picture);       %获得图片的大小
T = 90;                      %设定初始阈值
T1 = 0;T2 = 255;
while(1)
    sum1 = 0;sum2 = 0;                      %为了累加每一类的灰度值
    count1 = 0;count2 = 0;                  %为了累加每一类的数量
    for i = 1:m
        for j = 1:n
            if picture(i,j) > T
                sum1 = sum1 + picture(i,j);
                count1 = count1+1;
            else
                sum2 = sum2 + picture(i,j);
                count2 = count2 + 1;
            end
        end
    end
    if abs(T1-T2) == abs(sum1/count1-sum2/count2)     %循环的终止条件，不再变化，注意不是两者均值相等
        break;
    end
    T1 = sum1/count1;            %计算第一类平均灰度值
    T2 = sum2/count2;            %计算第二类平均灰度值
    T = (T1 + T2)/2;             %更新阈值T   
end
fprintf('阈值为： %8.5f\n',T)
J = zeros(m,n);
for i = 1:m
    for j = 1:n
        if picture(i,j)>T
            J(i,j) = 1;
        else
            J(i,j) = 0;
        end
    end
end
subplot(1,2,2);
imshow(J)
title('图像分割woman.bmp');


