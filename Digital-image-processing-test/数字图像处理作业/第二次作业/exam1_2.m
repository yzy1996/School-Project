%%����lenaͼ��ľ�ֵ����

picture=imread('��һ����ҵ\lena.bmp');

a=mean2(picture);      %�����ֵ
fprintf('��ֵΪ�� %8.5f\n',a)

b=std2(picture);       %�����׼��
c=b^2;                 %���㷽��
fprintf('����Ϊ�� %8.5f\n',c)
