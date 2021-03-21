import logging
import sys
import classini
import read

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

##########################################################
# 2.process

def Bellman_Ford(start, end, INF=999):
    # 初始化源点与所有点之间的最短距离
    dis = [INF for k in range(len(cross_list))]
    dis[start-1] = 0
    path = [[] for k in range(len(cross_list))]
    path_id = [[] for k in range(len(cross_list))]

    # 核心算法
    for k in range(len(cross_list)-1):   
        check = 0           
        for i in range(len(road_list)):     
            if dis[road_list[i].begin_id-1] + road_list[i].length < dis[road_list[i].end_id-1]:             
                dis[road_list[i].end_id-1] = dis[road_list[i].begin_id-1] + road_list[i].length

                path[road_list[i].end_id-1] = []
                path[road_list[i].end_id-1] = path[road_list[i].begin_id-1]+path[road_list[i].end_id-1]
                path[road_list[i].end_id-1].append(road_list[i].begin_id)
                path[road_list[i].end_id-1].append(road_list[i].end_id)

                path_id[road_list[i].end_id-1] = []
                path_id[road_list[i].end_id-1] = path_id[road_list[i].begin_id-1]+path_id[road_list[i].end_id-1]
                path_id[road_list[i].end_id-1].append(road_list[i].ID)
               
                check = 1 

            elif road_list[i].isDuplex == 1 and dis[road_list[i].end_id-1] + road_list[i].length < dis[road_list[i].begin_id-1]: 
                dis[road_list[i].begin_id-1] = dis[road_list[i].end_id-1] + road_list[i].length

                path[road_list[i].begin_id-1] = []
                path[road_list[i].begin_id-1] = path[road_list[i].end_id-1]+path[road_list[i].begin_id-1]
                path[road_list[i].begin_id-1].append(road_list[i].end_id)
                path[road_list[i].begin_id-1].append(road_list[i].begin_id)   

                path_id[road_list[i].begin_id-1] = []           
                path_id[road_list[i].begin_id-1] = path_id[road_list[i].end_id-1]+path_id[road_list[i].begin_id-1]
                path_id[road_list[i].begin_id-1].append(road_list[i].ID)
             
                check = 1 

        if check == 0: 
            break

    for p in path:
        if p != []:
            if p[-1] == end:
                return path_id[path.index(p)]

###########################################################
# 3.write output data

for i in range(len(car_list)):
    start = car_list[i].begin_id # 小车出发位置
    end = car_list[i].end_id  # 小车到达位置
    path = Bellman_Ford(start, end)

    with open(answer_path, 'a') as f:
        f.write('(')
        f.write(str(car_list[i].ID)+', ')

        ######################
        # 需要对出发时间进行规划
        f.write(str(car_list[i].planTime+int(i/10))+', ')
        ######################

        f.write(str(path).strip('[]'))
        f.write(')')
        f.write('\n')

##########################################################