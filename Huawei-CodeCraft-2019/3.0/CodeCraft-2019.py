import logging
import sys
import classini
import numpy

logging.basicConfig(level=logging.DEBUG,
                    filename='../logs/CodeCraft-2019.log',
                    format='[%(asctime)s] %(levelname)s [%(funcName)s: %(filename)s, %(lineno)d] %(message)s',
                    datefmt='%Y-%m-%d %H:%M:%S',
                    filemode='a')



if len(sys.argv) != 5:
    logging.info('please input args: car_path, road_path, cross_path, answerPath')
    exit(1)

car_path = sys.argv[1]
road_path = sys.argv[2]
cross_path = sys.argv[3]
answer_path = sys.argv[4]

logging.info("car_path is %s" % (car_path))
logging.info("road_path is %s" % (road_path))
logging.info("cross_path is %s" % (cross_path))
logging.info("answer_path is %s" % (answer_path))

###########################################################
# 1.read input data

def read_input_file(path):
    with open(path) as f:
        label = []
        label = f.readline().strip('#\n()').split(',')  # 将第一行的标签提取出来
        lines = f.readlines()
        data = []                          
        for line in lines:              #把lines中的数据逐行读取出来
            temp = line.strip('\n()').split(',')  # 去掉字符串首尾以及中间的分隔符
            data.append(temp)
        data = [list(map(int, x)) for x in data]
        
        # 把每一列提取出来    
        for i in range(len(label)):  
            label[i] = [col_data[i] for col_data in data]

    # 返回每一列组成的列表(2维)
    return label

# car(id,from,to,speed,planTime) 5_col
carfile=read_input_file(car_path)
car_list=[]
for i in range(len(carfile[0])):
    car_list.append(classini.Car(carfile[0][i],carfile[1][i],carfile[2][i],carfile[3][i],carfile[4][i]))

# road(id,length,speed,channel,from,to,isDuplex) 7_col
roadfile=read_input_file(road_path)
road_list=[]
for i in range(len(roadfile[0])):
    road_list.append(classini.road(roadfile[0][i],roadfile[1][i],roadfile[2][i],roadfile[3][i],roadfile[4][i],roadfile[5][i],roadfile[6][i]))

# cross(id,roadId,roadId,roadId,roadId) 5_col
crossfile=read_input_file(cross_path)
cross_list=[]
for i in range(len(crossfile[0])):
    cross_list.append(classini.Cross(crossfile[0][i],crossfile[1][i],crossfile[2][i],crossfile[3][i],crossfile[4][i]))

###########################################################
# 2.process

def searchid(id):
    for i in range(len(cross_list)):
        if id == cross_list[i].ID:
            return i

# 寻找最短路径
def Bellman_Ford(start, INF=9999):
    dis = [INF for k in range(len(cross_list))]
    dis[searchid(start)] = 0
    path = [[] for k in range(len(cross_list))]
    path_id = [[] for k in range(len(cross_list))]

    for k in range(len(cross_list)-1):   
        check = 0           
        for i in range(len(road_list)):
            begin = searchid(road_list[i].begin_id) #0
            end = searchid(road_list[i].end_id) #1

            weight = road_list[i].length / ((numpy.log10(road_list[i].path_number+1)) * road_list[i].maxspeed)

            if dis[begin] + weight < dis[end]:             
                dis[end] = dis[begin] + weight

                path[end] = []
                path[end] = path[begin]+path[end]
                path[end].append(road_list[i].begin_id)
                path[end].append(road_list[i].end_id)

                path_id[end] = []
                path_id[end] = path_id[begin]+path_id[end]
                path_id[end].append(road_list[i].ID)
               
                check = 1 

            elif road_list[i].isDuplex == 1 and dis[end] + weight < dis[begin]: 
                dis[begin] = dis[end] + weight

                path[begin] = []
                path[begin] = path[end]+path[begin]
                path[begin].append(road_list[i].end_id)
                path[begin].append(road_list[i].begin_id)   

                path_id[begin] = []           
                path_id[begin] = path_id[end]+path_id[begin]
                path_id[begin].append(road_list[i].ID)
             
                check = 1 
        if check == 0: 
            break
  
    return path_id

# 先全部生成所有的 [起点]->[终点]的路
pathlist = []

for i in range(len(cross_list)):  # 为每一个路起点生成最短的终点路径 
    pathlist.append(Bellman_Ford(cross_list[i].ID))

for i in range(len(car_list)):  # 为每辆车安排路线
    car_list[i].route = pathlist[searchid(car_list[i].begin_id)][searchid(car_list[i].end_id)]

###########################################################
# 3.ragular the realtime

N0=len(car_list)

car_list.sort(key=lambda x:x.maxspeed,reverse=True)#-+x.routelen/1000.0
#让快的先走

max_speed=[16,14,12,10,8,6,4,2]
N_in_a_time=[28,27,29,27,28,27,27,27]

timechange_point=0
timeflag=0
for i in range(len(car_list)):
    if car_list[i].maxspeed==max_speed[0]:
        if int(i/N_in_a_time[0])>timeflag:
            timeflag+=1
            timechange_point+=N_in_a_time[0]
        car_list[i].realtime=car_list[i].planTime+int(timeflag)
        
    elif car_list[i].maxspeed==max_speed[1]:
        if int((i-timechange_point)/N_in_a_time[1])>0:
            timeflag+=1
            timechange_point+=N_in_a_time[1]
        car_list[i].realtime=car_list[i].planTime+int(timeflag)

    elif car_list[i].maxspeed==max_speed[2]:
        if int((i-timechange_point)/N_in_a_time[2])>0:
            timeflag+=1
            timechange_point+=N_in_a_time[2]
        car_list[i].realtime=car_list[i].planTime+int(timeflag)
    
    elif car_list[i].maxspeed==max_speed[3]:
        if int((i-timechange_point)/N_in_a_time[3])>0:
            timeflag+=1
            timechange_point+=N_in_a_time[3]
        car_list[i].realtime=car_list[i].planTime+int(timeflag)

    elif car_list[i].maxspeed==max_speed[4]:
        if int((i-timechange_point)/N_in_a_time[4])>0:
            timeflag+=1
            timechange_point+=N_in_a_time[4]
        car_list[i].realtime=car_list[i].planTime+int(timeflag)

    elif car_list[i].maxspeed==max_speed[5]:
        if int((i-timechange_point)/N_in_a_time[5])>0:
            timeflag+=1
            timechange_point+=N_in_a_time[5]
        car_list[i].realtime=car_list[i].planTime+int(timeflag)

    elif car_list[i].maxspeed==max_speed[6]:
        if int((i-timechange_point)/N_in_a_time[6])>0:
            timeflag+=1
            timechange_point+=N_in_a_time[6]
        car_list[i].realtime=car_list[i].planTime+int(timeflag)

    elif car_list[i].maxspeed==max_speed[7]:
        if int((i-timechange_point)/N_in_a_time[7])>0:
            timeflag+=1
            timechange_point+=N_in_a_time[7]
        car_list[i].realtime=car_list[i].planTime+int(timeflag)
        
###########################################################
# 4.write output data


with open(answer_path, 'w') as f:
    for i in range(len(car_list)):   
        f.write('(')
        f.write(str(car_list[i].ID)+', ')

        ######################
        # 需要对出发时间进行规划
        f.write(str(car_list[i].realtime) + ', ')
        ######################

        f.write(str(car_list[i].route).strip('[]'))
        f.write(')')
        f.write('\n')
##########################################################

