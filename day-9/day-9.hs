import Debug.Trace
import Data.List

day_9a = calculate_with_inputs [1]
day_9b = calculate_with_inputs [2]

calculate_with_inputs inputs = do
    text <- readFile "input.txt"
    let input = map (\ln -> read ln :: Int) (split ',' ((lines text) !! 0))
    print $ (process (input ++ repeat 0) {- pc: -} 0 {- initial base: -} 0 {- temp output: -} [] inputs)

-- handle the opcode and return the new list and new instruction pointer
process :: [Int] -> Int -> Int -> [Int] -> [Int] -> [Int]
process ns pc base out inputs = let 

        -- Get full opcode in the format: [op 1's, op 10's, param1 mode, param2 mode, param3 mode]
        full_op = take 6 (reverse (digits (ns !! pc)) ++ repeat 0)

        -- Get the location indicated by relative or positional mode (this is not relevant for immediate mode)
        loc i = (ns !! (pc + i)) + (fromEnum (full_op !! (i + 1) == 2)) * base

        -- Get the value of the argument at a given position. This function considers relative/positional/immediate mode.
        arg i = if (full_op !! (i + 1) == 1) then (loc i) else ns !! (loc i)

    in case trace ("-------- instruction  " ++ (show pc) ++ " --------") $ ((full_op !! 0) + (10 * (full_op !! 1))) of
        1 -> trace ("add")      $ process (replace ns (loc 3) (arg 1 + arg 2)) (pc + 4) base out inputs 
        2 -> trace ("mult")     $ process (replace ns (loc 3) (arg 1 * arg 2)) (pc + 4) base out inputs 
        3 -> trace ("geti")     $ process (replace ns (loc 1) (head inputs)) (pc + 2) base out (tail inputs)
        4 -> trace ("show")     $ process ns (pc + 2) base ((arg 1) : out) inputs
        5 -> trace ("jift")     $ process ns (if (arg 1) /= 0 then (arg 2) else pc + 3) base out inputs 
        6 -> trace ("jiff")     $ process ns (if (arg 1) == 0 then (arg 2) else pc + 3) base out inputs
        7 -> trace ("less")     $ process (replace ns (loc 3) (fromEnum ((arg 1) < (arg 2)))) (pc + 4) base out inputs 
        8 -> trace ("eq")       $ process (replace ns (loc 3) (fromEnum ((arg 1) == (arg 2)))) (pc + 4) base out inputs
        9 -> trace ("adj")      $ process ns (pc + 2) (base + (arg 1)) out inputs
        99 -> trace ("STOP")    $ out

-- Replace value at index of list
replace :: [Int] -> Int -> Int -> [Int]
replace xs i e = case splitAt i xs of
    (before, _:after) -> before ++ e: after
    _ -> xs

-- Get separate digits of an integer
digits :: Int -> [Int]
digits n = map (\x -> read [x] :: Int) (show n)

-- Split each element from the string
split :: Eq a => a -> [a] -> [[a]]
split d [] = []
split d s = x : split d (drop 1 y) where (x,y) = span (/= d) s