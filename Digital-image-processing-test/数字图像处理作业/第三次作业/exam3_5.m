% 5.����ֱ��ͼ��ͼ��elain��woman���зָ�
%���������ֵ���Ĵ���
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

%���õ������Ĵ���
clc;clear;
picture = imread('woman.bmp');
subplot(1,2,1);
imshow(picture);             %��ʾͼƬ
title('ԭͼwoman.bmp');
picture = double(picture);   %ת��Ϊdouble����Ϊ�˺����ۼӼ���
[m,n] = size(picture);       %���ͼƬ�Ĵ�С
T = 90;                      %�趨��ʼ��ֵ
T1 = 0;T2 = 255;
while(1)
    sum1 = 0;sum2 = 0;                      %Ϊ���ۼ�ÿһ��ĻҶ�ֵ
    count1 = 0;count2 = 0;                  %Ϊ���ۼ�ÿһ�������
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
    if abs(T1-T2) == abs(sum1/count1-sum2/count2)     %ѭ������ֹ���������ٱ仯��ע�ⲻ�����߾�ֵ���
        break;
    end
    T1 = sum1/count1;            %�����һ��ƽ���Ҷ�ֵ
    T2 = sum2/count2;            %����ڶ���ƽ���Ҷ�ֵ
    T = (T1 + T2)/2;             %������ֵT   
end
fprintf('��ֵΪ�� %8.5f\n',T)
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
title('ͼ��ָ�woman.bmp');


