function [solution iter] = SinglePerceptron(Y,tau)
%
%   solution = SinglePerceptron(Y,tau) 固定增量单样本感知器算法实现
%
%   输入：规范化样本矩阵Y,裕量tau
%   输出：解向量solution，迭代次数iter
%
[y_k, d] = size(Y);
a = zeros(1,d);
k_max = 10000;
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

%在这里实现你的算法

for k = 1:k_max
    count = 0;
    for i = 1:y_k
        y = a * Y(i,:)';
        if(y <= tau)
            a = a + Y(i,:);
            count = count + 1;
        end       
    end
    if(count == 0)
        break
    end
end

%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
% k = k_max;
solution = a;
iter = k-1;
