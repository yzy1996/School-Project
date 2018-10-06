function plot_line_points(a,w1,w2)
%
%    plot_line_points(a,w1,w2)显示样本点和线性分类器
%    
%    a为解向量，w1、w2分别为两类样本点
%

%[su_w1,sv_w1] = size(w1);
%[su_w2,sv_w2] = size(w2);

%if sv_w1~=sv_w2
%    error('two input matrixs must have same columns');
%end

 x_min = min([w1(:,1);w2(:,1)]);
 x_max = max([w1(:,1);w2(:,1)]);
 y_min = min([w1(:,2);w2(:,2)]);
 y_max = max([w1(:,2);w2(:,2)]);
 x_deta = (x_max - x_min)/8;
 x_min = x_min-x_deta;
 x_max = x_max+x_deta;
 y_deta = (y_max - y_min)/8;
 y_min = y_min-y_deta;
 y_max = y_max+y_deta;

 plot(w1(:,1),w1(:,2),'ro'),axis([x_min x_max y_min y_max]),axis off;
 hold on
 plot(w2(:,1),w2(:,2),'b^')
 b = [x_min x_max];
 plot(b,-a(2)/a(3)*b-a(1)/a(3),'g','linewidth',2)
 hold off

end