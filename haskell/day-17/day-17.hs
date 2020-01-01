import Debug.Trace
import Data.List
import qualified Data.List.Split as Split
import Data.Ord
import qualified Data.Map.Strict as Map
import Data.Maybe

day_17a :: IO ()
day_17a = do
    text <- readFile "input.txt"
    let program = (map (\ln -> read ln :: Int) (split ',' ((lines text) !! 0))) ++ repeat 0
    let initial_program_state = ProgramState {
        memory = program,
        pc = 0,
        base = 0,
        outputs = [],
        inputs = [],
        finished = False
    }

    let coord_list = to_coordinates initial_program_state
    let coordinate_map = create_coordinate_map coord_list
    print $ sum (map (\(x,y) -> x * y) (Map.keys (check_intersections coordinate_map)))



neighbour (a,b) (c,d) = (a+c, b+d)

is_valid n coordinate_map = (Map.findWithDefault '?' n coordinate_map) == '#'

is_intersection coordinate_map n = is_valid (neighbour n (0,0)) coordinate_map && is_valid (neighbour n (0,1)) coordinate_map && is_valid (neighbour n (1,0)) coordinate_map && is_valid (neighbour n (0,-1)) coordinate_map && is_valid (neighbour n (-1,0)) coordinate_map

check_intersections coordinate_map = Map.filterWithKey (\(x,y) val -> is_intersection coordinate_map (x,y)) coordinate_map

create_coordinate_map :: [[((Int, Int), Char)]] -> Map.Map (Int, Int) Char 
create_coordinate_map coordinates =  Map.fromList $ concat coordinates

to_coordinates program_state = map (\(line,y) -> zipWith (\c x -> ((x,y), c)) line [0..]) (map_lines program_state)

map_lines program_state = zipWith (\a b -> (a,b)) (lines (outputs_to_str (outputs (process program_state)))) [0..]

outputs_to_str outputs = map handle_num outputs 
    where handle_num num = case num of
            10 -> '\n'
            35 -> '#'
            46 -> '.'
            _ -> '?'


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