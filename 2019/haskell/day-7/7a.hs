import Debug.Trace
import Data.List

list = [3,8,1001,8,10,8,105,1,0,0,21,42,67,84,109,122,203,284,365,446,99999,3,9,1002,9,3,9,1001,9,5,9,102,4,9,9,1001,9,3,9,4,9,99,3,9,1001,9,5,9,1002,9,3,9,1001,9,4,9,102,3,9,9,101,3,9,9,4,9,99,3,9,101,5,9,9,1002,9,3,9,101,5,9,9,4,9,99,3,9,102,5,9,9,101,5,9,9,102,3,9,9,101,3,9,9,102,2,9,9,4,9,99,3,9,101,2,9,9,1002,9,3,9,4,9,99,3,9,101,2,9,9,4,9,3,9,101,1,9,9,4,9,3,9,101,1,9,9,4,9,3,9,1001,9,1,9,4,9,3,9,101,1,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,1002,9,2,9,4,9,3,9,1001,9,2,9,4,9,3,9,101,1,9,9,4,9,3,9,1002,9,2,9,4,9,99,3,9,1001,9,1,9,4,9,3,9,101,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,101,1,9,9,4,9,3,9,102,2,9,9,4,9,3,9,1001,9,1,9,4,9,3,9,101,1,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,101,2,9,9,4,9,3,9,1002,9,2,9,4,9,99,3,9,101,2,9,9,4,9,3,9,101,2,9,9,4,9,3,9,101,2,9,9,4,9,3,9,101,1,9,9,4,9,3,9,101,1,9,9,4,9,3,9,102,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,1002,9,2,9,4,9,3,9,101,2,9,9,4,9,3,9,1001,9,1,9,4,9,99,3,9,1001,9,1,9,4,9,3,9,101,1,9,9,4,9,3,9,102,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,1001,9,2,9,4,9,3,9,1001,9,1,9,4,9,3,9,1001,9,2,9,4,9,3,9,1002,9,2,9,4,9,3,9,1002,9,2,9,4,9,3,9,102,2,9,9,4,9,99,3,9,102,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,101,2,9,9,4,9,3,9,101,2,9,9,4,9,3,9,101,1,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,101,1,9,9,4,9,3,9,1001,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,101,1,9,9,4,9,99]

main = print (maximum (outputs list))

outputs ns = map (calculate_permutation ns) (permutations [0,1,2,3,4])
calculate_permutation ns permutation = foldl (func ns) 0 permutation

-- [setting (permutation num), signal]
func ns out perm_num = trace ("perm_num: " ++ (show perm_num) ++ " out: " ++ show out) $ process ns {- pc: -} 0 {- temp output: -} 0 [perm_num, out]

-- handle the opcode and return the new list and new instruction pointer
process :: [Int] -> Int -> Int -> [Int] -> Int
process ns pc out inputs = let 
        val i = (ns !! i)
        opcode = (get_padded_opcode (val pc)) !! 0 -- we omit the 10s here
        p i = val (pc + i) -- arguments. 
        arg i 
            | (get_padded_opcode (val pc)) !! (i + 1) == 1 = (p i) -- immediate mode -- it is +1 since the first arg is arg 1 
            | otherwise = val (p i) -- positional mode
    in case opcode of
        1 -> trace ("add") $ process (replace ns (p 3) (arg 1 + arg 2)) (pc + 4) out inputs 
        2 -> trace ("mult") $ process (replace ns (p 3) (arg 1 * arg 2)) (pc + 4) out inputs 
        3 -> trace ("geti: " ++ show (head inputs)) $ process (replace ns (p 1) (head inputs)) (pc + 2) out (tail inputs)
        4 -> trace ("show") $ process ns (pc + 2) (val (p 1)) inputs
        5 -> trace ("jift") $ process ns (if (arg 1) /= 0 then (arg 2) else pc + 3) out inputs 
        6 -> trace ("jiff") $ process ns (if (arg 1) == 0 then (arg 2) else pc + 3) out inputs
        7 -> trace ("less") $ process (replace ns (p 3) (fromEnum ((arg 1) < (arg 2)))) (pc + 4) out inputs 
        8 -> trace ("eq") $ process (replace ns (p 3) (fromEnum ((arg 1) == (arg 2)))) (pc + 4) out inputs
        9 -> trace ("STOP") $ out

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