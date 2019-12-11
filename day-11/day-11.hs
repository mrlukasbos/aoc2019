{-# LANGUAGE RecordWildCards #-}
{-# OPTIONS_GHC -Wall -Werror #-}

import Debug.Trace
import qualified Data.Map.Strict as Map

data Dir = U | R | D | L deriving (Eq,Ord,Enum,Show)

data RobotState = RobotState { 
    location_map :: Map.Map (Int, Int) Int,
    location :: (Int, Int),
    direction :: Dir, 
    instructions :: [Int]
} deriving (Show)

day_11a :: IO ()
day_11a = do
    text <- readFile "input.txt"
    let program = (map (\ln -> read ln :: Int) (split ',' ((lines text) !! 0))) ++ repeat 0
    print $ length (location_map (last (runRobot program))) -- (runRobot program) -- length (location_map (runRobot program))

-- return a new direction after turning
turn :: Int -> Dir -> Dir
turn to_right = if (to_right == 1) then turn_right else turn_left

turn_right :: Dir -> Dir
turn_right L = U
turn_right curr = succ curr

turn_left :: Dir -> Dir
turn_left U = L
turn_left curr = pred curr

-- move from a point in a given direction and return the new point
move :: Dir -> (Int, Int) -> (Int, Int)
move direction (x, y) = case direction of 
        U -> (x, y+1) 
        L -> (x-1, y)  
        D -> (x, y-1)  
        R -> (x+1, y)

-- 'iterate while just' runs function a while a returns Something, and puts the output of a in an array
iterateWhileJust :: (a -> Maybe a) -> a -> [a]
iterateWhileJust func = go
    where
    go x = x : maybe [] go (func x)

get_current_location_color :: RobotState -> Int
get_current_location_color RobotState{..} = Map.findWithDefault 0 location location_map

-- we must know where we are now (to paint it and to determine where we have to go) (variable location)
-- we must know the colors of all locations  (variable location_map)
runRobot :: [Int] -> [RobotState]
runRobot program = all_states
    where 
        start_position = (0,0)

        initial_state = RobotState {
            location_map = Map.singleton start_position 0, -- singleton is a map with one element
            location = start_position,
            direction = U,
            instructions = outputs
        }

        all_states = iterateWhileJust doRobotStep initial_state

        inputs = map (get_current_location_color) (all_states)
        outputs = process program 0 0 [] (inputs)

doRobotStep :: RobotState -> Maybe RobotState
doRobotStep RobotState{..} = case instructions of -- this makes everything inside robotstate accessible
    color:rotation:rest ->  
        let new_direction = trace ((show direction) ++ " to " ++ (show  (turn rotation direction))) $ turn rotation direction
            new_location = move new_direction location
        in Just RobotState { 
            location_map = Map.insert new_location color location_map, 
            location = new_location, 
            direction = new_direction, 
            instructions = rest
        }
    [] -> Nothing
    [x] -> error $ "Robot only got one instruction: " ++ show x -- in this case something is really bad
        


-- /////// INTCODE COMPUTER ///////

-- handle the opcode and return the new list and new instruction pointer
process :: [Int] -> Int -> Int -> [Int] -> [Int] -> [Int]
process ns pc base out inputs = let 

        -- Get full opcode in the format: [op 1's, op 10's, param1 mode, param2 mode, param3 mode]
        full_op = take 6 (reverse (digits (ns !! pc)) ++ repeat 0)

        -- Get the location indicated by relative or positional mode (this is not relevant for immediate mode)
        loc i = (ns !! (pc + i)) + (fromEnum (full_op !! (i + 1) == 2)) * base

        -- Get the value of the argument at a given position. This function considers relative/positional/immediate mode.
        arg i = if (full_op !! (i + 1) == 1) then (loc i) else ns !! (loc i)

    in case trace ("--- instruction: " ++ (show pc)) $ ((full_op !! 0) + (10 * (full_op !! 1))) of
        1 -> process (replace ns (loc 3) (arg 1 + arg 2)) (pc + 4) base out inputs 
        2 -> process (replace ns (loc 3) (arg 1 * arg 2)) (pc + 4) base out inputs 
        3 -> process (replace ns (loc 1) (head inputs)) (pc + 2) base out (tail inputs)
        4 -> process ns (pc + 2) base (out ++ [(arg 1)]) inputs   -- it works when I append instead of prepend...
        5 -> process ns (if (arg 1) /= 0 then (arg 2) else pc + 3) base out inputs 
        6 -> process ns (if (arg 1) == 0 then (arg 2) else pc + 3) base out inputs
        7 -> process (replace ns (loc 3) (fromEnum ((arg 1) < (arg 2)))) (pc + 4) base out inputs 
        8 -> process (replace ns (loc 3) (fromEnum ((arg 1) == (arg 2)))) (pc + 4) base out inputs
        9 -> process ns (pc + 2) (base + (arg 1)) out inputs
        99 -> trace "DONE" $ out
        _ -> trace "Something went wrong! stopping..." $ out

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

-- 1932
-- EGHKGJER