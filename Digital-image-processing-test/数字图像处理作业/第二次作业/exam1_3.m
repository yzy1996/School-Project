%%把lena图像用近邻、双线性和双三次插值法zoom到2048*2048
%%利用现有函数做的：
picture=imread('第一次作业\lena.bmp');

subplot(2,3,1);
picture1=imresize(picture,[2048,2048],'nearest');    %最近邻域插值;
imshow(picture1);
title('函数实现：最近邻域插值');

subplot(2,3,2);
picture2=imresize(picture,[2048,2048],'bilinear');   %双线性插值;
imshow(picture2);
title('函数实现：双线性插值');

subplot(2,3,3);
picture3=imresize(picture,[2048,2048],'bicubic');    %双三次插值;
imshow(picture3);
title('函数实现：双三次插值');

%%自己写实现，原图像是512*512，要变成2048*2048，要扩大4倍

subplot(2,3,4);                                      %最近邻域插值;
for i=1:2048
    for j=1:2048
        picture4(i,j) = picture(ceil(i/4),ceil(j/4));
    end
end
imshow(picture4);
title('自己实现：最近邻域插值');

% subplot(2,3,5);                                      %双线性插值;
% for i=1:2048
%     for j=1:2048
%         aa=i/4;
%         bb=j/4;
%         a=ceil(aa);        %找到了距离目标点最近的原始点 横坐标
%         b=ceil(bb);        %找到了距离目标点最近的原始点 纵坐标
%         if(aa<a&&bb<b)     %在它的左上角
%             flag=1;
%         end
%         if(aa>a&&bb<b)     %在它的右上角
%             flag=2;
%         end
%         if(aa<a&&bb>b)     %在它的左下角
%             flag=3;
%         end
%         if(aa>a&&bb>b)     %在它的右下角
%             flag=3;
%         end
%         
%         switch flag
%             case 1
%                 picture5(i,j) = picture(a,b);
%             case 2
%                 ;
%             case 3
%                 ;
%             case 4
%                 ;
%         end
%     end
% end
% imshow(picture5);
% title('自己实现：双线性插值');




