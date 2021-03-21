import classini
import math

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

    # 返回所有数据的列表(2维) 以及 每一列组成的列表(2维)
    return label


## 读取三种数据
###########################################################
# car(id,from,to,speed,planTime) 5_col
car_path = r'car.txt'
carfile=read_input_file(car_path)
car_list=[]
for i in range(len(carfile[0])):
    car_list.append(classini.Car(carfile[0][i],carfile[1][i],carfile[2][i],carfile[3][i],carfile[4][i]))

# road(id,length,speed,channel,from,to,isDuplex) 7_col
road_path = r'road.txt'
roadfile=read_input_file(road_path)
road_list=[]
for i in range(len(roadfile[0])):
    road_list.append(classini.road(roadfile[0][i],roadfile[1][i],roadfile[2][i],roadfile[3][i],roadfile[4][i],roadfile[5][i],roadfile[6][i]))

# cross(id,roadId,roadId,roadId,roadId) 5_col
cross_path = r'cross.txt'
crossfile=read_input_file(cross_path)
cross_list=[]
for i in range(len(crossfile[0])):
    cross_list.append(classini.Cross(crossfile[0][i],crossfile[1][i],crossfile[2][i],crossfile[3][i],crossfile[4][i]))

#############################################################
# 生成道路的邻接矩阵
def adjacency_matrix(list):
    n = len(list)
    Na = 1000
    output_matrix = [ [ Na for i in range(n) ] for j in range(n) ]

    for i in range(n):
        output_matrix[i][i] = 0
        output_matrix[list[i].begin_id][list[i].end_id] = list[i].length
        output_matrix[list[i].end_id][list[i].begin_id] = list[i].length

    return output_matrix

###############################################################
# 输入图G，返回其边与端点的列表
def getEdges(G):
    
    v1 = []     # 出发点
    v2 = []     # 对应的相邻到达点
    w  = []     # 顶点v1到顶点v2的边的权值
    for i in range(len(G)):
        for j in range(len(G[i])):
            if G[i][j] != 0:
                w.append(G[i][j])
                v1.append(i)
                v2.append(j)
    return v1,v2,w

#############################################################
# 寻找最短路径
def Bellman_Ford(start, end, INF=9999):
    # v1,v2,w = getEdges(G)
    # 初始化源点与所有点之间的最短距离
    dis = [INF for k in range(len(cross_list))]
    dis[start-1] = 0
    path = [[] for k in range(len(cross_list))]
    path_id = [[] for k in range(len(cross_list))]
    # 核心算法
    for k in range(len(cross_list)-1):   
        check = 0           
        for i in range(len(road_list)):
            weight = (road_list[i].length/road_list[i].path_number)
            if dis[road_list[i].begin_id-1] + weight < dis[road_list[i].end_id-1]:             
                dis[road_list[i].end_id-1] = dis[road_list[i].begin_id-1] + weight

                path[road_list[i].end_id-1] = []
                path[road_list[i].end_id-1] = path[road_list[i].begin_id-1]+path[road_list[i].end_id-1]
                path[road_list[i].end_id-1].append(road_list[i].begin_id)
                path[road_list[i].end_id-1].append(road_list[i].end_id)

                path_id[road_list[i].end_id-1] = []
                path_id[road_list[i].end_id-1] = path_id[road_list[i].begin_id-1]+path_id[road_list[i].end_id-1]
                path_id[road_list[i].end_id-1].append(road_list[i].ID)
               
                check = 1 

            elif road_list[i].isDuplex == 1 and dis[road_list[i].end_id-1] + weight < dis[road_list[i].begin_id-1]: 
                dis[road_list[i].begin_id-1] = dis[road_list[i].end_id-1] + weight

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

#############################################################

for i in range(len(car_list)):
    start = car_list[i].begin_id # 小车出发位置
    end = car_list[i].end_id  # 小车到达位置
    path = Bellman_Ford(start, end)

    with open('answer.txt', 'a') as f:
        f.write('(')
        f.write(str(car_list[i].ID)+', ')
        f.write(str(car_list[i].planTime+int(i/10))+', ')
        f.write(str(path).strip('[]'))
        f.write(')')
        f.write('\n')