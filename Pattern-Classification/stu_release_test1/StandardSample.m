function Y = StandardSample(X1,X2)
%
%    Y = StandardSample(X1,X2)规范化样本集，输出规范化增广样本矩阵
%
%    输入：两类样本X1、X2（均为d维特征向量）
%    输出：规范化增广样本矩阵Y
%
%    若X1为N-D维矩阵，则此类共有N个样本；若X2为M-D维矩阵，则此类共有M个样本。
%    即共输入M+N个样本，那么输出的Y为(M+N)-(D+1)维矩阵
%

[su_x1,sv_x1] = size(X1);
[su_x2,sv_x2] = size(X2);

if sv_x1~=sv_x2
    error('two input matrixs must have same columns');
end

I_x1 = ones(su_x1,1);
I_x2 = ones(su_x2,1);

Y1 = [I_x1,X1];
Y2 = [I_x2,X2];

Y = [Y1;-Y2];