clc;clear;
picture = imread('test4 copy.bmp');
subplot(1,2,1);
imshow(picture);
title('ԭʼͼ��');

%Ƶ���˲�����Ҫ���и���Ҷ�任
picture = double(picture);
FourierTransform = fft2(picture);
FourierTransform = fftshift(FourierTransform);
[x, y] = size(FourierTransform);
D = zeros(x, y);
H = zeros(x, y);
G = zeros(x, y);
%�趨����
n = 2;       %������˹�˲����Ľ���
D0 = 25;     %��ֹƵ�ʰ뾶
for  u = 1:x
    for v = 1:y
        D(u, v) = sqrt((u - (x/2))^2 + (v - (y/2))^2);
        H(u, v) = 1 / (1 + (D0 / D(u, v))^(2 * n));
        G(u, v) = H(u, v) * FourierTransform(u, v);
    end
end
picture1 = ifftshift(G);
picture1 = ifft2(picture1);
picture1 = uint8(real(picture1));
subplot(1,2,2);
imshow(picture1);
title('������ͼ��');

%���㹦����
s = 0;
s1 = 0;
for u = 1:x
    for v = 1:y
        L1 = (abs(G(u, v)))^2;
        s1 = s1 + L1;
        L = (abs(FourierTransform(u, v)))^2;
        s = s + L;
    end
end
fprintf('�����ױ�Ϊ�� %8.5f\n',s1/s)