function y = myhist(img)
y = zeros(256,1);
[m,n] = size(img);
for i = 1:m
    for j = 1:n
        y(img(i,j)+1)=y(img(i,j)+1)+1;
    end
end
