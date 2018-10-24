function varargout = voicetest(varargin)
% VOICETEST MATLAB code for voicetest.fig
%      VOICETEST, by itself, creates a new VOICETEST or raises the existing
%      singleton*.
%
%      H = VOICETEST returns the handle to a new VOICETEST or the handle to
%      the existing singleton*.
%
%      VOICETEST('CALLBACK',hObject,eventData,handles,...) calls the local
%      function named CALLBACK in VOICETEST.M with the given input arguments.
%
%      VOICETEST('Property','Value',...) creates a new VOICETEST or raises the
%      existing singleton*.  Starting from the left, property value pairs are
%      applied to the GUI before voicetest_OpeningFcn gets called.  An
%      unrecognized property name or invalid value makes property application
%      stop.  All inputs are passed to voicetest_OpeningFcn via varargin.
%
%      *See GUI Options on GUIDE's Tools menu.  Choose "GUI allows only one
%      instance to run (singleton)".
%
% See also: GUIDE, GUIDATA, GUIHANDLES

% Edit the above text to modify the response to help voicetest

% Last Modified by GUIDE v2.5 09-Nov-2017 21:10:46

% Begin initialization code - DO NOT EDIT
gui_Singleton = 1;
gui_State = struct('gui_Name',       mfilename, ...
                   'gui_Singleton',  gui_Singleton, ...
                   'gui_OpeningFcn', @voicetest_OpeningFcn, ...
                   'gui_OutputFcn',  @voicetest_OutputFcn, ...
                   'gui_LayoutFcn',  [] , ...
                   'gui_Callback',   []);
if nargin && ischar(varargin{1})
    gui_State.gui_Callback = str2func(varargin{1});
end

if nargout
    [varargout{1:nargout}] = gui_mainfcn(gui_State, varargin{:});
else
    gui_mainfcn(gui_State, varargin{:});
end
% End initialization code - DO NOT EDIT


% --- Executes just before voicetest is made visible.
function voicetest_OpeningFcn(hObject, eventdata, handles, varargin)
% This function has no output args, see OutputFcn.
% hObject    handle to figure
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)
% varargin   command line arguments to voicetest (see VARARGIN)

% Choose default command line output for voicetest
handles.output = hObject;

% Update handles structure
guidata(hObject, handles);

% UIWAIT makes voicetest wait for user response (see UIRESUME)
% uiwait(handles.figure1);


% --- Outputs from this function are returned to the command line.
function varargout = voicetest_OutputFcn(hObject, eventdata, handles) 
% varargout  cell array for returning output args (see VARARGOUT);
% hObject    handle to figure
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Get default command line output from handles structure
varargout{1} = handles.output;


% --- Executes on button press in pushbutton1.
function pushbutton1_Callback(hObject, eventdata, handles)
% hObject    handle to pushbutton1 (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)
fs1=32000;
recObj = audiorecorder(fs1,8,1);
recordblocking(recObj, 2);
x=getaudiodata(recObj);
audiowrite('record.wav',x,fs1)

% --- Executes on button press in pushbutton1.
function pushbutton2_Callback(hObject, eventdata, handles)
% hObject    handle to pushbutton1 (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)
[x3,fs]=audioread('record.wav');
sound(x3,fs);

% --- Executes on button press in pushbutton3.
function pushbutton3_Callback(hObject, eventdata, handles)
% hObject    handle to pushbutton3 (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)
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
[m,n]=find(dist<=t(10),10);
t=[m,n];
t=[t;[row,column]];
% t=[t;[row,column]];
table = tabulate(t(:,2));
[maxCount,idx] = max(table(:,2));

set(handles.text2,'String',strcat('检测结果为：',num2str(table(idx)-1)));
