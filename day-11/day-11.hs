
-- //// the idea //// 
-- We know we are at [location] and all the colors of the squares
-- We put the color of the current square in the intcode computer en get a result 
-- we can give a new color to the current location and move to the new location
-- repeat 

-- the intcode computers input is the processed output of its previous iteration (lazy)


-- (color, turn) = process.... 
-- paintcolor color 
-- go_new_position turn
-- repeat

import Debug.Trace
import Data.List
import Data.Ix
import Data.Ord
import qualified Data.Map as Map

data Dir = U | R | D | L deriving (Eq,Ord,Enum,Show)

-- point free style for curr
turn to_right =  if (to_right == 1) then turn_right else turn_left

turn_right :: Dir -> Dir
turn_right L = U
turn_right curr = succ curr

turn_left :: Dir -> Dir
turn_left U = L
turn_left curr = pred curr

move :: Dir -> (Int, Int) -> (Int, Int)
move direction (x, y) = case direction of 
        U -> (x, y+1) 
        L -> (x-1, y)  
        D -> (x, y-1)  
        R -> (x+1, y)


paint :: [((Int, Int), Bool)] -> Dir -> [Int] -> ([Int], Int, Int, [Int], [Int], Bool) -> [((Int, Int), Bool)]
paint panels direction inputs prev_output = let

    -- we somehow need to copy the intcode computer data for reuse
    pc = get_intcode_pc prev_output 
    mem = get_intcode_memory prev_output
    base = get_intcode_base prev_output
    out = get_intcode_out prev_output -- the result [color, turn direction]
    finished = get_intcode_finished prev_output -- true if it is the final output

    -- calculate results using the intcode computer with the known memory results
    run_program = (process mem pc base [] inputs)

    -- if not finished we must take the output 
    color_to_paint 
        | out == [] = False
        | otherwise = (out !! 0) == 1 -- True if white, False if black 
    
    new_direction
        | out == [] = U
        | otherwise = turn (out !! 1) direction -- get the new direction



    new_location = (move direction (fst (head panels)))

    panels_with_location = filter (\panel -> (fst panel) == new_location) panels

    panels_without_location = filter (\panel -> (fst panel) /= new_location) panels

    -- TODO check if the new panel already exists in 'panels' and in that case get the color.
    new_panel 
        | length (panels_with_location) > 0 = (panels_with_location) !! 0
        | otherwise = (new_location, False)



    panels_with_new_paint = replace panels_without_location 0 (fst (head panels), color_to_paint)

    new_panels = new_panel : panels_with_new_paint


    -- determine what to do next
    calculate 
        | finished = panels
        | otherwise = paint new_panels new_direction [fromEnum (snd (head new_panels))] run_program
    in calculate



-- the intcode computer should maybe return every output immediately and it's memory as well
-- then we can just reboot it later
get_intcode_memory (a,_,_,_,_,_) = a
get_intcode_pc (_,a,_,_,_,_) = a
get_intcode_base (_,_,a,_,_,_) = a
get_intcode_out (_,_,_,a,_,_) = a
get_intcode_finished (_,_,_,_,_,a) = a


--- intcode computer
day_11a = do
    text <- readFile "input.txt"
    let input = map (\ln -> read ln :: Int) (split ',' ((lines text) !! 0))
    print $ length (Map.keys (Map.fromList ((paint [((0,0), False)] U [0] (input, 0, 0, [], [], False)))))

-- handle the opcode and return the new list and new instruction pointer
process :: [Int] -> Int -> Int -> [Int] -> [Int] -> ([Int], Int, Int, [Int], [Int], Bool)
process ns pc base out inputs = let 

        -- Get full opcode in the format: [op 1's, op 10's, param1 mode, param2 mode, param3 mode]
        full_op = take 6 (reverse (digits (ns !! pc)) ++ repeat 0)

        -- Get the location indicated by relative or positional mode (this is not relevant for immediate mode)
        loc i = (ns !! (pc + i)) + (fromEnum (full_op !! (i + 1) == 2)) * base

        -- Get the value of the argument at a given position. This function considers relative/positional/immediate mode.
        arg i = if (full_op !! (i + 1) == 1) then (loc i) else ns !! (loc i)

        total_output finished = (ns, pc, base, out, inputs, finished)

    in case ((full_op !! 0) + (10 * (full_op !! 1))) of
        1 -> process (replace ns (loc 3) (arg 1 + arg 2)) (pc + 4) base out inputs 
        2 -> process (replace ns (loc 3) (arg 1 * arg 2)) (pc + 4) base out inputs 
        3 -> if (inputs == []) then (total_output False) else (process (replace ns (loc 1) (head inputs)) (pc + 2) base out (tail inputs))
        4 -> process ns (pc + 2) base ((arg 1) : out) inputs
        5 -> process ns (if (arg 1) /= 0 then (arg 2) else pc + 3) base out inputs 
        6 -> process ns (if (arg 1) == 0 then (arg 2) else pc + 3) base out inputs
        7 -> process (replace ns (loc 3) (fromEnum ((arg 1) < (arg 2)))) (pc + 4) base out inputs 
        8 -> process (replace ns (loc 3) (fromEnum ((arg 1) == (arg 2)))) (pc + 4) base out inputs
        9 -> process ns (pc + 2) (base + (arg 1)) out inputs
        99 ->total_output True

-- Replace value at index of list
replace :: [a] -> Int -> a -> [a]
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


-- 1932