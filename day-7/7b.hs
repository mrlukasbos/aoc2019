import Debug.Trace
import Data.List

list = [3,8,1001,8,10,8,105,1,0,0,21,42,67,84,109,122,203,284,365,446,99999,3,9,1002,9,3,9,1001,9,5,9,102,4,9,9,1001,9,3,9,4,9,99,3,9,1001,9,5,9,1002,9,3,9,1001,9,4,9,102,3,9,9,101,3,9,9,4,9,99,3,9,101,5,9,9,1002,9,3,9,101,5,9,9,4,9,99,3,9,102,5,9,9,101,5,9,9,102,3,9,9,101,3,9,9,102,2,9,9,4,9,99,3,9,101,2,9,9,1002,9,3,9,4,9,99,3,9,101,2,9,9,4,9,3,9,101,1,9,9,4,9,3,9,101,1,9,9,4,9,3,9,1001,9,1,9,4,9,3,9,101,1,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,1002,9,2,9,4,9,3,9,1001,9,2,9,4,9,3,9,101,1,9,9,4,9,3,9,1002,9,2,9,4,9,99,3,9,1001,9,1,9,4,9,3,9,101,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,101,1,9,9,4,9,3,9,102,2,9,9,4,9,3,9,1001,9,1,9,4,9,3,9,101,1,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,101,2,9,9,4,9,3,9,1002,9,2,9,4,9,99,3,9,101,2,9,9,4,9,3,9,101,2,9,9,4,9,3,9,101,2,9,9,4,9,3,9,101,1,9,9,4,9,3,9,101,1,9,9,4,9,3,9,102,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,1002,9,2,9,4,9,3,9,101,2,9,9,4,9,3,9,1001,9,1,9,4,9,99,3,9,1001,9,1,9,4,9,3,9,101,1,9,9,4,9,3,9,102,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,1001,9,2,9,4,9,3,9,1001,9,1,9,4,9,3,9,1001,9,2,9,4,9,3,9,1002,9,2,9,4,9,3,9,1002,9,2,9,4,9,3,9,102,2,9,9,4,9,99,3,9,102,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,101,2,9,9,4,9,3,9,101,2,9,9,4,9,3,9,101,1,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,101,1,9,9,4,9,3,9,1001,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,101,1,9,9,4,9,99]
-- list = [3,26,1001,26,-4,26,3,27,1002,27,2,27,1,27,26,27,4,27,1001,28,-1,28,1005,28,6,99,0,0,5]

main = print (maximum (map (calc_permutation list) (permutations [5..9])))

calc_permutation :: [Int] -> [Int] -> Int
calc_permutation ns permutation =
    let outputsA = (process ns 0 [] ((permutation !! 0) : 0 : outputsE)) --(if (length outputsE) == 0 then [0] else outputsE)))
        outputsB = (process ns 0 [] ((permutation !! 1) : outputsA))
        outputsC = (process ns 0 [] ((permutation !! 2) : outputsB))
        outputsD = (process ns 0 [] ((permutation !! 3) : outputsC))
        outputsE = (process ns 0 [] ((permutation !! 4) : outputsD))
    in last outputsE


-- main = print (maximum (outputs_list list))

-- outputs_list :: [Int] -> [Int]
--outputs_list ns = map (\permutation -> calculate_permutation ns permutation 0 0) (permutations [5,6,7,8,9])

calculate_permutation :: [Int] -> [Int] -> Int -> Int -> Int
calculate_permutation ns permutation prev_val n = calculate_permutation ns permutation (func ns prev_val (permutation !! (n `mod` 5))) (n+1)

-- [setting (permutation num), signal]
func ns out perm_num = trace ("perm_num: " ++ (show perm_num) ++ " out: " ++ show out) $ head (process ns {- pc: -} 0 {- temp output: -} [] [perm_num, out])

-- handle the opcode and return the new list and new instruction pointer and a list of output values
-- the most recent output is first in the list
process :: [Int] -> Int -> [Int] -> [Int] -> [Int]
process ns pc outputs inputs = let 
       -- ins = [(head inputs), (last inputs)]
        val i = (ns !! i)
        opcode = (get_padded_opcode (val pc)) !! 0 -- we omit the 10s here
        p i = val (pc + i) -- arguments. 
        arg i 
            | (get_padded_opcode (val pc)) !! (i + 1) == 1 = (p i) -- immediate mode -- it is +1 since the first arg is arg 1 
            | otherwise = val (p i) -- positional mode
    in case opcode of
        1 -> process (replace ns (p 3) (arg 1 + arg 2)) (pc + 4) outputs inputs 
        2 -> process (replace ns (p 3) (arg 1 * arg 2)) (pc + 4) outputs inputs 
        3 -> process (replace ns (p 1) (head inputs)) (pc + 2) outputs (tail inputs) -- geti
        4 -> process ns (pc + 2) (outputs ++ [(val (p 1))]) inputs
        5 -> process ns (if (arg 1) /= 0 then (arg 2) else pc + 3) outputs inputs 
        6 -> process ns (if (arg 1) == 0 then (arg 2) else pc + 3) outputs inputs
        7 -> process (replace ns (p 3) (fromEnum ((arg 1) < (arg 2)))) (pc + 4) outputs inputs 
        8 -> process (replace ns (p 3) (fromEnum ((arg 1) == (arg 2)))) (pc + 4) outputs inputs
        9 -> trace ("STOP") $ outputs
        0 -> trace ("WTF") $ outputs

-- get opcode in reversed form and padded with 0
-- 2 -> [2, 0, 0, 0, 0, ...]
-- 102 -> [2, 0, 1, 0, 0, 0, ...]
-- it represents [op 1's, op 10's, param1 mode, param2 mode, param3 mode]
get_padded_opcode :: Int -> [Int]
get_padded_opcode n = reverse (digits n) ++ repeat 0

replace :: [Int] -> Int -> Int -> [Int]
replace xs i e = case splitAt i xs of
    (before, _:after) -> before ++ e: after
    _ -> xs

digits :: Int -> [Int]
digits n = map (\x -> read [x] :: Int) (show n)