%用的是找最小的10个然后找到频率
%检测单个

tic
[x2,fs]=audioread('record.wav');
x2=vad(x2);
mel2=melcepst(x2,fs);

for j=1:10
    for i=1:30
        load('mfccdata.mat','mel');
        dist(i,j)=dtw(mel{i,j}',mel2');
    end
end
[row,column]=find(dist==min(min(dist)));     %找到最小距离的横纵坐标
t=sort(dist(:));
[m,n]=find(dist<=t(10),10);                  %找到最小10个距离的横纵坐标
t=[m,n];
t=[t;[row,column]];
t=[t;[row,column]];
table = tabulate(t(:,2));
[maxCount,idx] = max(table(:,2));

result=table(idx)-1

% [row,column]=find(dist==min(min(dist)));
% column-1
toc