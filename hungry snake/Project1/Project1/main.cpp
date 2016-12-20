#include<stdio.h>
#include<stdlib.h>
#include<vector>
#include<thread>
#include<iostream>
#include<windows.h>
#include<cstdio>
#include"conio.h""
#include"time.h"  

#define LENGTH 30
#define HEIGHT 40
using namespace std;
int score;

bool moveing;
bool change;
bool gameOver;
bool gameStart;

int lastGameTime;
double nowGameTime;
clock_t nowGameClock;
clock_t lastGameClock;

pair<int, int> dir;
pair<int, int> food;
vector<pair<int, int>> snack;
vector<pair<int, int>> stone;

void Initial();

void input(){
	while (true){

		char c = _getch();
		if (c != '\0') change = true;

		if (c == ' '){
			if (gameOver) Initial();
			else gameStart = !gameStart;
		}
		
		if (gameStart && !moveing){
			pair<int, int> checkMoveing = dir;
			if ((c == 'h' || c == 'H') && dir != pair<int, int>(0, 1)) dir = pair<int, int>(0, -1);
			else if ((c == 'n' || c == 'N') && dir != pair<int, int>(0, -1)) dir = pair<int, int>(0, 1);
			else if ((c == 'b' || c == 'B') && dir != pair<int, int>(1, 0)) dir = pair<int, int>(-1, 0);
			else if ((c == 'm' || c == 'M') && dir != pair<int, int>(-1, 0)) dir = pair<int, int>(1, 0);

			if (dir != checkMoveing) moveing = true;
		}
	}
}
void drawMap(){
	while (true){
		if (change){
			vector<vector<int>> map;
			// map initial
			vector<int> row;
			row.assign(HEIGHT, 99);//配置一個row的大小
			map.assign(LENGTH, row);//配置2維

			for (int i = 0; i < LENGTH; i++){
				map[i][0] = -1;
				map[i][HEIGHT - 1] = -1;
			}
			for (int i = 0; i < HEIGHT; i++){
				map[0][i] = -1;
				map[LENGTH - 1][i] = -1;
			}

			map[snack[0].first][snack[0].second] = 0;
			map[food.first][food.second] = 2;
			for (int i = 1; i < snack.size(); i++){
				map[snack[i].first][snack[i].second] = 1;
			}

			for (int i = 0; i < stone.size(); i++){
				map[stone[i].first][stone[i].second] = 3;
			}

			
			system("cls");

			 printf("%c[%dmHELLO!\n", 0x1B, 32);

			for (int i = 0; i < HEIGHT; i++){
				for (int j = 0; j < LENGTH; j++){
					switch (map[j][i]){
					case -1: printf("牆"); break;
					case 0: printf("頭"); break;
					case 1: printf("身"); break;
					case 2: printf("食"); break;
					default: printf("　");
					}
				}
				if (i == 8){
					if (gameStart) printf("     Start");
					else printf("     Stop");
				}
				else if (i == 10) printf("     Time  : %4d", (int)nowGameTime / 2);
				else if (i == 12) printf("     Score : %4d", score);
				printf("\n");
			}
			if (gameOver) printf("Game Over press space to reStart");
			change = false;
		}
	}	
}
pair<int, int> randomFood(){
	pair<int, int> re;
	
	bool check = false;
	do{
		int x = (rand() % (LENGTH - 2)) + 1;
		int y = (rand() % (HEIGHT - 2)) + 1;
		re = pair<int, int>(x, y);

		for (int i = 1; i < snack.size(); i++){
			if (re == snack[i]) check = true;
		}
		for (int i = 1; i < stone.size(); i++){
			if (re == stone[i]) check = true;
		}

	} while (check);
	return re;
}
void Initial(){
	srand((unsigned)time(NULL));
	// snack initial
	snack.clear();
	snack.push_back(pair<int, int>(5, 20));
	snack.push_back(pair<int, int>(4, 20));
	snack.push_back(pair<int, int>(3, 20));

	dir = pair<int, int>(1, 0);
	food = randomFood();
	// score initial
	score = 0;
	// time initial
	nowGameTime = 0;
	lastGameTime = -1;
	nowGameClock = clock();
	lastGameClock = clock();

	moveing = false;
	gameStart = false;
	change = true;
	gameOver = false;
}
int main(){

	Initial();

	thread t1(drawMap);
	thread t2(input);
	while (true){
		nowGameClock = clock();
		
		if (gameStart && !change) nowGameTime += (nowGameClock - lastGameClock) / (CLOCKS_PER_SEC / 2.0f);
		if ((int)nowGameTime != lastGameTime){
			//前進
			pair<int, int> temp = pair<int, int>(snack.begin()->first + dir.first, snack.begin()->second + dir.second);
			bool check = true;
			bool add = false;
			//是否撞到
			if (temp.first <= 0 || temp.first >= LENGTH - 1) check = false;
			if (temp.second <= 0 || temp.second >= HEIGHT - 1) check = false;
			for each (pair<int,int> p in snack){
				if (temp == p) check = false;
			}
			//吃到東西
			if (food == temp) {
				add = true;
				score += 100;
			}

			if (check){
				snack.insert(snack.begin(), temp);
				if(!add) snack.pop_back();
			}
			else {
				gameOver = true;
				gameStart = false;
			}
			if (add) food = randomFood();


			lastGameTime = nowGameTime;
			change = true;
			moveing = false;
		}

		lastGameClock = nowGameClock;
	}
	
	t1.join();
	t2.join();

	system("pause");
	return 0;
}