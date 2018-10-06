% 2.把所有图像进行直方图均衡；输出均衡后的图像和源图像进行比对；分析改善内容；
clc;clear;
file_structure = dir('*.bmp');         %加载根目录下的.bmp格式的所有文件
edges = [0:256];           %设置直方图的显示边界
for i = 1:size(file_structure,1)
    figure(i);             %画第i个图
    figure('Name',sprintf(file_structure(i).name));
    [picture{i},map{i}] = imread(file_structure(i).name);        %读图片，包括了调色盘 
    p{i}=ind2gray(picture{i},map{i});                 %将调色盘的效果加载上去，产生真正效果的图
    subplot(2,2,1);                            
    imshow(p{i});                  %显示原图
    title(sprintf('原图'));
    subplot(2,2,2);        
    histogram(p{i},edges);         %显示原图的直方图
    title(sprintf('原图直方图'));
    pp=histeq(p{1,i});             %作直方图均衡变换
    subplot(2,2,3);
    imshow(pp);                    %显示均衡后的直方图
    title(sprintf('均衡后的图'));
    subplot(2,2,4);
    histogram(pp,edges);           %显示均衡后的直方图  
    title(sprintf('均衡后的直方图'));
end


