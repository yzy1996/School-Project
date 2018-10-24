%------------------------------
% 计算语音的MFCC系数，此处为利用现有函数
%------------------------------    
tic
for j=1:10
    for i=1:41
    [x,fs] = audioread(strcat('C:\Users\Jerry\Desktop\语音识别自我版\音频数据\',num2str(j-1),'\',num2str(i-1),'_',num2str(j-1),'.wav'));
    x=vad(x);
    mel{i,j}=melcepst(x,fs);    
    end
end
    save('mfccdata.mat','mel');
toc