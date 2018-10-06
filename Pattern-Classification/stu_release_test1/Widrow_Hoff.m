function [solution iter] = Widrow_Hoff(Y,stepsize)
%
%    solution = Widrow_Hoff(Y,stepsize)最小均方差实现算法
%
%   输入：规范化样本矩阵Y,裕量tau,初始步长stepsize
%   输出：解向量solution，迭代次数iter
%
[y_k, d] = size(Y);
a = zeros(1,d);
b = ones(1,y_k);
k_max = 10000;
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

%在这里实现你的算法
theta = 0.001;           %阈值

for k = 1:k_max
    YY = a * Y';
    a = a + stepsize / k * (b - YY) * Y;
    if(norm(stepsize / k * (b - YY) * Y) < theta)
        break
    end
end

%%%%%方案2
% for k = 1:k_max
%     count = 0;
%     for i = 1:y_k
%         y = a * Y(i,:)';
%         if(abs((b(i) - y)) >= 1)
%             a = a + stepsize * (b(i) - y) * Y(i,:);
%             count = count + 1;
%         end       
%     end
%     if(count == 0)
%         break
%     end
% end


%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
% k = k_max;
solution = a;
iter = k-1;