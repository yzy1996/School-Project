% 4.��elain��lenaͼ�����7*7�ľֲ�ֱ��ͼ��ǿ��
tic
picture1 = imread('elain.bmp');
subplot(2,2,1);
imshow(picture1);
p1 = imresize(picture1,[518,518],'bicubic');    %˫���β�ֵ;
for i=1:512
    for j=1:512                
        block = p1(i:i+6,j:j+6 );          %��̬��7*7�������������        
        pp1 = histeq(block);               %����������ֱ��ͼ���⴦��
        ppp1(i,j) = pp1(4,4);            %�ٰ�block�м���Ǹ�ֵ�����µ�ͼ�����
    end
end
subplot(2,2,2);
imshow(ppp1);   

picture2 = imread('lena.bmp');
subplot(2,2,3);
imshow(picture2);
p2 = imresize(picture2,[518,518],'bicubic');    %˫���β�ֵ;
for i=1:512
    for j=1:512                
        block = p2(i:i+6,j:j+6 );          %��̬��7*7�������������        
        pp2 = histeq(block);               %����������ֱ��ͼ���⴦��
        ppp2(i,j) = pp2(4,4);            %�ٰ�block�м���Ǹ�ֵ�����µ�ͼ�����
    end
end
subplot(2,2,4);
imshow(ppp2);   
toc