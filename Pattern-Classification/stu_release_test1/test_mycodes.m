%
%---------分类器测试平台1.0-------------------------------------------------------
%
%        目前可测试三种线性分类器算法，包括批处理感知器、单样本感知器、最小均方差
%        平台自带两类数据来自模式分类一书的习题，除自带数据外，还可自定义或加载数
%        据进行算法测试
%
%---------2013/3/25 by GaoZhanning----------------------------------------------- 
%
function varargout = test_mycodes(varargin)
% TEST_MYCODES MATLAB code for test_mycodes.fig
%      TEST_MYCODES, by itself, creates a new TEST_MYCODES or raises the existing
%      singleton*.
%
%      H = TEST_MYCODES returns the handle to a new TEST_MYCODES or the handle to
%      the existing singleton*.
%
%      TEST_MYCODES('CALLBACK',hObject,eventData,handles,...) calls the local
%      function named CALLBACK in TEST_MYCODES.M with the given input arguments.
%
%      TEST_MYCODES('Property','Value',...) creates a new TEST_MYCODES or raises the
%      existing singleton*.  Starting from the left, property value pairs are
%      applied to the GUI before test_mycodes_OpeningFcn gets called.  An
%      unrecognized property name or invalid value makes property application
%      stop.  All inputs are passed to test_mycodes_OpeningFcn via varargin.
%
%      *See GUI Options on GUIDE's Tools menu.  Choose "GUI allows only one
%      instance to run (singleton)".
%
% See also: GUIDE, GUIDATA, GUIHANDLES

% Edit the above text to modify the response to help test_mycodes

% Last Modified by GUIDE v2.5 25-Mar-2013 01:49:15

% Begin initialization code - DO NOT EDIT
gui_Singleton = 1;
gui_State = struct('gui_Name',       mfilename, ...
                   'gui_Singleton',  gui_Singleton, ...
                   'gui_OpeningFcn', @test_mycodes_OpeningFcn, ...
                   'gui_OutputFcn',  @test_mycodes_OutputFcn, ...
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

% --- Executes just before test_mycodes is made visible.
function test_mycodes_OpeningFcn(hObject, eventdata, handles, varargin)
% This function has no output args, see OutputFcn.
% hObject    handle to figure
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)
% varargin   command line arguments to test_mycodes (see VARARGIN)

% Choose default command line output for test_mycodes
handles.output = hObject;

% Update handles structure
guidata(hObject, handles);

% This sets up the initial plot - only do when we are invisible
% so window can get raised using test_mycodes.
if strcmp(get(hObject,'Visible'),'off')
    try
        load im.mat
        n = size(im,2);
        axes(handles.axes1);
        for i = 1:n
            pause(0.05);
            imshow(im{i});
            drawnow;
        end
    catch
    end
end

% UIWAIT makes test_mycodes wait for user response (see UIRESUME)
% uiwait(handles.figure1);


% --- Outputs from this function are returned to the command line.
function varargout = test_mycodes_OutputFcn(hObject, eventdata, handles)
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
global w1 w2 done;
axes(handles.axes1);
popup_index = get(handles.popupmenu2, 'Value');
if done&&size(w1,1)&&size(w2,1)
    test_w1 = w1;
    test_w2 = w2;
else
    switch popup_index
        case 1
            load w1_w2.mat;
            test_w1 = w1;
            test_w2 = w2;
        case 2
            load w3_w4.mat;
            test_w1 = w3;
            test_w2 = w4;
        case 3
            str = getappdata(handles.popupmenu2,'path');
            if str~=0
                load(str);
                ws = whos;
                n = size(ws,1);
                eval(['test_w1 = ' ws(n-1).name ';']);
                eval(['test_w2 = ' ws(n).name ';']);
            else
                errordlg('没有导入数据！', '错误');
                return;
            end
    end
end
popup_sel_index = get(handles.popupmenu1, 'Value');
para = str2double(get(handles.para,'String'));
switch popup_sel_index
    case 1
        tic;[a iter] = BatchPerceptron(StandardSample(test_w1,test_w2),para);t = toc*1000;
    case 2
        tic;[a iter] = SinglePerceptron(StandardSample(test_w1,test_w2),para);t = toc*1000;
    case 3
        tic;[a iter] = Widrow_Hoff(StandardSample(test_w1,test_w2),para);t = toc*1000; 
end
 plot_line_points(a,test_w1,test_w2);
 set(handles.text2, 'String', ['迭代次数：' num2str(iter) '  运行时间：' num2str(t) 'ms']);


% --------------------------------------------------------------------
function FileMenu_Callback(hObject, eventdata, handles)
% hObject    handle to FileMenu (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)


% --------------------------------------------------------------------
function OpenMenuItem_Callback(hObject, eventdata, handles)
% hObject    handle to OpenMenuItem (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)
%file = uigetfile('*.fig');
%if ~isequal(file, 0)
%    open(file);
%end

% --------------------------------------------------------------------
function PrintMenuItem_Callback(hObject, eventdata, handles)
% hObject    handle to PrintMenuItem (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)
%printdlg(handles.figure1)

% --------------------------------------------------------------------
function CloseMenuItem_Callback(hObject, eventdata, handles)
% hObject    handle to CloseMenuItem (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)
selection = questdlg(['Close ' get(handles.figure1,'Name') '?'],...
                     ['Close ' get(handles.figure1,'Name') '...'],...
                     'Yes','No','Yes');
if strcmp(selection,'No')
    return;
end

delete(handles.figure1)


% --- Executes on selection change in popupmenu1.
function popupmenu1_Callback(hObject, eventdata, handles)
% hObject    handle to popupmenu1 (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Hints: contents = get(hObject,'String') returns popupmenu1 contents as cell array
%        contents{get(hObject,'Value')} returns selected item from popupmenu1
popup_index = get(hObject, 'Value');
switch popup_index
    case 1
        set(handles.para_style, 'String', '裕量 b ');
    case 2
        set(handles.para_style, 'String', '裕量 b ');
    case 3
        set(handles.para_style, 'String', '初始步长');
end



% --- Executes during object creation, after setting all properties.
function popupmenu1_CreateFcn(hObject, eventdata, handles)
% hObject    handle to popupmenu1 (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    empty - handles not created until after all CreateFcns called

% Hint: popupmenu controls usually have a white background on Windows.
%       See ISPC and COMPUTER.
if ispc && isequal(get(hObject,'BackgroundColor'), get(0,'defaultUicontrolBackgroundColor'))
     set(hObject,'BackgroundColor','white');
end

set(hObject, 'String', {'批处理感知器算法','单样本感知器算法','最小均方差算法'});



function para_Callback(hObject, eventdata, handles)
% hObject    handle to para (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Hints: get(hObject,'String') returns contents of para as text
%        str2double(get(hObject,'String')) returns contents of para as a double


% --- Executes during object creation, after setting all properties.
function para_CreateFcn(hObject, eventdata, handles)
% hObject    handle to para (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    empty - handles not created until after all CreateFcns called

% Hint: edit controls usually have a white background on Windows.
%       See ISPC and COMPUTER.
if ispc && isequal(get(hObject,'BackgroundColor'), get(0,'defaultUicontrolBackgroundColor'))
    set(hObject,'BackgroundColor','white');
end





% --- Executes on selection change in popupmenu2.
function popupmenu2_Callback(hObject, eventdata, handles)
% hObject    handle to popupmenu2 (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Hints: contents = cellstr(get(hObject,'String')) returns popupmenu2 contents as cell array
%        contents{get(hObject,'Value')} returns selected item from popupmenu2
global done;
done = false;
popup_index = get(hObject, 'Value');
switch popup_index
    case 1
        load w1_w2.mat;
        plot_line_points([0 0 0],w1,w2);
        set(handles.text2, 'String', '数据1');
        clear w1 w2;
    case 2
        load w3_w4.mat;
        plot_line_points([0 0 0],w3,w4);
        set(handles.text2, 'String', '数据2');
        clear w3 w4;
    case 3
        [filename pathname]=uigetfile({'*.mat','m-files(*.mat)';'*.*','All-files(*.*)'},'其他数据','');
        if isequal(filename,0) || isequal(pathname,0)
            cla;
            errordlg('没有导入数据！', '错误');
            str = 0;
        else
            str=fullfile(pathname,filename);
            load(str);
            ws = whos;
            n = size(ws,1);
            eval(['w1 = ' ws(n-1).name ';']);
            eval(['w2 = ' ws(n).name ';']);
            try
                plot_line_points([0 0 0],w1,w2);
                set(handles.text2, 'String', str);
            catch
                cla;
                errordlg('不能读取导入的数据！', '错误');
                str = 0;
            end
        end
        setappdata(hObject,'path',str);
end



% --- Executes during object creation, after setting all properties.
function popupmenu2_CreateFcn(hObject, eventdata, handles)
% hObject    handle to popupmenu2 (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    empty - handles not created until after all CreateFcns called

% Hint: popupmenu controls usually have a white background on Windows.
%       See ISPC and COMPUTER.
if ispc && isequal(get(hObject,'BackgroundColor'), get(0,'defaultUicontrolBackgroundColor'))
    set(hObject,'BackgroundColor','white');
end

set(hObject, 'String', {'数据1','数据2','其他...'});


% --- Executes on button press in pushbutton4.
function pushbutton4_Callback(hObject, eventdata, handles)
% hObject    handle to pushbutton4 (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)
global w1 w2;
w1 = [];w2 = [];
cla reset;
axis off;
set(handles.pushbutton6,'Visible','on');
set(handles.pushbutton4,'Visible','off');
set(handles.pushbutton1,'Visible','off');
set(handles.clear,'Visible','on');
set(handles.para,'Visible','off');
set(handles.para_style,'Visible','off');
set(handles.popupmenu1,'Visible','off');
set(handles.popupmenu2,'Visible','off');
set(handles.text2,'Visible','off');
set(handles.save,'Visible','on');
set(handles.data1,'Visible','on');
set(handles.data2,'Visible','on');
set(handles.xmin,'Visible','on');
set(handles.xmax,'Visible','on');
set(handles.ymin,'Visible','on');
set(handles.ymax,'Visible','on');
set(handles.textlim,'Visible','on');
set(handles.xmin,'String','-2');
set(handles.ymin,'String','-2');
set(handles.xmax,'String','5');
set(handles.ymax,'String','5');
set(handles.axes1,'XLim',[-2 5]);
set(handles.axes1,'YLim',[-2 5]);
plotaxis2





% --- Executes on button press in pushbutton6.
function pushbutton6_Callback(hObject, eventdata, handles)
% hObject    handle to pushbutton6 (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)
cla;
set(handles.pushbutton6,'Visible','off');
set(handles.pushbutton4,'Visible','on');
set(handles.popupmenu2,'Visible','on');
set(handles.pushbutton1,'Visible','on');
set(handles.para,'Visible','on');
set(handles.para_style,'Visible','on');
set(handles.popupmenu1,'Visible','on');
set(handles.text2,'Visible','on');
set(handles.text2, 'String', '');
set(handles.save,'Visible','off');
set(handles.clear,'Visible','off');
set(handles.data1,'Visible','off');
set(handles.data2,'Visible','off');
set(handles.xmin,'Visible','off');
set(handles.xmax,'Visible','off');
set(handles.ymin,'Visible','off');
set(handles.ymax,'Visible','off');
set(handles.textlim,'Visible','off');
set(handles.data1,'Value',0);
set(handles.data2,'Value',0);
global w1 w2 done;

if size(w1,1)&&size(w2,1)
    plot_line_points([0 0 0],w1,w2);
    set(handles.text2, 'String', '自定义数据');
    done = true;
else
    if ~size(w1,1)&&size(w2,1)
       errordlg('没有自定义第一类数据！', '错误');
    end
    if ~size(w2,1)&&size(w1,1)
       errordlg('没有自定义第二类数据！', '错误');
    end
    if ~size(w2,1)&&~size(w1,1)
       errordlg('没有自定义数据！', '错误');
    end
end


% --- Executes on button press in save.
function save_Callback(hObject, eventdata, handles)
% hObject    handle to save (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)
global w1 w2;
if size(w1,1)&&size(w2,1)
    [filename ,pathname]=uiputfile({'*.mat','MAT-files(*.mat)'},'保存');
    str=fullfile(pathname,filename);
    save(str,'w1','w2');
else
    errordlg('没有自定义数据！', '错误');
end



% --- Executes on button press in data1.
function data1_Callback(hObject, eventdata, handles)
% hObject    handle to data1 (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Hint: get(hObject,'Value') returns toggle state of data1
set(handles.data2,'Value',0);
set(hObject,'Value',1);



% --- Executes on button press in data2.
function data2_Callback(hObject, eventdata, handles)
% hObject    handle to data2 (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Hint: get(hObject,'Value') returns toggle state of data2
set(handles.data1,'Value',0);
set(hObject,'Value',1);


% --- Executes on scroll wheel click while the figure is in focus.
function figure1_WindowScrollWheelFcn(hObject, eventdata, handles)
% hObject    handle to figure1 (see GCBO)
% eventdata  structure with the following fields (see FIGURE)
%	VerticalScrollCount: signed integer indicating direction and number of clicks
%	VerticalScrollAmount: number of lines scrolled for each click
% handles    structure with handles and user data (see GUIDATA)
%eventdata.VerticalScrollCount


% --- Executes on mouse motion over figure - except title and menu.
function figure1_WindowButtonMotionFcn(hObject, eventdata, handles)
% hObject    handle to figure1 (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)
global w1 w2;
if get(handles.data1,'Value')==1
    cla;
    x_min = str2double(get(handles.xmin,'String'));
    x_max = str2double(get(handles.xmax,'String'));
    y_min = str2double(get(handles.ymin,'String'));
    y_max = str2double(get(handles.ymax,'String'));
    set(handles.axes1,'XLim',[x_min x_max]);
    set(handles.axes1,'YLim',[y_min y_max]);
    plotaxis2
    
    cp = get(handles.axes1,'CurrentPoint');

    cx = cp(1,1);
    cy = cp(1,2);
    hold on;
    plot(handles.axes1,cx,cy,'ro');
    if size(w1,1)
        plot(w1(:,1),w1(:,2),'ro');
    end
    if size(w2,1)
        plot(w2(:,1),w2(:,2),'b^');
    end
    hold off;
end
if get(handles.data2,'Value')==1
    cla
    x_min = str2double(get(handles.xmin,'String'));
    x_max = str2double(get(handles.xmax,'String'));
    y_min = str2double(get(handles.ymin,'String'));
    y_max = str2double(get(handles.ymax,'String'));
    set(handles.axes1,'XLim',[x_min x_max]);
    set(handles.axes1,'YLim',[y_min y_max]);
    plotaxis2
    
    cp = get(handles.axes1,'CurrentPoint');

    cx = cp(1,1);
    cy = cp(1,2);
    hold on;
    plot(handles.axes1,cx,cy,'b^');
    if size(w1,1)
        plot(w1(:,1),w1(:,2),'ro');
    end
    if size(w2,1)
        plot(w2(:,1),w2(:,2),'b^');
    end
    hold off;
end



% --- Executes on mouse press over figure background, over a disabled or
% --- inactive control, or over an axes background.
function figure1_WindowButtonDownFcn(hObject, eventdata, handles)
% hObject    handle to figure1 (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)
global w1 w2;
if get(handles.data1,'Value')==1
    x_min = str2double(get(handles.xmin,'String'));
    x_max = str2double(get(handles.xmax,'String'));
    y_min = str2double(get(handles.ymin,'String'));
    y_max = str2double(get(handles.ymax,'String'));
    set(handles.axes1,'XLim',[x_min x_max]);
    set(handles.axes1,'YLim',[y_min y_max]);
    
    cp = get(handles.axes1,'CurrentPoint');

    cx = cp(1,1);
    cy = cp(1,2);
    
    w1 = [w1 ;cx cy];
    
end
if get(handles.data2,'Value')==1
    x_min = str2double(get(handles.xmin,'String'));
    x_max = str2double(get(handles.xmax,'String'));
    y_min = str2double(get(handles.ymin,'String'));
    y_max = str2double(get(handles.ymax,'String'));
    set(handles.axes1,'XLim',[x_min x_max]);
    set(handles.axes1,'YLim',[y_min y_max]);
    
    cp = get(handles.axes1,'CurrentPoint');

    cx = cp(1,1);
    cy = cp(1,2);
    
    w2 = [w2 ;cx cy];
    
end


% --- Executes on mouse press over figure background, over a disabled or
% --- inactive control, or over an axes background.
function figure1_WindowButtonUpFcn(hObject, eventdata, handles)
% hObject    handle to figure1 (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)
%global down;
%down = false;



function ymax_Callback(hObject, eventdata, handles)
% hObject    handle to ymax (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Hints: get(hObject,'String') returns contents of ymax as text
%        str2double(get(hObject,'String')) returns contents of ymax as a double
cla;
axis off;
x_min = str2double(get(handles.xmin,'String'));
x_max = str2double(get(handles.xmax,'String'));
y_min = str2double(get(handles.ymin,'String'));
y_max = str2double(get(handles.ymax,'String'));
if x_min>=x_max||y_min>=y_max
    errordlg('错误的坐标范围！','错误');
    return;
end
set(handles.axes1,'XLim',[x_min x_max]);
set(handles.axes1,'YLim',[y_min y_max]);
global w1 w2;
plotaxis2
hold on
    if size(w1,1)
        plot(w1(:,1),w1(:,2),'ro');
    end
    if size(w2,1)
        plot(w2(:,1),w2(:,2),'b^');
    end
hold off


% --- Executes during object creation, after setting all properties.
function ymax_CreateFcn(hObject, eventdata, handles)
% hObject    handle to ymax (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    empty - handles not created until after all CreateFcns called

% Hint: edit controls usually have a white background on Windows.
%       See ISPC and COMPUTER.
if ispc && isequal(get(hObject,'BackgroundColor'), get(0,'defaultUicontrolBackgroundColor'))
    set(hObject,'BackgroundColor','white');
end



function ymin_Callback(hObject, eventdata, handles)
% hObject    handle to ymin (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Hints: get(hObject,'String') returns contents of ymin as text
%        str2double(get(hObject,'String')) returns contents of ymin as a double
cla;
axis off;
x_min = str2double(get(handles.xmin,'String'));
x_max = str2double(get(handles.xmax,'String'));
y_min = str2double(get(handles.ymin,'String'));
y_max = str2double(get(handles.ymax,'String'));
if x_min>=x_max||y_min>=y_max
    errordlg('错误的坐标范围！','错误');
    return;
end
set(handles.axes1,'XLim',[x_min x_max]);
set(handles.axes1,'YLim',[y_min y_max]);
global w1 w2;
plotaxis2
hold on
    if size(w1,1)
        plot(w1(:,1),w1(:,2),'ro');
    end
    if size(w2,1)
        plot(w2(:,1),w2(:,2),'b^');
    end
hold off


% --- Executes during object creation, after setting all properties.
function ymin_CreateFcn(hObject, eventdata, handles)
% hObject    handle to ymin (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    empty - handles not created until after all CreateFcns called

% Hint: edit controls usually have a white background on Windows.
%       See ISPC and COMPUTER.
if ispc && isequal(get(hObject,'BackgroundColor'), get(0,'defaultUicontrolBackgroundColor'))
    set(hObject,'BackgroundColor','white');
end



function xmax_Callback(hObject, eventdata, handles)
% hObject    handle to xmax (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Hints: get(hObject,'String') returns contents of xmax as text
%        str2double(get(hObject,'String')) returns contents of xmax as a double
cla;
axis off;
x_min = str2double(get(handles.xmin,'String'));
x_max = str2double(get(handles.xmax,'String'));
y_min = str2double(get(handles.ymin,'String'));
y_max = str2double(get(handles.ymax,'String'));
if x_min>=x_max||y_min>=y_max
    errordlg('错误的坐标范围！','错误');
    return;
end
set(handles.axes1,'XLim',[x_min x_max]);
set(handles.axes1,'YLim',[y_min y_max]);
global w1 w2;
plotaxis2
hold on
    if size(w1,1)
        plot(w1(:,1),w1(:,2),'ro');
    end
    if size(w2,1)
        plot(w2(:,1),w2(:,2),'b^');
    end
hold off


% --- Executes during object creation, after setting all properties.
function xmax_CreateFcn(hObject, eventdata, handles)
% hObject    handle to xmax (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    empty - handles not created until after all CreateFcns called

% Hint: edit controls usually have a white background on Windows.
%       See ISPC and COMPUTER.
if ispc && isequal(get(hObject,'BackgroundColor'), get(0,'defaultUicontrolBackgroundColor'))
    set(hObject,'BackgroundColor','white');
end



function xmin_Callback(hObject, eventdata, handles)
% hObject    handle to xmin (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Hints: get(hObject,'String') returns contents of xmin as text
%        str2double(get(hObject,'String')) returns contents of xmin as a double
cla;
axis off;
x_min = str2double(get(handles.xmin,'String'));
x_max = str2double(get(handles.xmax,'String'));
y_min = str2double(get(handles.ymin,'String'));
y_max = str2double(get(handles.ymax,'String'));
if x_min>=x_max||y_min>=y_max
    errordlg('错误的坐标范围！','错误');
    return;
end
set(handles.axes1,'XLim',[x_min x_max]);
set(handles.axes1,'YLim',[y_min y_max]);
global w1 w2;
plotaxis2
hold on
    if size(w1,1)
        plot(w1(:,1),w1(:,2),'ro');
    end
    if size(w2,1)
        plot(w2(:,1),w2(:,2),'b^');
    end
hold off




% --- Executes during object creation, after setting all properties.
function xmin_CreateFcn(hObject, eventdata, handles)
% hObject    handle to xmin (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    empty - handles not created until after all CreateFcns called

% Hint: edit controls usually have a white background on Windows.
%       See ISPC and COMPUTER.
if ispc && isequal(get(hObject,'BackgroundColor'), get(0,'defaultUicontrolBackgroundColor'))
    set(hObject,'BackgroundColor','white');
end


% --- Executes on button press in clear.
function clear_Callback(hObject, eventdata, handles)
% hObject    handle to clear (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)
global w1 w2;
w1 = [];
w2 = [];
    cla;
    x_min = str2double(get(handles.xmin,'String'));
    x_max = str2double(get(handles.xmax,'String'));
    y_min = str2double(get(handles.ymin,'String'));
    y_max = str2double(get(handles.ymax,'String'));
    set(handles.axes1,'XLim',[x_min x_max]);
    set(handles.axes1,'YLim',[y_min y_max]);
    plotaxis2
    clear all
