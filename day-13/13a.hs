{-# LANGUAGE RecordWildCards #-}

import Debug.Trace
import Data.List
import qualified Data.List.Split as Split
import Data.Ord
import qualified Data.Map.Strict as Map

day_13b :: IO ()
day_13b = do
    text <- readFile "input.txt"
    let program = replace ((map (\ln -> read ln :: Int) (split ',' ((lines text) !! 0))) ++ repeat 0) 0 2 -- replace the first memory address with 2
    print $ solve program

solve program = calc initial_program_state Map.empty where
    initial_program_state = ProgramState {
        memory = program,
        pc = 0,
        base = 0,
        outputs = [],
        inputs = [],
        finished = False
    }


calc state known_map = let

    -- this calculates the new output
    new_process_state = process state 
    outs = outputs new_process_state
    chunks = Split.chunksOf 3 outs
    game_state = Map.union (Map.fromList (map output_ins_to_tup chunks)) known_map

    blocks = Map.toList (Map.filter (==2) game_state)
    paddle = fst $ head (Map.toList (Map.filter (==3) game_state))
    ball = fst $ head (Map.toList (Map.filter (==4) game_state))
    score = snd $ head (Map.toList (Map.filterWithKey (\k _ -> k == (-1,0)) game_state))

    -- generate a new state to calculate with given inputs
    new_process_state_with_new_input = new_process_state {
        outputs = [],
        inputs = {-trace ("setting input" ++ (show (comp_pos ball paddle))) $ -} [(comp_pos ball paddle)]
    }

    exec
        | (finished state) = error "unexpected finish"
        | (blocks == []) = score
        | otherwise = trace ({-"paddle location " ++ (show paddle) ++ " - ball location: " ++ (show ball) ++ -}"Score: " ++ (show score) ++  " - Blocks: " ++ (show (length blocks))) $ calc new_process_state_with_new_input game_state
    in exec

  



comp_pos :: (Int, Int) -> (Int, Int) -> Int
comp_pos (ball_x, _) (paddle_x, _)
    | ball_x > paddle_x = 1
    | ball_x < paddle_x = -1
    | otherwise = 0

output_ins_to_tup :: [Int] -> ((Int, Int), Int)
output_ins_to_tup ls
    | (length ls == 3) = ((ls !! 0, ls !! 1), ls !! 2)
    | otherwise = error "something went wrong"

-- /////// INTCODE COMPUTER ///////

data ProgramState = ProgramState {
    memory :: [Int],
    pc :: Int,
    base :: Int,
    outputs :: [Int],
    inputs :: [Int],
    finished :: Bool -- used for specific input requests
} deriving (Show, Eq)

-- handle the opcode and return the new list and new instruction pointer
process :: ProgramState -> ProgramState
process state = let 

        -- Get full opcode in the format: [op 1's, op 10's, param1 mode, param2 mode, param3 mode]
        full_op = take 6 (reverse (digits ((memory state) !! (pc state))) ++ repeat 0)

        -- Get the location indicated by relative or positional mode (this is not relevant for immediate mode)
        loc i = ((memory state) !! ((pc state) + i)) + (fromEnum (full_op !! (i + 1) == 2)) * (base state)

        -- Get the value of the argument at a given position. This function considers relative/positional/immediate mode.
        arg i = if (full_op !! (i + 1) == 1) then (loc i) else (memory state) !! (loc i)

    in case {- trace ("--- instruction: " ++ (show (pc state)) ++ " - op - " ++ (show ((full_op !! 0) + (10 * (full_op !! 1)))))  $ -} ((full_op !! 0) + (10 * (full_op !! 1))) of
        1 -> process state {
            memory  = replace (memory state) (loc 3) (arg 1 + arg 2),
            pc = (pc state) + 4
        }
        2 -> process state {
            memory = (replace (memory state) (loc 3) (arg 1 * arg 2)),
            pc = (pc state) + 4
        } 
        3 -> if (inputs state) /= [] then process state {
            memory = (replace (memory state) (loc 1) (head (inputs state))),
            pc = (pc state) + 2,
            inputs = tail (inputs state)
        } else state       
        4 -> process state {
            pc = (pc state) + 2,
            outputs = ((outputs state) ++ [(arg 1)])   -- Note: Appending instead of prepending...
        } 
        5 -> process state {
            pc = if (arg 1) /= 0 then (arg 2) else (pc state) + 3
        }
        6 -> process state {
            pc = if (arg 1) == 0 then (arg 2) else (pc state) + 3
        }
        7 -> process state {
            memory = replace (memory state) (loc 3) (fromEnum ((arg 1) < (arg 2))),
            pc = (pc state) + 4
        }
        8 -> process state {
            memory = replace (memory state) (loc 3) (fromEnum ((arg 1) == (arg 2))),
            pc = (pc state) + 4
        }
        9 -> process state {
            pc = (pc state) + 2,
            base = (base state) + (arg 1)
        }
        99 -> trace "DONE" $ state { finished = True }
        _ -> trace "Something went wrong! stopping..." $ state

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
split _ [] = []
split d s = x : split d (drop 1 y) where (x,y) = span (/= d) s

-- a: 309
-- b: 15410