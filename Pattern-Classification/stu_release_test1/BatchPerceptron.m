function [solution iter] = BatchPerceptron(Y,tau)
%
%   solution = BatchPerceptron(Y,tau) 批处理感知器算法
%
%   输入：规范化样本矩阵Y,裕量tau=1
%   输出：解向量solution，迭代次数iter
%
[y_k,d] = size(Y);       %Y是样本数，该数据下为20*3的矩阵    所以 y_k=20 d=3
a = zeros(1,d);          %a是权向量，这里是1*3的矩阵
k_max = 10000;           %最大迭代次数
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

%在这里实现你的算法
theta = 0.001;           %阈值
eta = 0.01;             %学习率

for k = 1:k_max
    YY = a * Y';               %求出分类后的情况，YY是1*20的矩阵,<b为错分
    a = a + eta * sum(Y(YY <= tau, :));   %修正向量a
    if(norm(eta * sum(Y(YY <= tau, :))) < theta)    %结束循环的判断条件
        break
    end
end


%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
% k = k_max;
solution = a;
iter = k-1;
