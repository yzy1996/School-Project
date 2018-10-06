function [solution iter] = SinglePerceptron(Y,tau)
%
%   solution = SinglePerceptron(Y,tau) �̶�������������֪���㷨ʵ��
%
%   ���룺�淶����������Y,ԣ��tau
%   �����������solution����������iter
%
[y_k, d] = size(Y);
a = zeros(1,d);
k_max = 10000;
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

%������ʵ������㷨

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
