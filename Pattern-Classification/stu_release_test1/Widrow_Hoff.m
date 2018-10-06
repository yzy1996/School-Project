function [solution iter] = Widrow_Hoff(Y,stepsize)
%
%    solution = Widrow_Hoff(Y,stepsize)��С������ʵ���㷨
%
%   ���룺�淶����������Y,ԣ��tau,��ʼ����stepsize
%   �����������solution����������iter
%
[y_k, d] = size(Y);
a = zeros(1,d);
b = ones(1,y_k);
k_max = 10000;
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

%������ʵ������㷨
theta = 0.001;           %��ֵ

for k = 1:k_max
    YY = a * Y';
    a = a + stepsize / k * (b - YY) * Y;
    if(norm(stepsize / k * (b - YY) * Y) < theta)
        break
    end
end

%%%%%����2
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