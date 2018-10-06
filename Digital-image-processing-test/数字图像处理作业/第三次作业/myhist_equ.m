function y = myhist_equ(img)
a = myhist(img);
[m,n] = size(img);
a_p = a / (m * n);
b = zeros(1,256);
for i = 1:256
    t = 0;
    for j = 1:i
        t = t + a_p(j);
    end
    b(i) = floor(255 * t);
end
y = uint8(zeros(m,n));
for i = 1:m
    for j = 1:n
        y(i,j) = uint8(b(img(i,j)+1));
    end
end