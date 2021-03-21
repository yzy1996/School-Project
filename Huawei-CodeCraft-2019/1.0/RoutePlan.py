
import classini
import read

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
    return data, label

##########################################################
## 读取三种数据
# car(id,from,to,speed,planTime) 5_col
car_path = r'car.txt'
car_data, car_label = read_input_file(car_path)

# road(id,length,speed,channel,from,to,isDuplex) 7_col
road_path = r'road.txt'
road_data, road_label = read_input_file(road_path)

# cross(id,roadId,roadId,roadId,roadId) 5_col
cross_path = r'cross.txt'
cross_data, cross_label = read_input_file(cross_path)

###########################################################
car_path = r'car.txt'
carfile=read.read_input_file(car_path)

car_list=[]
for i in range(len(carfile[0])):
    car_list.append(classini.Car(carfile[0][i],carfile[1][i],carfile[2][i],carfile[3][i],carfile[4][i]))

road_path = r'road.txt'
roadfile=read.read_input_file(road_path)

road_list=[]
for i in range(len(roadfile[0])):
    road_list.append(classini.road(roadfile[0][i],roadfile[1][i],roadfile[2][i],roadfile[3][i],roadfile[4][i],roadfile[5][i],roadfile[6][i]))

cross_path = r'cross.txt'
crossfile=read.read_input_file(cross_path)

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

def method1():
    # 对所有的车进行路径规划
    for i in range(len(car_label[0])):
        start_car = car_label[1][i]  # 小车出发位置
        target_car = car_label[2][i]  # 小车到达位置
        current_car = start_car  # 小车此刻位置
        start_road = 0  # 路的起点
        target_road = 0  # 路的终点
        choose_road_id = []  # 所选择走的路id
        # # 如果路是双向的
        # for i in range(len(road_label[0])):
        #     if road_label[6][i]:
        #         road_label

        # 储存一个新列表，因为要删除走过的路
        temp_road_data = road_data[:]
        temp_road_label = ['id', 'length', 'speed', 'channel', 'from', 'to', 'isDuplex']

        while(target_road != target_car):
            
            for i in range(len(road_label)):  
                temp_road_label[i] = [col_data[i] for col_data in temp_road_data]

            if current_car in temp_road_label[4]:
                road_index = temp_road_label[4].index(current_car)  # 找路的开始
                choose_road_id.append(temp_road_label[0][road_index])  # 添加路id

                print(temp_road_label[0][road_index])

                target_road = temp_road_label[5][road_index]  # 找路的终点
                current_car = target_road
                print(current_car)
                del temp_road_data[road_index]
            
            # 如果在路的起点搜索不到了，且路为双向时，交换两点再次进行搜索
            elif current_car in temp_road_label[5]:
                road_index = temp_road_label[5][::-1].index(current_car)  # 找路的开始
                if temp_road_label[6][::-1][road_index] == 1:
                    choose_road_id.append(temp_road_label[0][::-1][road_index])  # 添加路id

                    print(temp_road_label[0][::-1][road_index])

                    target_road = temp_road_label[4][::-1][road_index]  # 找路的终点
                    current_car = target_road
                    print(current_car)
                    del temp_road_data[len(temp_road_label[0])-road_index-1]

                else:
                    break

            else:
                print("无路可走了！")
                break

        print(choose_road_id)




###############################################################

def getEdges(G):
    """ 输入图G，返回其边与端点的列表 """
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

class CycleError(Exception):
    pass

def Bellman_Ford(v0, v3, INF=999):
    # v1,v2,w = getEdges(G)
    # 初始化源点与所有点之间的最短距离
    dis = [INF for k in range(len(cross_list))]
    dis[v0-1] = 0
    path = [[] for k in range(len(cross_list))]
    path_id = [[] for k in range(len(cross_list))]
    # 核心算法
    for k in range(len(cross_list)-1):   # 循环 n-1轮
        check = 0           # 用于标记本轮松弛中dis是否发生更新
        # print('a')
        for i in range(len(road_list)):     # 对每条边进行一次松弛操作
            if dis[road_list[i].begin_id-1] + road_list[i].length < dis[road_list[i].end_id-1]: 
                
                dis[road_list[i].end_id-1] = dis[road_list[i].begin_id-1] + road_list[i].length
                path_id[road_list[i].end_id-1] = []
                path_id[road_list[i].end_id-1] = path_id[road_list[i].begin_id-1]+path_id[road_list[i].end_id-1]
                path_id[road_list[i].end_id-1].append(road_list[i].ID)
                path[road_list[i].end_id-1] = []
                path[road_list[i].end_id-1] = path[road_list[i].begin_id-1]+path[road_list[i].end_id-1]
                path[road_list[i].end_id-1].append(road_list[i].begin_id)
                path[road_list[i].end_id-1].append(road_list[i].end_id)
                
                check = 1 
                # print(road_list[i].begin_id)  ## 起点
                # print(road_list[i].end_id)  ## 终点
                # print(path)
            elif road_list[i].isDuplex == 1 and dis[road_list[i].end_id-1] + road_list[i].length < dis[road_list[i].begin_id-1]: 
                dis[road_list[i].begin_id-1] = dis[road_list[i].end_id-1] + road_list[i].length
                
                path_id[road_list[i].begin_id-1] = []           
                path_id[road_list[i].begin_id-1] = path_id[road_list[i].end_id-1]+path_id[road_list[i].begin_id-1]
                path_id[road_list[i].begin_id-1].append(road_list[i].ID)

                path[road_list[i].begin_id-1] = []
                path[road_list[i].begin_id-1] = path[road_list[i].end_id-1]+path[road_list[i].begin_id-1]
                path[road_list[i].begin_id-1].append(road_list[i].end_id)
                path[road_list[i].begin_id-1].append(road_list[i].begin_id)
                
                check = 1 
            # if dis[v1[i]] + w[i] < dis[v2[i]]:   ## 意思是：
                # print(road_list[i].end_id)  ## 起点
                # print(road_list[i].begin_id)  ## 终点
            #     path[v2[i]].append(v1[i])
            #     path[v2[i]].append(v2[i])
            #     path[v2[i]]=path[v1[i]]+path[v2[i]]
            #     dis[v2[i]] = dis[v1[i]] + w[i]           
                # print(dis)
                # print(path)
                # print(path_id)
            #     check = 1
        if check == 0: 
            break
    
#     # 检测负权回路
#     # 如果在 n-1 次松弛之后，最短路径依然发生变化，则该图必然存在负权回路
#     flag = 0
#     for i in range(len(w)):             # 对每条边再尝试进行一次松弛操作
#         if dis[v1[i]] + w[i] < dis[v2[i]]: 
#             flag = 1
#             break
#     if flag == 1:
# #         raise CycleError()
#         return False

    for p in path:
        if p != []:
            if p[-1] == v3:
                # print(p)
                return path_id[path.index(p)]

# Dijkstra算法
# road_map = adjacency_matrix(road_list)
for i in range(len(car_list)):
    start = car_label[1][i]  # 小车出发位置
    end = car_label[2][i]  # 小车到达位置
    path = Bellman_Ford(start, end)

    # for i in range(len(path)):
    #     if path[i]
    with open('answer.txt', 'a') as f:
        f.write('(')
        f.write(str(car_list[i].ID)+', ')
        f.write(str(1+i)+', ')
        f.write(str(path).strip('[]'))
        f.write(')')
        f.write('\n')