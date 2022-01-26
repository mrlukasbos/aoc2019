#include <iostream>
#include <sstream>
#include <string>
#include <vector>
#include <fstream>
#include <algorithm>

int getTreesTotal(std::vector<std::vector<bool>> trees, int slope_x, int slope_y) {
    int x = 0; 
    int y = 0;
    int totalTreesEncountered = 0;
    while (y < trees.size()) {
        auto hasTree = trees.at(y).at(x);
        if (hasTree) {
            totalTreesEncountered++;
        }
        x = (x + slope_x) % trees.at(0).size();
        y += slope_y;
    }
    return totalTreesEncountered;
}

std::vector<std::vector<bool>> parseTrees() {
    std::ifstream read("input.txt");
    std::vector<std::vector<bool>> trees;
    // read until the end of the file
    for (std::string line; std::getline(read, line); ) {
        std::vector<bool> treeLine;
        for (int i = 0; i < line.length(); i++) {
            treeLine.push_back(line[i] == '#');
        }
        trees.push_back(treeLine);
    }
    read.close();
    return trees;
}

int parseResultA() {
    auto trees = parseTrees();
    return getTreesTotal(trees, 3, 1); 
}

int parseResultB() {
    auto trees = parseTrees();
    return getTreesTotal(trees, 1, 1)
        * getTreesTotal(trees, 3, 1)
        * getTreesTotal(trees, 5, 1)
        * getTreesTotal(trees, 7, 1)
        * getTreesTotal(trees, 1, 2);
}

int main () {  
    auto resultA = parseResultA();
    std::cout << "result A: " << std::to_string(resultA) << std::endl;

    auto resultB = parseResultB();
    std::cout << "result B: " << std::to_string(resultB) << std::endl;

    return 0;
}


