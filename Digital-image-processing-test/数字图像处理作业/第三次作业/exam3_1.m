% 1.�Ѹ���ͼ���ֱ��ͼ������ 
clear;clc;
file_structure = dir('*.bmp');
for i = 1:size(file_structure,1)
    subplot(3,4,i);
    [picture{i},map{i}] = imread(file_structure(i).name);     %��ͼƬ�ĵ�ɫ��Ҳ����ȥ
    p{i}=ind2gray(picture{i},map{i});
    edges = [0:256];
    histogram(p{1,i},edges);   
    title(sprintf(file_structure(i).name));
end

