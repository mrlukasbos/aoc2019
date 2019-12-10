-- 512

import Data.Ix
import Data.List
import Data.Ord
import qualified Data.Map as Map
import Debug.Trace

day_10a = do
    text <- readFile "input.txt"
    let asts = get_asteroids (lines text)
    print $ maximum $ map (\ast -> Map.size (get_angle_map asts ast)) asts

day_10b = do
    text <- readFile "input.txt"
    let asts = get_asteroids (lines text)
    print $ destroy_asteroid (base asts) (ast_map asts) 0 where 
        ast_map asts = trace (show (base asts)) $ (asteroid_map asts (base asts))
    
    
-- Get a list of all possible coordinates
all_coords :: [String] -> [(Int, Int)] 
all_coords raw_input = range ((0,0), ((length raw_input) - 1, length (raw_input !! 0)-1))

-- Get the char at a coordinate
get_coord_val :: [String] -> (Int, Int) -> Char 
get_coord_val raw_input coord = (raw_input !! (snd coord)) !! (fst coord) 

-- Get the locations of all asteroids
get_asteroids :: [String] -> [(Int, Int)] 
get_asteroids raw_input = filter (\coord -> (get_coord_val raw_input coord) == '#') (all_coords raw_input)


-- get the angle for between a point and an asteroid
get_vector point asteroid = (((fst asteroid) - (fst point)), ((snd asteroid) - (snd point)))

get_angle vec = atan2 (fromIntegral(fst vec)) (fromIntegral(snd vec))

asteroid_angle_pairs :: [(Int, Int)] -> (Int, Int) -> [(Double, [(Int, Int)])]
asteroid_angle_pairs asteroids point = map (\asteroid -> ((get_angle (get_vector point asteroid)), [asteroid])) (filter (point /=) asteroids)

get_angle_map :: [(Int, Int)] -> (Int, Int) -> Map.Map Double [(Int, Int)]
get_angle_map asteroids point = Map.fromListWith (++) (asteroid_angle_pairs asteroids point)



get_asteroids_sorted_on_sight asteroids = sortBy (comparing (\ast -> Map.size (get_angle_map asteroids ast))) asteroids

-- get the asteroid with the best sight
base asteroids = last $ get_asteroids_sorted_on_sight asteroids

start_angle :: (Int, Int) -> Double
start_angle (a, b) = get_angle $ get_vector (a, b) (a, b+1)

-- for part two we need to have each map sorted 
asteroid_map asteroids base = get_angle_map asteroids base

angles_in_order :: [(Int, Int)] -> (Int, Int) -> [Double]
angles_in_order asteroids base = Map.keys $ asteroid_map asteroids base

-- go from (0pi to 2pi) to (-pi to pi)
translate_angle angle = if (angle > pi) then (angle - 2*pi) else angle

len tup = abs (fst tup) + abs (snd tup)


destroy_asteroid :: (Int, Int) -> Map.Map Double [(Int, Int)] -> Int -> (Int, Int)
destroy_asteroid base map iteration = let 
    itt = (Map.findIndex pi map) - iteration
    it = if (itt < 0) then (itt + (length (Map.keys map))) else itt
    func  
        | iteration >= 199 = head list_for_angle
        | otherwise = trace ("destroying " ++ (show (head list_for_angle)) ++ " at angle: " ++ (show angle) ++ " at iteration " ++ (show iteration)) $ destroy_asteroid base (Map.insert angle (tail list_for_angle) map) (iteration + 1) 
    angle = (Map.keys map) !! it
    list_for_angle = trace (show (sortBy (\p -> comparing len (get_vector p base)) (Map.findWithDefault [] angle map))) $ sortBy (\p -> comparing len (get_vector p base)) (Map.findWithDefault [] angle map)
        in func     

