#include<stdio.h>
#include<stdlib.h>
#include<vector>
#include<thread>
#define LENGTH 40
#define HEIGHT 40
using namespace std;

vector<vector<int>> map;

void drawMap(){
	while (true){
		system("cls");
		for (int i = 0; i < HEIGHT; i++){
			for (int j = 0; j < LENGTH; j++){
				switch (map[j][i]){
					case -1: printf("T"); break;
					default: printf(" ");
				}
			}
			printf("\n");
		}
	}
	
}
void input(){
	while (true){
	}
	
}
void Initial(){
	vector<int> row;
	row.assign(LENGTH, 0);//配置一個row的大小
	map.assign(HEIGHT, row);//配置2維


	for (int i = 0; i < LENGTH; i++){
		map[i][0] = -1;
		map[i][HEIGHT - 1] = -1;
	}
	for (int i = 0; i < HEIGHT; i++){
		map[0][i] = -1;
		map[LENGTH - 1][i] = -1;
	}
}
int main(){

	Initial();


	thread t1(drawMap);
	thread t2(input);
	while (true)
	{

	}
	
	t1.join();
	t2.join();

	system("pause");
	return 0;
}