% 2.������ͼ�����ֱ��ͼ���⣻���������ͼ���Դͼ����бȶԣ������������ݣ�
clc;clear;
file_structure = dir('*.bmp');         %���ظ�Ŀ¼�µ�.bmp��ʽ�������ļ�
edges = [0:256];           %����ֱ��ͼ����ʾ�߽�
for i = 1:size(file_structure,1)
    figure(i);             %����i��ͼ
    figure('Name',sprintf(file_structure(i).name));
    [picture{i},map{i}] = imread(file_structure(i).name);        %��ͼƬ�������˵�ɫ�� 
    p{i}=ind2gray(picture{i},map{i});                 %����ɫ�̵�Ч��������ȥ����������Ч����ͼ
    subplot(2,2,1);                            
    imshow(p{i});                  %��ʾԭͼ
    title(sprintf('ԭͼ'));
    subplot(2,2,2);        
    histogram(p{i},edges);         %��ʾԭͼ��ֱ��ͼ
    title(sprintf('ԭͼֱ��ͼ'));
    pp=histeq(p{1,i});             %��ֱ��ͼ����任
    subplot(2,2,3);
    imshow(pp);                    %��ʾ������ֱ��ͼ
    title(sprintf('������ͼ'));
    subplot(2,2,4);
    histogram(pp,edges);           %��ʾ������ֱ��ͼ  
    title(sprintf('������ֱ��ͼ'));
end


