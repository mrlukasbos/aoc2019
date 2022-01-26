#include <iostream>
#include <sstream>
#include <string>
#include <vector>
#include <fstream>
#include <algorithm>

int parseResultA() {
    std::ifstream read("input.txt");
    int total = 0;
    // read until the end of the file
    for (std::string line; std::getline(read, line); ) {
        int min, max;
        char letter;
        char password[100];

        sscanf(line.c_str(), "%d-%d %c: %s", &min, &max, &letter, password);

        std::string pwd = std::string(password);
        size_t countOfLetter = std::count(pwd.begin(), pwd.end(), letter);
        if (countOfLetter >= min && countOfLetter <= max) {
          total++;
        }
    }

    read.close();
    return total;
}


int parseResultB() {
    std::ifstream read("input.txt");

    int total = 0;
    // read until the end of the file
    for (std::string line; std::getline(read, line); ) {
        int min, max;
        char letter;
        char password[100];

        sscanf(line.c_str(), "%d-%d %c: %s", &min, &max, &letter, password);

        if (password[min - 1] == letter xor password[max - 1] == letter) {
            total++;
        }
    }

    read.close();
    return total;
}

int main () {  
    auto resultA = parseResultA();
    std::cout << "result A: " << std::to_string(resultA) << std::endl;

    auto resultB = parseResultB();
    std::cout << "result B: " << std::to_string(resultB) << std::endl;

    return 0;
}


