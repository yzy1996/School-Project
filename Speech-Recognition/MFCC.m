%------------------------------
% ����������MFCCϵ�����˴�Ϊ�������к���
%------------------------------    
tic
for j=1:10
    for i=1:41
    [x,fs] = audioread(strcat('C:\Users\Jerry\Desktop\����ʶ�����Ұ�\��Ƶ����\',num2str(j-1),'\',num2str(i-1),'_',num2str(j-1),'.wav'));
    x=vad(x);
    mel{i,j}=melcepst(x,fs);    
    end
end
    save('mfccdata.mat','mel');
toc