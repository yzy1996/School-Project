%�õ�������С��10��Ȼ���ҵ�Ƶ��
%��ⵥ��

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
[row,column]=find(dist==min(min(dist)));     %�ҵ���С����ĺ�������
t=sort(dist(:));
[m,n]=find(dist<=t(10),10);                  %�ҵ���С10������ĺ�������
t=[m,n];
t=[t;[row,column]];
t=[t;[row,column]];
table = tabulate(t(:,2));
[maxCount,idx] = max(table(:,2));

result=table(idx)-1

% [row,column]=find(dist==min(min(dist)));
% column-1
toc