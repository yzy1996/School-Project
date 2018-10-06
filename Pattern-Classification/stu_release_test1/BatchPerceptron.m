function [solution iter] = BatchPerceptron(Y,tau)
%
%   solution = BatchPerceptron(Y,tau) �������֪���㷨
%
%   ���룺�淶����������Y,ԣ��tau=1
%   �����������solution����������iter
%
[y_k,d] = size(Y);       %Y������������������Ϊ20*3�ľ���    ���� y_k=20 d=3
a = zeros(1,d);          %a��Ȩ������������1*3�ľ���
k_max = 10000;           %����������
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

%������ʵ������㷨
theta = 0.001;           %��ֵ
eta = 0.01;             %ѧϰ��

for k = 1:k_max
    YY = a * Y';               %��������������YY��1*20�ľ���,<bΪ���
    a = a + eta * sum(Y(YY <= tau, :));   %��������a
    if(norm(eta * sum(Y(YY <= tau, :))) < theta)    %����ѭ�����ж�����
        break
    end
end


%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
% k = k_max;
solution = a;
iter = k-1;
