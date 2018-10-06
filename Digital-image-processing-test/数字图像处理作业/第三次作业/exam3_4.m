% 4.对elain和lena图像进行7*7的局部直方图增强；
tic
picture1 = imread('elain.bmp');
subplot(2,2,1);
imshow(picture1);
p1 = imresize(picture1,[518,518],'bicubic');    %双三次插值;
for i=1:512
    for j=1:512                
        block = p1(i:i+6,j:j+6 );          %动态的7*7矩阵来储存变量        
        pp1 = histeq(block);               %对这个块进行直方图均衡处理
        ppp1(i,j) = pp1(4,4);            %再把block中间的那个值赋给新的图像矩阵
    end
end
subplot(2,2,2);
imshow(ppp1);   

picture2 = imread('lena.bmp');
subplot(2,2,3);
imshow(picture2);
p2 = imresize(picture2,[518,518],'bicubic');    %双三次插值;
for i=1:512
    for j=1:512                
        block = p2(i:i+6,j:j+6 );          %动态的7*7矩阵来储存变量        
        pp2 = histeq(block);               %对这个块进行直方图均衡处理
        ppp2(i,j) = pp2(4,4);            %再把block中间的那个值赋给新的图像矩阵
    end
end
subplot(2,2,4);
imshow(ppp2);   
toc