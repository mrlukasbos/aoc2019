#include <iostream>
#include <sstream>
#include <string>
#include <vector>
#include <fstream>
#include <algorithm>


std::vector<int> parseNumbers() {
  std::ifstream read("input.txt");
    std::vector<int> nums;

    // read until the end of the file
    for (std::string line; std::getline(read, line); ) {
        nums.push_back(std::stoi(line));
    }

    read.close();
    return nums;
}


int parseInputA(std::vector<int> nums) {
    for (int i = 0; i < nums.size(); i++) {
        for (int j = 0; j < nums.size(); j++) {
            auto primary = nums[i];
            auto secondary = nums[j];
            if ((primary + secondary) == 2020) {
                return primary * secondary;
            }
        }
    }
    return -1;
}


int parseInputB(std::vector<int> nums) {
    for (int i = 0; i < nums.size(); i++) {
        for (int j = 0; j < nums.size(); j++) {
            for (int k = 0; k < nums.size(); k++) {
                auto primary = nums[i];
                auto secondary = nums[j];
                auto tertiary = nums[k];

                if ((primary + secondary + tertiary) == 2020) {
                    return primary * secondary * tertiary;
                }
            }
        }
    }
    return -1;
}


int main () {  
    auto nums = parseNumbers();
    auto resultA = parseInputA(nums);
    std::cout << "result A: " << std::to_string(resultA) << std::endl;

    auto resultB = parseInputB(nums);
    std::cout << "result B: " << std::to_string(resultB) << std::endl;

    return 0;
}


