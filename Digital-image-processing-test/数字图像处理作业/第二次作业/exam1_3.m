%%��lenaͼ���ý��ڡ�˫���Ժ�˫���β�ֵ��zoom��2048*2048
%%�������к������ģ�
picture=imread('��һ����ҵ\lena.bmp');

subplot(2,3,1);
picture1=imresize(picture,[2048,2048],'nearest');    %��������ֵ;
imshow(picture1);
title('����ʵ�֣���������ֵ');

subplot(2,3,2);
picture2=imresize(picture,[2048,2048],'bilinear');   %˫���Բ�ֵ;
imshow(picture2);
title('����ʵ�֣�˫���Բ�ֵ');

subplot(2,3,3);
picture3=imresize(picture,[2048,2048],'bicubic');    %˫���β�ֵ;
imshow(picture3);
title('����ʵ�֣�˫���β�ֵ');

%%�Լ�дʵ�֣�ԭͼ����512*512��Ҫ���2048*2048��Ҫ����4��

subplot(2,3,4);                                      %��������ֵ;
for i=1:2048
    for j=1:2048
        picture4(i,j) = picture(ceil(i/4),ceil(j/4));
    end
end
imshow(picture4);
title('�Լ�ʵ�֣���������ֵ');

% subplot(2,3,5);                                      %˫���Բ�ֵ;
% for i=1:2048
%     for j=1:2048
%         aa=i/4;
%         bb=j/4;
%         a=ceil(aa);        %�ҵ��˾���Ŀ��������ԭʼ�� ������
%         b=ceil(bb);        %�ҵ��˾���Ŀ��������ԭʼ�� ������
%         if(aa<a&&bb<b)     %���������Ͻ�
%             flag=1;
%         end
%         if(aa>a&&bb<b)     %���������Ͻ�
%             flag=2;
%         end
%         if(aa<a&&bb>b)     %���������½�
%             flag=3;
%         end
%         if(aa>a&&bb>b)     %���������½�
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
% title('�Լ�ʵ�֣�˫���Բ�ֵ');




