import Debug.Trace

list = [3,225,1,225,6,6,1100,1,238,225,104,0,1102,35,92,225,1101,25,55,225,1102,47,36,225,1102,17,35,225,1,165,18,224,1001,224,-106,224,4,224,102,8,223,223,1001,224,3,224,1,223,224,223,1101,68,23,224,101,-91,224,224,4,224,102,8,223,223,101,1,224,224,1,223,224,223,2,217,13,224,1001,224,-1890,224,4,224,102,8,223,223,1001,224,6,224,1,224,223,223,1102,69,77,224,1001,224,-5313,224,4,224,1002,223,8,223,101,2,224,224,1,224,223,223,102,50,22,224,101,-1800,224,224,4,224,1002,223,8,223,1001,224,5,224,1,224,223,223,1102,89,32,225,1001,26,60,224,1001,224,-95,224,4,224,102,8,223,223,101,2,224,224,1,223,224,223,1102,51,79,225,1102,65,30,225,1002,170,86,224,101,-2580,224,224,4,224,102,8,223,223,1001,224,6,224,1,223,224,223,101,39,139,224,1001,224,-128,224,4,224,102,8,223,223,101,3,224,224,1,223,224,223,1102,54,93,225,4,223,99,0,0,0,677,0,0,0,0,0,0,0,0,0,0,0,1105,0,99999,1105,227,247,1105,1,99999,1005,227,99999,1005,0,256,1105,1,99999,1106,227,99999,1106,0,265,1105,1,99999,1006,0,99999,1006,227,274,1105,1,99999,1105,1,280,1105,1,99999,1,225,225,225,1101,294,0,0,105,1,0,1105,1,99999,1106,0,300,1105,1,99999,1,225,225,225,1101,314,0,0,106,0,0,1105,1,99999,1008,677,677,224,1002,223,2,223,1005,224,329,101,1,223,223,7,677,677,224,102,2,223,223,1006,224,344,101,1,223,223,108,677,677,224,1002,223,2,223,1006,224,359,1001,223,1,223,7,677,226,224,1002,223,2,223,1005,224,374,1001,223,1,223,1107,677,226,224,1002,223,2,223,1005,224,389,1001,223,1,223,107,226,677,224,102,2,223,223,1005,224,404,1001,223,1,223,1108,226,677,224,1002,223,2,223,1006,224,419,101,1,223,223,107,226,226,224,102,2,223,223,1005,224,434,1001,223,1,223,108,677,226,224,1002,223,2,223,1006,224,449,101,1,223,223,108,226,226,224,102,2,223,223,1006,224,464,1001,223,1,223,1007,226,226,224,1002,223,2,223,1005,224,479,101,1,223,223,8,677,226,224,1002,223,2,223,1006,224,494,101,1,223,223,1007,226,677,224,102,2,223,223,1006,224,509,101,1,223,223,7,226,677,224,1002,223,2,223,1005,224,524,101,1,223,223,107,677,677,224,102,2,223,223,1005,224,539,101,1,223,223,1008,677,226,224,1002,223,2,223,1005,224,554,1001,223,1,223,1008,226,226,224,1002,223,2,223,1006,224,569,1001,223,1,223,1108,226,226,224,102,2,223,223,1005,224,584,101,1,223,223,1107,226,677,224,1002,223,2,223,1005,224,599,1001,223,1,223,8,226,677,224,1002,223,2,223,1006,224,614,1001,223,1,223,1108,677,226,224,102,2,223,223,1005,224,629,1001,223,1,223,8,226,226,224,1002,223,2,223,1005,224,644,1001,223,1,223,1107,677,677,224,1002,223,2,223,1005,224,659,1001,223,1,223,1007,677,677,224,1002,223,2,223,1005,224,674,101,1,223,223,4,223,99,226]
main = print(calc list 0)

-- no need to make things complicated, we know the user input already
request_input = 5

-- handle the opcode and return the new list and new instruction pointer
process_instruction :: [Int] -> Int -> ([Int], Int)
process_instruction ns n = let 
        val i = (ns !! i)
        opcode = (get_padded_opcode (val n)) !! 0 -- we omit the 10s here
        p i = val (n + i) -- arguments. 
        arg i 
            | (get_padded_opcode (val n)) !! (i + 1) == 1 = (p i) -- immediate mode -- it is +1 since the first arg is arg 1 
            | otherwise = val (p i) -- positional mode
    in case opcode of
        1 -> (replace ns (p 3) (arg 1 + arg 2), n+4) -- add
        2 -> (replace ns (p 3) (arg 1 * arg 2), n+4) -- mult
        3 -> (replace ns (p 1) request_input, n+2) -- get input
        4 -> trace ("show " ++ show (val (p 1))) $ (ns, n+2) -- show the output as debug value
        5 -> (ns, if (arg 1) /= 0 then (arg 2) else n + 3) -- jump if true
        6 -> (ns, if (arg 1) == 0 then (arg 2) else n + 3) -- jump if false
        7 -> (replace ns (p 3) (fromEnum ((arg 1) < (arg 2))), n+4) -- less than
        8 -> (replace ns (p 3) (fromEnum ((arg 1) == (arg 2))), n+4) -- equals

-- get opcode in reversed form and padded with 0
-- 2 -> [2, 0, 0, 0, 0, ...]
-- 102 -> [2, 0, 1, 0, 0, 0, ...]
-- it represents [op 1's, op 10's, param1 mode, param2 mode, param3 mode]
get_padded_opcode :: Int -> [Int]
get_padded_opcode n = reverse (digits n) ++ repeat 0
     
-- If the opcode is 99 return the list
-- Otherwise process the instruction and move to the next
calc :: [Int] -> Int -> [Int]
calc ns n 
    |  op !! 0 == 9 && op !! 1 == 9 = ns  -- 99
    |  otherwise = calc (fst result) (snd result) where 
        op = get_padded_opcode (ns !! n)
        result = trace ("instruction: " ++ show n ++ " --- " ++ show ns) $(process_instruction ns n)
            

-- Get the instruction length for a given opcode to know how much further we must move the instruction pointer 
instruction_steps opcode
        | opcode == 3 || opcode == 4 = 2
        | otherwise = 4
    

replace :: [Int] -> Int -> Int -> [Int]
replace xs i e = case splitAt i xs of
    (before, _:after) -> before ++ e: after
    _ -> xs

digits :: Int -> [Int]
digits n = map (\x -> read [x] :: Int) (show n)