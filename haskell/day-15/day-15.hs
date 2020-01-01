import Debug.Trace
import Data.List
import qualified Data.List.Split as Split
import Data.Ord
import qualified Data.Map.Strict as Map
import Data.Maybe

data Direction = N | E | S | W deriving (Show, Eq, Enum)

data Node = Node {
    location :: (Int, Int),
    steps :: Int,
    parent :: Maybe Node,
    program_state :: ProgramState -- remember the program state at each node so we can more easily do a bfs
} deriving (Eq, Show)

handle_movement :: Direction -> Int
handle_movement movement = case movement of 
        N -> 1 
        S -> 2 
        W -> 3
        E -> 4

inverse :: Direction -> Direction
inverse direction = case direction of 
        N -> S
        S -> N 
        W -> E
        E -> W
        

get_coordinate_for_direction :: (Int, Int) -> Direction -> (Int, Int)
get_coordinate_for_direction (x,y) direction = case direction of 
    N -> (x, y+1)
    S -> (x, y-1)
    W -> (x-1, y)
    E -> (x+1, y)

all_directions = [N, S, W, E]

day_15a :: IO ()
day_15a = do
    text <- readFile "input.txt"
    let program = (map (\ln -> read ln :: Int) (split ',' ((lines text) !! 0))) ++ repeat 0
    let initial_program_state = ProgramState {
        memory = program,
        pc = 0,
        base = 0,
        outputs = [],
        inputs = [1],
        finished = False
    }
    let initial_node = Node {
        location = (0,0),
        steps = 0,
        parent = Nothing,
        program_state = initial_program_state
    }

    print $ steps (move_robot [initial_node] []) + 1


day_15b :: IO ()
day_15b = do
    text <- readFile "input.txt"
    let program = (map (\ln -> read ln :: Int) (split ',' ((lines text) !! 0))) ++ repeat 0
    let initial_program_state = ProgramState {
        memory = program,
        pc = 0,
        base = 0,
        outputs = [],
        inputs = [0],
        finished = False
    }
    let initial_node = Node {
        location = (0,0),
        steps = 0,
        parent = Nothing,
        program_state = initial_program_state
    }

    let oxygen_node = move_robot [initial_node] []

    let start_node = oxygen_node {
        steps = 0
    }

    print $ fill_oxygen [start_node] [] start_node


fill_oxygen :: [Node] -> [Node] -> Node -> Int
fill_oxygen stack visited_nodes prev_node = let

    sorted_stack = sortBy (comparing steps) stack;
    best_node = head sorted_stack

    -- update the stack for all directions
    north_stack = run_program_for_direction best_node N (tail sorted_stack) visited_nodes
    south_stack = run_program_for_direction best_node S (snd north_stack) visited_nodes
    west_stack = run_program_for_direction best_node W (snd south_stack) visited_nodes
    new_stack = run_program_for_direction best_node E (snd west_stack) visited_nodes

    finished = ((fst north_stack) || (fst south_stack) || (fst west_stack) || (fst new_stack))

    exec
        | stack == [] = (steps prev_node) - 1
        | otherwise = {- trace (show (map location stack)) $ -} fill_oxygen (snd new_stack) (best_node:visited_nodes) best_node
    
    --      in exec
  -- in trace (show (location best_node)) exec
    in trace (show (length stack)) $ exec



move_robot :: [Node] -> [Node] -> Node
move_robot stack visited_nodes = let

    sorted_stack = sortBy (comparing steps) stack;
    best_node = head sorted_stack

    -- update the stack for all directions
    north_stack = run_program_for_direction best_node N (tail sorted_stack) visited_nodes
    south_stack = run_program_for_direction best_node S (snd north_stack) visited_nodes
    west_stack = run_program_for_direction best_node W (snd south_stack) visited_nodes
    new_stack = run_program_for_direction best_node E (snd west_stack) visited_nodes

    exec
        | (fst north_stack) = head $ snd north_stack 
        | (fst south_stack) = head $ snd south_stack 
        | (fst west_stack) = head $ snd west_stack
        | (fst new_stack) = head $ snd new_stack 

        | otherwise = {- trace (show (map location stack)) $ -} move_robot (snd new_stack) (best_node:visited_nodes)
    
  --      in exec
  --  in trace (show (location best_node)) exec
    in trace (show (length stack)) $ exec

run_program_for_direction :: Node -> Direction -> [Node] -> [Node] -> (Bool, [Node])
run_program_for_direction node direction stack visited_nodes = let
    
    node_already_visited = (location node) `elem` (map location visited_nodes)
    node_already_in_stack = (location node) `elem` (map location stack)
    next_coordinate = get_coordinate_for_direction (location node) direction

    -- determin the program to run for the given direction
    program_to_run = (program_state node) {
        inputs = [handle_movement direction]
    }

    -- execute the function and update the stack according to the result
    exec 
        | node_already_in_stack = (False, stack) 
        | node_already_visited = (False, stack)
        | result == 0 = (False, stack) 
        | result == 2 = trace ("found the coordinate at: " ++ (show next_coordinate)) $ (True, updated_node:stack)
        | result == 1 = (False, updated_node:stack)
            where 
                new_program_state = process program_to_run
                result =  last (outputs new_program_state)
                updated_node = Node {
                    location = next_coordinate,
                    steps = (steps node) + 1,
                    parent = Just node,
                    program_state = new_program_state
                } 

    -- in trace (show (length stack)) $ exec
    in exec


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




-- the starting location for b =
-- -16 14
-- 242 too low
-- 512 too high
-- 511 too high
-- 510 